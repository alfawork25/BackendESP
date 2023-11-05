using ESP.Context;
using ESP.Models;
using ESP.Request;
using ESP.Response;
using Microsoft.EntityFrameworkCore;

using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace ESP.Services
{
	public class ProcessService
	{

		private readonly ApplicationContext _context;

		public ProcessService(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<HttpSubjectTypeResponse> GetFilteredSubjectsAsync(int blockId)
		{
			var result = await _context.SubjectTypes.Include(x => x.CheckBlocks.Where(x => x.BlockId == blockId))
													.Where(x => x.CheckBlocks.Any(x => x.BlockId == blockId))
													.ToListAsync();

			if (result.Count == 0)
			{
				throw new Exception("Не удалось загрузить субъекты");
			}

			return new HttpSubjectTypeResponse()
			{
				Body = result
			};
		}

		public async Task<HttpProcessResponse> GetProcessesAsync()
		{

			var response = await _context.Processes
				.Include(z => z.SystemType)
				.Include(z => z.ClientType)
				.AsQueryable()
				.ToListAsync();


			List<Revision> revisionsList = new List<Revision>();
			if(response.FirstOrDefault(z => z.ClientType != null) != null)
			{
				var result = response.FirstOrDefault(z => z.ClientType != null);
			}
			foreach (var process in response)
			{
				var revisions = _context.Revisions.Include(z => z.InfoBlock)
												  .ThenInclude(z => z.Status)
												  .Where(z => z.ProcessId == process.Id);

				revisionsList.AddRange(revisions);

			}



			if (response.Count == 0) throw new Exception("Не удлаось излвечь процессы");

			return new HttpProcessResponse()
			{
				Body = response.Select(x => new
				{
					x.Id,
					x.Name,
					x.StartDate,
					x.PrimaryDate,
					x.StartDateLastRevision,
					x.StatusDate,
					x.LastDateRevision,
					x.BlockTechnologist,
					x.SystemCode,
					Count = revisionsList.Where(z => z.ProcessId == x.Id).Count(),
					PrimaryConnectionStatus = revisionsList.FirstOrDefault(z => "Первичная доработка" == z.RevisionType && z.ProcessId == x.Id)?.InfoBlock?.Status?.Name,
					LastRevisionStatus = revisionsList.LastOrDefault(z => "Последняя доработка" == z.RevisionType && z.ProcessId == x.Id)?.InfoBlock?.Status?.Name,
				    ClientType = x.ClientType?.Name,
				})
			};

		}

		public async Task<HttpCheckResponse> GetChecksAsync(HttpCheckRequest request)
		{

			List<CheckBlock> checks = new List<CheckBlock>();

			var result = await _context.CheckBlocks
									 .Include(x => x.Block)
									 .Include(x => x.CheckCodes)
									 .ThenInclude(x => x.SubjectTypes)
									 .Include(x => x.CheckCodes)
									 .ThenInclude(x => x.Routes)
									 .Include(x => x.CheckCodes)
									 .ThenInclude(x => x.ProhibitionCodes)
									 .ThenInclude(x => x.Routes)
									 .Include(x => x.ClientTypes)
									 .AsSplitQuery()
									 .Where(x => x.ClientTypes.Contains(_context.ClientTypes.Find(request.ClientType) ?? new ClientType()))
									 .Select(x => new { x.Id, x.ShortName, x.SequenceNumber, x.Block, CheckCodes = x.CheckCodes, x.ClientTypes, label = "Проверки" })
									 .ToListAsync();

			foreach (var cb in result)
			{
				var checkblock = new CheckBlock() { Id = cb.Id, ShortName = cb.ShortName, SequenceNumber = cb.SequenceNumber, Block = cb.Block };
				var filteredCheckCode = cb.CheckCodes.Where(x => request.subjects.Contains(request.subjects.Find(c => c.IsNewClient == x.IsActive && x.SubjectTypes.Where(x => x.Id == c.SubjectId).Count() != 0) ?? new SubjectDto()))
					.Select(x => new CheckCode
					{
						Id = x.Id,
						Name = x.Name,
						IsActive = x.IsActive,
						Title = x.Title,
						ProhibitionCodes = x.ProhibitionCodes,
						Routes = x.Routes,
						SubjectTypes = x.SubjectTypes.Where(p => request.subjects.Contains(request.subjects.Find(s => s.SubjectId == p.Id && x.IsActive == s.IsNewClient) ?? new SubjectDto())).ToList()
					}).ToList();
				checkblock.CheckCodes.AddRange(filteredCheckCode);
				checks.Add(checkblock);
			}


			if (checks.Count == 0)
			{
				throw new Exception("Не удалось извлечь проверки");
			}

			return new HttpCheckResponse()
			{

				Body = checks.Select(x => new
				{

					x.Id,
					x.ShortName,
					x.SequenceNumber,
					Block = new { Id = x?.Block?.Id, Name = x?.Block?.Name },
					CheckCodes = x != null ? x.CheckCodes.Select(x => new
					{
						x.Id,
						x.Name,
						x.IsActive,
						x.Title,
						ProhibitionCodes = x.ProhibitionCodes.OrderBy(x => x.Name).Select(c => new { c.Id, c.Name, c.IsActive, c.Default, c.StartDate, c.EndDate, Routes = c.Routes.Select(z => new { z.Id, z.Name, z.Code }) }),
						Routes = x.Routes.Select(z => new { z.Id, z.Name, z.Code }),
						SubjectTypes = x.SubjectTypes.Select(st => new { st.Id, st.Name, ClientTypes = st.ClientTypes.Select(cl => new { cl.Id, cl.Name }) })

					}).OrderBy(x => x.Name) : null
				}).OrderBy(x => x.SequenceNumber)
			};
		}
		public async Task<HttpProcessResponse> GetProcessByIdAsync(int id)
		{

			var process = await _context.Processes.Include(x => x.CheckBlocks)
												  .Include(x => x.CheckCodes)
												  .Include(x => x.SubjectTypes)
												  .Include(x => x.ProhibitionCodes)
												  .Include(x => x.SystemType)
												  .Include(x => x.ClientType)
												  .Include(z => z.StatusHistories)
												  .AsSplitQuery()
												  .FirstOrDefaultAsync(x => x.Id == id);


			var checkBlocks = process?.CheckBlocks.ConvertAll(x => x.Id);
			var checkCodes = process?.CheckCodes?.ConvertAll(x => x.Id);
			var subjectTypes = process?.SubjectTypes.ConvertAll(x => x.Id);
			var prohibitionCodes = process?.ProhibitionCodes.ConvertAll(x => x.Id);

			if (checkBlocks == null || checkCodes == null || subjectTypes == null || prohibitionCodes == null)
			{
				return new HttpProcessResponse();
			}

			var processSubjectState = new List<ProcessSubjectState>();

			if (process != null)
			{
				processSubjectState = _context.ProcessSubjectStates.Where(x => x.ProcessId == process.Id).ToList();
			}


			var checks = _context.CheckBlocks.Include(x => x.Block)
											 .Include(x => x.CheckCodes.Where(x => checkCodes.Contains(x.Id)))
											 .ThenInclude(x => x.SubjectTypes.Where(x => subjectTypes.Contains(x.Id)))
											 .Include(x => x.CheckCodes.Where(x => checkCodes.Contains(x.Id)))
											 .ThenInclude(x => x.Routes)
											 .Include(x => x.CheckCodes.Where(x => checkCodes.Contains(x.Id)))
											 .ThenInclude(x => x.ProhibitionCodes)
											 .ThenInclude(x => x.Routes)
											 .AsSplitQuery()
											 .Where(x => checkBlocks.Contains(x.Id))
											 .Select(x => new
											 {
												 x.Id,
												 x.ShortName,
												 x.SequenceNumber,
												 x.Block,
												 CheckCodes = x.CheckCodes
											 .Where(x => checkCodes.Contains(x.Id))
											 .Select(r => new
											 {
												 r.Id,
												 r.Name,
												 r.Title,
												 r.IsActive,
												 r.Routes,
												 ProhibitionCodes = r.ProhibitionCodes,
												 SubjectTypes = r.SubjectTypes.Select(z => new { z.Id, z.Name, z.ClientTypes })
											 })
											 }).ToList();


			if (checks.Count() == 0)
			{
				checks = null;
			}



			return new HttpProcessResponse()
			{
				Body = new
				{
					Id = process?.Id,

					ProcessName = process?.Name,
					SystemType = process?.SystemType,
					StartDate = process?.StartDate,
					PrimaryDate = process?.PrimaryDate,
					Count = process?.Count,
					StartDateLastRevision = process?.StartDateLastRevision,
					LastDateRevision = process?.LastDateRevision,
					StatusHistories = process?.StatusHistories.OrderByDescending(z => z.Date),
					StatusDate = process?.StatusDate,
					BlockTechnologist = process?.BlockTechnologist,
					SystemCode = process?.SystemCode,
					Checks = checks?.Select(x => new
					{
						x.Id,
						x.ShortName,
						x.SequenceNumber,
						Block = x.Block != null ? new { Id = x.Block.Id, Name = x.Block.Name } : null,
						CheckCodes = x != null ? x.CheckCodes.Select(x => new
						{
							x.Id,
							x.Name,
							x.IsActive,
							x.Title,
							ProhibitionCodes = x.ProhibitionCodes.OrderBy(x => x.Name).Select(c => new { c.Id, c.Name, c.IsActive, c.Default, c.StartDate, c.EndDate, Routes = c.Routes.Select(z => new { z.Id, z.Name, z.Code }) }),
							Routes = x.Routes.Select(z => new { z.Id, z.Name, z.Code }),
							SubjectTypes = x.SubjectTypes.Where(s => processSubjectState.Contains(processSubjectState.Find(pr => pr.SubjectId == s.Id && pr.IsNewClient == x.IsActive) ?? new ProcessSubjectState())).Select(st => new { st.Id, st.Name, ClientTypes = st.ClientTypes.Select(cl => new { cl.Id, cl.Name }) })

						}).OrderBy(x => x.Name) : null
					}).OrderBy(z => z.SequenceNumber),
					ProhibitionCodes = prohibitionCodes,
					ClientId = process?.ClientType?.Id,
					SubjectTypes = process?.SubjectTypes.Select(x => new { x.Id, x.Name }),
					ProcessSubjectState = processSubjectState

				}
			};


		}

		public async Task<HttpProcessResponse> AddProcessAsync(HttpProcessRequest request)
		{

			var processToCreate = new Process()
			{
				Name = request.Name,
				BlockTechnologist = request.BlockTechnologist,
				SystemCode = request.SystemCode,
				ClientType = _context.ClientTypes.Find(request.ClientTypeId),
				SystemType = _context.SystemTypes.Find(request.SystemTypeId)
			};



			processToCreate.CheckBlocks.AddRange(_context.CheckBlocks.Where(x => request.CheckBlockIds.Contains(x.Id)));
			processToCreate.CheckCodes.AddRange(_context.CheckCodes.Where(x => request.CheckCodeIds.Contains(x.Id)));
			processToCreate.SubjectTypes.AddRange(_context.SubjectTypes.Where(x => request.SubjectIds.Contains(x.Id)));
			processToCreate.ProhibitionCodes.AddRange(_context.ProhibitionCodes.Where(x => request.ProhibitionCodeIds.Contains(x.Id)));
			_context.Processes.Add(processToCreate);

			await _context.SaveChangesAsync();


			request.ProcessSubjectStates.ForEach(x => x.ProcessId = processToCreate.Id);

			_context.ProcessSubjectStates.AddRange(request.ProcessSubjectStates);

			int executed = _context.SaveChanges();

			if (executed == 0)
			{
				throw new Exception("Не удалось сохранить процесс");
			}

			return new HttpProcessResponse()
			{
				Body = processToCreate.Id
			};
		}

		public async Task<HttpProcessResponse> UpdateProcessAsync(HttpProcessRequest request)
		{

			var processToUpdate = await _context.Processes
												 .Include(x => x.CheckBlocks)
												 .Include(x => x.CheckCodes)
												 .Include(x => x.SubjectTypes)
												 .Include(x => x.ProhibitionCodes)
												 .Include(x => x.SystemType)
												 .Include(x => x.StatusHistories)
												 .AsSplitQuery()
												 .FirstOrDefaultAsync(x => x.Id == request.Id);


			if (processToUpdate == null)
			{
				throw new Exception("Не удалось обновить процесс.");
			}


			//if (request.Type == "Base")
			//{
			//    processToUpdate.Enrollments.Clear();

			//    //processToUpdate.StartDate = request.StartDate != null ? DateTime.Parse(request.StartDate) : null;

			//    if (int.TryParse(request.PrimaryConnectionStatus, out int primaryConnectionStatus))
			//    {
			//        var statusToAdd = _context.Statuses.Find(primaryConnectionStatus);

			//        processToUpdate.Enrollments.Add(new ProcessesToStatuses
			//        {
			//            Status = statusToAdd,
			//            Type = "primaryConnectionStatus"
			//        });

			//    }
			//    //processToUpdate.PrimaryDate = request.PrimaryDate != null ? DateTime.Parse(request.PrimaryDate) : null;
			//    processToUpdate.Count = request.Count;
			//   // processToUpdate.StartDateLastRevision = request.StartDateLastRevision != null ? DateTime.Parse(request.StartDateLastRevision) : null;

			//    if (int.TryParse(request.LastRevisionStatus, out int lastRevisionStatus))
			//    {
			//        var statusToAdd = _context.Statuses.Find(lastRevisionStatus);

			//        processToUpdate.Enrollments.Add(new ProcessesToStatuses
			//        {
			//            Status = statusToAdd,
			//            Type = "lastRevisionStatus"
			//        });

			//    }


			//    //processToUpdate.StatusDate = request.StatusDate != null ? DateTime.Parse(request.StatusDate) : null;

			//    //processToUpdate.LastDateRevision = request.LastDateRevision != null ? DateTime.Parse(request.LastDateRevision) : null;

			//    if(request.StatusHistories.Count != 0) 
			//    {
			//        processToUpdate.StatusHistories.AddRange(request.StatusHistories);
			//    }
			//    _context.SaveChanges();

			//    return new HttpProcessResponse
			//    {
			//        Body = "Информация сохранена"
			//    };
			//}
			processToUpdate.Name = request.Name;

			processToUpdate.SystemCode = request.SystemCode;
			processToUpdate.BlockTechnologist = request.BlockTechnologist;
			processToUpdate.SystemType = _context.SystemTypes.Find(request.SystemTypeId);
			processToUpdate.ClientType = _context.ClientTypes.Find(request.ClientTypeId);

			processToUpdate.CheckBlocks.Clear();
			processToUpdate.CheckCodes.Clear();
			processToUpdate.SubjectTypes.Clear();
			processToUpdate.ProhibitionCodes.Clear();


			processToUpdate.CheckBlocks.AddRange(_context.CheckBlocks.Where(x => request.CheckBlockIds.Contains(x.Id)));
			processToUpdate.CheckCodes.AddRange(_context.CheckCodes.Where(x => request.CheckCodeIds.Contains(x.Id)));
			processToUpdate.SubjectTypes.AddRange(_context.SubjectTypes.Where(x => request.SubjectIds.Contains(x.Id)));
			processToUpdate.ProhibitionCodes.AddRange(_context.ProhibitionCodes.Where(x => request.ProhibitionCodeIds.Contains(x.Id)));

			var processSubjectTypes = _context.ProcessSubjectStates.Where(x => processToUpdate.Id == x.ProcessId);


			_context.ProcessSubjectStates.RemoveRange(processSubjectTypes);

			request.ProcessSubjectStates.ForEach(x => x.ProcessId = processToUpdate.Id);
			_context.ProcessSubjectStates.AddRange(request.ProcessSubjectStates);

			int executed = _context.SaveChanges();

			if (executed == 0)
			{
				throw new Exception("Не удалось сохранить процесс");
			}

			return new HttpProcessResponse()
			{
				Message = "Процесс успешно сохранен"
			};

		}

		public async Task<HttpProcessResponse> DeleteProcessAsync(int id)
		{

			_context.Processes.Remove(_context.Processes.Find(id) ?? new Process());

			_context.ProcessSubjectStates.RemoveRange(_context.ProcessSubjectStates.Where(x => x.ProcessId == id));

			var result = await _context.SaveChangesAsync();

			if (result == 0)
			{
				throw new Exception("Не удалось удалить процесс");
			}

			return new HttpProcessResponse()
			{
				Message = "Процесс успешно удален"
			};
		}

		public async Task<HttpProcessResponse> DownloadFileAsync(HttpDownloadFileRequest request)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			var bytes = new byte[] { };



			using (ExcelPackage excel = new ExcelPackage())
			{
				var firstWorkSheet = excel.Workbook.Worksheets.Add("1-й лист таблицы проверок");



				var pattern = _context.Patterns.FirstOrDefault();

				if (pattern != null)
				{
					for (int x = 1; x < 9; x++)
					{
						firstWorkSheet.Cells[x, 1].Value = x;
					}
					firstWorkSheet.Cells[1, 2].Value = "Название ПО";
					firstWorkSheet.Cells[1 + 1, 2].Value = "Этапы проведения проверок";
					firstWorkSheet.Cells[3, 2].Value = "Субъекты проверки";
					firstWorkSheet.Cells[4, 2].Value = "Наличие ограничения по клиентам";

					firstWorkSheet.Cells[5, 2].Value = "Скрипт";
					firstWorkSheet.Cells[5 + 1, 2].Value = "Информация по тестированию";
					firstWorkSheet.Cells[7, 2].Value = "Предоставление кейсов";
					firstWorkSheet.Cells[7 + 1, 2].Value = "Информация по подключению в бой";

					firstWorkSheet.Cells[5, 3].Value = pattern.Script;
					firstWorkSheet.Cells[5 + 1, 3].Value = pattern.TestInfo;
					firstWorkSheet.Cells[7, 3].Value = pattern.Cases;
					firstWorkSheet.Cells[7 + 1, 3].Value = pattern.ConnectionInformationToProduction;

					var range = firstWorkSheet.Cells[5, 1, 7 + 1, 3];
					range.Style.WrapText = true;
					range.Style.VerticalAlignment = ExcelVerticalAlignment.Justify;

					firstWorkSheet.Column(2).Width = 45;
					firstWorkSheet.Column(3).Width = 70;
				}



				var workSheet = excel.Workbook.Worksheets.Add("Проверки");
				workSheet.Cells[1, 1].Value = "Блок Проверки";
				workSheet.Cells[1, 2].Value = "Комплаенс-проверка";
				workSheet.Cells[1, 3].Value = "Параметры";

				int counter = 1 + 1;
				foreach (var check in request.Checks)
				{
					workSheet.Cells[counter, 1].Value = check.BlockName;

					workSheet.Cells[counter, 2].Value = check.ComplianceCheck;

					workSheet.Cells[counter, 3].Value = "Код проверки";
					workSheet.Cells[++counter, 3].Value = "Код запрета";
					workSheet.Cells[++counter, 3].Value = "Маршрут";
					counter++;
					foreach (var subject in check.Subjects)
					{
						bool newClient = true;
						bool existClient = false;
						string subjectName = subject.SubjectName;


						if (subject.Value[Convert.ToByte(newClient)].validationCodes.Count != 0)
						{
							var column = workSheet.Cells.FirstOrDefault(x => x.Text.ToString() == $"{subjectName}/Нет ПИН");

							var result = 1;
							if (column == null)
							{
								result += workSheet.Dimension.End.Column;
								workSheet.Cells[1, result].Value = $"{subjectName}/Нет ПИН";
							}
							else
							{
								result = column.Start.Column;
							}

							counter -= 3 + 1;
							counter++;


							List<string> checkCodes = new List<string>();
							List<ProhibitionCode> prohibitionCodes = new List<ProhibitionCode>();
							var routes = new List<Models.Route>();
							foreach (var validationCode in subject.Value[Convert.ToByte(newClient)].validationCodes)
							{

								checkCodes.Add(validationCode.Name);

								if (validationCode.newProhibitionCodes.Count != 0)
								{
									foreach (var item in validationCode.newProhibitionCodes)
									{
										prohibitionCodes.Add(item);
									}
								}
								else
								{
									foreach (var item in validationCode.ProhibitionCodes.Where(x => x.Default))
									{
										prohibitionCodes.Add(item);
									}
								}


								if (prohibitionCodes.FirstOrDefault(x => x.Routes.Count != 0) != null)
								{
									foreach (var prohibitionCode in prohibitionCodes)
									{
										var rt = new List<Models.Route>();
										foreach (var item in prohibitionCode.Routes)
										{
											item.Code = $"{prohibitionCode.Name}-{item.Code}";
											rt.Add(item);
										}
										routes.AddRange(rt);
									}
								}
								else
								{
									foreach (var route in validationCode.Routes)
									{
										routes.Add(route);
									}
								}


							}

							workSheet.Cells[counter, result].Value = string.Join(",", checkCodes.ToArray());
							counter += 1;
							workSheet.Cells[counter, result].Value = string.Join(",", prohibitionCodes.ConvertAll(x => x.Name).ToArray());
							counter += 1;

							workSheet.Cells[counter, result].Value = string.Join(",", routes.ConvertAll(x => x.Code).ToArray());
							counter++;


						}
						else
						{
							var result = request.checkedSubjects.FirstOrDefault(x => x.Name == subjectName);
							var column = workSheet.Cells.FirstOrDefault(x => x.Text == $"{subjectName}/Нет ПИН");
							if (result != null)
							{
								int validationCodeRow = counter - 3;
								if (result.clientTypes[1] && column == null)
								{
									int s = 1 + workSheet.Columns.EndColumn;

									workSheet.Cells[1, s].Value = $"{subjectName}/Нет ПИН";
									if (column != null)
									{
										workSheet.Cells[validationCodeRow, column.End.Column].Value = "Не применимо";
									}
								}
								else
								{

									if (column != null)
									{
										workSheet.Cells[validationCodeRow, column.End.Column].Value = "Не применимо";
									}

								}


							}
						}



						if (subject.Value[Convert.ToByte(existClient)].validationCodes.Count != 0)
						{
							var column = workSheet.Cells.FirstOrDefault(x => x.Text == $"{subjectName}/Есть ПИН");

							var result = 1;
							if (column == null)
							{
								result += workSheet.Columns.EndColumn;

								workSheet.Cells[1, result].Value = $"{subjectName}/Есть ПИН";
							}
							else
							{

								result = column.Start.Column;
							}

							counter -= 3 + 1;
							counter++;



							List<string> checkCodes = new List<string>();
							List<ProhibitionCode> prohibitionCodes = new List<ProhibitionCode>();
							var routes = new List<Models.Route>();

							foreach (var validationCode in subject.Value[Convert.ToByte(existClient)].validationCodes)
							{

								checkCodes.Add(validationCode.Name);

								if (validationCode.newProhibitionCodes.Count != 0)
								{
									foreach (var item in validationCode.newProhibitionCodes)
									{
										prohibitionCodes.Add(item);
									}
								}
								else
								{
									foreach (var item in validationCode.ProhibitionCodes.Where(x => x.Default))
									{
										prohibitionCodes.Add(item);
									}
								}

								if (prohibitionCodes.FirstOrDefault(x => x.Routes.Count != 0) != null)
								{
									foreach (var prohibitionCode in prohibitionCodes)
									{
										routes.AddRange(prohibitionCode.Routes.ConvertAll(s => new Models.Route
										{

											Name = s.Name,
											Code = $"{prohibitionCode.Name}-{s.Code}"
										}));
									}
								}
								else
								{
									foreach (var route in validationCode.Routes)
									{
										routes.Add(route);
									}
								}
							}
							workSheet.Cells[counter, result].Value = string.Join(",", checkCodes.ToArray());
							counter += 1;

							workSheet.Cells[counter, result].Value = string.Join(",", prohibitionCodes.ConvertAll(x => x.Name).ToArray());
							counter += 1;

							workSheet.Cells[counter, result].Value = string.Join(",", routes.ConvertAll(x => x.Code).ToArray());
							counter++;


						}
						else
						{
							var result = request.checkedSubjects.FirstOrDefault(x => x.Name == subjectName);
							var column = workSheet.Cells.FirstOrDefault(x => x.Text == $"{subjectName}/Есть ПИН");
							int validationCodeRow = counter - 3;
							if (result != null && column == null)
							{
								if (result.clientTypes[0])
								{
									int s = 1 + workSheet.Columns.EndColumn;
									workSheet.Cells[1, s].Value = $"{subjectName}/Есть ПИН";

									workSheet.Cells[validationCodeRow, s].Value = "Не применимо";
								}
							}
							else
							{
								if (column != null)
								{
									workSheet.Cells[validationCodeRow, column.End.Column].Value = "Не применимо";
								}
							}

						}

					}

					counter += 1;
				}


				workSheet.Cells.AutoFitColumns();
				workSheet.InsertColumn(1, 1);
				workSheet.Cells[1, 1].Value = "№";
				workSheet.Column(1).Width = 4;
				workSheet.Cells[1 + 1, 4 - 1, workSheet.Dimension.End.Row, 4 - 1].Style.WrapText = true;

				var commentColumn = workSheet.Cells[1, workSheet.Dimension.End.Column + 1];
				commentColumn.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				commentColumn.Value = "Комментарии";
				commentColumn.AutoFitColumns();


				workSheet.Column(4 - 1).Width = 25;
				workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column].Style.Font.Bold = true;
				workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
				workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thick;
				workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thick;
				workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thick;
				workSheet.Cells[1, 1, 1, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


				int sequence = 1;
				int number = 3 + 1;

				for (int x = 2; x < workSheet.Dimension.End.Row; x += number)
				{
					var mergeNCells = workSheet.Cells[x, 1, x + 3, 1];
					mergeNCells.Merge = true;

					mergeNCells.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
					mergeNCells.Value = sequence++;
					var mergeBlockCells = workSheet.Cells[x, 2, x + 3, 2];
					mergeBlockCells.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
					mergeBlockCells.Merge = true;

					var mergeCompliancCells = workSheet.Cells[x, 3, x + 3, 3];

					mergeCompliancCells.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
					mergeCompliancCells.Merge = true;

				}
				Dictionary<int, Total> blockDataToTotal = new Dictionary<int, Total>();
				for (int column = 5; column < workSheet.Dimension.End.Column; column++)
				{

					int row = 1 + 1;
					List<string> checkCodes = new List<string>();
					List<string> prohibitionCodes = new List<string>();

					while (row < workSheet.Dimension.End.Row)
					{
						if (!workSheet.Cells[row, column].Text.Contains("Не применимо", StringComparison.OrdinalIgnoreCase))
						{
							checkCodes.Add(workSheet.Cells[row, column].Text);
						}

						row++;

						if (!string.IsNullOrEmpty(workSheet.Cells[row, column].Text))
						{
							prohibitionCodes.Add(workSheet.Cells[row, column].Text);
						}

						row += 4;
					}
					blockDataToTotal.Add(column, new Total
					{
						CheckCodes = checkCodes,
						ProhibitionCodes = prohibitionCodes
					});

				}
				var columnResult = workSheet.Cells[workSheet.Dimension.End.Row + 1, 1, workSheet.Dimension.End.Row + 2, 4];

				columnResult.Merge = true;
				columnResult.Value = "ИТОГ";
				columnResult.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				columnResult.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				var totalRow = workSheet.Dimension.End.Row - 1;

				foreach (var data in blockDataToTotal)
				{
					var checkCodeTotalRow = workSheet.Cells[totalRow, data.Key];
					checkCodeTotalRow.Value = string.Join(",", data.Value.CheckCodes.Distinct());
					checkCodeTotalRow.Style.WrapText = true;
					checkCodeTotalRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
					checkCodeTotalRow.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

					var prohibitionCodeTotalRow = workSheet.Cells[totalRow + 1, data.Key];
					prohibitionCodeTotalRow.Value = string.Join(",", data.Value.ProhibitionCodes);
					prohibitionCodeTotalRow.Style.WrapText = true;
					prohibitionCodeTotalRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
					prohibitionCodeTotalRow.Style.VerticalAlignment = ExcelVerticalAlignment.Top;


				}


				workSheet.Cells[2, 1, workSheet.Dimension.End.Row, workSheet.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[2, 1, workSheet.Dimension.End.Row, workSheet.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[2, 1, workSheet.Dimension.End.Row, workSheet.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[2, 1, workSheet.Dimension.End.Row, workSheet.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;

				foreach (var cell in workSheet.Cells[1, 5, 1, workSheet.Dimension.End.Column].ToList())
				{
					cell.Style.WrapText = true;
					cell.Value = cell.Value?.ToString()?.Replace("/", "\n");
					cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				}

				bytes = await excel.GetAsByteArrayAsync();


			}

			return new HttpProcessResponse()
			{
				Body = bytes
			};
		}


	}
}

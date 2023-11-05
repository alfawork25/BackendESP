using Microsoft.EntityFrameworkCore;
using ESP.Context;
using ESP.Models;
using ESP.Request;
using ESP.Response;


namespace ESP.Services
{
	public class RevisionService
	{
		private readonly ApplicationContext _context;

		public RevisionService(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<HttpRevisionResponse> GetRevisionsAsync(int processId)
		{
			var revisions = await _context.Revisions.Where(z => z.ProcessId == processId).ToListAsync();

			return new HttpRevisionResponse
			{
				Body = revisions
			};
		}

		public async Task<HttpRevisionResponse> AddRevisionAsync(HttpRevisionRequest request)
		{

			var revision = new Revision
			{
				Name = request.ModificationName,
				RevisionType = request.RevisionType,
			};


			var status = _context.Statuses.Find(Convert.ToInt32(request.Status));

			if (!string.IsNullOrEmpty(request.PrimaryModification) || request.Current != null || !string.IsNullOrEmpty(request.StartDate) ||
				!string.IsNullOrEmpty(request.ProcessInfo))
			{
				revision.InfoBlock = new ProcessInfoBlock
				{
					Modification = request.PrimaryModification,
					Current = request.Current,
					Status = status,
					StartDate = request.StartDate,
					ProcessInfo = request.ProcessInfo
				};
			}

			var techBlocksData = new List<string>
			{
				 request.ProcessCode,request.Profile,request.RsOneCode,request.RsOneName,
				 request.GroupCode,request.CheckCall
			};

			if (techBlocksData.Count(z => !(z.Length == 0)) != 0)
			{
				revision.TechnologistBlock = new TechnologistBlock
				{
					ProcessCode = request.ProcessCode,
					Profile = request.Profile,
					RsOneCode = request.RsOneCode,
					RsOneName = request.RsOneName,
					GroupCode = request.GroupCode,
					CheckCall = request.CheckCall,
				};
			}

			var blockTestData = new List<string>
			{
				request.RequestDate,
				request.DirectionDate,
				request.ResultDate,request.EndDate

			};

			if (blockTestData.Count(z => !(z.Length == 0)) != 0)
			{
				revision.BlockTest = new BlockTest
				{
					RequestDate = request.RequestDate,
					DirectionDate = request.DirectionDate,
					ResultDate = request.ResultDate,
					Note = request.Note,
					EndDate = request.EndDate
				};
			};

			var personData = new List<string>
			{
				request.ContactName,
				request.ResponsibleOKBP,
				request.ResponsibleTechnologist,
			};

			if (personData.Count(z => !(z.Length == 0)) != 0)
			{
				revision.Person = new Person
				{
					ContactName = request.ContactName,
					ResponsibleOKBP = request.ResponsibleOKBP,
					ResponsibleTechnoglogist = request.ResponsibleTechnologist,
				};
			};


			var implementaionData = new List<string>
			{
				request.ApprovedProfile,
				request.ApprovedProm,
				request.ApprovedWithNote,
				request.integrated
			};

			if (implementaionData.Count(z => !(z.Length == 0)) != 0)
			{
				revision.Integration = new Integration
				{
					ApprovedProfile = request.ApprovedProfile,
					ApprovedProm = request.ApprovedProm,
					ApprovedWithNote = request.ApprovedWithNote,
					Integrated = request.integrated
				};
			}


			var process = _context.Processes.First(x => x.Id == request.ProcessId);

			if (revision.RevisionType == "Первичная доработка")
			{

				process.StartDate = revision.InfoBlock != null ? request.StartDate : string.Empty;

				process.PrimaryDate = revision.Integration != null ? request.integrated : string.Empty;

			}
			if (revision.RevisionType == "Последняя доработка")
			{

				process.StartDateLastRevision = revision.InfoBlock != null ? request.StartDate : string.Empty;

				process.LastDateRevision = revision.Integration != null ? request.integrated : string.Empty;

			}

			revision.Process = process;
			_context.Revisions.Add(revision);

			await _context.SaveChangesAsync();

			return new HttpRevisionResponse
			{
				Body = revision.Id,
				Message = "доработка добавлена",
			};
		}

		public async Task<HttpRevisionResponse> GetRevisionByIdAsync(int id)
		{
			var revision = await _context.Revisions.Include(x => x.InfoBlock).ThenInclude(z => z.Status)
												   .Include(x => x.TechnologistBlock)
												   .Include(x => x.BlockTest)
												   .Include(x => x.Integration)
												   .Include(x => x.Person)
												   .FirstOrDefaultAsync(z => z.Id == id);

			if (revision == null)
			{
				throw new Exception("Не удалось найти доработку");
			}

			var data = new
			{
				Id = revision.Id,
				Name = revision.Name,
				RevisionType = revision.RevisionType,
				PrimaryModification = revision.InfoBlock != null ?
									  revision.InfoBlock.Modification : string.Empty,
				Current = revision.InfoBlock != null ? revision.InfoBlock.Current : null,
				StartDate = revision.InfoBlock != null ? revision.InfoBlock.StartDate : string.Empty,
				Status = revision.InfoBlock != null ? revision.InfoBlock?.Status?.Id : null,
				ProcessInfo = revision.InfoBlock != null ? revision.InfoBlock.ProcessInfo : string.Empty,
				ProcessCode = revision.TechnologistBlock != null ?
							   revision.TechnologistBlock.ProcessCode : string.Empty,
				Profile = revision.TechnologistBlock != null ? revision.TechnologistBlock.Profile : string.Empty,
				RsOneCode = revision.TechnologistBlock != null ? revision.TechnologistBlock.RsOneCode : string.Empty,
				RsOneName = revision.TechnologistBlock != null ? revision.TechnologistBlock.RsOneName : string.Empty,
				GroupCode = revision.TechnologistBlock != null ? revision.TechnologistBlock.GroupCode : string.Empty,
				CheckCall = revision.TechnologistBlock != null ? revision.TechnologistBlock.CheckCall : string.Empty,
				RequestDate = revision.BlockTest != null ? revision.BlockTest.RequestDate : string.Empty,
				DirectionDate = revision.BlockTest != null ? revision.BlockTest.DirectionDate : string.Empty,
				ResultDate = revision.BlockTest != null ? revision.BlockTest.ResultDate : string.Empty,
				EndDate = revision.BlockTest != null ? revision.BlockTest.EndDate : string.Empty,
				Note = revision.BlockTest != null ? revision.BlockTest.Note : null,
				ApprovedProfile = revision.Integration != null ? revision.Integration.ApprovedProfile : string.Empty,
				ApprovedProm = revision.Integration != null ? revision.Integration.ApprovedProm : string.Empty,
				ApprovedWithNote = revision.Integration != null ? revision.Integration.ApprovedWithNote : string.Empty,
				Integrated = revision.Integration != null ? revision.Integration.Integrated : string.Empty,
				ContactName = revision.Person != null ? revision.Person.ContactName : string.Empty,
				ResponsibleOKBP = revision.Person != null ? revision.Person.ResponsibleOKBP : string.Empty,
				ResponsibleTechnologist = revision.Person != null ?
										  revision.Person.ResponsibleTechnoglogist : string.Empty,
			};

			return new HttpRevisionResponse()
			{
				Body = data
			};
		}

		public async Task<HttpRevisionResponse> UpdateRevisionAsync(HttpRevisionRequest request)
		{
			var revisionUpdate = _context.Revisions.Include(x => x.InfoBlock)
												   .ThenInclude(x => x.Status)
												   .Include(x => x.TechnologistBlock)
												   .Include(x => x.BlockTest)
												   .Include(x => x.Integration)
												   .Include(x => x.Person)
												   .FirstOrDefault(x => x.Id == request.Id);


			if (revisionUpdate == null)
			{
				throw new Exception("Не удалось найти доработку");
			}

			revisionUpdate.Name = request.ModificationName;
			revisionUpdate.RevisionType = request.RevisionType;
			int statusIndex = 0;
			if (int.TryParse(request.Status, out int result))
			{
				statusIndex = result;
			}

			var status = _context.Statuses.Find(Convert.ToInt32(statusIndex));

			if (!string.IsNullOrEmpty(request.PrimaryModification) || request.Current != null || !string.IsNullOrEmpty(request.StartDate) ||
				!string.IsNullOrEmpty(request.ProcessInfo))
			{
				if (revisionUpdate.InfoBlock != null)
				{
					revisionUpdate.InfoBlock.Modification = request.PrimaryModification;
					revisionUpdate.InfoBlock.Current = request.Current;
					revisionUpdate.InfoBlock.StartDate = request.StartDate;
					revisionUpdate.InfoBlock.ProcessInfo = request.ProcessInfo;
					revisionUpdate.InfoBlock.Status = status;

				}
				else
				{
					revisionUpdate.InfoBlock = new ProcessInfoBlock
					{

						Modification = request.PrimaryModification,
						Current = request.Current,
						Status = status,
						StartDate = request.StartDate,
						ProcessInfo = request.ProcessInfo,
					};
				}



			}

			var techBlocksData = new List<string>
			{
				 request.ProcessCode,request.Profile,request.RsOneCode,request.RsOneName,
				 request.GroupCode,request.CheckCall
			};

			if (techBlocksData.Count(z => !(z.Length == 0)) != 0)
			{
				if (revisionUpdate.TechnologistBlock != null)
				{
					revisionUpdate.TechnologistBlock.ProcessCode = request.ProcessCode;
					revisionUpdate.TechnologistBlock.Profile = request.Profile;
					revisionUpdate.TechnologistBlock.RsOneCode = request.RsOneCode;
					revisionUpdate.TechnologistBlock.RsOneName = request.RsOneName;
					revisionUpdate.TechnologistBlock.GroupCode = request.GroupCode;
					revisionUpdate.TechnologistBlock.CheckCall = request.CheckCall;
				}
				else
				{
					revisionUpdate.TechnologistBlock = new TechnologistBlock
					{
						ProcessCode = request.ProcessCode,
						Profile = request.Profile,
						RsOneCode = request.RsOneCode,
						RsOneName = request.RsOneName,
						GroupCode = request.GroupCode,
						CheckCall = request.CheckCall,
					};
				}

			}

			var blockTestData = new List<string>
			{
				request.RequestDate,
				request.DirectionDate,
				request.ResultDate,request.EndDate

			};

			if (blockTestData.Count(z => !(z.Length == 0)) != 0)
			{
				if (revisionUpdate.BlockTest != null)
				{
					revisionUpdate.BlockTest.RequestDate = request.RequestDate;
					revisionUpdate.BlockTest.DirectionDate = request.DirectionDate;
					revisionUpdate.BlockTest.ResultDate = request.ResultDate;
					revisionUpdate.BlockTest.Note = request.Note;
					revisionUpdate.BlockTest.EndDate = request.EndDate;
				}
				else
				{
					revisionUpdate.BlockTest = new BlockTest
					{
						RequestDate = request.RequestDate,
						DirectionDate = request.DirectionDate,
						ResultDate = request.ResultDate,
						Note = request.Note,
						EndDate = request.EndDate
					};

				};
			}

			var personData = new List<string>
			{
				request.ContactName,
				request.ResponsibleOKBP,
				request.ResponsibleTechnologist,
			};

			if (personData.Count(z => !(z.Length == 0)) != 0)
			{

				if (revisionUpdate.Person != null)
				{
					revisionUpdate.Person.ContactName = request.ContactName;
					revisionUpdate.Person.ResponsibleOKBP = request.ResponsibleOKBP;
					revisionUpdate.Person.ResponsibleTechnoglogist = request.ResponsibleTechnologist;
				}
				else
				{
					revisionUpdate.Person = new Person
					{
						ContactName = request.ContactName,
						ResponsibleOKBP = request.ResponsibleOKBP,
						ResponsibleTechnoglogist = request.ResponsibleTechnologist,
					};
				}

			};


			var implementaionData = new List<string>
			{
				request.ApprovedProfile,
				request.ApprovedProm,
				request.ApprovedWithNote,
				request.integrated
			};

			if (implementaionData.Count(z => !(z.Length == 0)) != 0)
			{
				if (revisionUpdate.Integration != null)
				{
					revisionUpdate.Integration.ApprovedProfile = request.ApprovedProfile;
					revisionUpdate.Integration.ApprovedProm = request.ApprovedProm;
					revisionUpdate.Integration.ApprovedWithNote = request.ApprovedWithNote;
					revisionUpdate.Integration.Integrated = request.integrated;
				}
				else
				{
					revisionUpdate.Integration = new Integration
					{
						ApprovedProfile = request.ApprovedProfile,
						ApprovedProm = request.ApprovedProm,
						ApprovedWithNote = request.ApprovedWithNote,
						Integrated = request.integrated
					};
				}
			}



			var process = _context.Processes.Include(x => x.Revisions)
				.First(x => x.Id == request.ProcessId);


			var sortedRevision = process.Revisions.OrderBy(x => x.Id).First();

			if (revisionUpdate.RevisionType == "Первичная доработка")
			{

				revisionUpdate.Process.StartDate = revisionUpdate.InfoBlock != null ? request.StartDate : string.Empty;

				process.PrimaryDate = revisionUpdate.Integration != null ? request.integrated : string.Empty;

			}
			else if (revisionUpdate.RevisionType == "Последняя доработка")
			{

				process.StartDateLastRevision = revisionUpdate.InfoBlock != null ? request.StartDate : string.Empty;

				process.LastDateRevision = revisionUpdate.Integration != null ? request.integrated : string.Empty;

			}


			await _context.SaveChangesAsync();

			return new HttpRevisionResponse
			{
				Body = revisionUpdate.Id,
				Message = "доработка обновлен",
			};
		}

		public async Task<HttpRevisionResponse> DeleteRevisionAsync(int id)
		{
			var revisionToDelete = _context.Revisions.Find(id);

			if (revisionToDelete != null)
			{
				_context.Revisions.Remove(revisionToDelete);

				var relatedInfoBlock = _context.ProcessInfoBlocks.Find(revisionToDelete.ProcessInfoBlockId);
				if (relatedInfoBlock != null)
				{
					_context.ProcessInfoBlocks.Remove(relatedInfoBlock);
				}

				var relatedTechologistBlock = _context.TechnologistBlocks.Find(revisionToDelete.TechnologistBlockId);
				if (relatedTechologistBlock != null)
				{
					_context.TechnologistBlocks.Remove(relatedTechologistBlock);
				}

				var relateBlockTest = _context.BlockTests.Find(revisionToDelete.BlockTestId);

				if (relateBlockTest != null)
				{
					_context.BlockTests.Find(revisionToDelete.BlockTestId);
				}

				var relatedImplementationDelete = _context.Integrations.Find(revisionToDelete.IntegrationId);

				if (relatedImplementationDelete != null)
				{
					_context.Integrations.Remove(relatedImplementationDelete);
				}

				var relatedPersonDelete = _context.Persons.Find(revisionToDelete.PersonId);

				if (relatedPersonDelete != null)
				{
					_context.Persons.Remove(relatedPersonDelete);
				}

			}

			int result = await _context.SaveChangesAsync();

			if (result == 0)
			{
				throw new Exception("Не удалось удалить доработку");
			}

			return new HttpRevisionResponse
			{
				Message = "Доработка успешно удалено"
			};
		}
	}
}

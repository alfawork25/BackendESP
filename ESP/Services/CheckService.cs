using ESP.Context;
using ESP.Models;
using ESP.Repository;
using ESP.Request;
using ESP.Response;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ESP.Services
{
    public class CheckService
    {

        const int FAILED = 0;
        private IRepository<CheckBlock> _checkBlockRepository { get; set; }
        private IRepository<CheckCode> _checkCodeRepository { get; set; }
        private IRepository<SubjectType> _subjectTypeRepository { get; set; }

        private IRepository<Block> _blockRepository { get; set; }
        private IRepository<ClientType> _clientTypeRepository { get; set; }
        private IRepository<SystemType> _systemTypeRepository { get; set; }
        private IRepository<SystemBlock> _systemBlockRepository { get; set; }
        private IRepository<Models.Route> _routeRepository { get; set; }
        private IRepository<Pattern> _patternRepository { get; set; }
        private IRepository<Status> _statusRepository { get; set; }
        private IRepository<ProcessOneLevel> _processFirstLevelRepository { get; set; }

        private IRepository<ProcessTwoLevel> _processTwoLevelRepository { get; set; }

        private IRepository<ReferenceProcess> _processRegistryRepository { get; set; }

        private ApplicationContext _context = null!;

        public CheckService(ApplicationContext appContext)
        {
            _context = appContext;
            _checkBlockRepository = new CheckBlockRepository(appContext);
            _checkCodeRepository = new CheckCodeRepository(appContext);
            _subjectTypeRepository = new SubjectTypeRepository(appContext);
            _blockRepository = new BlockRepository(appContext);
            _clientTypeRepository = new ClientTypeRepository(appContext);
            _systemTypeRepository = new SystemTypeRepository(appContext);
            _systemBlockRepository = new SystemBlockRepository(appContext);
            _routeRepository = new RouteRepository(appContext);
            _patternRepository = new PatternRepository(appContext);
            _statusRepository = new StatusRepository(appContext);
            _processFirstLevelRepository = new ProcessOneLevelRepository(appContext);
            _processTwoLevelRepository = new ProcessTwoLevelRepository(appContext);
            _processRegistryRepository = new ProcessRegistryRepository(appContext);

        }

        public HttpCheckBlockResponse AddCheckBlock(HttpCheckBlockRequest httpCheckBlockRequest)
        {
            var checkBlockToAdd = new CheckBlock();
            
            checkBlockToAdd.ShortName = httpCheckBlockRequest.ShortName;
            checkBlockToAdd.SequenceNumber = httpCheckBlockRequest.SequenceNumber;
            checkBlockToAdd.Block = _blockRepository.GetById(httpCheckBlockRequest.BlockId);
            
            var subjects = _subjectTypeRepository.GetAll().Where(x => httpCheckBlockRequest.Subjects.Contains(x.Name)).ToList();

            if(subjects.Count != 0)
            {
                checkBlockToAdd.SubjectTypes.AddRange(subjects);
            }

           
            var checkCodeIds = httpCheckBlockRequest.CheckCodes.ConvertAll(x => x.Id);
            var checkCodes = _checkCodeRepository.GetAll().Where(x => checkCodeIds.Contains(x.Id)).ToList();

            if(checkCodes.Count != 0)
            {
                foreach(var checkCode in checkCodes.ToList())
                {
                    var sub = httpCheckBlockRequest.CheckCodes.First(x => x.Id == checkCode.Id).subjectsToCheckCode.ConvertAll(x => x.Id);
                    checkCode.SubjectTypes.AddRange(_subjectTypeRepository.GetAll().Where(x => sub.Contains(x.Id)));
                }
                checkBlockToAdd.CheckCodes.AddRange(checkCodes);
            }

            checkBlockToAdd.ClientTypes.AddRange(_clientTypeRepository.GetAll().Where(x => httpCheckBlockRequest.ClientTypes.Contains(x.Id)));
            var result = _checkBlockRepository.Add(checkBlockToAdd);

            if (result == FAILED)
            {
                throw new Exception("Не удалось добавить блок проверки");
            }

            return new HttpCheckBlockResponse() 
            {
                Message = "Блок проверки успешно добавлен" 
            };
        }




        public HttpCheckBlockResponse GetCheckBlockById(int id) 
        {
            
            var checkBlock = _checkBlockRepository.GetById(id);
            return new HttpCheckBlockResponse()
            {
                Body = new {
                    Id = checkBlock.Id,
                    Block = checkBlock.BlockId,
                    SequenceNumber = checkBlock.SequenceNumber,
                    checkBlock.ClientTypes,
                    ShortName = checkBlock.ShortName,
                    CheckCodes = checkBlock?.CheckCodes
                    .Select(x => new {
                        id = x.Id,
                        title = x.Title,    
                        name = x.Name,
                        isActive = x.IsActive,
                        subjectsToCheckCode = x.SubjectTypes.Select(x => new { x.Id, x.Name, }),
                        prohibitionCodes = x.ProhibitionCodes
                    }),
                    Subjects = checkBlock?.SubjectTypes.Select(x => x.Name)
                }
            };
        }

        public HttpCheckBlockResponse GetCheckBlocks() 
        {

            var result = _checkBlockRepository.GetAll().Include(x => x.Block).Include(x => x.SubjectTypes).Include(x => x.ClientTypes)
                                              .Select(x => new
                                              {
                                                  Id = x.Id,
                                                  BlockName = x.Block != null ? x.Block.Name : String.Empty,
                                                  SequenceNumber = x.SequenceNumber,
                                                  x.ClientTypes,
                                                  ShortName = x.ShortName,
                                                  Subjects = x.SubjectTypes,
                                                  CheckCodes = x.CheckCodes.Select(code => new
                                                  {
                                                      Id = code.Id,
                                                      isActive = code.IsActive,
                                                      Name = code.Name
                                                  })
                                              }).OrderBy(z => z.SequenceNumber).ToList();

            if(result.Count() == FAILED) 
            {
                throw new Exception("Не удалось загрузить блоки проверок");
            }

            return new HttpCheckBlockResponse() 
            {
                Body = result
            };
        }

        public HttpCheckBlockResponse UpdateCheckBlock(HttpCheckBlockRequest request) 
        {
            
            var checkBlockToUpdate = _checkBlockRepository.GetById(request.Id);
            checkBlockToUpdate.ShortName = request.ShortName;
            checkBlockToUpdate.SequenceNumber = request.SequenceNumber;
            
            if(checkBlockToUpdate.BlockId != request.BlockId)
            {
                checkBlockToUpdate.Block = _blockRepository.GetById(request.BlockId);
            }

            

            if(request.Subjects.Count > 0)
            {
                var checkBlockSubjects = checkBlockToUpdate.SubjectTypes.Select(x => x.Name);

                var newSubjects = request.Subjects.Where(x => !checkBlockSubjects.Contains(x));

                var subjectsToAdd = _subjectTypeRepository.GetAll().Where(subject => newSubjects.Contains(subject.Name));

                checkBlockToUpdate.SubjectTypes.AddRange(subjectsToAdd);
                checkBlockToUpdate.SubjectTypes.RemoveAll(x => !request.Subjects.Contains(x.Name));
            }

            if (request.CheckCodes.Count != 0)
            {
                
                foreach(var code in checkBlockToUpdate.CheckCodes)
                {
                    code.SubjectTypes.Clear();
                }

                checkBlockToUpdate.CheckCodes.Clear();

                var checkCodeIds = request.CheckCodes.ConvertAll(x => x.Id);
                var checkCodes = _checkCodeRepository.GetAll().Where(x => checkCodeIds.Contains(x.Id));

                if (checkCodes.ToList().Count != 0)
                {
                    foreach (var checkCode in checkCodes.ToList())
                    {
                        var sub = request.CheckCodes.First(x => x.Id == checkCode.Id).subjectsToCheckCode.ConvertAll(x => x.Id);
                        checkCode.SubjectTypes.AddRange(_subjectTypeRepository.GetAll().Where(x => sub.Contains(x.Id)));
                    }
                    checkBlockToUpdate.CheckCodes.AddRange(checkCodes);
                }

            }
            else
            {
                foreach (var code in checkBlockToUpdate.CheckCodes.ToList())
                {
                    code.SubjectTypes.Clear();
                }

                checkBlockToUpdate.CheckCodes.Clear();
            }
            
            checkBlockToUpdate.ClientTypes.Clear();

            checkBlockToUpdate.ClientTypes.AddRange(_clientTypeRepository.GetAll().Where(x => request.ClientTypes.Contains(x.Id)));

            var result = _checkBlockRepository.Update(checkBlockToUpdate);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось обновить блок проверки");
            }

            return new HttpCheckBlockResponse() 
            {
                Message = "Блок проверки обновлен" 
            };
        }

        public HttpCheckBlockResponse DeleteCheckBlock(int id) 
        {

            var deleteCheckCodeToFromSubject = _checkBlockRepository.GetAll().Include(x => x.CheckCodes).ThenInclude(x => x.SubjectTypes)
                                                                             .FirstOrDefault(x => x.Id == id);

            if(deleteCheckCodeToFromSubject != null)
            {
                foreach (var checkCode in deleteCheckCodeToFromSubject.CheckCodes)
                {
                    checkCode.SubjectTypes.Clear();
                }
            }
           

            
            
            var result = _checkBlockRepository.Delete(id);

            if (result == FAILED)
            {
                throw new Exception("Не удалось удалить блок проверки");
            }

            return new HttpCheckBlockResponse()
            {
                Message = "Блок проверки удален"
            };
        }

        public HttpBlockResponse AddBlock(Block block) 
        {
            var result = _blockRepository.Add(block);

            if (result == FAILED)
            {
                throw new Exception("Не удалось добавить блок");
            }

            return new HttpBlockResponse()
            {
                Message = "Блок успешно добавлен"
            };
        }

        public HttpBlockResponse GetBlockId(int id)
        {
            var result = _blockRepository.GetById(id);
            return new HttpBlockResponse()
            {
                Body = result
            };
        }

        public HttpBlockResponse GetBlocks() 
        {
            var result = _blockRepository.GetAll();

            if (result.Count() == FAILED)
            {
                throw new Exception("Не удалось загрузить блоки");
            }

            return new HttpBlockResponse()
            {
                Body = result
            };
        }

        public HttpBlockResponse UpdateBlock(HttpBlockRequest httpBlockUpdateRequest) 
        {
            var blockToUpdate = new Block()
            {
                Id = httpBlockUpdateRequest.Id,
                Name = httpBlockUpdateRequest.Name
            };
            var result = _blockRepository.Update(blockToUpdate);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось обновить блок");
            }
            return new HttpBlockResponse()
            {
                Message = "Блок обновлен"
            };
        }

        public HttpBlockResponse DeleteBlock(int id) 
        {
            var result = _blockRepository.Delete(id);

            if (result == FAILED)
            {
                throw new Exception("Не удалось удалить блок");
            }

            return new HttpBlockResponse()
            {
                Message = "Блок удален"
            };
        }

        public HttpCheckCodeResponse AddCheckCode(HttpCheckCodeRequest httpCheckCodeRequest) 
        {
            var checkCodeToAdd = new CheckCode()
            {
                Name = httpCheckCodeRequest.Name,
                Title = httpCheckCodeRequest.Title,
                IsActive = httpCheckCodeRequest.IsActive
            };

            var result = _checkCodeRepository.Add(checkCodeToAdd);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось добавить код проверки");
            }

            return new HttpCheckCodeResponse()
            {
                Message = "Код проверки успешно добавлен"
            };
        }

        public HttpCheckCodeResponse GetCheckCodeById(int id) 
        {
            var result = _checkCodeRepository.GetById(id);

            return new HttpCheckCodeResponse()
            {
                Body = result
            };
        }

        public HttpCheckCodeResponse GetCheckCodes(bool isOnlyFreeCheckCodes) 
        {
            
            var result = _checkCodeRepository.GetAll();
            
            if(isOnlyFreeCheckCodes)
            {
                result = result.Include(x => x.CheckBlocks).Where(x => x.CheckBlocks.Count == 0);
            }

            if (result.Count() == FAILED)
            {
                throw new Exception("Не удалось загрузить коды проверок");
            }

            return new HttpCheckCodeResponse()
            {
                Body = result.ToList()
            };
        }

        public HttpCheckCodeResponse UpdateCheckCode(HttpCheckCodeRequest httpUpdateCheckCodeRequest) 
        {
            var checkCodeToUpdate = _checkCodeRepository.GetById(httpUpdateCheckCodeRequest.Id);
            checkCodeToUpdate.Title = httpUpdateCheckCodeRequest.Title;
            checkCodeToUpdate.Name = httpUpdateCheckCodeRequest.Name;
            checkCodeToUpdate.IsActive = httpUpdateCheckCodeRequest.IsActive;

            var result = _checkCodeRepository.Update(checkCodeToUpdate);
            
            if(result == FAILED) 
            {
               throw new Exception("Не удалось обновить код проверки");
            }

            return new HttpCheckCodeResponse()
            {
                 Message = "Код проверки успешно обновлен"
            };
        }

        public HttpCheckCodeResponse DeleteCheckCode(int id) 
        {
            var result = _checkCodeRepository.Delete(id);

            if (result == FAILED) 
            {
                throw new Exception("Не удалось удалить код проверки");
            }

            return new HttpCheckCodeResponse()
            {
                Message = "Код проверки успешно удален"
            };
        }

        public HttpSubjectTypeResponse AddSubjectType(HttpSubjectTypeRequest request) 
        {
            var subjectTypeToAdd = new SubjectType()
            {
                Name = request.Name.ToUpperInvariant()
            };

            var result = _subjectTypeRepository.Add(subjectTypeToAdd);

            if (result == FAILED)
            {
                throw new Exception("Не удалось добавить тип субъекта");
            }

            return new HttpSubjectTypeResponse()
            {
                Message = "Тип субъекта успешно добавлен"
            };
        }

        public HttpSubjectTypeResponse GetSubjectTypeById(int id) 
        {
            var result = _subjectTypeRepository.GetById(id);

            return new HttpSubjectTypeResponse()
            {
                Body = result
            };
        }

        public HttpSubjectTypeResponse GetSubjectTypes() 
        {
            var result = _subjectTypeRepository.GetAll().ToList();


            if (result.Count() == FAILED)
            {
                throw new Exception("Не удалось загрузить типы субъектов");
            }

            return new HttpSubjectTypeResponse()
            {
                Body = result
            };
        }

        public HttpSubjectTypeResponse UpdateSubjectType(HttpSubjectTypeRequest request) 
        {
            
            var subjectToUpdate = _subjectTypeRepository.GetById(request.Id);
            subjectToUpdate.Name = request.Name;

            var result = _subjectTypeRepository.Update(subjectToUpdate);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось обновить тип субъекта");
            }

            return new HttpSubjectTypeResponse()
            {
                Message = "Тип субъекта успешно обновлен"
            };
        }

        public HttpSubjectTypeResponse DeleteSubjectType(int id) 
        {
            var result = _subjectTypeRepository.Delete(id);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось удалить тип субъекта");
            }

            return new HttpSubjectTypeResponse()
            {
                Message = "Тип субъекта успешно удален"
            };
        }
        public HttpProhibitionCodeResponse AddProhibitionCode(HttpProhibitonCodeRequest request)
        {
            var checkCode = _checkCodeRepository.GetById(request.CheckCodeId);
            var prohibitionCodeToAdd = new ProhibitionCode
            {
                Name = request.Name,
                IsActive = request.IsActive,
                Default = request.Default,
                StartDate = DateTime.Parse(request.StartDate),
                EndDate = request.EndDate != null ? DateTime.Parse(request.EndDate) : null,
                CheckCode = checkCode,
            };
            _context.ProhibitionCodes.Add(prohibitionCodeToAdd);

            var result = _context.SaveChanges();
            
            if(result == 0)
            {
                throw new Exception("Не удалось добавить код запрета");
            }

            return new HttpProhibitionCodeResponse()
            {
                Body = prohibitionCodeToAdd,
                Message = "Код запрета успешно добавлен"
            };
        }

        public HttpProhibitionCodeResponse UpdateProhibitionCode(HttpProhibitonCodeRequest request)
        {
            var prohibitionCodeToUpdate = _context.ProhibitionCodes.Find(request.Id);
            
            if(prohibitionCodeToUpdate == null)
            {
                return new HttpProhibitionCodeResponse();
            }

            prohibitionCodeToUpdate.Name = request.Name;
            prohibitionCodeToUpdate.IsActive = request.IsActive;
            prohibitionCodeToUpdate.Default = request.Default;
            prohibitionCodeToUpdate.StartDate = DateTime.Parse(request.StartDate);
            prohibitionCodeToUpdate.EndDate =  request.EndDate != null ? DateTime.Parse(request.EndDate) : null;

            var result = _context.SaveChanges();

            if (result == 0)
            {
                throw new Exception("Не удалось обновить код запрета");
            }

            return new HttpProhibitionCodeResponse()
            {
                Body= prohibitionCodeToUpdate,
                Message = "Код запрета успешно обновлен"
            };
        }

        public HttpProhibitionCodeResponse DeleteProhibitionCode(int id)
        {
            var prohibitionCodeToDelete = _context.ProhibitionCodes.FirstOrDefault(x => x.Id == id);

            if(prohibitionCodeToDelete != null)
            {
                _context.ProhibitionCodes.Remove(prohibitionCodeToDelete);
            }

            var result = _context.SaveChanges();

            return new HttpProhibitionCodeResponse()
            {
                Message = "Код запрета успешно удален"
            };
        }

        public HttpProhibitionCodeResponse GetProhibitionCodes(string? filter)
        {
            var prohibitionCode = new List<ProhibitionCode>();

            if(!string.IsNullOrEmpty(filter))
            {
                prohibitionCode = _context.ProhibitionCodes.Include(x => x.CheckCode).Where(x => x.Name == filter).ToList();
            }
            else
            {
                prohibitionCode = _context.ProhibitionCodes.Include(x => x.CheckCode).ToList();
            }
            
            if(prohibitionCode.Count == 0)
            {
                throw new Exception("Нет удалось загрузить коды запрета");
            }

            return new HttpProhibitionCodeResponse
            {
                Body = prohibitionCode.Select(x => new { x.Id,x.Name, x.IsActive,x.Default,
                    x.StartDate,
                    x.EndDate,
                    CheckCode = new { 
                        Id = x?.CheckCode?.Id,
                        Name =  x?.CheckCode?.Name, 
                        IsActive = x?.CheckCode?.IsActive,
                        Title =  x?.CheckCode?.Title}}).OrderBy(x => x.Name)
            };
        }

        public HttpClientTypeResponse GetClientTypeById(int id)
        {
            var response = _clientTypeRepository.GetById(id);

            return new HttpClientTypeResponse()
            {
                Body = response
            };
        }

        public HttpClientTypeResponse GetClientTypes() 
        {
            var response = _clientTypeRepository.GetAll().Include(x => x.SubjectTypes).ToList();

            if(response.Count == 0)
            {
                throw new Exception("Не удалось извлечь тип клиента процесса");
            }


            return new HttpClientTypeResponse
            {
                Body = response
            };
        }
       
        public HttpClientTypeResponse SaveClientType(HttpClientTypeRequest request) 
        {
            var clientType = _clientTypeRepository.GetById(request.Id);
            clientType.Code = request.Code;
            clientType.Name = request.Name;

            if (request.ClientTypeIds.Count != 0)
            {
                clientType.SubjectTypes.AddRange(_context.SubjectTypes.Where(x => request.ClientTypeIds.Contains(x.Id)));
                clientType.SubjectTypes.RemoveAll(x => !request.ClientTypeIds.Contains(x.Id));
            }
            else
            {
                clientType.SubjectTypes.Clear();
            }

            var state = _context.Attach(clientType).State;

            if (state == EntityState.Added) 
            {
                _clientTypeRepository.Add(clientType);                
            }

            if(state == EntityState.Unchanged) 
            {
               _clientTypeRepository.Update(clientType);
            }

            return new HttpClientTypeResponse()
            {
                Body = clientType,
                Message = "Тип клиента успешно сохранен"
            };
        }

        public HttpClientTypeResponse DeleteClientType(int id)
        {
            var response = _clientTypeRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить тип клиента");
            }

            return new HttpClientTypeResponse()
            {
                Body = response
            };
        }


        public HttpSystemTypeResponse GetSystemTypeById(int id)
        {
            var response = _systemTypeRepository.GetById(id);

            return new HttpSystemTypeResponse()
            {
                Body = response
            };
        }

        public HttpSystemTypeResponse GetSystemTypes()
        {
            var response = _systemTypeRepository.GetAll().ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь тип системы");
            }


            return new HttpSystemTypeResponse
            {
                Body = response
            };
        }

        public HttpSystemTypeResponse SaveSystemType(HttpSystemTypeRequest request)
        {
            var systemType = _systemTypeRepository.GetById(request.Id);
            systemType.Code = request.Code;
            systemType.Name = request.Name;

            var state = _context.Attach(systemType).State;

            if (state == EntityState.Added)
            {
                _systemTypeRepository.Add(systemType);
            }

            if (state == EntityState.Unchanged)
            {
                _systemTypeRepository.Update(systemType);
            }

            return new HttpSystemTypeResponse
            {
                Body = systemType,
                Message = "Тип системы успешно сохранен"
            };
        }

        public HttpSystemTypeResponse DeleteSystemType(int id)
        {
            var response = _systemTypeRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить тип систеы процесса");
            }

            return new HttpSystemTypeResponse
            {
                Body = response
            };
        }


        public HttpSystemBlockResponse GetSystemBlockById(int id)
        {
            var response = _systemBlockRepository.GetById(id);

            return new HttpSystemBlockResponse()
            {
                Body = response
            };
        }

        public HttpSystemBlockResponse GetSystemBlocks()
        {
            var response = _systemBlockRepository.GetAll().OrderBy(x => x.Code).ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь блок системы");
            }


            return new HttpSystemBlockResponse
            {
                Body = response
            };
        }

        public HttpSystemBlockResponse SaveSystemBlock(HttpSystemBlockRequest request)
        {
            var systemBlock = _systemBlockRepository.GetById(request.Id);
            systemBlock.Code = request.Code;
            systemBlock.Name = request.Name;

            var state = _context.Attach(systemBlock).State;

            if (state == EntityState.Added)
            {
                _systemBlockRepository.Add(systemBlock);
            }

            if (state == EntityState.Unchanged)
            {
                _systemBlockRepository.Update(systemBlock);
            }

            return new HttpSystemBlockResponse
            {
                Body = systemBlock,
                Message = "Блок системы успешно сохранен"
            };
        }

        public HttpSystemBlockResponse DeleteSystemBlock(int id)
        {
            var response = _systemBlockRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить блок системы");
            }

            return new HttpSystemBlockResponse
            {
                Body = response
            };
        }

        public HttpRouteResponse GetRouteById(int id)
        {
            var response = _routeRepository.GetById(id);

            return new HttpRouteResponse()
            {
                Body = response
            };
        }

        public HttpRouteResponse GetRoutes()
        {
            var response = _routeRepository.GetAll().Select(x => new
            {

                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ProhibitionCodes = x.ProhibitionCodes.Select(x => x.Name),
                CheckCodes = x.CheckCodes.Select(x => x.Name)

            }).ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь маршрут");
            }


            return new HttpRouteResponse
            {
                Body = response
            };
        }

        public HttpRouteResponse SaveRoute(HttpRouteRequest request)
        {
            var route = _routeRepository.GetById(request.Id);
            route.Code = request.Code;
            route.Name = request.Name;
   

            var state = _context.Attach(route).State;

            if(request.ProhibitionCodeIds.Count !=0 )
            {
                route.ProhibitionCodes.AddRange(_context.ProhibitionCodes.Where(x => request.ProhibitionCodeIds.Contains(x.Id)));
                route.ProhibitionCodes.RemoveAll(x => !request.ProhibitionCodeIds.Contains(x.Id));
            }
            else
            {
                route.ProhibitionCodes.Clear();
            }

            if(request.CheckCodeIds.Count != 0) 
            {
                route.CheckCodes.AddRange(_context.CheckCodes.Where(x => request.CheckCodeIds.Contains(x.Id)));
                route.CheckCodes.RemoveAll(x => !request.CheckCodeIds.Contains(x.Id));
            }
            else
            {
                route.CheckCodes.Clear();
            }

            if (state == EntityState.Added)
            {
                _routeRepository.Add(route);
            }

            if (state == EntityState.Unchanged)
            {
                _routeRepository.Update(route);
            }

            return new HttpRouteResponse
            {
                Body = route,
                Message = "Маршрут успешно сохранен"
            };
        }

        public HttpRouteResponse DeleteRoute(int id)
        {
            var response = _routeRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить маршрут");
            }

            return new HttpRouteResponse
            {
                Body = response
            };
        }

        public HttpPatternResponse GetPatternById(int id)
        {
            var response = _patternRepository.GetById(id);

            if(response == null)
            {
                throw new Exception("Не удалось извлечь паттерн");
            }

            return new HttpPatternResponse()
            {
                Body = response
            };
        }

        public HttpPatternResponse GetPatterns()
        {
            var response = _patternRepository.GetAll();

            if(response.Count() == 0)
            {
                throw new Exception("Не удалось извлечь паттерны");
            }

            return new HttpPatternResponse()
            {
                Body = response
            };
        }

        public HttpPatternResponse AddPattern(HttpPatternRequest request)
        {

            var patternToAdd = new Pattern()
            {
                Id = request.Id,
                Script = request.Script,
                TestInfo = request.TestInfo,
                Cases = request.Cases,
                ConnectionInformationToProduction = request.ConnectionInformationToProduction
            };

            var result = _patternRepository.Add(patternToAdd);

            
            if(result == 0)
            {
                throw new Exception("Не удалось сохранить шаблон");
            }

            return new HttpPatternResponse()
            {
                Body = patternToAdd.Id,
            };
        }

        public HttpPatternResponse DeletePattern(int id)
        {

            var result = _patternRepository.Delete(id);

            if (result == 0)
            {
                throw new Exception("Не удалось сохранить шаблон");
            }

            return new HttpPatternResponse()
            {
                Body = "Шаблон удален"
            };
        }

        public HttpPatternResponse UpdatePattern(HttpPatternRequest request)
        {
            var patternToUpdate = _patternRepository.GetById(request.Id);

            patternToUpdate.Script = request.Script;
            patternToUpdate.TestInfo = request.TestInfo;
            patternToUpdate.Cases = request.Cases;
            patternToUpdate.ConnectionInformationToProduction = request.ConnectionInformationToProduction;

            var result = _patternRepository.Update(patternToUpdate);

            if (result == 0)
            {
                throw new Exception("Не удалось сохранить шаблон");
            }

            return new HttpPatternResponse()
            {
                Body = "Изменения приняты"
            };
        }

        public HttpStatusResponse GetStatusById(int id)
        {
            var response = _statusRepository.GetById(id);

            return new HttpStatusResponse()
            {
                Body = response
            };
        }

        public HttpStatusResponse GetStatusList()
        {
            var response = _statusRepository.GetAll().OrderBy(x => x.Code).ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь статусы");
            }


            return new HttpStatusResponse
            {
                Body = response
            };
        }

        public HttpStatusResponse AddStatus(HttpStatusRequest request)
        {

            var status = new Status()
            {
                Code = request.Code,
                Name = request.Name
            };

            _statusRepository.Add(status);

            return new HttpStatusResponse
            {
                Body = status,
                Message = "Статус системы успешно сохранен"
            };
        }

        public HttpStatusResponse UpdateStatus(HttpStatusRequest request)
        {
            var status = _statusRepository.GetById(request.Id);
            status.Code = request.Code;
            status.Name = request.Name;
            
            _statusRepository.Update(status);
            

            return new HttpStatusResponse
            {
                Body = status,
                Message = "Статус системы успешно сохранен"
            };
        }

        public HttpStatusResponse DeleteStatus(int id)
        {
            var response = _statusRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить блок системы");
            }

            return new HttpStatusResponse
            {
                Body = response
            };
        }

        public HttpProcessFirstLevelResponse GetProcessFirstLevelById(int id)
        {
            var response = _processFirstLevelRepository.GetById(id);

            return new HttpProcessFirstLevelResponse()
            {
                Body = response
            };
        }

        
        public  HttpProcessFirstLevelResponse GetProcessFirstLevels()
        {
            var response = _processFirstLevelRepository.GetAll().OrderBy(x => x.Code).ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь процесса 1-го уровня");
            }


            return new HttpProcessFirstLevelResponse
            {
                Body = response
            };
        }

        public HttpProcessFirstLevelResponse AddProcessFirstLevel(HttpProcessFirstLevelRequest request)
        {

            var processFirstLevel = new ProcessOneLevel()
            {
                Code = request.Code,
                Name = request.Name
            };

            _processFirstLevelRepository.Add(processFirstLevel);

            return new HttpProcessFirstLevelResponse
            {
                Body = processFirstLevel,
                Message = "Процесс 1-го уровная успешно сохранен"
            };
        }

        public HttpProcessFirstLevelResponse DeleteProcessFirstLevel(int id)
        {
            var response = _processFirstLevelRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить процесс 1-го уровня");
            }

            return new HttpProcessFirstLevelResponse
            {
                Body = response
            };
        }

        public HttpProcessFirstLevelResponse UpdateProcessFirstLevel(HttpProcessFirstLevelRequest request)
        {
            var processFirstLevel = _processFirstLevelRepository.GetById(request.Id);
            processFirstLevel.Code = request.Code;
            processFirstLevel.Name = request.Name;

            _processFirstLevelRepository.Update(processFirstLevel);


            return new HttpProcessFirstLevelResponse
            {
                Body = processFirstLevel,
                Message = "Процесс 1-го уровня успешно сохранен"
            };
        }

        public HttpProcessTwoLevelResponse GetProcessTwoLevelById(int id)
        {
            var response = _processTwoLevelRepository.GetById(id);

            return new HttpProcessTwoLevelResponse()
            {
                Body = response
            };
        }


        public HttpProcessTwoLevelResponse GetProcessTwoLevels()
        {
                var response = _processTwoLevelRepository.GetAll().OrderBy(x => x.Code).ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь процесса 2-го уровня");
            }


            return new HttpProcessTwoLevelResponse
            {
                Body = response
            };
        }

        public HttpProcessTwoLevelResponse AddProcessTwoLevel(HttpProcessTwoLevelRequest request)
        {

            var processTwoLevel = new ProcessTwoLevel()
            {
                Code = request.Code,
                Name = request.Name
            };

            _processTwoLevelRepository.Add(processTwoLevel);

            return new HttpProcessTwoLevelResponse
            {
                Body = processTwoLevel,
                Message = "Процесс 2-го уровная успешно сохранен"
            };
        }

        public HttpProcessTwoLevelResponse DeleteProcessTwoLevel(int id)
        {
            var response = _processTwoLevelRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить процесс 2-го уровня");
            }

            return new HttpProcessTwoLevelResponse
            {
                Body = response
            };
        }

        public HttpProcessTwoLevelResponse UpdateProcessSecondLevel(HttpProcessTwoLevelRequest request)
        {
            var processTwoLevel = _processTwoLevelRepository.GetById(request.Id);
            processTwoLevel.Code = request.Code;
            processTwoLevel.Name = request.Name;

            _processTwoLevelRepository.Update(processTwoLevel);


            return new HttpProcessTwoLevelResponse
            {
                Body = processTwoLevel,
                Message = "Процесс 2-го уровня успешно сохранен"
            };
        }

        public HttpProcessRegistryResponse GetProcessRegistryById(int id)
        {
            var response = _processRegistryRepository.GetById(id);
           
            return new HttpProcessRegistryResponse()
            {
                Body = response
            };
        }

        public HttpProcessRegistryResponse GetProcessesRegistry()
        {
            var response = _processRegistryRepository.GetAll()
                .Include(z => z.SystemBlock)
                .Include(z => z.ProcessOneLevel)
                .Include(z => z.ProcessTwoLevel).ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь реестр");
            }


            return new HttpProcessRegistryResponse
            {
                Body = response
            };
        }

        public HttpProcessRegistryResponse AddProcessRegistry(HttpProcessRegistryRequest request)
        {
            var processRegistry = new ReferenceProcess
            {
                SystemBlock = _systemBlockRepository.GetById(request.SystemCode),
                ProcessOneLevel = _processFirstLevelRepository.GetById(request.ProcessFirstLevel),
                ProcessTwoLevel = _processTwoLevelRepository.GetById(request.ProcessSecondLevel),
                ProcessNameThirdLevel = request.ProcessNameThirdLevel,
                ProcessCodeThirdLevel = request.ProcessCodeThirdLevel,

                ProcessReferenceUniqueName = request.ProcessReferenceUniqueName,
                ProcessName = request.ProcessName,
                Process = new Process()
                
            };

            if(!string.IsNullOrEmpty(processRegistry.ProcessName))
            {
                processRegistry.Process.Name = processRegistry.ProcessName;
            }

            processRegistry.Process.SystemCode = processRegistry.SystemBlock.Code;

           

            int result = _processRegistryRepository.Add(processRegistry);

            if (result == 0)
            {
                throw new Exception("Не удалось добавить процесс");
            }

            return new HttpProcessRegistryResponse
            {
                Body = processRegistry.Id,
                Message = "Процесс успешно сохранен"
            };

        }

        public HttpProcessRegistryResponse UpdateProcessRegistry(HttpProcessRegistryRequest request)
        {
            var processRegistryUpdate = _processRegistryRepository.GetById(request.Id);
            processRegistryUpdate.SystemBlock = _systemBlockRepository.GetById(request.SystemCode);
            processRegistryUpdate.ProcessOneLevel = _processFirstLevelRepository.GetById(request.ProcessFirstLevel);
            processRegistryUpdate.ProcessTwoLevel = _processTwoLevelRepository.GetById(request.ProcessSecondLevel);
            processRegistryUpdate.ProcessCodeThirdLevel = request.ProcessCodeThirdLevel;
            processRegistryUpdate.ProcessNameThirdLevel = request.ProcessNameThirdLevel;
            processRegistryUpdate.ProcessReferenceUniqueName = request.ProcessReferenceUniqueName;
            processRegistryUpdate.ProcessName = request.ProcessName;

            processRegistryUpdate.Process.Name = processRegistryUpdate.ProcessReferenceUniqueName;
            processRegistryUpdate.Process.SystemCode = processRegistryUpdate.SystemBlock.Code;

            int result = _processRegistryRepository.Update(processRegistryUpdate);

            if (result == 0)
            {
                throw new Exception("Не удалось добавить процесс");
            }


            return new HttpProcessRegistryResponse
            {
                Message = "Процесс успешно сохранен"
            };
        }

        public HttpProcessRegistryResponse DeleteProcessRegistry(int id)
        {
            var processRegistryToDelete = _processRegistryRepository.GetById(id);
 

            _context.SaveChanges();

            int status = _processRegistryRepository.Delete(processRegistryToDelete.Id);

            if (status == 0)
            {
                throw new Exception("Не удалось удалить процесс ");
            }

            return new HttpProcessRegistryResponse
            {
                Body = processRegistryToDelete
            };
        }
    }
}

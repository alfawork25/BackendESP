using ESP.Context;
using ESP.Models;
using ESP.Request;
using ESP.Response;
using ESP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckController : ControllerBase
    {

        private readonly CheckService _checkService;

        public CheckController(ApplicationContext applicationDbContext)
        {
            _checkService = new CheckService(applicationDbContext);
        }

        [HttpGet("{id}")]
        public HttpCheckBlockResponse GetCheckBlockById(int id)
        {
            var httpCheckBlockResponse = new HttpCheckBlockResponse();

            try
            {
                httpCheckBlockResponse = _checkService.GetCheckBlockById(id);
            }

            catch (Exception exception)
            {
                httpCheckBlockResponse.ErrorMessage = exception.Message;
            }

            return httpCheckBlockResponse;
        }

        [HttpGet]
        public HttpCheckBlockResponse GetCheckBlocks()
        {
            var httpCheckBlockResponse = new HttpCheckBlockResponse();

            try
            {
                httpCheckBlockResponse = _checkService.GetCheckBlocks();
            }
            catch (Exception exception)
            {
                httpCheckBlockResponse.ErrorMessage = exception.Message;
            }

            return httpCheckBlockResponse;
        }

        [HttpPost]
        public HttpCheckBlockResponse AddCheckBlock(HttpCheckBlockRequest httpCheckBlockRequest)
        {
            var httpCheckBlockResponse = new HttpCheckBlockResponse();
            try
            {
                httpCheckBlockResponse = _checkService.AddCheckBlock(httpCheckBlockRequest);
            }
            catch (Exception exception)
            {
                httpCheckBlockResponse.ErrorMessage = exception.Message;
            }

            return httpCheckBlockResponse;
        }

        [HttpPut]
        public HttpCheckBlockResponse UpdateCheckBlock(HttpCheckBlockRequest request)
        {
            var httpCheckBlockResponse = new HttpCheckBlockResponse();
            try
            {
                httpCheckBlockResponse = _checkService.UpdateCheckBlock(request);
            }
            catch (Exception exception)
            {
                httpCheckBlockResponse.ErrorMessage = exception.Message;
            }

            return httpCheckBlockResponse;
        }

        [HttpDelete("{id}")]
        public HttpCheckBlockResponse DeleteCheckBlock(int id)
        {
            var httpCheckBlockResponse = new HttpCheckBlockResponse();
            try
            {
                httpCheckBlockResponse = _checkService.DeleteCheckBlock(id);
            }
            catch (Exception exception)
            {
                httpCheckBlockResponse.ErrorMessage = exception.Message;
            }

            return httpCheckBlockResponse;
        }


        [HttpPost]

        public HttpBlockResponse AddBlock(Block block)
        {
            var httpBlockResponse = new HttpBlockResponse();

            try
            {
                httpBlockResponse = _checkService.AddBlock(block);
            }
            catch (Exception exception)
            {

                httpBlockResponse.ErrorMessage = exception.Message;
            }

            return httpBlockResponse;
        }

        [HttpGet("{id}")]
        public HttpBlockResponse GetBlockById(int id)
        {
            var httpBlockResponse = new HttpBlockResponse();

            try
            {
                httpBlockResponse = _checkService.GetBlockId(id);
            }
            catch (Exception exception)
            {

                httpBlockResponse.ErrorMessage = exception.Message;
            }

            return httpBlockResponse;
        }

        [HttpGet]
        public HttpBlockResponse GetBlocks()
        {
            var httpBlockResponse = new HttpBlockResponse();

            try
            {
                httpBlockResponse = _checkService.GetBlocks();
            }
            catch (Exception exception)
            {

                httpBlockResponse.ErrorMessage = exception.Message;
            }

            return httpBlockResponse;
        }

        [HttpPut]
        public HttpBlockResponse UpdateBlock(HttpBlockRequest httpBlockUpdateRequest)
        {
            var httpBlockResponse = new HttpBlockResponse();

            try
            {
                httpBlockResponse = _checkService.UpdateBlock(httpBlockUpdateRequest);
            }
            catch (Exception exception)
            {

                httpBlockResponse.ErrorMessage = exception.Message;
            }

            return httpBlockResponse;
        }

        [HttpDelete("{id}")]
        public HttpBlockResponse DeleteBlock(int id)
        {
            var httpBlockResponse = new HttpBlockResponse();

            try
            {
                httpBlockResponse = _checkService.DeleteBlock(id);
            }
            catch (Exception exception)
            {

                httpBlockResponse.ErrorMessage = exception.Message;
            }

            return httpBlockResponse;
        }

        [HttpPost]
        public HttpCheckCodeResponse AddCheckCode(HttpCheckCodeRequest httpCheckCodeRequest)
        {

            var httpCheckCodeResponse = new HttpCheckCodeResponse();


            try
            {
                httpCheckCodeResponse = _checkService.AddCheckCode(httpCheckCodeRequest);
            }
            catch (Exception exception)
            {

                httpCheckCodeResponse.ErrorMessage = exception.Message;
            }

            return httpCheckCodeResponse;
        }

        [HttpGet("{id}")]
        public HttpCheckCodeResponse GetCheckCodeById(int id)
        {
            var httpCheckCodeResponse = new HttpCheckCodeResponse();

            try
            {
                httpCheckCodeResponse = _checkService.GetCheckCodeById(id);
            }
            catch (Exception exception)
            {

                httpCheckCodeResponse.ErrorMessage = exception.Message;
            }

            return httpCheckCodeResponse;
        }

        [HttpGet]
        public HttpCheckCodeResponse GetCheckCodes(bool isOnlyFreeCheckCodes)
        {
            var httpCheckCodeResponse = new HttpCheckCodeResponse();

            try
            {
                httpCheckCodeResponse = _checkService.GetCheckCodes(isOnlyFreeCheckCodes);
            }
            catch (Exception exception)
            {

                httpCheckCodeResponse.ErrorMessage = exception.Message;
            }

            return httpCheckCodeResponse;
        }

        [HttpPut]
        public HttpCheckCodeResponse UpdateCheckCode(HttpCheckCodeRequest httpUpdateCheckCodeRequest)
        {
            var httpCheckCodeResponse = new HttpCheckCodeResponse();

            try
            {
                httpCheckCodeResponse = _checkService.UpdateCheckCode(httpUpdateCheckCodeRequest);
            }
            catch (Exception exception)
            {

                httpCheckCodeResponse.ErrorMessage = exception.Message;
            }

            return httpCheckCodeResponse;
        }

        [HttpDelete("{id}")]
        public HttpCheckCodeResponse DeleteCheckCode(int id)
        {
            var httpCheckCodeResponse = new HttpCheckCodeResponse();

            try
            {
                httpCheckCodeResponse = _checkService.DeleteCheckCode(id);
            }
            catch (Exception exception)
            {

                httpCheckCodeResponse.ErrorMessage = exception.Message;
            }

            return httpCheckCodeResponse;
        }

        [HttpPost]
        public HttpSubjectTypeResponse AddSubjectType(HttpSubjectTypeRequest request)
        {
            var httpSubjectTypeResponse = new HttpSubjectTypeResponse();

            try
            {
                httpSubjectTypeResponse = _checkService.AddSubjectType(request);
            }
            catch (Exception exception)
            {

                httpSubjectTypeResponse.ErrorMessage = exception.Message;
            }

            return httpSubjectTypeResponse;
        }

        [HttpGet("{id}")]
        public HttpSubjectTypeResponse GetSubjectTypeById(int id)
        {
            var httpSubjectTypeResponse = new HttpSubjectTypeResponse();

            try
            {
                httpSubjectTypeResponse = _checkService.GetSubjectTypeById(id);
            }
            catch (Exception exception)
            {

                httpSubjectTypeResponse.ErrorMessage = exception.Message;
            }

            return httpSubjectTypeResponse;
        }

        [HttpGet]
        public HttpSubjectTypeResponse GetSubjectTypes()
        {
            var httpSubjectTypeResponse = new HttpSubjectTypeResponse();

            try
            {
                httpSubjectTypeResponse = _checkService.GetSubjectTypes();
            }
            catch (Exception exception)
            {

                httpSubjectTypeResponse.ErrorMessage = exception.Message;
            }

            return httpSubjectTypeResponse;
        }

        [HttpPut]

        public HttpSubjectTypeResponse UpdateSubjectType(HttpSubjectTypeRequest request)
        {
            var httpSubjectTypeResponse = new HttpSubjectTypeResponse();

            try
            {
                httpSubjectTypeResponse = _checkService.UpdateSubjectType(request);
            }
            catch (Exception exception)
            {

                httpSubjectTypeResponse.ErrorMessage = exception.Message;
            }

            return httpSubjectTypeResponse;
        }

        [HttpDelete("{id}")]
        public HttpSubjectTypeResponse DeleteSubjectType(int id)
        {
            var httpSubjectTypeResponse = new HttpSubjectTypeResponse();

            try
            {
                httpSubjectTypeResponse = _checkService.DeleteSubjectType(id);
            }
            catch (Exception exception)
            {

                httpSubjectTypeResponse.ErrorMessage = exception.Message;
            }

            return httpSubjectTypeResponse;
        }

        [HttpPost]
        public HttpProhibitionCodeResponse AddProhibitionCode(HttpProhibitonCodeRequest request)
        {


            var httpProhibitionCodeResponse = new HttpProhibitionCodeResponse();
            try
            {
                httpProhibitionCodeResponse = _checkService.AddProhibitionCode(request);
            }
            catch (Exception exception)
            {

                httpProhibitionCodeResponse.ErrorMessage = exception.Message;
            }

            return httpProhibitionCodeResponse;
        }

        [HttpPut]
        public HttpProhibitionCodeResponse UpdateProhibitionCode(HttpProhibitonCodeRequest request)
        {
            var httpProhibitionCodeResponse = new HttpProhibitionCodeResponse();
            try
            {
                httpProhibitionCodeResponse = _checkService.UpdateProhibitionCode(request);
            }
            catch (Exception exception)
            {

                httpProhibitionCodeResponse.ErrorMessage = exception.Message;
            }

            return httpProhibitionCodeResponse;
        }
        [HttpGet]
        public HttpProhibitionCodeResponse GetProhibitionCodes(string? filter)
        {
            var httpProhibitionCodeResponse = new HttpProhibitionCodeResponse();
            try
            {
                httpProhibitionCodeResponse = _checkService.GetProhibitionCodes(filter);
            }
            catch (Exception exception)
            {

                httpProhibitionCodeResponse.ErrorMessage = exception.Message;
            }

            return httpProhibitionCodeResponse;
        }

        [HttpDelete("{id}")]
        public HttpProhibitionCodeResponse DeleteProhibitionCode(int id)
        {
            var httpProhibitionCodeResponse = new HttpProhibitionCodeResponse();
            try
            {
                httpProhibitionCodeResponse = _checkService.DeleteProhibitionCode(id);
            }
            catch (Exception exception)
            {

                httpProhibitionCodeResponse.ErrorMessage = exception.Message;
            }

            return httpProhibitionCodeResponse;
        }

       
        [HttpGet("{id}")]
        public HttpClientTypeResponse GetClientTypeById(int id)
        {
            var response = new HttpClientTypeResponse();
            try
            {
                response = _checkService.GetClientTypeById(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }
        [HttpGet]
        public HttpClientTypeResponse GetClientTypes() 
        {
            var response = new HttpClientTypeResponse();
            try
            {
                response = _checkService.GetClientTypes();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpClientTypeResponse SaveClientType(HttpClientTypeRequest request)
        {
            var response = new HttpClientTypeResponse();
            try
            {
                response = _checkService.SaveClientType(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpClientTypeResponse DeleteClientType(int id)
        {
            var response = new HttpClientTypeResponse();
            try
            {
                response = _checkService.DeleteClientType(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public HttpSystemTypeResponse GetSystemTypeById(int id)
        {
            var response = new HttpSystemTypeResponse();
            try
            {
                response = _checkService.GetSystemTypeById(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpSystemTypeResponse GetSystemTypes()
        {
            var response = new HttpSystemTypeResponse();
            try
            {
                response = _checkService.GetSystemTypes();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpSystemTypeResponse SaveSystemType(HttpSystemTypeRequest request)
        {
            var response = new HttpSystemTypeResponse();
            try
            {
                response = _checkService.SaveSystemType(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpSystemTypeResponse DeleteSystemType(int id)
        {
            var response = new HttpSystemTypeResponse();
            try
            {
                response = _checkService.DeleteSystemType(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }


        [HttpGet("{id}")]
        public HttpSystemBlockResponse GetSystemBlockById(int id)
        {
            var response = new HttpSystemBlockResponse();
            try
            {
                response = _checkService.GetSystemBlockById(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpSystemBlockResponse GetSystemBlocks()
        {
            var response = new HttpSystemBlockResponse();
            try
            {
                response = _checkService.GetSystemBlocks();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpSystemBlockResponse SaveSystemBlock(HttpSystemBlockRequest request)
        {
            var response = new HttpSystemBlockResponse();
            try
            {
                response = _checkService.SaveSystemBlock(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpSystemBlockResponse DeleteSystemBlock(int id)
        {
            var response = new HttpSystemBlockResponse();
            try
            {
                response = _checkService.DeleteSystemBlock(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public HttpRouteResponse GetRouteById(int id)
        {
            var response = new HttpRouteResponse();
            try
            {
                response = _checkService.GetRouteById(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpRouteResponse GetRoutes()
        {
            var response = new HttpRouteResponse();
            try
            {
                response = _checkService.GetRoutes();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpRouteResponse SaveRoute(HttpRouteRequest request)
        {
            var response = new HttpRouteResponse();
            try
            {
                response = _checkService.SaveRoute(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpRouteResponse DeleteRoute(int id)
        {

            var response = new HttpRouteResponse();
            try
            {
                response = _checkService.DeleteRoute(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }


        [HttpGet("{id}")]
        public HttpPatternResponse GetPatternById(int id)
        {
            var response = new HttpPatternResponse();
            try
            {
                response = _checkService.GetPatternById(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpPatternResponse GetPatterns()
        {
            var response = new HttpPatternResponse();
            try
            {
                response = _checkService.GetPatterns();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpPatternResponse AddPattern(HttpPatternRequest request)
        {
            var response = new HttpPatternResponse();
            try
            {
                response = _checkService.AddPattern(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpPatternResponse DeletePattern(int id)
        {
            var response = new HttpPatternResponse();
            try
            {
                response = _checkService.DeletePattern(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPut]
        public HttpPatternResponse UpdatePattern(HttpPatternRequest request)
        {
            var response = new HttpPatternResponse();
            try
            {
                response = _checkService.UpdatePattern(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public HttpStatusResponse GetStatusById(int id)
        {
            var response = new HttpStatusResponse();
            try
            {
                response = _checkService.GetStatusById(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpStatusResponse GetStatusList()
        {
            var response = new HttpStatusResponse();

            try
            {
                response = _checkService.GetStatusList();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpStatusResponse AddStatus(HttpStatusRequest request)
        {
            var response = new HttpStatusResponse();
            
            try
            {
                response = _checkService.AddStatus(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpStatusResponse DeleteStatus(int id)
        {
            var response = new HttpStatusResponse();

            try
            {
                response = _checkService.DeleteStatus(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPut]
        public HttpStatusResponse UpdateStatus(HttpStatusRequest request)
        {
            var response = new HttpStatusResponse();

            try
            {
                response = _checkService.UpdateStatus(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public HttpProcessFirstLevelResponse GetProcessFirstLevelById(int id)
        {
            var response = new HttpProcessFirstLevelResponse();
            try
            {
                response = _checkService.GetProcessFirstLevelById(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpProcessFirstLevelResponse GetProcessFirstLevels()
        {
            var response = new HttpProcessFirstLevelResponse();

            try
            {
                response = _checkService.GetProcessFirstLevels();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpProcessFirstLevelResponse AddProcessFirstLevel(HttpProcessFirstLevelRequest request)
        {
            var response = new HttpProcessFirstLevelResponse();

            try
            {
                response = _checkService.AddProcessFirstLevel(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpProcessFirstLevelResponse DeleteProcessFirstLevel(int id)
        {
            var response = new HttpProcessFirstLevelResponse();

            try
            {
                response = _checkService.DeleteProcessFirstLevel(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPut]
        public HttpProcessFirstLevelResponse UpdateProcessFirstLevel(HttpProcessFirstLevelRequest request)
        {
            var response = new HttpProcessFirstLevelResponse();

            try
            {
                response = _checkService.UpdateProcessFirstLevel(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public HttpProcessTwoLevelResponse GetProcessTwoLevelById(int id)
        {
            var response = new HttpProcessTwoLevelResponse();
            try
            {
                response = _checkService.GetProcessTwoLevelById(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpProcessTwoLevelResponse GetProcessTwoLevels()
        {
            var response = new HttpProcessTwoLevelResponse();

            try
            {
                response = _checkService.GetProcessTwoLevels();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpProcessTwoLevelResponse AddProcessTwoLevel(HttpProcessTwoLevelRequest request)
        {
            var response = new HttpProcessTwoLevelResponse();

            try
            {
                response = _checkService.AddProcessTwoLevel(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpProcessTwoLevelResponse DeleteProcessTwoLevel(int id)
        {
            var response = new HttpProcessTwoLevelResponse();

            try
            {
                response = _checkService.DeleteProcessTwoLevel(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPut]
        public HttpProcessTwoLevelResponse UpdateProcessSecondLevel(HttpProcessTwoLevelRequest request)
        {
            var response = new HttpProcessTwoLevelResponse();

            try
            {
                response = _checkService.UpdateProcessSecondLevel(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public HttpProcessRegistryResponse GetProcessRegistryById(int id)
        {
            var response = new HttpProcessRegistryResponse();
            try
            {
                response = _checkService.GetProcessRegistryById(id);

            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpProcessRegistryResponse GetProcessesRegistry()
        {
            var response = new HttpProcessRegistryResponse();

            try
            {
                response = _checkService.GetProcessesRegistry();
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpPost]
        public HttpProcessRegistryResponse AddProcessRegistry(HttpProcessRegistryRequest request)
        {
            var response = new HttpProcessRegistryResponse();

            try
            {
                response = _checkService.AddProcessRegistry(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public HttpProcessRegistryResponse DeleteProcessRegistry(int id)
        {
            var response = new HttpProcessRegistryResponse();

            try
            {
                response = _checkService.DeleteProcessRegistry(id);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

         [HttpPut]
        public HttpProcessRegistryResponse UpdateProcessRegistry(HttpProcessRegistryRequest request)
        {
            var response = new HttpProcessRegistryResponse();

            try
            {
                  response = _checkService.UpdateProcessRegistry(request);
            }
            catch (Exception exception)
            {
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

    }

}

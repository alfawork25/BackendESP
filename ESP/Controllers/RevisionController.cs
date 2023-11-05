using ESP.Context;
using ESP.Request;
using ESP.Response;
using ESP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RevisionController : ControllerBase
	{
		private readonly RevisionService _revisionService;

		public RevisionController(ApplicationContext applicationDbContext)
		{
			_revisionService = new RevisionService(applicationDbContext);
		}

        [HttpGet]
        public async Task<IActionResult> GetRevisions(int processId)
        {
            var response = new HttpRevisionResponse();

			try
			{
				response = await _revisionService.GetRevisionsAsync(processId);
			}
			catch (Exception exception)
			{
				response.ErrorMessage = exception.Message;
			}

			return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddRevision(HttpRevisionRequest request)
        {
            var response = new HttpRevisionResponse();

			try
			{
			response = await _revisionService.AddRevisionAsync(request);
			}
			catch (Exception exception)
			{
				response.ErrorMessage = exception.Message;
			}

			return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRevisionById(int id)
        {

            var response = new HttpRevisionResponse();

			try
			{
				response = await _revisionService.GetRevisionByIdAsync(id);
			}
			catch (Exception exception)
			{
				response.ErrorMessage = exception.Message;
			}

			return Ok(response);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateRevision(HttpRevisionRequest request)
        {
            var response = new HttpRevisionResponse();

			try
			{
				response = await _revisionService.UpdateRevisionAsync(request);
			}
			catch (Exception exception)
			{
				response.ErrorMessage = exception.Message;
			}

			return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRevision(int id)
        {
            var response = new HttpRevisionResponse();

			try
			{
				response = await _revisionService.DeleteRevisionAsync(id);
			}
			catch (Exception exception)
			{
				response.ErrorMessage = exception.Message;
			}

			return Ok(response);
        }
    }
}

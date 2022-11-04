using AdvertApi.Models;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvertAPI.Controllers
{
    [ApiController]
    [Route("adverts/v1")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;

       public  AdvertController(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        /*produces some kind of output but we don't have the model for that ouput
         * so we go an create it
        */
        [ProducesResponseType(201,Type=typeof(CreateAdvertResponse))] //create 201 response
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string recordId;
            try
            {
                recordId = await _advertStorageService.Add(model);

            }
            catch(KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
            return StatusCode(201, new CreateAdvertResponse { Id = recordId });

        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]

        [ProducesResponseType(200)]

        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
            return new OkResult();//return 200
        }
    }
}

using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Core;
using WebApplicationApi.DTOs;
using WebApplicationApi.Services.Abstractions;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedApiController : ControllerBase // Fix: Inherit from ControllerBase
    {
        private readonly IRedService _redService;

        public RedApiController(IRedService redService)
        {
            _redService = redService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RedDTO>>> GetListAsync()
        {
            Response<List<RedDTO>> response = await _redService.GetListAsync();
            return ControllerBaseValidation(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RedDTO>> GetOneSpecialists([FromRoute] int id)
        {
            Response<RedDTO> response = await _redService.GetOneAsync(id);
            return ControllerBaseValidation(response);
        }
        [HttpPost]
        public async Task<ActionResult<RedDTO>> CreateRed([FromBody] RedDTO dto)
        {
            Response<RedDTO> response = await _redService.CreateAsync(dto);
            return ControllerBaseValidation(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteRed([FromRoute] int id)
        {
            Response<object> response = await _redService.DeleteAsync(id);
            return ControllerBaseValidation(response);
        }
        [HttpPut]
        public async Task<ActionResult<RedDTO>> UpdateRed([FromBody] RedDTO tdo)
        {
            Response<RedDTO> response = await _redService.EditAsync(tdo);
            return ControllerBaseValidation(response);
        }
        // Fix: Add ControllerBaseValidation method to handle WebApplicationApi.Core.Response<T>
        private ActionResult<T> ControllerBaseValidation<T>(Response<T> response)
        {
            if (response.IsSuccess)
            {
                return Ok(response.Result);
            }
            else
            {
                return BadRequest(new { response.Message, response.Errors });
            }
        }
    }
}

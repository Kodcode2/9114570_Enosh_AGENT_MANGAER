using agent_api.Dto;
using agent_api.Model;
using agent_api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace agent_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(ITargetInterface targetService) : ControllerBase
    {

        [HttpPost("Targets")]
        [ProducesResponseType(typeof(TargetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TargetDto>> CreateTarget([FromBody] TargetDto targetDto)
        {
            try
            {
                TargetDto newTarget = await targetService.CreateTargetAsync(targetDto);
                return Created("new target", newTarget);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Targets")]
        [ProducesResponseType(typeof(List<TargetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<TargetDto>>> GetAllTargets()
        {
            try
            {
                return Ok(await targetService.GetAllTargetsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

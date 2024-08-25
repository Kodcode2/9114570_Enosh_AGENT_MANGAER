using agent_api.Dto;
using agent_api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace agent_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionsController(IMissionService missionService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<MissionDto>>> GetAllMissions()
        {
            return Ok(await missionService.GetAllMissionsAsync());

        }


        [HttpPost("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateMissions()
        {
            await missionService.UpdateMissionsAsync();
            return Ok();

        }

        [HttpPut("Assign/{missionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AssignMission(long missionId)
        {
            try
            {
                await missionService.AssignMissionAsync(missionId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using agent_api.Dto;
using agent_api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace agent_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ViewController(ITargetService targetService, IAgentService agentService, IMissionService missionService ) : ControllerBase
    {

        [HttpGet("Agents")]
        [Authorize]
        [ProducesResponseType(typeof(List<TargetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AgentDto>>> GetAllAgents()
        {
            try
            {
                return Ok(await agentService.GetAllAgentsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Missions")]
        [Authorize] 
        [ProducesResponseType(typeof(List<MissionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MissionDto>>> GetAllMissions()
        {
            return Ok(await missionService.GetAllMissionsAsync());

        }


        [HttpGet("Targets")]
        [Authorize] 
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

﻿using agent_api.Dto;
using agent_api.Model;
using agent_api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace agent_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TargetsController(ITargetService targetService) : ControllerBase
    {

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(TargetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<TargetDto>> CreateTarget([FromBody] TargetDto targetDto)
        {
            try
            {
                TargetDto newTarget = await targetService.CreateTargetAsync(targetDto);
                return CreatedAtAction(nameof(CreateTarget),newTarget);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



       


        [HttpPut("{id}/Pin")]
        [Authorize] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PinTarget([FromBody] LocationDto pinLocation, long id)
        {
            
                await targetService.PinTargetLocationAsync(pinLocation, id);
                return NoContent();
           
        }
        
        [HttpPut("{id}/Move")]
        [Authorize] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> MoveTarget([FromBody] DirectionDto direction, long id)
        {
            try
            {
                await targetService.MoveTargetLocationAsync(direction, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

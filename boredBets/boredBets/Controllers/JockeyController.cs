﻿using boredBets.Models.Dtos;
using boredBets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using boredBets.Repositories.Interface;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JockeyController : ControllerBase
    {
        private readonly IJockeyInterface jockeyInterface;

        public JockeyController(IJockeyInterface jockeyInterface)
        {
            this.jockeyInterface = jockeyInterface;
        }

        [HttpPost("JockeyPost")]
        public async Task<ActionResult<Jockey>> Post(JockeyCreateDto jockeyCreateDto)
        {
            return StatusCode(201, await jockeyInterface.Post(jockeyCreateDto));
        }

        [HttpGet("GetAllJockeys")]
        public async Task<ActionResult<Jockey>> GetAllJockey()
        {
            return StatusCode(201, await jockeyInterface.GetAllJockey());
        }

        [HttpGet("GetJockeyById")]
        public async Task<ActionResult<Jockey>> GetJockeyById(Guid JockeyId) 
        {
            var result = await jockeyInterface.GetJockeyById(JockeyId);

            if (result == null)
            {
                NotFound();
            }

            return result;
        }
    }
}

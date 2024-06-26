﻿using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantInterface _participant;

        public ParticipantController(IParticipantInterface participant)
        {
            _participant = participant;
        }

        [HttpPost("ParticipantPost")]
        public async Task<ActionResult<Participant>> Post(ParticipantDto participantDto) 
        {
            return StatusCode(201, await _participant.Post(participantDto));
        }

        
    }
}

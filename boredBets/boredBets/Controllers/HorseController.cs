﻿using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorseController : ControllerBase
    {
        private readonly IHorseInterface horseInterface;

        public HorseController(IHorseInterface horseInterface)
        {
            this.horseInterface = horseInterface;
        }

        [HttpPost]
        public async Task<ActionResult<Horse>> Post(HorseCreateDto horseCreateDto) 
        {
            return StatusCode(201, await horseInterface.Post(horseCreateDto));
        }
    }
}

﻿using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IHorseInterface
    {
        Task<Horse> Post(HorseCreateDto horseCreateDto);
        Task<IEnumerable<HorseContentDto>> GetAllHorse();

        Task<HorseContentDto> GetHorseById(Guid HorseId);
        Task<object> DeleteHorseAndJockeyByHorseId(Guid Id);

    }
}

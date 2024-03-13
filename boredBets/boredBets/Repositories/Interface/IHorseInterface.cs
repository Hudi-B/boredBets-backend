﻿using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IHorseInterface
    {
        Task<Horse> Post(HorseCreateDto horseCreateDto);
        Task<IEnumerable<Horse>> GetAllHorse();

        Task<Horse> GetHorseById(Guid HorseId);
        Task<object> DeleteHorseAndJockeyByHorseId(Guid Id);

    }
}

namespace boredBets.Models.Dtos
{
  public record HorseContentDto(Guid Id, string Name, int Age,
      bool Stallion);
  public record HorseUpdateDto(string Name, int Age, bool Stallion);

  public record HorseCreateDto(string Name, int Age, bool Stallion);
}

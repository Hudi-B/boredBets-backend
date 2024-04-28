namespace boredBets.Models
{
    public class FilePathConstants
    {
        public static readonly string StaticData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "staticData");

        // Common files
        public static readonly string FamilyNames = Path.Combine(StaticData, "familyNames.txt");
        public static readonly string MaleNames = Path.Combine(StaticData, "maleNames.txt");
        public static readonly string FemaleNames = Path.Combine(StaticData, "femaleNames.txt");
        public static readonly string MaleMiddleNames = Path.Combine(StaticData, "maleMiddleNames.txt");
        public static readonly string FemaleMiddleNames = Path.Combine(StaticData, "femaleMiddleNames.txt");
        public static readonly string Countries = Path.Combine(StaticData, "countries.txt");

        // Specific files
        public static readonly string Tracks = Path.Combine(StaticData, "trackNames.txt");
        public static readonly string MaleHorses = Path.Combine(StaticData, "maleHorses.txt");
        public static readonly string FemaleHorses = Path.Combine(StaticData, "femaleHorses.txt");
        public static readonly string HorseFirst = Path.Combine(StaticData, "horseFirst.txt");
        public static readonly string HorseSecond = Path.Combine(StaticData, "horseSecond.txt");
    }
}

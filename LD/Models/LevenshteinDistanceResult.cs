
namespace LD.Models
{
    public class LevenshteinDistanceResult
    {
        public int Distance { get; set; }
        public int[,] GraphData { get; set; }
    }
}

namespace BingoModels.ViewModels
{
    public class BingoGameViewModel
    {
        public BingoCardViewModel Card { get; init; } = null!;
        public int[,] SelectedLines { get; init; } = null!;
        public int LinesCount { get; set; } = 0;
        public List<int> OrderedSelectedNumbers { get; init; } = null!;
    }
}

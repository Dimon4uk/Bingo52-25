using BingoCore.GameModels.Interfaces;

namespace BingoCore.GameModels.Card
{
    public class BingoCard : ICard
    {
        public BingoCard()
        {
            //automapper use it
        }
        public BingoCard(int size,  int[,] cells)
        {
            Size = size;
          
            CardGrid = cells;
        }

        public int[,] CardGrid { get; private set; } = null!;

        public int Size { get; private set; }

        public int Id { get; private set; }

    }
}

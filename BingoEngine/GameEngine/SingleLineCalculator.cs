using BingoCore.GameEngine.Interfaces;
using BingoCore.GameModels.Interfaces;
using System.Collections.Concurrent;

namespace BingoCore.GameEngine
{
    public class SingleLineCalculator : IWinningLineCalculator
    {
        private readonly SemaphoreSlim semaphoreSlim = new(1, 1);
        public (int[,] winLineArray, int lines) Calculate(ICard card, IPlayer player)
        {
            try
            {
                int winLineCount = 0;
                int[,] winLinesMask = new int[card.Size, card.Size];
                semaphoreSlim.Wait();
                bool winMainDiagonal = true;
                bool winSecondaryDiagonal = true;
                bool winRow = true;
                bool winCol = true;
                for (int i = 0; i < card.Size; i++)
                {
                    
                    for(int j = 0; j < card.Size - 1; j++)
                    
                    {
                        
                        //win rows check
                        winRow = IsNeighborSelected(
                        player.SelectedNumbers,
                        card.CardGrid,
                        i, j,
                        i, j + 1) && winRow;

                        //win cols check
                        winCol = IsNeighborSelected(
                        player.SelectedNumbers,
                        card.CardGrid,
                        j, i,
                        j + 1, i) && winCol;

                        //check main diagonal
                        winMainDiagonal = i == 0 && IsNeighborSelected(
                        player.SelectedNumbers,
                        card.CardGrid,
                        j, j,
                        j + 1, j + 1) && winMainDiagonal;

                        //check secondary diagonal
                        winSecondaryDiagonal = i == 0 && IsNeighborSelected(
                        player.SelectedNumbers,
                        card.CardGrid,
                        j, card.Size - (j + 1),
                        j + 1, card.Size - (j + 2)) && winSecondaryDiagonal;

                    }

                    //write mask
                    if (winRow
                        || winCol
                        || winMainDiagonal
                        || winSecondaryDiagonal)
                    {
                      
                    for(int k = 0; k < card.Size; k++)
                    //Parallel.For(0, card.Size, (k) =>
                    {
                        if (winRow) 
                        {
                            winLinesMask[i, k] = card.CardGrid[i, k];
                            winLineCount++;
                        }

                        if (winCol) 
                        { 
                            winLinesMask[k, i] = card.CardGrid[k, i];
                            winLineCount++;
                        }
                        // these 2 'if' below can be true only when i == 0
                        if (winMainDiagonal) 
                        { 
                            winLinesMask[k, k] = card.CardGrid[k, k];
                            winLineCount++;
                        }
                        if (winSecondaryDiagonal)
                        {
                            winLinesMask[k, card.Size - (k + 1)] =
                            card.CardGrid[k, card.Size - (k + 1)];
                            winLineCount++;
                        }
                    }
                        
                    }
                    winCol = true;
                    winRow = true;
                }
                return (winLinesMask, winLineCount/card.Size);
            }
            finally 
            {
                semaphoreSlim.Release();
            }
            
        }

        private bool IsNeighborSelected(
          ConcurrentBag<int> selectedNumbers,
          int[,] matrix,
          int y, int x,
          int nextY, int nextX)
        {
            return selectedNumbers.Contains(matrix[y, x])
                 && selectedNumbers.Contains(matrix[nextY, nextX]);
        }
    }
}

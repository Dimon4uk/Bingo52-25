using BingoCore.GameParameters.Enums;
using BingoCore.GameParameters.Interfaces;


namespace BingoCore.GameParameters
{
    public class BingoGameParameters : IGameParameters
    {
        public GameCodes GameCode { get; private set; } 

        public int Size { get; private set; } 

        public int MinValue { get; private set; } 

        public int MaxValue { get; private set; }

        public BingoGameParameters(GameCodes gameCode, int size, int minValue, int maxValue )
        {
            GameCode = gameCode;
            Size = size;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}


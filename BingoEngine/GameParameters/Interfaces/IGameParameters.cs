using BingoCore.GameParameters.Enums;

namespace BingoCore.GameParameters.Interfaces
{
    public interface IGameParameters
    {
        public GameCodes GameCode { get; }
        public int Size { get; }
        public int MinValue { get; }
        public int MaxValue { get; }
    }
}



namespace BingoCore.GameModels.Interfaces
{
    public interface ICard
    {
        int[,] CardGrid { get; }
        int Size { get; }
        int Id { get; }
        
    }
}


using BingoModels.ViewModels;

namespace BingoServices.Services.Interfaces
{
    public interface IBingoService
    {
        Task<BingoGameViewModel> CreateBingoCard(PlayerViewModel playerInfo);
        Task<BingoGameViewModel> GetBingoCard(PlayerViewModel playerInfo, int cardId);
        Task<BingoGameViewModel> GetNext(int cardId);
    }
}


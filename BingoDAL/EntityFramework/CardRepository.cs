
using BingoDAL.EntityFramework.Entities;

namespace BingoDAL.EntityFramework
{
    public class CardRepository : BaseBingoRepository<Card>, ICardRepository
    {
        public CardRepository(BingoDbContext context) : base(context)
        {
        }
    }

    public interface ICardRepository : IBingoRepository<Card> { }
}

using BingoDAL.EntityFramework.Entities;
using BingoDAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoDAL.EntityFramework
{
    public class PlayerRepository : BaseBingoRepository<Player>, IPlayerRepository
    {
    public PlayerRepository(BingoDbContext context) : base(context)
    {
    }
}

public interface IPlayerRepository : IBingoRepository<Player> { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceshipGame.DataLayer.Entity;

namespace SpaceshipGame.DataLayer.Repository
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<IEnumerable<Player>> GetHighScoresAsync(int topN);
        Task<Player> AddPlayer(Player player);

    }
}

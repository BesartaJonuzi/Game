using System;
using System.Collections.Generic;
using System.Text;
using SpaceshipGame.DataLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace SpaceshipGame.DataLayer.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {    
        public PlayerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Player>> GetHighScoresAsync(int topN)
        {
            return await _dbContext.Set<Player>()
                .OrderByDescending(p => p.Score)
                .Take(topN)
                .ToListAsync();
        }

        public async Task<Player> AddPlayer(Player player)
        {

            player.PlayerName = "New Player";
            player.Score = 0;
            
            await AddAsync(player);
            return player;
        }

    }
}

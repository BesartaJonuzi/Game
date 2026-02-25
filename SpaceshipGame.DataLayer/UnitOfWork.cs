using Microsoft.EntityFrameworkCore.Storage;
using SpaceshipGame.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipGame.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext appContext;
        private IDbContextTransaction _currentTransaction;
        public IPlayerRepository Players { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            appContext = context;
            Players = new PlayerRepository(appContext);
        }
        public void Dispose()
        {
            appContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await appContext.SaveChangesAsync();
        }
        
    }
}

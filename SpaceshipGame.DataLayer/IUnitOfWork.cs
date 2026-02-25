using Microsoft.EntityFrameworkCore.Storage;
using SpaceshipGame.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipGame.DataLayer
{
    public interface IUnitOfWork : IDisposable
    {

        IPlayerRepository Players { get; }
        Task<int> SaveChangesAsync();

    }
}

using SpaceshipGame.DataLayer;
using SpaceshipGame.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipGame.BusinessLayer.Controller
{
    public class PlayerController
    {
        private readonly IUnitOfWork _unitOfWork;

        public async Task AddPlayerAsync(Player player)
        {
            _unitOfWork.Players.AddPlayer(player);
            _unitOfWork.SaveChangesAsync();
        }
    }
}

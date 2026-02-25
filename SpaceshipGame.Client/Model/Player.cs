using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpaceshipGame.Model
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; } = string.Empty;
        [Required]
        public int Score { get; set; }   
    }
}

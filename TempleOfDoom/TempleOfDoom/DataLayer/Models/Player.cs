using System;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class Player : IGameObject
    {
        public int CurrentRoomId { get; set; }
        public Position Position { get; set; }
        public int Lives { get; set; }
    }
}

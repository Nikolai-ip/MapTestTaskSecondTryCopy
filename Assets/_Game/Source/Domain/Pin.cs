using System;
using UnityEngine;

namespace _Game.Source.Domain
{
    public class Pin
    {
        public Guid Id { get; set; }
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
    
}
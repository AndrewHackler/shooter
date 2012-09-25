using Microsoft.Xna.Framework;

namespace Shooter.Gameplay
{
    public class Robot
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }

        public float TurretRotation { get; set; }
        public float LowerBodyRotation { get; set; }
    }
}
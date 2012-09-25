using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Engine;
using Shooter.Engine.Scene;
using Shooter.Engine.Xna.Extensions;

namespace Shooter.Gameplay
{
    public class RobotView : IDrawableSceneObject
    {
        private readonly ShooterEngine engine;
        private readonly Robot robot;
        private Texture2D texture;

        public RobotView(ShooterEngine engine, Robot robot)
        {
            this.engine = engine;
            this.robot = robot;
            this.texture = engine.Game.Content.Load<Texture2D>("Textures/Player");
        }

        public bool Intersects(object bounds)
        {
            return true;
        }

        public void Draw(float dt)
        {
            engine.SpriteBatch.Draw(texture,
                       this.robot.Position,
                       new Rectangle(64, 0, 64, 64),
                       Color.White,
                       this.robot.LowerBodyRotation + MathHelper.ToRadians(90f),
                       texture.Size() / 4f,
                       Vector2.One / texture.Size() * 2,
                       SpriteEffects.None, 0f);

            engine.SpriteBatch.Draw(texture,
                       this.robot.Position,
                       new Rectangle(0, 64, 64, 64),
                       Color.White,
                       this.robot.TurretRotation + MathHelper.ToRadians(90f),
                       texture.Size() / 4f,
                       Vector2.One / texture.Size() * 2,
                       SpriteEffects.None, 0f);
        }
    }
}
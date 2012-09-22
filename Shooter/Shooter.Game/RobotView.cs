using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Engine;

namespace Shooter.Game
{
    public class RobotView : ICanDraw
    {
        private readonly Robot robot;
        private readonly SpriteBatch batch;
        private Texture2D texture;

        public RobotView(Robot robot, SpriteBatch batch, Texture2D texture)
        {
            this.robot = robot;
            this.batch = batch;
            this.texture = texture;
        }

        public void Draw(float f)
        {
            batch.Draw(texture,
                       this.robot.Position,
                       new Rectangle(64, 0, 64, 64),
                       Color.White,
                       this.robot.LowerBodyRotation + MathHelper.ToRadians(90f),
                       texture.Size() / 4f,
                       Vector2.One / texture.Size() * 2,
                       SpriteEffects.None, 0f);

            batch.Draw(texture,
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
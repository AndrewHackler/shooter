using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shooter.Engine.Xna.Extensions;

namespace Shooter.Engine.Input
{
    public class InputManager
    {
        private readonly ShooterEngine engine;

        public InputManager(ShooterEngine engine)
        {
            this.engine = engine;
        }

        public Vector2? GetMouseTarget()
        {
            var point = Mouse.GetState().PositionToPoint();

            foreach (var perspective in this.engine.PerspectiveManager)
            {
                if (point.X < perspective.Viewport.X ||
                    point.X > perspective.Viewport.X + perspective.Viewport.Width ||
                    point.Y < perspective.Viewport.Y ||
                    point.Y > perspective.Viewport.Y + perspective.Viewport.Height)
                {
                    continue;
                }

                var sbMatrix = perspective.GetMatrix() * SpriteBatchExtensions.GetUndoMatrix(perspective.Viewport);
                var invMatrix = Matrix.Invert(sbMatrix);

                var position = new Vector2(point.X - perspective.Viewport.X, point.Y - perspective.Viewport.Y);

                return Vector2.Transform(position, invMatrix);
            }

            return null;
        }
    }
}
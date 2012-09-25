using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Engine;
using Shooter.Engine.Scene;
using Shooter.Engine.Xna.Extensions;

namespace Shooter
{
    public class Tiler : IDrawableSceneObject
    {
        private readonly ShooterEngine engine;
        private readonly Texture2D texture;

        public Tiler(ShooterEngine engine)
        {
            this.engine = engine;
            this.texture = engine.Game.Content.Load<Texture2D>("Textures/BackgroundTile");
        }

        public bool Intersects(object bounds)
        {
            return true;
        }

        public void Draw(float dt)
        {
            var perspective = this.engine.PerspectiveManager.CurrentPerspective;

            var bounds = perspective.GetBounds();

            var xmin = (int)Math.Floor(bounds.Left);
            var xmax = (int)Math.Ceiling(bounds.Right + 1);
            var ymin = (int)Math.Floor(bounds.Bottom);
            var ymax = (int)Math.Ceiling(bounds.Top + 1);

            var scale = 1;

            while (Math.Abs(xmax - xmin) / scale > 10 || Math.Abs(ymax - ymin) / scale > 10)
            {
                scale *= 10;
            }

            while (xmin % scale != 0)
            {
                xmin--;
                xmax++;
            }

            while (ymin % scale != 0)
            {
                ymin--;
                ymax++;
            }

            for (var x = xmin; x <= xmax; x += scale)
            {
                for (var y = ymin; y <= ymax; y += scale)
                {
                    var position = new Vector2(x, y);

                    this.engine.SpriteBatch.Draw(
                        texture,
                        position,
                        null,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        Vector2.One / texture.Size() * scale,
                        SpriteEffects.None,
                        -1f
                        );
                }
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter.Engine.Xna.Extensions
{
    public static class Texture2DExtensions
    {
        public static Vector2 Size(this Texture2D tex)
        {
            return new Vector2(tex.Width, tex.Height);
        }
    }
}
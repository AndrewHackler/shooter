using Shooter.Engine.Core;

namespace Shooter.Engine.Scene
{
    public interface IDrawableSceneObject
    {
        bool Intersects(Rectangle2D bounds);
        void Draw(float dt);
    }
}
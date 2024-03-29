using System;
using System.Collections.Generic;
using System.Linq;
using Shooter.Engine.Core;

namespace Shooter.Engine.Scene
{
    public class SceneManager
    {
        private readonly ShooterEngine engine;
        private readonly List<IDrawableSceneObject> objects;

        public SceneManager(ShooterEngine engine)
        {
            this.engine = engine;
            this.objects = new List<IDrawableSceneObject>();
        }

        public void Update(float dt)
        {
            // throw new NotImplementedException();
        }

        public void Draw(float dt, Rectangle2D bounds)
        {
            foreach (var item in objects.Where(x => x.Intersects(bounds)))
            {
                item.Draw(dt);
            }
        }

        public void Add(IDrawableSceneObject obj)
        {
            this.objects.Add(obj);
        }

        public void Remove(IDrawableSceneObject obj)
        {
            this.objects.Remove(obj);
        }
    }
}
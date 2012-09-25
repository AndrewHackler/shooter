using System;
using System.Collections.Generic;
using Shooter.Engine.Core;

namespace Shooter.Engine.Scene
{
    public class SceneManager
    {
        private readonly List<IDrawableSceneObject> objects;

        protected Rectangle2D Bounds { get; set; }

        public SceneManager()
        {
            this.objects = new List<IDrawableSceneObject>();
        }

        public void Update(float dt)
        {
            // throw new NotImplementedException();
        }

        public void Draw(float dt)
        {
            foreach (var item in objects)
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
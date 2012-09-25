using System;
using FarseerPhysics.Dynamics;
using Shooter.Engine;
using Shooter.Gameplay;

namespace Shooter
{
    public class RobotProxy : IDisposable
    {
        public RobotBodyLinker Linker { get; private set; }
        public ShooterEngine Engine { get; private set; }

        public Robot Model { get; private set; }
        public RobotView View { get; private set; }
        public AbsoluteRobotController Controller { get; private set; }
        public Body Body { get; private set; }

        public bool HasBeenInitialized { get; private set; }

        public RobotProxy(ShooterEngine engine, Robot model, RobotView view, AbsoluteRobotController controller, Body body, RobotBodyLinker linker)
        {
            if(engine == null)
            {
                throw new ArgumentException("Engine cannot be null", "engine");
            }

            this.Engine = engine;
            this.Model = model;
            this.View = view;
            this.Controller = controller;
            this.Body = body;
            this.Linker = linker;
        }

        public void Initialize()
        {
            if (this.HasBeenInitialized)
            {
                return;
            }

            this.HasBeenInitialized = true;

            if (this.View != null)
            {
                this.Engine.SceneManager.Add(this.View);
            }

            if (this.Body != null && !this.Engine.World.BodyList.Contains(this.Body))
            {
                this.Engine.World.BodyList.Add(this.Body);
            }

            if(this.Linker != null && !this.Engine.Linkers.Contains(this.Linker))
            {
                this.Engine.Linkers.Add(this.Linker);
            }
        }

        public void Dispose()
        {
            if (!this.HasBeenInitialized)
            {
                return;
            }

            this.HasBeenInitialized = false;

            if (View != null)
            {
                this.Engine.SceneManager.Remove(this.View);
            }

            if (Body != null)
            {
                this.Engine.World.RemoveBody(this.Body);
            }
        }
    }
}
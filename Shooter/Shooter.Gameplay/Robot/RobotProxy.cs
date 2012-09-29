using System;
using FarseerPhysics.Dynamics;
using Krypton.Lights;
using Shooter.Engine;
using Shooter.Gameplay;

namespace Shooter
{
    public class RobotProxy : IDisposable
    {
        public RobotLinker Linker { get; private set; }
        public PointLight Light { get; set; }
        public ShooterEngine Engine { get; private set; }

        public RobotModel Model { get; private set; }
        public RobotView View { get; private set; }
        public AbsoluteRobotController Controller { get; private set; }
        public Body Body { get; private set; }

        public bool HasBeenInitialized { get; private set; }

        public RobotProxy(ShooterEngine engine, RobotModel model, RobotView view, AbsoluteRobotController controller, Body body, RobotLinker linker, PointLight light)
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
            this.Light = light;
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

            if (this.Controller != null && !this.Engine.Controllers.Contains(this.Controller))
            {
                this.Engine.Controllers.Add(this.Controller);
            }

            if (this.Body != null && !this.Engine.World.BodyList.Contains(this.Body))
            {
                this.Engine.World.BodyList.Add(this.Body);
            }

            if (this.Linker != null && !this.Engine.Linkers.Contains(this.Linker))
            {
                this.Engine.Linkers.Add(this.Linker);
            }

            if (this.Light != null && !this.Engine.LightmapGenerator.Lights.Contains(this.Light))
            {
                this.Engine.LightmapGenerator.Lights.Add(this.Light);
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
                // this.Body.Dispose();
                this.Engine.World.RemoveBody(this.Body);
            }

            if(this.Light != null)
            {
                this.Engine.LightmapGenerator.Lights.Remove(this.Light);
            }
        }
    }
}
using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Krypton.Lights;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Engine;
using Shooter.Engine.Core;

namespace Shooter.Gameplay
{
    public class RobotFactory
    {
        private static Random random = new Random();
        private static Texture2D LightTexture { get; set; }

        public static RobotProxy SpawnPlayableRobot(ShooterEngine engine)
        {
            var model = new RobotModel();
            model.TurretRotation = (float)(random.NextDouble() * Math.PI * 2);

            var view = new RobotView(engine, model);
            var body = BodyFactory.CreateCircle(engine.World, 0.5f, 1f);
            body.BodyType = BodyType.Dynamic;
            body.LinearDamping = 2f;
            body.AngularDamping = 2f;
            body.Rotation = (float)(random.NextDouble() * Math.PI * 2);

            Texture2D lightTexture;

            if(RobotFactory.LightTexture == null)
            {
                RobotFactory.LightTexture = Krypton.Factories.TextureFactory.CreatePoint(engine.GraphicsDevice, 32);
                //RobotFactory.LightTexture = engine.Game.Content.Load<Texture2D>("Textures/Lights/RobotHeadlight");
            }

            var light = new PointLight(RobotFactory.LightTexture);
            light.Intensity = 1;
            light.Radius = 10f;
            light.Color = Color.Gray;
            light.Fov = MathHelper.TwoPi / 3f;
            light.Rotation = model.TurretRotation;
            light = null;
            var controller = new AbsoluteRobotController(engine, model, body, light);
            var linker = new RobotLinker(body, model, light);
            var proxy = new RobotProxy(engine, model, view, controller, body, linker, light);

            proxy.Initialize();

            return proxy;
        }

        public static RobotProxy SpawnDummyRobot(ShooterEngine engine)
        {
            var model = new RobotModel();
            model.TurretRotation = (float)(random.NextDouble() * Math.PI * 2);

            var view = new RobotView(engine, model);
            var body = BodyFactory.CreateCircle(engine.World, 0.5f, 1f);
            body.BodyType = BodyType.Dynamic;
            body.LinearDamping = 2f;
            body.AngularDamping = 2f;
            body.Rotation = (float)(random.NextDouble() * Math.PI * 2);

            Texture2D lightTexture;

            if(RobotFactory.LightTexture == null)
            {
                RobotFactory.LightTexture = Krypton.Factories.TextureFactory.CreatePoint(engine.GraphicsDevice, 32);
                //RobotFactory.LightTexture = engine.Game.Content.Load<Texture2D>("Textures/Lights/RobotHeadlight");
            }

            var light = new PointLight(RobotFactory.LightTexture);
            light.Intensity = 1;
            light.Radius = 10f;
            light.Color = Color.Gray;
            light.Fov = MathHelper.TwoPi / 3f;
            light.Rotation = model.TurretRotation;
            light = null;
            var linker = new RobotLinker(body, model, light);
            var proxy = new RobotProxy(engine, model, view, null, body, linker, light);

            proxy.Initialize();

            return proxy;
        }

    }
}
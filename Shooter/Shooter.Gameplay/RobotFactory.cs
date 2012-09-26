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
        private static Texture2D LightTexture { get; set; }

        public static RobotProxy Spawn(ShooterEngine engine)
        {
            var model = new Robot();
            var view = new RobotView(engine, model);
            var body = BodyFactory.CreateCircle(engine.World, 0.5f, 1f);
            body.BodyType = BodyType.Dynamic;
            body.LinearDamping = 2f;
            body.AngularDamping = 2f;

            Texture2D lightTexture;

            if(RobotFactory.LightTexture == null)
            {
                RobotFactory.LightTexture = Krypton.Factories.TextureFactory.CreatePoint(engine.GraphicsDevice, 32);
            }

            var light = new PointLight(RobotFactory.LightTexture);
            light.Intensity = 0.75f;
            light.Radius = 2f;
            light.Color = Color.LightGray;
            var controller = new AbsoluteRobotController(engine, model, body);
            var linker = new RobotLinker(body, model, light);
            var proxy = new RobotProxy(engine, model, view, controller, body, linker, light);

            proxy.Initialize();

            return proxy;
        }

    }
}
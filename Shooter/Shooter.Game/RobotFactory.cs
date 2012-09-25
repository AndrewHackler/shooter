using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Shooter.Engine;
using Shooter.Engine.Core;

namespace Shooter.Gameplay
{
    public class RobotFactory
    {
        public static RobotProxy Spawn(ShooterEngine engine)
        {
            var model = new Robot();
            var view = new RobotView(engine, model);
            var body = BodyFactory.CreateCircle(engine.World, 0.5f, 1f);
            body.BodyType = BodyType.Dynamic;
            body.LinearDamping = 2f;
            body.AngularDamping = 2f;

            var controller = new AbsoluteRobotController(engine, model, body);
            var linker = new RobotBodyLinker(model, body);
            var proxy = new RobotProxy(engine, model, view, controller, body, linker);

            proxy.Initialize();

            return proxy;
        }
    }
}
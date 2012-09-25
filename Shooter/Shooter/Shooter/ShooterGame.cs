using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Engine;
using Shooter.Engine.Cameras;
using Shooter.Engine.Viewports;
using Shooter.Gameplay;

namespace Shooter
{
    public class ShooterGame : Game
    {
        private GraphicsDeviceManager graphics;
        private ShooterEngine engine;
        private RobotProxy robot;

        private CameraController camController;
        private Camera camera1;
        private Camera camera2;
        private Camera camera3;

        public ShooterGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            engine = new ShooterEngine(this);
            this.Components.Add(engine);
        }

        protected override void LoadContent()
        {
            // Cameras and viewports
            camera1 = new Camera(engine);
            camera2 = new Camera(engine) { Zoom = 0.70f };
            camera3 = new Camera(engine) { Zoom = 0.65f };

            camController = new CameraController(camera1);

            this.engine.PerspectiveManager.Perspectives.Clear();
            this.engine.PerspectiveManager.Perspectives.AddRange(new[]
                {
                    new Perspective(camera1, new Viewport(000, 000, 400, 240)),
                    new Perspective(camera1, new Viewport(400, 000, 400, 240)),
                    new Perspective(camera2, new Viewport(000, 240, 400, 240)),
                    new Perspective(camera3
                        , new Viewport(400, 240, 400, 240))
                });

            base.LoadContent();

            // Tiler
            var tiler = new Tiler(engine);
            this.engine.SceneManager.Add(tiler);

            // Robot
            robot = RobotFactory.Spawn(engine);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                RobotFactory.Spawn(this.engine);
            }

            this.camController.Update(dt);

            this.engine.Update(gameTime);

            this.camera2.Position += (this.robot.Body.Position + this.robot.Body.LinearVelocity / 2 - this.camera2.Position) * 5 * dt;
            this.camera3.Position = this.robot.Body.Position;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0f, 0);

            this.robot.Controller.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            this.engine.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
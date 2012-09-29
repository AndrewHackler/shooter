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
        private readonly GraphicsDeviceManager graphics;
        private readonly ShooterEngine engine;

        private CameraController camController;
        private Camera camera1;
        private Camera camera2;
        private Camera camera3;

        public ShooterGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;

            engine = new ShooterEngine(this);
            this.Components.Add(engine);

            graphics.DeviceReset += (sender, args) => this.DeviceReset();
            graphics.DeviceResetting += (sender, args) => this.DeviceResetting();
        }

        public void DeviceResetting()
        {
            if (this.graphics.IsFullScreen)
            {
                //this.graphics.PreferredBackBufferWidth = this.GraphicsDevice.DisplayMode.Width;
                //this.graphics.PreferredBackBufferHeight = this.GraphicsDevice.DisplayMode.Height;
            }
        }

        public void DeviceReset()
        {
            var w = this.GraphicsDevice.PresentationParameters.BackBufferWidth / 2;
            var h = this.GraphicsDevice.PresentationParameters.BackBufferHeight / 2;

            this.engine.PerspectiveManager.Perspectives.Clear();
            this.engine.PerspectiveManager.Perspectives.AddRange(new[]
                {
                    new Perspective(camera1, new Viewport(0, 0, w*2, h*2)),
                    //new Perspective(camera1, new Viewport(w, 0, w, h)),
                    //new Perspective(camera2, new Viewport(0, h, w, h)),
                    //new Perspective(camera3, new Viewport(w, h, w, h)),
                });
        }

        protected override void LoadContent()
        {
            // Cameras and viewports
            camera1 = new Camera(engine);
            camera2 = new Camera(engine) { Zoom = 0.70f };
            camera3 = new Camera(engine) { Zoom = 0.65f };

            camController = new CameraController(camera1);

            this.engine.Controllers.Add(camController);

            base.LoadContent();

            // Tiler
            var tiler = new Tiler(engine);
            this.engine.SceneManager.Add(tiler);

            this.GraphicsDevice.Reset();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                RobotFactory.SpawnPlayableRobot(this.engine);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                RobotFactory.SpawnDummyRobot(this.engine);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F) && !this.graphics.IsFullScreen)
            {
                this.graphics.ToggleFullScreen();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.G) && this.graphics.IsFullScreen)
            {
                this.graphics.ToggleFullScreen();
            }

            this.engine.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0f, 0);

            // this.robot.Controller.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            this.engine.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
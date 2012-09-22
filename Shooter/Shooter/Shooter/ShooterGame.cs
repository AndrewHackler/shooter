using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Engine;
using Shooter.Game;

namespace Shooter
{
    public class ShooterGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D playerTexture;
        private World world;

        protected LinkedList<ICanDraw> Views { get; set; }

        public ShooterGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.Controllers = new Collection<ICanUpdate>();
            this.PhysicalLinkers = new Collection<ICanUpdate>();
            this.Views = new LinkedList<ICanDraw>();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("Textures/Player");

            world = new World(Vector2.Zero);

            camera = new Camera(this.GraphicsDevice);

            AddBody(this.Controllers, this.PhysicalLinkers, this.Views, world, spriteBatch, playerTexture, camera);

            this.IsMouseVisible = true;
        }

        private static Random random = new Random();

        private static void AddBody(ICollection<ICanUpdate> controllers, ICollection<ICanUpdate> linkers, LinkedList<ICanDraw> views, World world, SpriteBatch spriteBatch, Texture2D playerTexture, Camera camera)
        {
            // Model
            var robot = new Robot();

            // Physical Body
            var robotBody = BodyFactory.CreateCircle(world, 0.5f, 1);
            robotBody.BodyType = BodyType.Dynamic;
            robotBody.AngularDamping = 5.0f;
            robotBody.LinearDamping = 5.00f;

            // View
            var robotView = new RobotView(robot, spriteBatch, playerTexture);

            // Controller
            var robotController = new AbsoluteRobotController(robot, robotBody, camera);

            // Linker for Physical Body and Model
            var robotBodyLinker = new RobotBodyLinker(robot, robotBody);

            // Add controllers, linkers, and views
            controllers.Add(robotController);
            linkers.Add(robotBodyLinker);
            views.AddFirst(robotView);

            world.BodyList.Add(robotBody);
        }

        // (world, controllers, linkers, views)

        protected ICollection<ICanUpdate> PhysicalLinkers { get; set; }

        private KeyboardState oldState;
        private KeyboardState newState;
        private Camera camera;

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if(Keyboard.GetState().IsKeyDown(Keys.N))
            {
                AddBody(this.Controllers, this.PhysicalLinkers, this.Views, world, spriteBatch, playerTexture, camera);
            }

            foreach (var controller in this.Controllers)
            {
                controller.Update(dt);
            }

            world.Step(dt);

            foreach (var linker in this.PhysicalLinkers)
            {
                linker.Update(dt);
            }

            base.Update(gameTime);
        }

        protected ICollection<ICanUpdate> Controllers { get; set; }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, RasterizerState.CullNone, null, camera.GetMatrix());

            foreach (var view in this.Views)
            {
                view.Draw(dt);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

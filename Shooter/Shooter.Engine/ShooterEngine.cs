// -----------------------------------------------------------------------
// <copyright file="ShooterEngine.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Dynamics;
using Krypton;
using Krypton.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Engine.Core;
using Shooter.Engine.Input;
using Shooter.Engine.Scene;
using Shooter.Engine.Viewports;
using Shooter.Engine.Xna.Extensions;

namespace Shooter.Engine
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ShooterEngine : DrawableGameComponent
    {
        public World World { get; set; }
        public SceneManager SceneManager { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public PerspectiveManager PerspectiveManager { get; set; }

        public InputManager InputManager { get; private set; }

        public List<ICanUpdate> Linkers { get; private set; }

        public LightmapGeneratorComponent LightmapGenerator { get; private set; }
        protected LightmapPresenterComponent LightmapPresenter { get; set; }

        public ShooterEngine(Game game)
            : base(game)
        {
        }

        protected override void LoadContent()
        {
            // Load
            this.World = new World(Vector2.Zero);
            this.SpriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            this.PerspectiveManager = new PerspectiveManager();
            this.SceneManager = new SceneManager();
            this.InputManager = new InputManager(this);
            this.Linkers = new List<ICanUpdate>();

            var lightmapPasses = this.PerspectiveManager.Select(x => new LightmapPass(x.Viewport, x.GetMatrix()));
            this.LightmapGenerator = new LightmapGeneratorComponent(this.Game, lightmapPasses);
            this.LightmapPresenter = new LightmapPresenterComponent(this.Game, this.LightmapGenerator);

            this.LightmapGenerator.Initialize();
            this.LightmapPresenter.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.SceneManager.Update(dt);
            this.World.Step(dt);

            foreach(var linker in this.Linkers)
            {
                linker.Update(dt);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var oldViewport = this.GraphicsDevice.Viewport;

            this.LightmapGenerator.Draw(gameTime);

            foreach (var perspective in this.PerspectiveManager)
            {
                this.GraphicsDevice.Viewport = perspective.Viewport;
                this.PerspectiveManager.CurrentPerspective = perspective;

                var sbMatrix = perspective.GetMatrix() * SpriteBatchExtensions.GetUndoMatrix(perspective.Viewport);

                this.SpriteBatch.Begin(
                    SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    SamplerState.LinearClamp,
                    null,
                    RasterizerState.CullNone,
                    null,
                    sbMatrix
                    );

                this.SceneManager.Draw(dt);

                this.SpriteBatch.End();
            }

            this.GraphicsDevice.Viewport = oldViewport;

            this.LightmapPresenter.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
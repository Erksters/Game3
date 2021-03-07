using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game3;
using GameArchitectureExample.StateManagement;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Joints;

namespace Game3.Screens
{
    public class GamePlayScreen2 : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;
        
        #region Transition animations
        private float _pauseAlpha;
        private readonly InputAction _pauseAction;
        #endregion

        #region Game Contents
        private Protagonist1 protagonist;
        //private Border _border;
        private Body protagonistBody;
        private Vector2 protagonistSize = new Vector2(1f, 1f);
        private World world;
        #endregion

        private readonly Random _random = new Random();

        /// <summary>
        /// My public constructor
        /// </summary>
        public GamePlayScreen2()
        {
            if (!Constants.inDevelopment)
            {
                TransitionOnTime = TimeSpan.FromSeconds(1.5);
                TransitionOffTime = TimeSpan.FromSeconds(0.5);
            }
            else
            {
                TransitionOnTime = TimeSpan.FromSeconds(0);
                TransitionOffTime = TimeSpan.FromSeconds(0);
            }

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape, Keys.P }, true);

            
            protagonist = new Protagonist1(new Vector2(100, 100));
        }

        /// <summary>
        /// Load in graphics for this Game Plays Screen
        /// </summary>
        public override void Activate()
        {
            if (_content == null) { _content = new ContentManager(ScreenManager.Game.Services, "Content"); }

            _gameFont = _content.Load<SpriteFont>("gamefont");
            protagonist.LoadContent(_content);
        }


        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            HandlePauseTransition(coveredByOtherScreen);

            //If this screen is active
            if (IsActive)
            {
                //TODO: Add your games update methods here 
                protagonist.Update(gameTime);
            }
        }




        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);


            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            protagonist.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            HandleScreenTransition();
        }

        /// <summary>
        /// Helper method to animate a transition from another screen (MenuScreen)
        /// Used in Draw()
        /// </summary>
        private void HandleScreenTransition()
        {
            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        /// <summary>
        /// Helper method to handle animations for pausing the gamescreen
        /// Used in HandleInput()
        /// </summary>
        /// <param name="coveredByOtherScreen">Tell this method if we are fading in or out</param>
        private void HandlePauseTransition(bool coveredByOtherScreen)
        {
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);
        }

        /// <summary>
        /// Windows phone requirement
        /// </summary>
        public override void Deactivate()
        {
            base.Deactivate();
        }

        /// <summary>
        /// Windows phone requirement
        /// </summary>
        public override void Unload()
        {
            _content.Unload();
        }
    }
}

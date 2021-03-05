using System;
using System.Collections.Generic;
using System.Text;
using GameArchitectureExample.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace GameArchitectureExample.Screens
{
    public class CutSceneScreen : GameScreen
    {
        ContentManager content;
        Video video;
        VideoPlayer videoPlayer;
        bool isPlaying;
        InputAction skip;

        public CutSceneScreen()
        {
            videoPlayer = new VideoPlayer();
            skip = new InputAction(new Buttons[] { Buttons.A }, new Keys[] { Keys.Space, Keys.Enter }, true);
        }

        public override void Activate()
        {
            if(content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            video = content.Load<Video>("liftoff_of_smap");
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if(!isPlaying)
            {
                videoPlayer.Play(video);
                isPlaying = true;
            }
            PlayerIndex player;
            if (skip.Occurred(input, null, out player))
            {
                videoPlayer.Stop();
                ExitScreen();
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (videoPlayer.PlayPosition >= video.Duration) { ExitScreen(); }
        }

        public override void Deactivate()
        {
            videoPlayer.Pause();
            isPlaying = false;
        }

        public override void Draw(GameTime gameTime)
        {
            if (isPlaying)
            {
                ScreenManager.SpriteBatch.Begin();
                ScreenManager.SpriteBatch.Draw(videoPlayer.GetTexture(), Vector2.Zero, Color.White);
                ScreenManager.SpriteBatch.End();
            }

        }
    }
}

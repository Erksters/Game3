using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameArchitectureExample.StateManagement;
using Game3.Screens;
using Game3;
using Microsoft.Xna.Framework.Audio;
using GameArchitectureExample.Screens;

namespace Game3.Screens
{
    // The main menu screen is the first thing displayed when the game starts up.
    public class MyMainMenu : MenuScreen
    {
        private ContentManager _content;

        public MyMainMenu() : base("Main Menu")
        {

            var playGameMenuEntry = new MenuEntry("New Game");
            var LevelSelectMenuEntry = new MenuEntry("Level Select");
            var optionsMenuEntry = new MenuEntry("Options");
            var exitMenuEntry = new MenuEntry("Exit");

            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            LevelSelectMenuEntry.Selected += LevelSelectMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(LevelSelectMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);

        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            MenuMusic = _content.Load<SoundEffect>("Grand-Adventure");
            MenuMusicLooped = MenuMusic.CreateInstance();
            MenuMusicLooped.IsLooped = true;

            PlayMainMenuMusic();
        }
        private void PlayMainMenuMusic()
        {
            PlayMenuMusic();
        }



        #region Audio
        /// <summary>
        /// Menu Music without loop
        /// </summary>
        public SoundEffect MenuMusic;

        /// <summary>
        /// MenuMusic 
        /// </summary>
        public SoundEffectInstance MenuMusicLooped;

        /// <summary>
        /// Add the content of the sound
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent()
        {
            //MenuMusic = _content.Load<SoundEffect>("Grand-Adventure");
            //MenuMusicLooped = MenuMusic.CreateInstance();
            //MenuMusicLooped.IsLooped = true;
        }

        public void SetNewMenuMusicVolume(int newVolume)
        {
            MenuMusicLooped.Volume = newVolume;
        }

        public void StopMenuMusic()
        {
            MenuMusicLooped.Stop();
        }

        public void PlayMenuMusic()
        {
            MenuMusicLooped.Play();
        }
        #endregion

































        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GamePlayScreen1(), new CutSceneScreen1());
        }

        private void LevelSelectMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new LevelSelectScreen(), null);
        }

        private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";
            var confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}

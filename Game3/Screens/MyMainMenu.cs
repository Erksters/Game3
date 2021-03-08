using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using GameArchitectureExample.StateManagement;
using Game3;
using GameArchitectureExample.Screens;

namespace Game3.Screens
{
    // The main menu screen is the first thing displayed when the game starts up.
    public class MyMainMenu : MenuScreen
    {
        private ContentManager _content;

        /// <summary>
        /// Menu Music without loop
        /// </summary>
        public SoundEffect MenuMusic;

        /// <summary>
        /// MenuMusic 
        /// </summary>
        public SoundEffectInstance MenuMusicLooped;

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
            if (_content == null) { _content = new ContentManager(ScreenManager.Game.Services, "Content"); }
            AudioConstants.LoadContent(_content);
            AudioConstants.PlayMenuMusic();

        }

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
            ScreenManager.AddScreen(new MyOptionsScreen(), e.PlayerIndex);
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

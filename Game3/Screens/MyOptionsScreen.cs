using Game3;
using GameArchitectureExample.Screens;

namespace Game3.Screens
{
    // The options screen is brought up over the top of the main menu
    // screen, and gives the user a chance to configure the game
    // in various hopefully useful ways.
    public class MyOptionsScreen : MenuScreen
    {
        /// <summary>
        /// Languages we could try to support
        /// </summary>
        private static readonly string[] Languages = { "English", "French", "Spanish" };


        /// <summary>
        /// Select the language to be used in this game
        /// </summary>
        private readonly MenuEntry _languageMenuEntry;
        private MenuEntry masterVolumeIncreaseEntry;
        private MenuEntry masterVolumeDecreaseEntry;
        private MenuEntry Volume;

        /// <summary>
        /// Pointer to the current language in the Languages[]
        /// </summary>
        private static int _currentLanguage;

        /// <summary>
        /// Pointer to the current audio level
        /// </summary>
        private int MasterVolume = 50;

        public MyOptionsScreen() : base($"Options")
        {
            _languageMenuEntry = new MenuEntry(string.Empty);
            masterVolumeIncreaseEntry = new MenuEntry("Increase Volume");
            masterVolumeDecreaseEntry = new MenuEntry("Decrease Volume");
            Volume = new MenuEntry($"Volume : {MasterVolume.ToString()}");

            SetMenuEntryText();
            SetVolumeMenuEntryText();
            //SetVolumeMenuEntryText();

            var back = new MenuEntry("Back");

            _languageMenuEntry.Selected += LanguageMenuEntrySelected;
            masterVolumeIncreaseEntry.Selected += IncreaseAudioMenuEntrySelected;
            masterVolumeDecreaseEntry.Selected += DecreaseAudioMenuEntrySelected;
            back.Selected += OnCancel;

            MenuEntries.Add(_languageMenuEntry);
            MenuEntries.Add(masterVolumeIncreaseEntry);
            MenuEntries.Add(masterVolumeDecreaseEntry);
            MenuEntries.Add(Volume);
            MenuEntries.Add(back);
        }

        /// <summary>
        /// Method to decrease the MasterVolume of the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecreaseAudioMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            MasterVolume--;
            //AudioConstants.SetNewMenuMusicVolume(MasterVolume);
            SetVolumeMenuEntryText();
        }

        /// <summary>
        /// Method to increase the MasterVolume of the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncreaseAudioMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            MasterVolume++;
            SetVolumeMenuEntryText();
        }

        /// <summary>
        /// Method to be used after adjusting the volume
        /// </summary>
        private void SetVolumeMenuEntryText()
        {
            Volume.Text = $"Volume : {MasterVolume}";
        }

        /// <summary>
        /// Increments the _currentLanguge value and loops through the languages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _currentLanguage = (_currentLanguage + 1) % Languages.Length;
            SetMenuEntryText();
        }

        /// <summary>
        /// Helper method to load in the correct language
        /// </summary>
        private void SetMenuEntryText()
        {
            _languageMenuEntry.Text = $"Language: {Languages[_currentLanguage]}";
        }

    }
}
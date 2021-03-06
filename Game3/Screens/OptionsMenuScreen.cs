

namespace GameArchitectureExample.Screens
{
    // The options screen is brought up over the top of the main menu
    // screen, and gives the user a chance to configure the game
    // in various hopefully useful ways.
    public class OptionsMenuScreen : MenuScreen
    {
        /// <summary>
        /// Languages we could try to support
        /// </summary>
        private static readonly string[] Languages = { "English", "French", "Spanish" };

        /// <summary>
        /// Select the language to be used in this game
        /// </summary>
        private readonly MenuEntry _languageMenuEntry;   

        /// <summary>
        /// Pointer to the current language in the Languages[]
        /// </summary>
        private static int _currentLanguage;
        
        public OptionsMenuScreen() : base("Options")
        {
            _languageMenuEntry = new MenuEntry(string.Empty);
           
            SetMenuEntryText();

            var back = new MenuEntry("Back");

            _languageMenuEntry.Selected += LanguageMenuEntrySelected;
            back.Selected += OnCancel;

            MenuEntries.Add(_languageMenuEntry);
            MenuEntries.Add(back);
        }

        // Fills in the latest values for the options screen menu text.
        private void SetMenuEntryText()
        {
            _languageMenuEntry.Text = $"Language: {Languages[_currentLanguage]}";
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using GameArchitectureExample.Screens;
using Game3.Screens;

namespace Game3
{

    public static class Constants
    {
        /// <summary>
        /// For Development purposes
        /// True will make the game easier to test
        /// False will should be ready for production
        /// </summary>
        public static bool inDevelopment = true;

        /// <summary>
        /// The width of the game world
        /// </summary>
        public static int GAME_WIDTH = 760;

        /// <summary>
        /// The height of hte game world
        /// </summary>
        public static int GAME_HEIGHT = 480;

        /// <summary>
        /// Get the current Game Screen
        /// </summary>
        public static  GamePlayScreen4 currentGameScreen;

        public static void ResetGame()
        {
            currentGameScreen.Reset();
        }
        
        /// <summary>
        /// Set the current GameScreen
        /// </summary>
        /// <param name="gameScreen"></param>
        public static void ChangeGameScreen(GamePlayScreen4 gameScreen)
        {
            currentGameScreen = gameScreen;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game3
{
    public static class AudioConstants
    {
        /// <summary>
        /// Menu Music without loop
        /// </summary>
        public static SoundEffect MenuMusic;

        /// <summary>
        /// MenuMusic 
        /// </summary>
        public static SoundEffectInstance MenuMusicLooped;


        /// <summary>
        /// MenuMusic Volume
        /// </summary>
        public static SoundEffectInstance MasterVolume;

        /// <summary>
        /// Add the content of the sound
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {
            MenuMusic = content.Load<SoundEffect>("Grand-Adventure");
            MenuMusicLooped = MenuMusic.CreateInstance();
            MenuMusicLooped.IsLooped = true;
        }

        public static void SetNewMenuMusicVolume(int newVolume)
        {
            MenuMusicLooped.Volume = newVolume;
        }

        public static void StopMenuMusic()
        {
            MenuMusicLooped.Stop();
        }

        public static void PlayMenuMusic()
        {
            MenuMusicLooped.Play();
        }

        public static void DecreaseMasterVolume()
        {

            MenuMusicLooped.Volume += (float)-0.01;
        }

        public static void IncreaseMasterVolume()
        {
            MenuMusicLooped.Volume += (float)+0.01;
        }
    }
}

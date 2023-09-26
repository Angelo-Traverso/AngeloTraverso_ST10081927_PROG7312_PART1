using System;
using WMPLib;

namespace DeweyDecimal_Latest
{
    public class SoundPlayer
    {
        private WindowsMediaPlayer mediaPlayer;

        public SoundPlayer()
        {
            mediaPlayer = new WindowsMediaPlayer();
        }

        public void PlaySound(string filePath)
        {
            try
            {
                mediaPlayer.URL = filePath;
                mediaPlayer.controls.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing sound: " + ex.Message);
            }
        }

        public void StopSound()
        {
            try
            {
                mediaPlayer.controls.stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error stopping sound: " + ex.Message);
            }
        }
    }
}

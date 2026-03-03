using System.Windows.Media;

namespace WerWirdMioWPF.Service
{
    public class SoundService
    {


        private MediaPlayer _mediaPlayer = new MediaPlayer();

        public void playRetrySound()
        {
            _mediaPlayer.Open(new Uri("Assets/neuerversuch.wav", UriKind.RelativeOrAbsolute));
            _mediaPlayer.Play();
        }

        public void playDanielSound()
        {
            _mediaPlayer.Open(new Uri("Assets/leckerschmecker.wav", UriKind.RelativeOrAbsolute));
            _mediaPlayer.Play();
        }


        public void playHuetherSound()
        {
            _mediaPlayer.Open(new Uri("Assets/huther.wav", UriKind.RelativeOrAbsolute));
            _mediaPlayer.Play();
        }


    }
}

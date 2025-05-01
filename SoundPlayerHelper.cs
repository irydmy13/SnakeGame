using System.Media;

class SoundPlayerHelper
{
    public static void PlayFoodSound()
    {
        SoundPlayer sp = new SoundPlayer("media/eat.wav");
        sp.Play();
    }
}

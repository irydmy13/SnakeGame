using System.Media;

public class Sounds
{
    private SoundPlayer background;
    private SoundPlayer eat;
    private SoundPlayer gameOver;

    public Sounds()
    {
        background = new SoundPlayer("sounds\\background.wav");
        eat = new SoundPlayer("sounds\\eat.wav");
        gameOver = new SoundPlayer("sounds\\gameover.wav");
    }

    public void PlayBackground() => background.PlayLooping();
    public void StopBackground() => background.Stop();
    public void PlayEat() => eat.Play();
    public void PlayGameOver() => gameOver.Play();
}

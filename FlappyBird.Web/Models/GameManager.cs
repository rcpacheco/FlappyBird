using System;
using System.Threading.Tasks;

namespace FlappyBird.Web.Models
{
    public class GameManager
    {
        private readonly int _gravity = 2;

        public event EventHandler MainLoopCompleted;

        public BirdModel Bird { get; private set; }

        public PipeModel Pipe { get; private set; }

        public bool IsRunning { get; private set; } = false;

        public GameManager()
        {
            Bird = new BirdModel();
            Pipe = new PipeModel();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                Bird.Fall(_gravity);
                Pipe.Move();

                if (Bird.DistanceFromGround <= 0)
                {
                    GameOver();
                }

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20).ConfigureAwait(false);
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new BirdModel();
                MainLoop();
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}

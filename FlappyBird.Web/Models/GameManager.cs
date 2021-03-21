using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Web.Models
{
    public class GameManager
    {
        private readonly int _gravity = 2;

        private readonly int _delayTime = 20;

        public event EventHandler MainLoopCompleted;

        public BirdModel Bird { get; private set; }

        public IList<PipeModel> Pipes { get; private set; }

        public bool IsRunning { get; private set; } = false;

        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                MoveObjects();
                CheckForCollisions();
                ManagePipes();

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(_delayTime).ConfigureAwait(false);
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }
        }

        public void Jump()
        {
            if (IsRunning)
            {
                Bird.Jump();
            }
        }

        private void CheckForCollisions()
        {
            if (Bird.IsOnGround())
            {
                GameOver();
            }
            var centeredPipe = Pipes.FirstOrDefault(p => p.IsCentered());
            if (centeredPipe != null)
            {
                bool hasCollodedWithBottom = Bird.DistanceFromGround < centeredPipe.GapButton - 150; // GroundHeigh
                bool hasCollodedWithTop = Bird.DistanceFromGround + 45 > centeredPipe.GapTop - 150; // BirdHeigh / GroundHeigh
                if (hasCollodedWithBottom || hasCollodedWithTop)
                {
                    GameOver();
                }
            }
        }
        private void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250) // GameWidth/2
            {
                Pipes.Add(new PipeModel());
            }

            if (Pipes.First().IsOffScreen())
            {
                Pipes.Remove(Pipes.First());
            }
        }

        private void MoveObjects()
        {
            Bird.Fall(_gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}

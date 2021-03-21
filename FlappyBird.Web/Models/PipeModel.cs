using System;

namespace FlappyBird.Web.Models
{
    public class PipeModel
    {

        public int DistanceFromLeft { get; private set; } = 500;

        public int DistanceFromBottom { get; private set; } = new Random().Next(0, 60);
    }
}

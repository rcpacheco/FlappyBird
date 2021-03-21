using System;

namespace FlappyBird.Web.Models
{
    public class PipeModel
    {

        public int DistanceFromLeft { get; set; } = 500;

        public int DistanceFromBottom { get; set; } = new Random().Next(0, 60);
    }
}

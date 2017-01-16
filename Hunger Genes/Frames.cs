using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace HungerGenes
{
    class Frames
    {
        public DateTime first { get; set; }
        public DateTime prev { get; set; }
        public DateTime frame { get; set;}
        public int fcount { get; set; }
        public double fps { get; set; }

        public Frames(DateTime initial)
        {
            first = initial;
        }

        public void updateFPS()
        {
            prev = frame;
            frame = DateTime.Now;
            fcount++;
            fps = 1000 / (frame - prev).TotalMilliseconds;
        }

    }
}

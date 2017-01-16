using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HungerGenes
{
    /// <summary>
    /// Produces the next generation
    /// </summary>
    class DNA
    {
        
        Random r;
        public int length { get; set; }
        public Vector2[] genes { get; set; }

        public DNA(int length)
        {
            this.length = length;
            r = new Random();
            genes = new Vector2[length];
        }
        public DNA(Vector2[] newgenes, Random r)
        {
            this.r = r;
            genes = newgenes;
        }
        public DNA(int length, DNA dna)
        {
            genes = new Vector2[dna.genes.Length + length];


        }

        /// <summary>
        /// Takes in two Player objects, p1 and p2
        /// and crosses their DNA.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public DNA crossover(DNA partner, Random r)
        {
            int spc;
  
            Vector2[] childmov = new Vector2[partner.genes.Length];
            spc = r.Next(0, length);
            for (int i = 0; i < partner.genes.Length; i++)
            {
                if (i < spc)
                {
                    childmov[i] = genes[i];
                    childmov[i] = mutate(childmov[i]);
                }
                else
                {
                    childmov[i] = partner.genes[i];
                    childmov[i] = mutate(childmov[i]);
                }
            }
            DNA newg = new DNA(childmov, r);
            return newg;
        }

        /// <summary>
        /// Chance to modify a newly created Player.
        /// </summary>
        /// <param name="i"></param>
        private Vector2 mutate(Vector2 i)
        {
            double prob = r.NextDouble();
            if(prob < .15)
            {
                float direction = (float)(r.NextDouble() * 2 * Math.PI);
                float time = r.Next() % 3 + 4;
                i = new Vector2(direction, time);
            }
            return i;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
namespace HungerGenes
{
    class Population
    {
        
        public Enemy[] enemy { get; set; }
        Random r;
        public int gen { get; set; }
        List<Enemy> pool;
        int popsize = HungerGenes.pop;
        int totalBlocksDestroyed;
        public int lifetime { get; set; }
        
        public Population(int type, float x, float y, Random r, ContentManager content)
        {
            enemy = new Enemy[popsize];
            pool = new List<Enemy>();
            totalBlocksDestroyed = 25;
            updateLifetime();
            
            this.r = r;
            
            for(int i = 0; i < popsize; i++)
            {
                int bonus = r.Next(11, 15);
              
                switch (type)
                {
                    case 0:
                        
                        enemy[i] = new Enemy(x, y, 100+bonus, 10, 0.001f, r, content, type);
                        enemy[i].createPath(r, lifetime);
                        break;
                    case 1:
                        float s = bonus / 10000f;
                        enemy[i] = new Enemy(x, y, 10, 10, s, r, content, type);
                        enemy[i].createPath(r, lifetime);
                        break;
                    case 2:
                        enemy[i] = new Enemy(x, y, 10, bonus, 0.001f, r, content, type);
                        enemy[i].createPath(r, lifetime);
                        break;
                }
                
            }
        }

        //Updates the lifetime of a population based on the number of blocks destroyed.
        public void updateLifetime()
        {
            lifetime = totalBlocksDestroyed * 8;
        }
        public void addDestroyedblocks(Enemy e)
        {
            if (e.getHits() > 0)
            {

                totalBlocksDestroyed += e.getHits();
            }
        }
        public int getTotal()
        {
            return totalBlocksDestroyed;
        }

        //Gets the fitness of each enemy in the population
        public void fitness()
        {
            for(int i = 0; i < popsize; i++)
            {
                enemy[i].evaluateFitness();
                this.addDestroyedblocks(enemy[i]);
                enemy[i].addHits(-1);
            }
        }

        //Adds enemies to a pool, creating copies based on how fit they are
        public void selection()
        {
            pool.Clear();
            List<Enemy> toSort = new List<Enemy>(popsize);
            double maxFitness = max();
            for(int i = 0; i < popsize; i++)
            {
                toSort.Add(enemy[i]);

            }
            toSort.Sort((x, y) => x.fitness.CompareTo(y.fitness));
            for (int i = 0; i < popsize; i++)
            {
                double norm = enemy[i].fitness / maxFitness;
                int n = (int)(norm * 100);
                int index = i;
                for (int j = 0; j <= index; j++)
                {
                    pool.Add(toSort[i]);
                }

            }
        }

        //Selects parents for each member in the new generation
        //Calls the crossover in DNA
        //Splits up the traits (health. power and speed) randomly from each parent
        public void reproduction()
        {
            for(int i = 0; i < popsize; i++)
            {
                int p1 = (int)(r.NextDouble() * pool.Count);
                int p2 = (int)(r.NextDouble() * pool.Count);

                while(p1 == p2)
                {
                    double f = r.NextDouble();
                    p1 = (int)(f * pool.Count);
                }

                Enemy mom = pool[p1];
                Enemy dad = pool[p2];

                DNA p1genes = mom.dna;
                DNA p2genes = dad.dna;

                DNA child = p1genes.crossover(p2genes, r);
                double chance = r.NextDouble();
                enemy[i].dna = child;
                if(chance < .5)
                {
                    enemy[i].setHealth(mom.getHealth());
                    chance = r.NextDouble();
                }
                else
                {
                    enemy[i].setHealth(dad.getHealth());
                    chance = r.NextDouble();
                }
                if(chance < .5)
                {
                    enemy[i].setPower(mom.getHealth());
                }else
                {
                    enemy[i].setPower(dad.getHealth());
                    chance = r.NextDouble();
                }
                if(chance < .5)
                {
                    enemy[i].setSpeed(mom.getSpeed());
                }
                else
                {
                    enemy[i].setSpeed(dad.getSpeed());
                }

                if (child.genes.Length < lifetime)
                {
                    enemy[i].pathAdd((lifetime-child.genes.Length), r);
                }
                enemy[i].setX(enemy[i].origin.X);
                enemy[i].setY(enemy[i].origin.Y);
                enemy[i].indx = 0;
            }
        }

        //Gets the max fitness, used for nomralization
        public double max()
        {
            double curmax=0;
            for(int i = 0; i < popsize; i++)
            {
                if(enemy[i].fitness > curmax)
                {
                    curmax = enemy[i].fitness;
                }
            }
            return curmax;
        }
        

        //Resets the entire population back to their starting location
        public void resetPopPosition()
        {
            foreach(Enemy e in enemy)
            {
                e.setX(e.origin.X);
                e.setY(e.origin.Y);
            }
        }
    }
}

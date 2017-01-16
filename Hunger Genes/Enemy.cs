using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerGenes
{
    class Enemy : Character
    {
        public int gen { get; set; }
        public int pcnt { get; set; }
        public List<Tuple<double, int>> path { get; set; }
        public DNA dna { get; set; }
        public float elapsedTime {get;set;}
        public float direction { get; set; }
        public int indx { get; set; } 
        public int total { get; set; }
        public double fitness { get; set; }
        public Vector2 origin { get; set; }
        public Texture2D texture{ get; set; }

        //Enemy constructor
        //Takes the position, traits and weapon
        public Enemy(float x, float y, int p, int h, float s, Random r, ContentManager Content, int weapon)
        {
            bulletHits = 0;
            this.x = x;
            this.y = y;
            origin = new Vector2(x, y);
            indx = 0;
            power = p;
            health = h;
            speed = s;
            this.weapon = weapon;
            gen = 0;
            Texture2D weptex;
            
                
            switch (this.getWeapon())
            {
                case 0:
                    weptex = Content.Load<Texture2D>("magemissile4816");
                    texture = Content.Load<Texture2D>("mage16");
                    bullet = new Bullet(getX(), getY(), getWeapon(), weptex, this);
                    break;
                case 1:
                    weptex = Content.Load<Texture2D>("SlashSpriteSheetR");
                    texture = Content.Load<Texture2D>("rogue16");
                    bullet = new Bullet(getX(), getY(), getWeapon(), weptex, this);
                    break;
                case 2:
                    weptex = Content.Load<Texture2D>("SmashSpriteSheet16020");
                    texture = Content.Load<Texture2D>("brute16");
                    bullet = new Bullet(getX(), getY(), getWeapon(), weptex, this);
                    break;
            }
        }
        public Enemy(DNA genes)
        {
            this.dna = genes;
            direction = dna.genes[0].Y;
            x = 50;
            y = 50;
            gen++;
        }

        //Measures the performance of an enemy object
        public void evaluateFitness()
        {
           
            double fit, targetx, targety;
            fitness = health * power * speed;
            targetx =  360 - this.getX();
            targety = 360 - this.getY();
            fit = Math.Sqrt((targetx * targetx) + (targety * targety));
            fit = (1 / fit)*10000;
            
            fit = fit*2 + (2*bulletHits) + power + health + speed;
            this.fitness = fit;           
        }

        //Creates the initial path for an enemy
        public void createPath(Random r, int l)
        {
            dna = new DNA(l);

           
            for (int i = 0; i < l; i++)
            {
                float direction = (float)(r.NextDouble()*2*Math.PI);
                float time = r.Next() % 3 + 4;
                //path.Add(Tuple.Create(direction, time));

                float xd = (float)(Math.Cos(direction) * r.NextDouble());
                float yd = (float)(Math.Sin(direction) * r.NextDouble());
                dna.genes[i] = new Vector2(direction, time);
                //dna.genes[i] = new Vector2(xd, yd); 
            }
            direction = dna.genes[0].X;
           Console.WriteLine();
        }

        //Adds to an existing path
        //Used when the lifetime is increased
        public void pathAdd(int length, Random r)
        {
            DNA n = new DNA(length+1, dna);
            for(int i = 0; i < n.genes.Length; i++)
            {
                if(i < this.dna.genes.Length)
                {
                    n.genes[i] = dna.genes[i];
                }
                else
                {
                    float direction = (float)(r.NextDouble() * 2 * Math.PI);
                    float time = r.Next() % 3 + 4;
                    n.genes[i] = new Vector2(direction, time);

                }
            }
            dna.genes = n.genes;
        }
        public float getSpeed()
        {
            return speed;
        }

    }
}

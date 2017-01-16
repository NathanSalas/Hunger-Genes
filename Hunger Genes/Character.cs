using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;

namespace HungerGenes
{
    class Character
    {
       
        protected int health { get; set; }
        protected float speed { get; set; }
        protected int power { get; set; }
        protected float x { get; set; }
        protected float y { get; set; }
        protected Bullet bullet { get; set; }
        protected int weapon { get; set; }
        protected int bulletHits { get; set; }

        public void setHealth(int health)
        {
            this.health = health;
        }
        public int getHealth()
        {
            return health;
        }
        public void setPower(int power)
        {
            this.power = power;
        }
        public int getPower()
        {
            return power;
        }
        public void setSpeed(float speed)
        {
            this.speed = speed;
        }
        public float getSpeed()
        {
            return speed;
        }
        public Bullet getBullet()
        {
            return bullet;
        }
        public void setBullet(Bullet b)
        {
            bullet =b ;
        }
        public float getX()
        {
            return x;
        }
        public void setX(float nx)
        {
            x = nx;
        }
        public float getY()
        {
            return y;
        }
        public void setY(float ny)
        {
            y = ny;
        }
        public int getWeapon()
        {
            return weapon;
        }
        public int getStrength()
        {
            return power;
        }

        //Add number of blocks destroyed to bulletHits
        public void addHits(int hits)
        {
            
            bulletHits += hits;


            if (hits == -1) { bulletHits = 0; }
        }
        public int getHits()
        {
            return bulletHits;
        }

        //Applies a power up if one was destroyed
        public void applyPU(int type)
        {
            if(type == 4)
            {
                health = (int)Math.Floor(health*1.5);
            }else if(type == 5)
            {
                power = (int)Math.Floor(power*1.5);
            }else if (type == 6)
            {
                speed *= 1.5f;
            }
            
        }
    }
    
}

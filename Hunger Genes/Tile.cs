using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace HungerGenes
{
    class Tile
    {
        Vector2 location;
        Vector2 offset;
        int health;
        int tileID;
        ContentManager c;

        Texture2D tileIcon;

        public Tile()
        {
            tileID = -1;
            offset = new Vector2(64);
            health = 15;
        }

        public Tile(int type, ContentManager content)
        {
           
            c = content;
            switch (type)
            {
                //Floor
                case 0:
                    
                    health = 0;
                    tileID = type;
                    tileIcon = content.Load<Texture2D>("Floor");
                    break;
                    
               //Standard
                case 1:
                    health = 100;
                    tileID = type;
                    tileIcon = content.Load<Texture2D>("SingleSB");
                    break;

                //Unbreakable
                case 2:
                    health = int.MaxValue;
                    tileID = type;
                    tileIcon = content.Load<Texture2D>("Unbreakable2D");
                    break;

                //Goal Floor
                case 3:
                    health = 0;
                    tileID = type;
                    tileIcon = content.Load<Texture2D>("GoalFloor");
                    break;

                //Health Power Up
                case 4:
                    health = 10;
                    tileID = type;
                    tileIcon = content.Load<Texture2D>("SingleSBHealthPU");
                    break;

                //Power Power Up
                case 5:
                    health = 1;
                    tileID = type;
                    tileIcon = content.Load<Texture2D>("SingleSBPowerPU");
                    break;

                //Speed Power Up
                case 6:
                    health = 1;
                    tileID = type;
                    tileIcon = content.Load<Texture2D>("SingleSBSpeedPU");
                    break;
            }
        }

        //Updates a tiles health (and icon, if destroyed) when a tile is hit
        //Awards points to the attacking character
        public int onHit(Character p)
        {
            int bulletHits = 0;
            if(4 < getID() && getID() < 6)
            {
                switch (getID())
                {
                    case 4:
                        this.health -= p.getStrength();
                        if(health <= 0)
                        {
                            tileID = 0;
                            tileIcon = c.Load<Texture2D>("floor");
                        }
                        p.applyPU(4);
                        bulletHits++;
                        break;
                    case 5:
                        this.health -= p.getStrength();
                        if (health <= 0)
                        {
                            tileID = 0;
                            tileIcon = c.Load<Texture2D>("floor");
                        }
                        p.applyPU(5);
                        bulletHits++;
                        break;
                    case 6:
                        this.health -= p.getStrength();
                        if (health <= 0)
                        {
                            tileID = 0;
                            tileIcon = c.Load<Texture2D>("floor");
                        }
                        p.applyPU(6);
                        bulletHits++;
                        break;

                }
            }else
            if (getID() != 0 && getID() != 3) { 
                this.health -= p.getStrength();
                
                if (this.health <= 0 && c != null  )
                {
                    tileID = 0;
                    tileIcon = c.Load<Texture2D>("Floor");
                    bulletHits ++;

                }
            }
            return bulletHits;
        }
        public int getID()
        {
            return tileID;
        }
        public Texture2D getTexture()
        {
            return tileIcon;
        }
        public void setLocation(Vector2 loc)
        {
            location = loc;
        }
        public Vector2 getLocation()
        {
            return location;
        }
    }
}

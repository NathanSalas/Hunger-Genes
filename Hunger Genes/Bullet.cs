using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace HungerGenes
{
    class Bullet
    {
        float x;
        float y;
        Character parent;
        Texture2D btexture;
        int frameIndex;
        float time;
        int bulletID;
        int fWidth, fHeight;
        bool fired { get; set; }
        float direction;
        Rectangle source;
        Vector2 origin, position;
        Interactions rules;
         
        //Bullet constructor
        public Bullet(float x, float y, int type, Texture2D texture, Character p)
        {
            parent = p;
            this.x = x;
            this.y = y;
            frameIndex = 0;
            btexture = texture;
            bulletID = type;
            switch (type)
            {
                case 0:
                    fWidth = 48;
                    fHeight = 16;
                   
                    break;
                case 1:
                    fWidth = 16;
                    fHeight = 16;
                    break;
                case 2:
                    fWidth = 32;
                    fHeight = 20;
                    break;

            }
            time = 0f;
            fired = false;
        }
        public int getID()
        {
            return bulletID;
        }
        public float getX()
        {
            return x;
        }
        public float getY()
        {
            return y;
        }
        public void setX(float x)
        {
            this.x = x;
        }
        public void setY(float y)
        {
            this.y = y;
        }
        public bool isFired()
        {
            return fired;
        }

        //Called when a bullet is fired.
        //Sets up what is needed to update and draw the bullet
        public void Fired(float x, float y, Interactions i)
        {
            rules = i;
            fired = true;
            float xcom;
            float ycom;
            switch (getID())
            {
                
                    
                case 1:
                    xcom = (float)(Math.Cos(direction));
                    ycom = (float)(Math.Sin(direction));
                    setX(x);
                    setY(y );
                    break;
                case 2:
                    xcom = (float)(Math.Cos(direction) + 1);
                    ycom = (float)(Math.Sin(direction) + 1);
                    setX(x + xcom);
                    setY(y + ycom);
                    break;
                default:
                    setX(x);
                    setY(y);
                    break;
            }

        }
        public Vector2 getPosition()
        {
            return position;
        }
        public int getWidth()
        {
            return fWidth;
        }
        public int getHeight()
        {
            return fHeight;
        }
        public float getDirection()
        {
            return direction; 
        }
        public void setDirection(float dir)
        {
            direction = dir;
        }
        public void incIndex()
        {
            frameIndex++;
        }
        public int getIndex()
        {
            return frameIndex;
        }
        public void setIndex(int s)
        {
            frameIndex = s;
        }
        public void resetTime()
        {
            time = 0f;
        }
        public Texture2D getSS()
        {
            return btexture;
        }
        public void addTime(GameTime gt) {
            time += (float)gt.ElapsedGameTime.TotalSeconds;
        }
        public float getTime()
        {
            return time;
        }

        //Updates the position or animation of a bullet
        public void updateBullet(GameTime gameTime)
        {
            float frameTime;
            float xcom, ycom;
            switch (this.getID())
            {
                //Mage bullet, moves the missile in the direction it was fired
                case 0:
                    xcom = (float)(Math.Cos(direction)*3);
                    ycom = (float)(Math.Sin(direction)*3);
                    this.setX(getX() + xcom);
                    this.setY(getY() + ycom);
                    source = new Rectangle(0, 0, fWidth, fHeight);
                    position = new Vector2(x, y);
                    origin = new Vector2(fWidth, fHeight/2);
                    if(rules.onHit(parent)) { fired = false; }
                    break;

                //Rogue bullet, loops through the spritesheet for the rogue attack
                case 1:
                    time += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    frameTime = 0.05f;
                    while (getTime() > frameTime)
                    {
                        frameIndex++;
                        time = 0;

                    }
                    if (frameIndex > 5)
                    {
                        fired = false;
                        frameIndex = 0;
                    }
                    xcom = (float)Math.Cos(direction)*3;
                    ycom = (float)Math.Sin(direction)*3; 
                    
                    source = new Rectangle(frameIndex * fWidth, 0, fWidth, fHeight);
                    setX(xcom + getX());
                    setY(ycom + getY());
                    position = new Vector2(x, y);
                    origin = new Vector2(fWidth / 2, fHeight/2);
                    if (rules.onHit(parent)&&frameIndex==5) { fired = false; }
                    break;

                //Brute bullet, loops through the spritesheet for the brute attack
                case 2:        
                    time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    
                    frameTime = 0.04f;
                    while (getTime() > frameTime)
                    {
                        frameIndex++;
                        time = 0;
                        
                    }
                    if(frameIndex > 5)
                    {
                        fired = false;
                        frameIndex = 0;
                    }
                    xcom = (float)Math.Cos(direction)*20;
                    ycom = (float)Math.Sin(direction)*20;

                    source = new Rectangle(frameIndex * fWidth, 0, fWidth, fHeight);
                    
                    position = new Vector2(getX(), getY());
                    origin = new Vector2(fWidth/2, fHeight/2);
                    
                    if (rules.onHit(parent) && frameIndex == 5) { fired = false; }
                    break;
                default:

                    break;

            }
        
        }
        
        //Uses the spritebatch from HungerGenes.cs to draw the bullet on screen
        public void drawBullet(SpriteBatch spriteBatch)
        {
            if (fired)
            {
                if (this.getX() > 720 || this.getY() > 720 || this.getX() < 0 || this.getY() < 0)
                {
                    fired = false;              
                }else {
                    //spriteBatch.Draw(btexture, new Vector2(getX(), getY()), null, null, new Vector2(btexture.Width / 2, btexture.Height / 2), direction, null, null, SpriteEffects.None, 0);
                    spriteBatch.Draw(btexture, position, source, Color.White, direction, origin, 1.0f, SpriteEffects.None, 0.0f);
                }
            }
            

        }
        
    }
}

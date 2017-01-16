using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace HungerGenes
{
    class Bullets
    {
        List<Bullet> bullets;
        Bullet b;
        float time;
        float frameTime = 0.01f;
        int fWidth = 16;
        int fHeight = 10;
        public Bullets()
        {
            bullets = new List<Bullet>();
            
        }
        public Bullets(Bullet addBullet)
        {
            b = addBullet;
        }
        public void Update()
        {
            foreach (Bullet b in bullets)
            {
                if (b.getID() == 0)
                {
                    b.setX(b.getX() + 10);
                    b.setY(b.getX() + 10);
                }
                while (time > frameTime)
                {

                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {


            //foreach(Bullet b in bullets)
            //{
            if (b.Equals(null))
            {

            }
            else { 
                b.addTime(gameTime);

                
                while (b.getTime() > frameTime)
                {
                    b.incIndex();
                    b.resetTime();
                }
                if(b.getIndex() > 5)
                {
                    //b.setIndex(1);
                }else if (b.getIndex() == 5)
                {
                    
                }
                Rectangle source = new Rectangle(b.getIndex() * fWidth, 0, fWidth, fHeight);
                Vector2 position = new Vector2(b.getX(),b.getY());
                Vector2 origin = new Vector2(fWidth / 2.0f, fHeight);
                
                spriteBatch.Begin();
                spriteBatch.Draw(b.getSS(), position, source, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
                spriteBatch.End();
            }
            
        }
    }
}

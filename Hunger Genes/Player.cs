using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace HungerGenes
{
    class Player: Character
    {
        public int score;
        Texture2D weptex;
        public Texture2D texture { get; set; }
        Vector2 origin;

        public Player(int id, float x, float y, ContentManager Content)
        {
            bulletHits = 0;
            this.x = x;
            this.y = y;
            origin = new Vector2(x, y);
            this.weapon = id;
            score = 0;

            switch (this.getWeapon())
            {
                case 0:
                    weptex = Content.Load<Texture2D>("magemissile4816");
                    texture = Content.Load<Texture2D>("mage");
                    bullet = new Bullet(getX(), getY(), getWeapon(), weptex, this);
                    health = 15;
                    power = 30;
                    speed = 2.5f;
                    break;
                case 1:
                    weptex = Content.Load<Texture2D>("SlashSpriteSheetR");
                    texture = Content.Load<Texture2D>("rogue32");
                    bullet = new Bullet(getX(), getY(), getWeapon(), weptex, this);
                    health = 15;
                    power = 3;
                    speed = 5;
                    break;
                case 2:
                    weptex = Content.Load<Texture2D>("SmashSpriteSheet16020");
                    texture = Content.Load<Texture2D>("brute");
                    bullet = new Bullet(getX(), getY(), getWeapon(), weptex, this);
                    health = 15;
                    power = 1;
                    speed = 1;
                    break;
            }
        }
        public float getSpeed()
        {
            return speed;
        }
        public void updateScore()
        {
            score += bulletHits;
            bulletHits = 0;
        }
        public void resetPlayerPosition()
        {
            setX(50);
            setY(50);
        }
    }
}

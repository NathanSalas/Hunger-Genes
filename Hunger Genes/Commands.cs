using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace HungerGenes
{
    class Commands
    {
        Bullets attacks;
        Interactions rules;
        public Commands(Map m, Interactions i) {
            attacks = new Bullets();
            rules = i;
        }
 
        //Player Movement Command
        public Vector2 MoveCom(Player c, KeyboardState ks)
        {
            int d = 0;

            if (ks.IsKeyDown(Keys.W))
            {
                float n = c.getY() - c.getSpeed();
                c.setY(n);
                if (!rules.canMove(c))
                {
                    c.setY(n + c.getSpeed());
                }
                d += 8;
            }
            if (ks.IsKeyDown(Keys.A))
            {

                float m = c.getX() - c.getSpeed();
                c.setX(m);
                if (!rules.canMove(c))
                {
                    c.setX(m + c.getSpeed());
                }
                d += 4;
            }
            if (ks.IsKeyDown(Keys.S))
            {

                float o = c.getY() + c.getSpeed();
                c.setY(o);
                if (!rules.canMove(c))
                {
                    c.setY(o - c.getSpeed());
                }
                d += 2;
            }
            if (ks.IsKeyDown(Keys.D))
            {

                float p = c.getX() + c.getSpeed();
                c.setX(p);
                if (!rules.canMove(c))
                {
                    c.setX(p - c.getSpeed());
                }
                d += 1;
            }
           
            return new Vector2(c.getX(), c.getY());
        }

        //AI Movement Command
        public Vector2 MoveCom(Vector2[] force, Enemy c) { 
        

            float xcom = (float)(Math.Cos(c.dna.genes[c.indx].X)*5);
            float ycom = (float)(Math.Sin(c.dna.genes[c.indx].X)*5);
            c.direction =(c.dna.genes[c.indx].X);
            if (c.dna.genes[c.indx].X == 0) xcom = 0;

            float xd = c.getX() + xcom;
            float yd = c.getY() + ycom;
                   
            c.setX(xd);
            c.setY(yd);

            if (!rules.canMove(c))
            {
                c.setX(c.getX() - xcom);
                c.setY(c.getY() - ycom);
            }
            return new Vector2(c.getX(), c.getY());
        }

        //Universal Attack Command function
        //Takes the attacking character
        //
        public void AttackCom(Character c)
        {
            c.getBullet().Fired(c.getX(), c.getY(), rules);
        }    
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HungerGenes
{
    class Map
    {
        Tile[,] map;
        Tile[,] omap;
        SpriteBatch sb;
        ContentManager content;
        
       
        public Map(SpriteBatch sb, ContentManager content, Tile[,] map)
        {
            this.sb = sb;
            this.content = content;
            this.map = map;
            omap = (Tile[,])map.Clone();
            drawMap();
        }

        //Uses the HungerGenes.cs spritebatch to draw the map
        public void drawMap()
        {

            int x = 0, y = 0;
            for(int i = 0; i < 45; i++)
            {
                for(int j = 0; j < 45; j++)
                {
                    Vector2 location = new Vector2(x,y);
                    map[i, j].setLocation(location);
                    sb.Begin();
                    sb.Draw(map[i, j].getTexture(), location);
                    sb.End();
                    x += 16;
                   

                }
                x = 0;
                y += 16;
            }
        }
        
        //Return the tile at a given location
        public Tile tileAt(float x, float y)
        {
            int nx = (int)Math.Floor(x / 16) * 16;
            int ny = (int)Math.Floor(y / 16) * 16;
            Vector2 r = new Vector2(nx, ny);
            
            int tileID=0;
            
            foreach(Tile t in map)
            {
                if (r == t.getLocation())
                {
                    tileID = t.getID();
                    
                    return t;
                }
            }
            return new Tile();
        }
    
        //Updates the map from the changes made by Interactions
        public void updateMap(Tile[,] map)
        {
            this.map = map;
        }
    
        //Gets all tiles adjacent to a specified tile
        public List<Tile> getAdjacent(Tile t)
        {
            List<Tile> returnTiles = new List<Tile>(6);
            Tile tile;
            
            returnTiles.Add(tileAt(t.getLocation().X, t.getLocation().Y - 16));
            returnTiles.Add(tileAt(t.getLocation().X + 16, t.getLocation().Y - 16));
            returnTiles.Add(tileAt(t.getLocation().X + 16, t.getLocation().Y));
            returnTiles.Add(tileAt(t.getLocation().X + 16, t.getLocation().Y + 16));
            returnTiles.Add(tileAt(t.getLocation().X , t.getLocation().Y + 16));
            returnTiles.Add(tileAt(t.getLocation().X - 16, t.getLocation().Y + 16));
            returnTiles.Add(tileAt(t.getLocation().X - 16, t.getLocation().Y));
            returnTiles.Add(tileAt(t.getLocation().X - 16, t.getLocation().Y - 16));

            return returnTiles;
        }
        public List<Tile> getLine(Tile t, Enemy p)
        {
            List<Tile> line = new List<Tile>(3);
            line.Add(t);
            return line;
        }

        //Gets the x and y coordinates of a tile
        public string getCoords(Tile t)
        {
            int nx = (int)Math.Floor(t.getLocation().X / 16);
            int ny = (int)Math.Floor(t.getLocation().Y / 16);
            return (nx + " , " + ny);
        }

        //Resets the map to the default map and sets new power up locations
        public void resetMap(Random r, ContentManager Content)
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (j < 5 && i < 5)
                    {
                        map[i, j] = new Tile(0, Content);
                    }
                    else if (j >= 40 && i < 5)
                    {
                        map[i, j] = new Tile(0, Content);
                    }
                    else if (j >= 40 && i >= 40)
                    {
                        map[i, j] = new Tile(0, Content);
                    }
                    else if (j < 5 && i >= 40)
                    {
                        map[i, j] = new Tile(0, Content);
                    }
                    else
                    {
                        map[i, j] = new Tile(1, Content);
                    }
                }
                for (int a = 20; a < 25; a++)
                {
                    for (int b = 20; b < 25; b++)
                    {
                        map[a, b] = new Tile(3, Content);
                    }
                }

            }
            for (int i = 0; i < 16; i++)
            {
                map[i, 22] = new Tile(2, Content);

            }
            for (int i = 29; i < 45; i++)
            {
                map[i, 22] = new Tile(2, Content);

            }
            for (int i = 0; i < 16; i++)
            {
                map[22, i] = new Tile(2, Content);

            }
            for (int i = 29; i < 45; i++)
            {
                map[22, i] = new Tile(2, Content);

            }
            for (int x = 0; x < 20; x++)
            {
                int ranx = r.Next(0, 44);
                int rany = r.Next(0, 44);
                int ranp = r.Next(4, 7);
                if (map[ranx, rany].getID() == 1)
                {
                    map[ranx, rany] = new Tile(ranp, Content);
                }
            }
        }
    }
}

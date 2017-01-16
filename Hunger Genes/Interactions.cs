using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerGenes
{
    class Interactions
    {
        Map map;
        public Interactions(Map m)
        {
            map = m;
        }
        public Map getMap()
        {
            return map;
        }

        //Called when a bullet object hits a tile
        public bool onHit(Character p)
        {    
            List<Tile> tiles = new List<Tile>();
            Tile t = map.tileAt(p.getBullet().getPosition().X, p.getBullet().getPosition().Y);
            int tilesHit = 0;
            

            tiles = map.getAdjacent(t);
            tiles.Add(t);
          
            //Handles what blocks are hit for each bullet
            switch (p.getWeapon())
            {
                //Mage attack, gets the tiles in an X around the tile hit
                case 0:
                    if(t == null || t.getID() <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        p.addHits(t.onHit(p));
                        p.addHits(tiles[1].onHit(p));
                        p.addHits(tiles[3].onHit(p));
                        p.addHits(tiles[5].onHit(p));
                        p.addHits(tiles[7].onHit(p));

                        return true;
                    }
                  
                default:
                    foreach (Tile ts in tiles)
                    {
                        
                        if(ts.getID() > 0)
                        {

                            p.addHits(ts.onHit(p));
                            tilesHit++;
                        }
                    }
                    if (tilesHit == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
            }
            
        }
        
        //Checks if a character is moving to a valid location
        public bool canMove(Character c)
        {
            
            Tile at = map.tileAt(c.getX(), c.getY());
            if (at == null || at.getID() != 0 && at.getID() != 3) { return false; } else { return true; }       
            
        }

        //Checks if a character hits a goal tile
        public bool goalReached(Character c)
        {
            Tile t = map.tileAt(c.getX(), c.getY());
            if(t.getID() == 3)
            {
                return true;
            }
            return false;
        }
    }
}

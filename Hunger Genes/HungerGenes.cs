using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Threading;
namespace HungerGenes
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
    public class HungerGenes : Game
    {

        public static int pop = 20;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Commands c;
        Vector2 ppos; 
        Player p;
        Map m;
        SpriteFont font;
        Random r = new Random(DateTime.Now.Millisecond);
        float angle;
        Population magepop, roguepop, brutepop;
        int magecycle, roguecycle, brutecycle;
        MouseState lastMS, currentMS;
        Interactions i;
        bool phase1;
        Tile[,] originalMap;
        Thread mt, roguethread, brutethread;
        Viewport gameScene, details, defaultView;

        public HungerGenes()
        {
            graphics = new GraphicsDeviceManager(this);
            this.Window.Title = "Hunger Genes: A Genetic Algorithm-Based Game";
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            phase1 = true;
            defaultView = GraphicsDevice.Viewport;
            gameScene = defaultView;
            details = defaultView;
            details.Width = defaultView.Width / 5;
            details.X = 720;
            this.Window.Position = new Point(252, 19);
            currentMS = Mouse.GetState();
            lastMS = currentMS;

            magecycle = 0;roguecycle = 0;brutecycle = 0;
 
            //Initialize the player and enemies in their appropriate locations
            p = new Player(0, 50f, 50f, Content);
           
            magepop = new Population(0, 650, 650, r, Content);
            roguepop = new Population(1, 50, 650, r, Content);
            brutepop = new Population(2, 650, 50, r, Content);
            
            this.IsMouseVisible = true;
            base.Initialize();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 900;
            gameScene.Height = 720;
            gameScene.Width = 720;
            graphics.ApplyChanges();
            font = Content.Load<SpriteFont>("DetailFont");
            
            //Creates the basic/default map
            originalMap = new Tile[50,50];
            for(int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (j < 5 && i < 5)
                    {
                        originalMap[i, j] = new Tile(0, Content);
                    }
                    else if (j >= 40 && i < 5)
                    {
                        originalMap[i, j] = new Tile(0,Content);
                    }
                    else if (j >= 40 && i >= 40)
                    {
                        originalMap[i, j] = new Tile(0, Content);
                    }
                    else if (j < 5 && i >= 40)
                    {
                        originalMap[i, j] = new Tile(0, Content);
                    }
                    else
                    {
                        originalMap[i, j] = new Tile(1, Content);
                    }
                }
                for(int a = 20; a < 25; a++)
                {
                    for(int b = 20; b < 25; b++)
                    {
                        originalMap[a, b] = new Tile(3, Content);
                    }
                }
                
            }

            for (int i = 0; i < 16; i++) 
            {
                originalMap[i, 22] = new Tile(2, Content);
    
            }
            for (int i = 29; i < 45; i++)
            {
                originalMap[i, 22] = new Tile(2, Content);

            }
            for (int i = 0; i < 16; i++)
            {
                originalMap[22, i] = new Tile(2, Content);

            }
            for (int i = 29; i < 45; i++)
            {
                originalMap[22, i] = new Tile(2, Content);

            }
            for (int x = 0; x < 20; x++)
            {
                int ranx = r.Next(0, 44);
                int rany = r.Next(0, 44);
                int ranp = r.Next(4, 7);
                if (originalMap[ranx, rany].getID() == 1)
                {
                    originalMap[ranx, rany] = new Tile(ranp, Content);
                }
            }
            Tile[,] t = originalMap;
            m = new Map(spriteBatch, this.Content, t);
            i = new Interactions(m);
            c = new Commands(m, i);
            
            
            
        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!phase1)
            {
                Console.WriteLine("GOALLL");
                
                p.resetPlayerPosition();
                magepop.resetPopPosition();
                roguepop.resetPopPosition();
                brutepop.resetPopPosition();

                m.resetMap(r, Content);
        
                phase1 = true;
                
            }

            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            //End of lifetime for each population
            if(magecycle == magepop.lifetime)
            {
                magepop.fitness();
                magepop.selection();
                magepop.updateLifetime();
                magepop.reproduction();
                magepop.gen++;
                magecycle = 0;
            }
            if (roguecycle == roguepop.lifetime)
            {
                
                roguepop.fitness();
                roguepop.selection();
                roguepop.updateLifetime();
                roguepop.reproduction();
                roguepop.gen++;
                roguecycle = 0;
            }
            if (brutecycle == brutepop.lifetime)
            {
                brutepop.fitness();
                brutepop.selection();
                brutepop.updateLifetime();
                brutepop.reproduction();
                brutepop.gen++;
                brutecycle = 0;
            }

            //Update each population while the generation is in progress
            //Checks if they reached a goal, allows them to move, and checks if they fired a bullet
            foreach (Enemy e in magepop.enemy)
            {
                if (i.goalReached(e))
                {
                    phase1 = false;
                }
                double fire = r.NextDouble();

                c.MoveCom(e.dna.genes, e);
                if (fire < e.getSpeed() && !e.getBullet().isFired()) { c.AttackCom(e); e.getBullet().setDirection(e.direction); }
                if (e.getBullet().isFired())
                {

                    e.getBullet().updateBullet(gameTime);

                }

                e.indx++;
                
                e.evaluateFitness();
            }

            foreach (Enemy e in roguepop.enemy)
            {
                if (i.goalReached(e))
                {
                    phase1 = false;
                }
                double fire = r.NextDouble();
                
                c.MoveCom(e.dna.genes, e);

                if (fire < e.getSpeed() && !e.getBullet().isFired()) { c.AttackCom(e); e.getBullet().setDirection(e.direction); }
                if (e.getBullet().isFired())
                {

                    e.getBullet().updateBullet(gameTime);

                }
               
                e.indx++;
                e.evaluateFitness();
            }

            foreach (Enemy e in brutepop.enemy)
            {
                if (i.goalReached(e))
                {
                    phase1 = false;
                }
                double fire = r.NextDouble();
                
                c.MoveCom(e.dna.genes, e);
                if (fire < e.getSpeed() && !e.getBullet().isFired()) { c.AttackCom(e); e.getBullet().setDirection(e.direction); }
                if (e.getBullet().isFired())
                {

                    e.getBullet().updateBullet(gameTime);

                }
                e.indx++;
                e.evaluateFitness();
            }
            //Increment all the cycle variables
            magecycle++;
            roguecycle++;
            brutecycle++;
            
            //Update player movement and attacks 
            ppos = c.MoveCom(p, ks);
            if (i.goalReached(p)) { phase1 = false; }
            Vector2 direction = ms.Position.ToVector2() - ppos;
            lastMS = currentMS;
            currentMS = Mouse.GetState();
            angle = (float)Math.Atan2(direction.Y, direction.X);

            if (lastMS.LeftButton == ButtonState.Released && currentMS.LeftButton == ButtonState.Pressed)
            {
                if (!p.getBullet().isFired())
                {
                    c.AttackCom(p);
                    p.getBullet().setDirection(angle);
                }
                Tile test = m.tileAt(ms.Position.X, ms.Position.Y);
                Console.WriteLine("X: {0}, Y: {1} Angle {2}", ms.Position.X, ms.Position.Y, angle);
                Console.WriteLine("Location" + m.getCoords(test) + " ID:" +test.getID());
            }
            if (p.getBullet().isFired())
            {

                p.getBullet().updateBullet(gameTime);
                p.updateScore();

            }

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Draws the information in the details pane
            GraphicsDevice.Viewport = defaultView;
            GraphicsDevice.Clear(Color.White);
            GraphicsDevice.Viewport = details;
            DrawDetails();

            //Draws what is happening in the game
            //Draws map first, then all of the players and bullets  
            GraphicsDevice.Viewport = gameScene;
            m.drawMap();
            spriteBatch.Begin();
            
            //Player Draw
            spriteBatch.Draw(p.texture, ppos, null, null, new Vector2(p.texture.Width/2, p.texture.Height/2), angle, null, null, SpriteEffects.None, 0) ;
            p.getBullet().drawBullet(spriteBatch);

            //Mage Draw
            foreach(Enemy e in magepop.enemy)
            {
                spriteBatch.Draw(e.texture, new Vector2(e.getX(), e.getY()), null, null, new Vector2(e.texture.Width / 2, e.texture.Height / 2), e.direction, null, null, SpriteEffects.None, 0);
                e.getBullet().drawBullet(spriteBatch);
            }

            //Rogue Draw
            foreach (Enemy e in roguepop.enemy)
            {
                
                spriteBatch.Draw(e.texture, new Vector2(e.getX(), e.getY()), null, null, new Vector2(e.texture.Width / 2, e.texture.Height / 2), e.direction, null, null, SpriteEffects.None, 0);
                e.getBullet().drawBullet(spriteBatch);
            }

            //Brute Draw
            foreach (Enemy e in brutepop.enemy)
            {
                spriteBatch.Draw(e.texture, new Vector2(e.getX(), e.getY()), null, null, new Vector2(e.texture.Width / 2, e.texture.Height / 2), e.direction, null, null, SpriteEffects.None, 0);
                e.getBullet().drawBullet(spriteBatch);
            }
            
            spriteBatch.End();                       

            base.Draw(gameTime);
        }
        
        //Draws information about the game.
        //This includes the generations and scores of the characters
        public void DrawDetails()
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Generations", new Vector2(2, 0), Color.Black);
            spriteBatch.DrawString(font, "Brutes: " + brutepop.gen, new Vector2(2, 20), Color.Brown);
            spriteBatch.DrawString(font, "Rogues: " + roguepop.gen, new Vector2(2, 40), Color.Red);
            spriteBatch.DrawString(font, "Mages: " + magepop.gen, new Vector2(2, 60), Color.Purple);
            spriteBatch.DrawString(font, "Scores", new Vector2(2, 100), Color.Black);
            spriteBatch.DrawString(font, "Player: " + p.score, new Vector2(2, 120), Color.Blue);
            spriteBatch.DrawString(font, "Brutes: " + brutepop.getTotal(), new Vector2(2, 140), Color.Brown);
            spriteBatch.DrawString(font, "Rogues: " + roguepop.getTotal(), new Vector2(2, 160), Color.Red);
            spriteBatch.DrawString(font, "Mages: " + magepop.getTotal(), new Vector2(2, 180), Color.Purple);
            

            spriteBatch.End();
        }
       
    }
}

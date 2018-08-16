using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Jazz
{
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        //player
        Player p = new Player();
        //bomblist

        List<Bomb> BombList = new List<Bomb>();

        //jets
        public int jetsBulletDamage;
        //jetslist
        List<Jets> jetsList = new List<Jets>();

        //HUD
        HUD h = new HUD();
        //explosion
        List<Explosion> explosionList = new List<Explosion>();
       
      //background
        WarField sf = new WarField();
        //sound
        SoundManager sound = new SoundManager();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 600;//width and height of game window
            graphics.PreferredBackBufferHeight = 800;

            this.Window.Title = "Jazz";
            Content.RootDirectory = "Content";
            //jets
            jetsBulletDamage = 10;


        }

      
        //init
        protected override void Initialize()
        {


           

            base.Initialize();
        }

     
        //load content
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            p.LoadContent(Content);
            sf.LoadContent(Content);
            h.LoadContent(Content);
            sound.LoadContent(Content);
            MediaPlayer.Play(sound.backgroundMusic);//coz its mp3
          
            
        }

      //uppload conten
        protected override void UnloadContent()
        {
            
        }

    //update
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                this.Exit();
            //---------------------------------jets------------------
            //forech jetslist, update it . and che colliion och jets with player
            foreach(Jets j in jetsList)
            {
                if (j.boundingBox.Intersects(p.boundingBox))
                {
                    //sound.explodeSound.Play();
                  // explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion17"), new Vector2(j.position.X, j.position.Y)));//create explotion where the jet died

                   //p.health -= 40;
                    j.isVisible = true;
                }
                //if jets bullet hits the player
                for (int i = 0; i < j.bulletList.Count; i++) //kolla genom om bullet hits the player then decrese health
                { 

                    if (p.boundingBox.Intersects(j.bulletList[i].boundingBox))
                    {
                        sound.explodeSound.Play();
                        p.health -= jetsBulletDamage;//declared on the jets klass damage =10
                        j.bulletList[i].isVisible = false;//we dont wanna see bullet passing the jets

                    }
                }
                //if players bullet hits the jets
                for(int i = 0; i < p.bulletList.Count; i++)
                {
                    if (j.boundingBox.Intersects(p.bulletList[i].boundingBox))
                    {
                        sound.explodeSound.Play();
                        h.playerScore += 10;
                        p.bulletList[i].isVisible = false;//we dont wanna see bullet passing the jets
                                                          // j.health -= 1;
                                                          //------------------------------------------expoloion-----------------------------------
                        
                        j.isVisible = false;
                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion17"), new Vector2(j.position.X, j.position.Y)));//create explotion where the jet died
                        
                    }
                }
                
                j.Update(gameTime);
               
            }
            //___________________________expoltion_________________________
            foreach(Explosion ex in explosionList)
            {
            ex.Update(gameTime);
            }
            //--------------------------------------------bomb-------------------------------------------------
            //forech bomblist, update it, and check for colliion
            foreach (Bomb a in BombList)
            {
                //check if any of bomb colliding with jet.. remove them from bomblist
                if (a.boundingBox.Intersects(p.boundingBox))
                {
                    sound.explodeSound.Play();
                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion17"), new Vector2(a.position.X, a.position.Y)));//create explotion where the jet died

                    p.health -= 20;//when player jet colliding with bombs. decrease health bar by 20 pix
                    a.isVisible = false; //dont show it 
                }
                //if the bullet colliding with bombs remove bomb
                for(int i = 0; i < p.bulletList.Count; i++)
                {
                    if(a.boundingBox.Intersects(p.bulletList[i].boundingBox))
                    {
                        sound.explodeSound.Play();
                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion17"), new Vector2(a.position.X, a.position.Y)));//create explotion where thebomb died
                        a.isVisible = false;
                        p.bulletList.ElementAt(i).isVisible = false;
                        h.playerScore += 5;
                    }
                }
                a.Update(gameTime);
            }
            p.Update(gameTime);
            sf.Update(gameTime);
            ManageExplosions();//it will look after when the explosion is finished the it will go to remove it
            LoadBombs();
            LoadJets();
            //h.Update(gameTime);
           
            base.Update(gameTime);
        }

   //draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
          
            sf.Draw(spriteBatch);
            h.Draw(spriteBatch);
            //_______________________________bomb------------------------
            foreach (Bomb a in BombList)
            {

                a.Draw(spriteBatch);
            }
            //_______________________expoltion--------------------
            foreach(Explosion ex in explosionList)
            {
                ex.Draw(spriteBatch);
            }
            
           
            //_____________________________jets________________
            foreach (Jets j in jetsList)
            {
                j.Draw(spriteBatch);
            }

            //____________________________________________________

            p.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        //loadbob´mbs
        public void LoadBombs()
        {
            //creating random  bombs
            int randY = random.Next(-800, -100);//-100 how close the bombs are
            int randX = random.Next(0, 590); // x postion
            //if less then 3 add more until 3
            if(BombList.Count() <3)
            {
                BombList.Add(new Bomb(Content.Load<Texture2D>("Bomb2"), new Vector2(randX, randY)));
            }
            //if bomb is destryoed oc visible the remove it

            for(int i= 0; i < BombList.Count; i++)
            {
                if (!BombList[i].isVisible)
                {
                    BombList.RemoveAt(i);
                    i--;
                }
            }
        }
        //--------------------------------------------------------jets-------------------------------------------
        public void LoadJets()
        {
            //creating random  jets
            int randY = random.Next(-800, -100);//-100 how close the jets are
            int randX = random.Next(150, 400); // x postion
            //if less then 2 add more until 5
            if (jetsList.Count() < 2)
            {
                jetsList.Add(new Jets(Content.Load<Texture2D>("tank4"), new Vector2(randX, randY), Content.Load<Texture2D>("JetsBullet")));
            }
            //if jets is destryoed oc visible the remove it

            for (int i = 0; i < jetsList.Count; i++)
            {
                if (!jetsList[i].isVisible)
                {
                    jetsList.RemoveAt(i);
                    i--;
                }
            }
        }
        //manage explostion
        public void ManageExplosions()
        {
            for(int i = 0; i < explosionList.Count; i++)
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}

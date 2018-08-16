using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
namespace Jazz
{
    public class Player
    {
        public Texture2D texture;
        public Vector2 position;
        public int speed;
        //sound
        SoundManager sound = new SoundManager();
        //bullet varible
        public Texture2D bulletTexture;
        public float bulletDelay;
        public List<Bullet> bulletList;
        //health
        public Texture2D healthTexture;
        public Rectangle healthRectangle;
        public int health;
        public Vector2 healthbarposition;

        // collision veriables
        public Rectangle boundingBox;
        public bool isColliding;
        
        //constructr , methods
        public Player()
        {

            texture = null;
            position = new Vector2(220, 300);
            speed = 10;
            isColliding = false;
            bulletList = new List<Bullet>();
            bulletDelay = 10;//how fast
            health = 200;//200 pixale long and 25 hi
            healthbarposition = new Vector2(50,50);
        }
        //load content
        public void LoadContent( ContentManager Content)
        {
            texture = Content.Load<Texture2D>("jet1");//
            bulletTexture = Content.Load<Texture2D>("bullet");
            healthTexture = Content.Load<Texture2D>("health2");
            sound.LoadContent(Content);
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);//healthrectagle is 25x25. here the rectangle draws exact the same size
            foreach (Bullet b in bulletList)
                b.Draw(spriteBatch);
            
        }
        //update
        public void Update(GameTime gametime)
        {
            //rectangle for healthbar
            healthRectangle = new Rectangle((int)healthbarposition.X,(int)healthbarposition.Y, health, 25);//healthbarposition (50,50) declared uppe//health =200 pixl//25= height of over image
            //getting keyboard state
            KeyboardState keystate = Keyboard.GetState();
            //colision
            //boudingbox for ou playerjet
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);//jet rectangle . jet är 100x100
            //fire bullet
            if (keystate.IsKeyDown(Keys.Space))
            {
                Shoot();
            }
            UpdateBullets();
            // jek controls
            if (keystate.IsKeyDown(Keys.Right))
                position.X = position.X + speed;
            if (keystate.IsKeyDown(Keys.Left))
                position.X = position.X - speed;
            if (keystate.IsKeyDown(Keys.Up))
                position.Y = position.Y - speed;
            if (keystate.IsKeyDown(Keys.Down))
                position.Y = position.Y + speed;

            //keep jet inside screen
            if (position.X <= 0)
                position.X = 0;
            if (position.X >= 600 - texture.Width)
                position.X = 600 - texture.Width;
            if (position.Y <= 0)
                position.Y = 0;
            if (position.Y >= 800 - texture.Height)
                position.Y = 800 - texture.Height;

        }
        //shoot method
        public void Shoot()
        {
            //shoot when bullet delay resets
            if (bulletDelay >= 0)
                bulletDelay--;
            //if bullet delay is at 0 creat new bullet at player positoin make it visible on the screen , the add that bull to the list
            if(bulletDelay <= 0 )
            {
                sound.playerFireSound.Play();
                //when bullet is = 0 creat new bullet
                Bullet newBullet = new Bullet(bulletTexture);
                //new bullets position to the midell on fighter
                newBullet.position = new Vector2(position.X + 27 - newBullet.texture.Width/2, position.Y + 10); //breddeen på jet =54 och 27 är halva.
                //
                newBullet.isVisible = true;
                if (bulletList.Count() < 20)//20 bullet is visible on the screen
                    bulletList.Add(newBullet);
            }
            //reset bullet delay
            if (bulletDelay == 0)
                bulletDelay = 10; //10 is the speed of bullet
        }
        //update bullet
        public void UpdateBullets()
        {
            
            //for each bullet in over bulletlist , update th movement and if the bullet hits the top of the screen reamove it from the lsit
            foreach(Bullet b in bulletList) {
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                //movement for the bullet 
            b.position.Y = b.position.Y - b.speed;//shoot towards the top of screen
                //if bullet hits the top of the screen, the make visile fasle
            if (b.position.Y <= 0) //  zero is the top of screec coz rectangle starts from topleft
                    
                b.isVisible = false;
        }

            for( int i = 0; i < bulletList.Count; i++)
            {
                if(!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}

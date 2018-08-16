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
     public  class Bomb
    {
        public Rectangle boundingBox;//for collision
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotatioAngle;
        public int speed;
        public bool isVisible;
        Random random = new Random();
        public float randX, randY;



        //constructor
        public Bomb(Texture2D newTexture, Vector2 newPosition)
        {
            //position = new Vector2(400, -100);// above window 50 pixle
            position = newPosition;
            texture = newTexture;
            speed = 1;
            randX = random.Next(400,540);//handle the x position//betwwen 0 and 600 // we givi t-540 coz bomb rectangle is also 50x50
            randY = random.Next(-600,-50);//
            isVisible = true;

        }
        // load content
        public void LoadContent(ContentManager Content)
        {
            //texture = Content.Load<Texture2D>("Bomb1");

        }
        // update
        public void Update( GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 35,50);//bomb rectangle

            //update movement
            position.Y = position.Y + speed;//bomb will go rakt ner med speed = 10 declared uppe
            if (position.Y >= 800)//position of bombs on screen//när ska bomben förvinna från screen efter 800 pix
                position.Y = -50;//när ska bomben dyka up on sscreen

            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

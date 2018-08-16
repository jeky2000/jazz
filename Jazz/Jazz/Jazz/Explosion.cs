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
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public float timer;
        public float interval;
        public Vector2 origin;
        public int currentFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRct;
        public bool isVisible;

        //construtor
        public Explosion(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            timer = 0f;//coz its float
            interval=150f; //if you want it to slow donw o speed up
            currentFrame = 1;
            spriteWidth = 64;//64;
            spriteHeight = 64;//64;
            isVisible = true;
        
        }
        //load content
        public void LoadContent(ContentManager Content)
        {
            
        }
        //update
        public void Update(GameTime gameTime)
        {
            //increase the timer by the number of milliseconds
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //check the time is more than the chosen interval
            if(timer > interval)//timer will run the whole interval
            {
                //20f is the last pic. and 0 is the first.. when it is the last pic then start from zero agian
                //show next frame
                currentFrame++;
                //reset timer
                timer = 0f;
            }
            //if were on the last frame, make the exposion invisible and reset currentFrame to begining of spritesheet
            if(currentFrame == 25)//we have 17 expolosoin pic
            {
                isVisible = false;
                currentFrame = 0;


            }
            sourceRct = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);//put in the rectangle
            origin = new Vector2(sourceRct.Width /2, sourceRct.Height/2 );//
        }
        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(texture, position, sourceRct, Color.White, 0f, origin, 1.0f,SpriteEffects.None, 0);
            }
        }
    }
}

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
    public class WarField
    {
        public Texture2D texture ;
        public Vector2 bgPos1, bgPos2;//background postion1 and postion2
        public int speed;
        
        //constructor
        public WarField()
        {
            texture = null;
            bgPos1 = new Vector2(0, 0);// cover entire screen
            bgPos2 = new Vector2(0, -800);
          
           
            speed = 2;



        }
        //load content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Road22");
           // texture = Content.Load<Texture2D>("background");


        }
        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPos1, Color.White);
            spriteBatch.Draw(texture, bgPos2, Color.White);
         
        }
        //update
        public void Update(GameTime gameTime)
        {
            //setting speed fro background scoliing
            bgPos1.Y = bgPos1.Y + speed;
            bgPos2.Y = bgPos2.Y + speed;
          

            //scrolling background (Repeating)


            if (bgPos1.Y >=800)
            {
                
                bgPos1.Y = 0;
                bgPos2.Y = -800;
            }

        }
    }
}



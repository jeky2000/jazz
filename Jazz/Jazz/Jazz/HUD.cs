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
    public class HUD//heads updesplay
    {
        public int playerScore, Width, Height;
        public SpriteFont playerScoreFont;//
        public Vector2 playerScorePos;//
        public bool showHud; //to show hud or not... if you want to hide the score bar

        //constructor
        public HUD()
        {
            playerScore = 0;//
            showHud = true;
            Height = 10;
            Width = 10;
            playerScoreFont = null;

            playerScorePos = new Vector2(Width ,Height);//
        }
        //loadcontent
        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("georgia");

        }
        //update
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
        }
        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            //if we are shoing our hud (if hsowhud = true then dispay ou hud
            if (showHud)
            {
               spriteBatch.DrawString(playerScoreFont, "Score-" + playerScore, playerScorePos, Color.Red);//drawString. to draw a text
            }
        }
    }
}

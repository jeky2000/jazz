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
    public class Jets
    {
        public Rectangle boundingBox;
        public Texture2D texture, bulletTexture; //bulletexture for bullet from tanks
        public Vector2 position;
        public int health, speed, bulletDelay,currentDifficultiLevel;
        public bool isVisible; // to remove ot show tanks 
        public List<Bullet> bulletList;
      



        //constructor
        public Jets(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            bulletList = new List<Bullet>();
            texture = newTexture;
            bulletTexture = newBulletTexture;
            health = 5;//it depent on damage of player bullet . if damage is = 1 then 5 bullet will kill the jet
            position = newPosition;
            currentDifficultiLevel = 1;

            isVisible = true;

            bulletDelay = 60;
            speed = 3;

        }


        //we will make an jets list in game1

        //update
        public void Update(GameTime gameTime)
        {
            //update collision rectanlge
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            //update movement
            position.Y += speed;// position.Y = position.Y + speed;//bomb will go rakt ner med speed = 10 declared uppe
            if (position.Y >= 800)//position of bombs on screen//när ska bomben förvinna från screen efter 800 pix
                position.Y = -50;//när ska bomben dyka up on sscreen

            Shoot(); //call
            UpdateBullets();//Call

        }
        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)

                spriteBatch.Draw(texture, position, Color.White);

            //draw jets bullets
            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);//we call it from bullet.cs under Draw
            }
        }
        //update bullet
        public void UpdateBullets()
        {

            //for each bullet in over bulletlist , update th movement and if the bullet hits the top of the screen reamove it from the lsit
            foreach (Bullet b in bulletList)
            {
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                //movement for the bullet 
                b.position.Y = b.position.Y + b.speed;// +b shoot towards the botom of screen // -b top
                                                      //if bullet hits the rop of the screen, the make visile fasle
                if (b.position.Y >= 800) //when the bullet reach the bottom of screen the delete it

                    b.isVisible = false;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }

        //jets shoot
        public void Shoot()
        {
            //shoot only if bulletdelay resets
            if (bulletDelay >= 0)
                bulletDelay--;

            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + texture.Width / 2 - newBullet.texture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;

                if (bulletList.Count() < 10)
                    bulletList.Add(newBullet);

            }
            //bulletdelay
            if (bulletDelay == 0)
                bulletDelay = 100;// bullet speed
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
namespace Jazz
{
    public class SoundManager
    {
        
        
        public SoundEffect playerFireSound;
        public SoundEffect explodeSound;
        public Song backgroundMusic;

        //contructor
        public SoundManager()
        {
            playerFireSound = null;
            explodeSound = null;
            backgroundMusic = null;

        }
        // load content
        public void LoadContent(ContentManager Content)
        {
            playerFireSound = Content.Load<SoundEffect>("playerFire");//wav
            explodeSound = Content.Load<SoundEffect>("explode");//wav
             backgroundMusic = Content.Load<Song>("igi");//mp3
        }
    }
   
}

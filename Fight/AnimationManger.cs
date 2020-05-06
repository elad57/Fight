using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fight
{
   
    class AnimationManger
    {
        public animation _animation;
        public float timer;
        public double rot;
        public Vector2 pos;
        public Vector2 scale;


        public AnimationManger(animation a)
        {

            _animation = a;
            scale = new Vector2(5);
            rot = 0;

        }
        public void play(animation a)
        {
            if (a == _animation)
                return;
            timer = 0;
            _animation.index = 0;
            _animation = a;
        }
        public void update(GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalSeconds;
            if(timer>_animation.speed)
            {
                timer = 0;
                _animation.index++;
            }
            if(_animation.index>=_animation.frames&&_animation.isloopable)
            {
                _animation.index = 0;

            }
            if(!_animation.isloopable&& _animation.index >= _animation.frames)
            {
                _animation.index = _animation.frames-1;
            }
        }
        public virtual void drawleft()
        {
            G.sb.Begin();
            G.sb.Draw(_animation.texture, pos, null,_animation.rec[_animation.index], null,(float)rot, new Vector2(5), Color.White, SpriteEffects.None, 1);
            G.sb.End();
        }
        public virtual void drawright()
        {
            G.sb.Begin();
            G.sb.Draw(_animation.texture, pos,null, _animation.rec[_animation.index], null,(float)rot,new Vector2(5),Color.White,SpriteEffects.FlipHorizontally,1);
            G.sb.End();
        }
    }
}

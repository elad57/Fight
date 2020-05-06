using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fight
{
    abstract class basekeys
    {
        public abstract bool Left();
        public abstract bool Right();
        public abstract bool Up();
        public abstract bool Down();
        public abstract bool Light();
        public abstract bool Heavy();
        public abstract bool block();
        public abstract void whoami(Fighter me);
        

    }
    class PlayerKeys : basekeys
    {
        Keys up, down, left, right,light,heavy,block1;
        public PlayerKeys(Keys left, Keys right, Keys up, Keys down,Keys light, Keys heavy,Keys block)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
            this.light = light;
            this.heavy = heavy;
        }




        public override bool Down()
        {
            return G.ks.IsKeyDown(this.down);
        }

        public override bool Left()
        {
            return G.ks.IsKeyDown(this.left); 
        }

        public override bool Right()
        {
            return G.ks.IsKeyDown(this.right);
        }

        public override bool Up()
        {
            return G.ks.IsKeyDown(this.up);
        }
        public override bool Light()
        {
            return G.ks.IsKeyDown(this.light);
        }
        public override bool Heavy()
        {
            return G.ks.IsKeyDown(this.heavy);
        }
        public override bool block()
        {
            return G.ks.IsKeyDown(this.block1);
        }

        public override void whoami(Fighter me)
        {
            
        }
    }
    class PadKeys : basekeys
    {
        GamePadState s;
        GamePadCapabilities c;
        public PadKeys()
        {
            c = G.c;
            s = G.pad;
        }
        public override bool block()
        {
            return c.HasRightShoulderButton&&s.IsButtonDown(Buttons.LeftShoulder);
        }

        public override bool Down()
        {
            return c.HasDPadDownButton && s.IsButtonDown(Buttons.DPadDown);
        }

        public override bool Heavy()
        {
            return c.HasYButton && s.IsButtonDown(Buttons.Y); ;
        }

        public override bool Left()
        {
            return c.HasDPadLeftButton && s.IsButtonDown(Buttons.DPadLeft); ;
        }

        public override bool Light()
        {
            return c.HasXButton && s.IsButtonDown(Buttons.X);
        }

        public override bool Right()
        {
            return c.HasDPadRightButton && s.IsButtonDown(Buttons.DPadRight); ;
        }

        public override bool Up()
        {
            return c.HasDPadUpButton && s.IsButtonDown(Buttons.DPadUp);
        }

        public override void whoami(Fighter me)
        {
            
        }
    }
    class Botkeys : basekeys
    {
       bool block1,light,heavy,up,down,left,right;
        Fighter target,me;
        Queue<status> lastmove;
        
        public Botkeys(Fighter t)
        {
            this.target = t;
            block1 = false;
            light = false;
            heavy = false;
            up = false;
            down = false;
            left = false;
            right = false;
            lastmove = new Queue<status>();
            Game1.Event_Update += AI;


        }
        #region keys
        public override bool block()
        {
            return block1;
        }

        public override bool Down()
        {
            return down;
        }

        public override bool Heavy()
        {
            return heavy;
        }

        public override bool Left()
        {
           return left;
        }

        public override bool Light()
        {
            return light;
        }

        public override bool Right()
        {
           return right;
        }

        public override bool Up()
        {
            return up;
        }
        #endregion
        public override void whoami(Fighter me)
        {
            this.me=me;
        }
        public void AI()
        {
            #region ipus
            block1 = false;
            light = false;
            heavy = false;
            up = false;
            down = false;
            left = false;
            right = false;
            #endregion
            Vector2 distance = target.Pos - me.Pos;
            if(target.status!=status.standing)
            {
                lastmove.Enqueue(target.status);
                if(lastmove.Count>=20)
                {
                    lastmove.Dequeue();
                }
            }
            walktowrds(distance);
            if (distance.Length() < G.ScaleCircle(target.a._animation.body[target.a._animation.index], target.a.scale).radius + G.ScaleCircle(Thedict.dic[me.id][status.light].head[1], me.a.scale).radius&&target.status!=status.hurt)
                light = true;
        }
        public void walktowrds(Vector2 distance)
        {
            if (me.fcaingright && (G.ScaleCircle(me.a._animation.body[me.a._animation.index], me.a.scale).radius + G.ScaleCircle(target.a._animation.body[target.a._animation.index], target.a.scale).radius) < distance.Length() /*- 200*/)
            {
                right = true;
            }
            if (!me.fcaingright && (G.ScaleCircle(me.a._animation.body[me.a._animation.index], me.a.scale).radius + G.ScaleCircle(target.a._animation.body[target.a._animation.index], target.a.scale).radius) < distance.Length() /*- 200*/)
            {
                left = true;
            }
        }
    }

}

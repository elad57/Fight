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
            this.block1 = block;
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
        status nextMove;
        int timer = 0;
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
            nextMove = status.standing;
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
            if (target.status != status.standing && target.status != endQ()&&target.status!=status.hurt&&target.a._animation.index<= target.a._animation.frames)
            {
                lastmove.Enqueue(target.status);
                if (lastmove.Count > 20)
                {
                    lastmove.Dequeue();
                }
                
                 
                     choose(nextMove);

                
               
                    nextMove = scanQ(target.status);

            } 
            
    
                
            

            walktowrds(distance);
            

        }
        private void walktowrds(Vector2 distance)
        {
            if (me.fcaingright && (G.ScaleCircle(me.a._animation.body[me.a._animation.index], me.a.scale).radius + G.ScaleCircle(target.a._animation.body[target.a._animation.index], target.a.scale).radius) < distance.Length() - 200)
            {
                right = true;
            }
            if (!me.fcaingright && (G.ScaleCircle(me.a._animation.body[me.a._animation.index], me.a.scale).radius + G.ScaleCircle(target.a._animation.body[target.a._animation.index], target.a.scale).radius) < distance.Length() - 200)
            {
                left = true;
            }
        }
        private status scanQ(status s)
        {
            if(lastmove.Count==0)
            {
                return status.standing;
            }
            Dictionary<status, int> count = new Dictionary<status, int>();
            foreach (status state in Enum.GetValues(typeof(status)))
            {
                count.Add(state, 0);
            }
            status[] temp = new status[100];
            lastmove.CopyTo(temp, 0);
            for (int i = 0; i <temp.Length-1; i++)
            {
                if (temp[i] == s)
                    count[temp[i + 1]]++;
            }
            status maxs=status.standing;
            int maxi=0;
            foreach (status state in Enum.GetValues(typeof(status)))
            {
                if(count[state]>maxi)
                {
                    maxi = count[state];
                    maxs = state;
                }
            }
            return maxs;
        }
        private void choose(status move)
        {
            if (move == status.light)
                block1 = true;
            if (move == status.heavy)
                light = true;
            if (move == status.block)
                heavy = true;
            if (move == status.jumping)
            {
                up = true;
                light = true;
            }
            if (move == status.jumpattack)
                block1 = true;
            
                
        }
        private status endQ()
        {
            status[] temp = new status[100];
            lastmove.CopyTo(temp, 0);
            return temp[temp.Length-1];
        }
      

    }
    class Dumbkeys : basekeys
    {
        bool block1, light, heavy, up, down, left, right;
        status[] statuses;
        int index,timer;

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

        public override void whoami(Fighter me)
        {
            
        }

        public Dumbkeys()
        {
            block1 = false;
            light = false;
            heavy = false;
            up = false;
            down = false;
            left = false;
            right = false;
            statuses = new status[7];
           status[] statusesT = { status.forwordstep,status.backwordstep,status.light,status.heavy,status.block,status.jumping,status.light};

            for (int i = 0; i < statuses.Length; i++)
            {
                statuses[i] = statusesT[i];
            }

            index = 0;
            timer = 0;
            Game1.Event_Update += DumbMove;
        }

        private void DumbMove()
        {
            block1 = false;
            light = false;
            heavy = false;
            up = false;
            down = false;
            left = false;
            right = false;

            if (statuses[index] == status.forwordstep)
                right = true;
            if (statuses[index] == status.backwordstep)
                left = true;
            switch (statuses[index])
            {
                case status.light:
                    light = true;
                    break;
                case status.forwordstep:
                    right = true;
                    break;
                case status.backwordstep:
                    left = true;
                    break;
                case status.heavy:
                    heavy = true;
                    break;
                case status.block:
                    block1 = true;
                    break;
                case status.jumping:
                    up = true;
                    break;
            }
            if (timer >= 30)
            {
                //index = new Random(statuses.Length).Next();
                index++;
                timer = 0;
            }
            if (index >= statuses.Length)
                index = 0;

            timer++;
        }


    }
    }

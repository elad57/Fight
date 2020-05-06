using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fight
{
    enum status { standing, forwordstep, backwordstep, jumping, light, heavy, hurt, block, jumpattack }
    enum Folders { ryu }
    class Fighter : Ifocus
    {
        public Folders id { get; set; }
        //public Dictionary<string,Animation> sprite { get; set; }

        public status status { get; set; }
        private float hp { get; set; }
        public Vector2 speed { get; private set; }
        public basekeys keys { get; set; }
        private Vector2 jumppower { get; set; }
        public Vector2 wallleft { get; set; }
        public Vector2 wallright { get; set; }

        public bool fcaingright { get; set; }
        public Vector2 Pos;
        public Physics physics;

        public AnimationManger a { get; set; }
        animation a1;

        private KeyboardState lastbutton;

        public Fighter(Folders id, basekeys keys, Vector2 pos) : base(pos, 0)
        {

            this.id = id;
            this.status = status.standing;
            this.hp = 100;
            this.keys = keys;
            this.speed = new Vector2(25, 0);
            this.jumppower = new Vector2(0, -250);
            this.Pos = pos;
            this.fcaingright = true;
            this.physics = new Physics(Pos);
            this.a1 = a1;
            this.a = new AnimationManger(Thedict.dic[id][this.status]);
            this.lastbutton = Keyboard.GetState();
            keys.whoami(this);


        }


        public void update()
        {
            if (status == status.hurt && Pos.Y >= 500 && a._animation.index >= a._animation.frames - 1)
                status = status.standing;
            if(status!=status.hurt&&status!=status.jumping&&status!=status.jumpattack)
            {  if (keys.Light())
                {
                    if (status == status.jumping || status == status.jumpattack)
                        jumpattack();
                    else
                        quickattack();
                }
               if (keys.Heavy())
                heavy();
               if (keys.Left())
                walkleft();
               if (keys.Right())
                walkright();
               if (keys.block())
                block();
            }
            

           
            if (status != status.jumping&&status!=status.jumpattack)
            {
                if (keys.Up())
                {
                    if (keys.Left())
                        jumpleft();
                    else
                    {
                        if (keys.Right())
                            jumpright();
                        else
                            jump();
                    }
                }
            }
            
            if(status==status.light)
            {
                if (a._animation.index >= a._animation.frames - 1)
                {
                    status = status.standing;
                }
            }
           
           if(status==status.heavy)
           {
              if (a._animation.index >= a._animation.frames - 1)
              {
                        status = status.standing;
              }
           }
            if (status == status.jumpattack)
            {
                if (a._animation.index >= a._animation.frames - 1)
                {
                    status = status.standing;
                }
            }
            if (status==status.jumping)
            {
                if (keys.Light())
                    jumpattack();
                if (Pos.Y >= 500)
                    status = status.standing;
            }
            if (!keys.Left() && !keys.Right() && !keys.Up() && !keys.Light()
                 && !keys.Heavy() && !keys.block() && status != status.jumping
                 && status != status.hurt && status != status.heavy && status != status.block)
                status = status.standing;


            if (status != status.hurt)
            {
                a.rot = 0;
            }
            else
            {
                if (!fcaingright)
                {
                    if (physics.ma.Y > 0)
                        a.rot =MathHelper.Pi- Math.Atan2(physics.ma.X, physics.ma.Y);
                    else
                        a.rot = 0;
                }
                else
                {
                    if (physics.ma.Y > 0)
                        a.rot = MathHelper.Pi- Math.Atan2(physics.ma.X, physics.ma.Y);
                    else
                        a.rot = 0;
                }
            }
            physics.Pos = Pos;
            physics.update();

            a.pos = physics.Pos;
            Pos = physics.Pos;
            if (keys.block())
                block();
            a.play(Thedict.dic[id][status]);
            a.update(G.gameTime);
            lastbutton = Keyboard.GetState();
        }
        public void walkright()
        {
            if (fcaingright)
                status = status.forwordstep;
            else
                status = status.backwordstep;
            Pos += speed;
        }
        public void walkleft()
        {
            if (!fcaingright)
                status = status.forwordstep;
            else
                status = status.backwordstep;
            Pos -= speed;
        }
        public void jump()
        {
            status = status.jumping;
            physics.calcpower(jumppower);
        }
        public void jumpleft()
        {
            status = status.jumping;
            physics.calcpower(jumppower-new Vector2(5,0));
        }
        public void jumpright()
        {
            status = status.jumping;
            physics.calcpower(jumppower + new Vector2(5, 0));
        }
        public void quickattack()
        {
            if(status==status.light)
            {
                if (a._animation.index >= a._animation.frames - 1)
                {
                    status = status.standing;
                }

            }
            else
            {
                status = status.light;
            }
        }
        public void heavy()
        {
            if (status == status.heavy)
            {
                if (a._animation.index == a._animation.frames - 1)
                {
                    status = status.standing;
                }

            }
            else
            {
                status = status.heavy;
            }
        }
        public void block()
        {
            status = status.block;

        }
        public void jumpattack()
        {
            status = status.jumpattack; 
        }



    }
}

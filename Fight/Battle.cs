using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Fight
{
    class Battle
    {
        public Texture2D background;
        public Fighter f1;
        public Fighter f2;
        public Vector2 wallleft;
        public Vector2 wallright;
        public Camera camera;
        public Vector2 Gravity;


        public Battle(Texture2D background, Fighter f1, Fighter f2)
        {
            this.background = background;
            this.f1 = f1;
            this.f2 = f2;
            

            this.wallright = new Vector2(background.Width);
            this.wallleft = new Vector2(0);
            this.f1.wallleft = wallleft;
            this.f1.wallright = wallright;
            this.f2.wallleft = wallleft;
            this.f2.wallright = wallright;
            Game1.Event_Update += update;
            Game1.Event_Draw += drawbackground;
            Game1.Event_Draw += drawfighters;
        }

        public void drawbackground()
        {
            G.sb.Begin(/*SpriteSortMode.Deferred,null,null,null,null,null,G.cam.Mat*/);
            G.sb.Draw(background, new Vector2(0), Color.White);
            G.sb.End();

        }

        public void drawfighters()
        {
            
            f1.fcaingright = true;
            f2.fcaingright = false;

            if (f1.Pos.X - f2.Pos.X < 0)
            {
                f1.fcaingright = true;
                f2.fcaingright = false;
                f1.a.drawleft();
                f2.a.drawright();
            }
            else
            {
                f2.fcaingright = true;
                f1.fcaingright = false;
                f2.a.drawleft();
                f1.a.drawright();
            }


            // f2.AnimationManger.DrawObject();


        }
        public void update()
        {
            collision();
            f1.update();
            f2.update();

            
            //G.cam.Focus = new Ifocus(new Vector2((f1.Pos.X + f2.Pos.X) / 2, (f1.Pos.Y + f2.Pos.Y) / 2), 0);
            if(f1.Pos.X<wallleft.X)
            {
                f1.Pos.X = wallleft.X;
            }
            if(f1.Pos.X>wallright.X)
            {
                f1.Pos.X = wallright.X;
            }
            if (f2.Pos.X < wallleft.X)
            {
                f2.Pos.X = wallleft.X;
            }
            if (f2.Pos.X > wallright.X)
            {
                f2.Pos.X = wallright.X;
            }
            List<Ifocus> l = new List<Ifocus>();
            l.Add(f1);
            l.Add(f2);
            G.cam.UpdateMat(l) ;


        }

        private void collision()
        {
            if (f1.fcaingright && f1.status==status.forwordstep && (G.ScaleCircle(f1.a._animation.body[f1.a._animation.index],f1.a.scale).radius + G.ScaleCircle(f2.a._animation.body[f2.a._animation.index],f2.a.scale).radius) >= Vector2.Distance(f1.Pos, f2.Pos))
            {
                f2.Pos += f1.speed;
            }
            if (f2.fcaingright && f2.keys.Right() && ((f1.a._animation.body[f1.a._animation.index].radius + f2.a._animation.body[f2.a._animation.index].radius) * f1.a.scale.X >= Vector2.Distance(f1.Pos, f2.Pos)))
            {
                f1.Pos += f2.speed;
            }
            if (!f1.fcaingright && f1.keys.Left() && ((f1.a._animation.body[f1.a._animation.index].radius + f2.a._animation.body[f2.a._animation.index].radius) * f1.a.scale.X >= Vector2.Distance(f1.Pos, f2.Pos)))
            {
                f2.Pos -= f1.speed;
            }
            if (!f2.fcaingright && f2.keys.Left() && ((f1.a._animation.body[f1.a._animation.index].radius + f2.a._animation.body[f2.a._animation.index].radius) * f1.a.scale.X >= Vector2.Distance(f1.Pos, f2.Pos)))
            {
                f1.Pos -= f2.speed;
            }
            if(f1.status==status.light)
            { 
                if(G.ScaleCircle(f1.a._animation.head[f1.a._animation.index],f1.a.scale).radius+ G.ScaleCircle(f2.a._animation.body[f2.a._animation.index],f2.a.scale).radius>= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/)*2)
                {
                    if (f2.status != status.block&&f2.status!=status.hurt)
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(10, -100), f2, 0);
                        else
                            hurtfighter(new Vector2(-10, -100), f2, 0);
                    }
                    else
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(10,0), f2, 0);
                        else
                            hurtfighter(new Vector2(-10, 0), f2, 0);
                    }
                }
            }
            if (f2.status == status.light)
            {
                if (G.ScaleCircle(f2.a._animation.head[f2.a._animation.index], f2.a.scale).radius + G.ScaleCircle(f1.a._animation.head[f1.a._animation.index], f1.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/) * 2)
                {
                    if(f1.status != status.block&&f1.status!=status.hurt)
                    {
                        if (f2.fcaingright)
                            hurtfighter(new Vector2(10, -100), f1, 0);
                        else
                            hurtfighter(new Vector2(-10, -100), f1, 0);
                    }
                    else
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(10, 0), f1, 0);
                        else
                            hurtfighter(new Vector2(-10, 0), f1, 0);
                    }

                }
            }
            if (f1.status == status.heavy)
            {
                if (f1.a._animation.index < f1.a._animation.frames - 2 && f1.a._animation.index > 1)
                {
                    if (G.ScaleCircle(f1.a._animation.head[f1.a._animation.index], f1.a.scale).radius + G.ScaleCircle(f2.a._animation.head[f2.a._animation.index], f2.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/)/1.5f )
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(90, 0), f2, 0);
                        else
                            hurtfighter(new Vector2(-90, 0), f2, 0);

                    }
                }
            }
            if (f2.status == status.heavy)
            {
                if (f2.a._animation.index < f2.a._animation.frames - 2 && f2.a._animation.index > 1)

                {
                    if (G.ScaleCircle(f2.a._animation.body[f2.a._animation.index], f2.a.scale).radius + G.ScaleCircle(f1.a._animation.body[f1.a._animation.index], f1.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/)/1.5f )
                    {
                        if (f2.fcaingright)
                            hurtfighter(new Vector2(60, 0), f1, 0);
                        else
                            hurtfighter(new Vector2(-60, 0), f1, 0);

                    }
                }
            }
            if (f2.status == status.jumpattack)
            {
                if (G.ScaleCircle(f2.a._animation.body[f2.a._animation.index], f2.a.scale).radius + G.ScaleCircle(f1.a._animation.head[f1.a._animation.index], f1.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/) * 2)
                {
                    if (f1.status != status.block)
                    {
                        if (f2.fcaingright)
                            hurtfighter(new Vector2(10, 100), f1, 0);
                        else
                            hurtfighter(new Vector2(-10, 100), f1, 0);
                    }
                    else
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(10, 0), f1, 0);
                        else
                            hurtfighter(new Vector2(-10, 0), f1, 0);
                    }

                }

            }
            if (f1.status == status.jumpattack)
            {
                if (G.ScaleCircle(f1.a._animation.body[f1.a._animation.index], f1.a.scale).radius + G.ScaleCircle(f2.a._animation.body[f2.a._animation.index], f2.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/) * 2)
                {
                    if (f2.status != status.block)
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(10, 100), f2, 0);
                        else
                            hurtfighter(new Vector2(-10, 100), f2, 0);
                    }
                    else
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(10, 0), f2, 0);
                        else
                            hurtfighter(new Vector2(-10, 0), f2, 0);
                    }
                }
            }
            if (f1.fcaingright && f1.status == status.jumping && (G.ScaleCircle(f1.a._animation.body[f1.a._animation.index], f1.a.scale).radius + G.ScaleCircle(f2.a._animation.body[f2.a._animation.index], f2.a.scale).radius) >= Vector2.Distance(f1.Pos, f2.Pos))
            {
                f2.Pos += new Vector2(10, 0);
            }
            if (f2.fcaingright && f2.status==status.jumping && ((f1.a._animation.body[f1.a._animation.index].radius + f2.a._animation.body[f2.a._animation.index].radius) * f1.a.scale.X >= Vector2.Distance(f1.Pos, f2.Pos)))
            {
                f1.Pos += new Vector2(10, 0);
            }
            if (!f1.fcaingright && f1.status==status.jumping && ((f1.a._animation.body[f1.a._animation.index].radius + f2.a._animation.body[f2.a._animation.index].radius) * f1.a.scale.X >= Vector2.Distance(f1.Pos, f2.Pos)))
            {
                f2.Pos -= new Vector2(10, 0);
            }
            if (!f2.fcaingright && f2.status==status.jumping && ((f1.a._animation.body[f1.a._animation.index].radius + f2.a._animation.body[f2.a._animation.index].radius) * f1.a.scale.X >= Vector2.Distance(f1.Pos, f2.Pos)))
            {
                f1.Pos -= new Vector2(10, 0);
            }

        }
        private void hurtfighter(Vector2 attack, Fighter targert,int hitbox)
        {
            targert.status = status.hurt;
             targert.physics.calcpower(attack);
            targert.a.rot=Math.Atan2(-targert.physics.ma.Y,targert.physics.ma.X);
        }



    }
}
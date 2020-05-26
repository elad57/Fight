using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Fight
{
    class Battle :state
    {
        public Texture2D background;
        public Fighter f1;
        public Fighter f2;
        public Vector2 wallleft;
        public Vector2 wallright;
        public Camera camera;
       


        public Battle(Texture2D background, Fighter f1, Fighter f2,Game1 g,ContentManager c,GraphicsDevice graphics)
            :base(c,graphics,g)
        {
            this.background = background;
            this.f1 = f1;
            this.f2 = f2;
            this.wallright = new Vector2(1650);
            this.wallleft = new Vector2(0);
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            drawbackground();
            drawfighters();
        }

        public void drawbackground()
        {
            G.sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, G.cam.Mat);
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

            G.sb.Begin();
            G.sb.DrawString(G.content.Load<SpriteFont>("font"), "num of punches: " + (int)f1.stats.quickattack,
                new Vector2(5, 70), Color.Black);
            G.sb.End();
            G.sb.Begin();
            G.sb.DrawString(G.content.Load<SpriteFont>("font"), "num of kicks: " + (int)f1.stats.kicks,
                new Vector2(5, 90), Color.Black);
            G.sb.End();
            G.sb.Begin();
            G.sb.DrawString(G.content.Load<SpriteFont>("font"), "num of jumpattack: " + (int)f1.stats.jumpattack,
                new Vector2(5, 110), Color.Black);
            G.sb.End();
            G.sb.Begin();
            G.sb.DrawString(G.content.Load<SpriteFont>("font"), "succssesful punches: " + Math.Min((int)Math.Round((f1.stats.succQAttack / f1.stats.quickattack) * 100, 2), 100) +
                "%\nsuccssesful kicks: " + Math.Min((int)Math.Round(f1.stats.succKicks / f1.stats.kicks * 100, 0), 100)
              + "%\nsuccssesful jump attacks: " + Math.Min((int)Math.Round(f1.stats.succJAttack / f1.stats.jumpattack * 100, 0), 100) + "%",
              new Vector2(5, 130), Color.Black);
            G.sb.End();

            G.sb.Begin();
            G.sb.DrawString(G.content.Load<SpriteFont>("font"), "num of punches: " + (int)f2.stats.quickattack,
                new Vector2(G.cam.View.Width - 380, 70), Color.Black);
            G.sb.End();
            G.sb.Begin();
            G.sb.DrawString(G.content.Load<SpriteFont>("font"), "num of kicks: " + (int)f2.stats.kicks,
                new Vector2(G.cam.View.Width-380, 90), Color.Black);
            G.sb.End();
            G.sb.Begin();
            G.sb.DrawString(G.content.Load<SpriteFont>("font"), "num of jumpattack: " + (int)f2.stats.jumpattack,
                new Vector2(G.cam.View.Width - 380, 110), Color.Black);
            G.sb.End();
            G.sb.Begin();
            G.sb.DrawString(G.content.Load<SpriteFont>("font"), "succssesful punches: " + Math.Min((int)Math.Round(f2.stats.succQAttack / f2.stats.quickattack*100, 2),100) +
                "%\nsuccssesful kicks: " + Math.Min((int)Math.Round(f2.stats.succKicks/f2.stats.kicks*100, 0),100)
              + "%\nsuccssesful jump attacks: " + Math.Min((int)Math.Round(f2.stats.succJAttack / f2.stats.jumpattack*100, 0),100) + "%",
              new Vector2(G.cam.View.Width - 380, 130), Color.Black);
           
            G.sb.End();
            G.sb.Begin();
            G.sb.Draw(content.Load<Texture2D>("red"), new Vector2(0), new Rectangle((100 - (int)G.btl.f1.hp) * 5, 0, (int)G.btl.f1.hp * 5, 50), Color.White);
            G.sb.End();
            G.sb.Begin();
            G.sb.Draw(content.Load<Texture2D>("red"), new Vector2(G.cam.View.Width - (int)G.btl.f2.hp * 5, 0), new Rectangle(G.btl.background.Width - (int)G.btl.f2.hp * 5, 0, (int)G.btl.f2.hp * 5, 50), Color.White);

            G.sb.End();



            // f2.AnimationManger.DrawObject();


        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void update()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            reset();
            collision();
            f1.update();
            f2.update();


            //G.cam.Focus = new Ifocus(new Vector2((f1.Pos.X + f2.Pos.X) / 2, (f1.Pos.Y + f2.Pos.Y) / 2), 0);
            if (f1.Pos.X < wallleft.X)
            {
                f1.Pos.X = wallleft.X;
            }
            if (f1.Pos.X > wallright.X)
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
            G.cam.UpdateMat(f1, f2);

            
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
                            hurtfighter(new Vector2(10, -100), f2, 15);
                        else
                            hurtfighter(new Vector2(-10, -100), f2, 15);

                        f1.stats.succQAttack++;
                    }
                    //else
                    //{
                    //    if (f1.fcaingright)
                    //        hurtfighter(new Vector2(10,0), f2, 0);
                    //    else
                    //        hurtfighter(new Vector2(-10, 0), f2, 0);
                    //}
                }
            }
            if (f2.status == status.light)
            {
                if (G.ScaleCircle(f2.a._animation.head[f2.a._animation.index], f2.a.scale).radius + G.ScaleCircle(f1.a._animation.head[f1.a._animation.index], f1.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/) * 2)
                {
                    if(f1.status != status.block&&f1.status!=status.hurt)
                    {
                        if (f2.fcaingright)
                            hurtfighter(new Vector2(10, -100), f1,15);
                        else
                            hurtfighter(new Vector2(-10, -100), f1, 15);
                        f2.stats.succQAttack++;
                    }
                    //else
                    //{
                    //    if (f1.fcaingright)
                    //        hurtfighter(new Vector2(10, 0), f1, 0);
                    //    else
                    //        hurtfighter(new Vector2(-10, 0), f1, 0);
                    //}

                }
            }
            if (f1.status == status.heavy)
            {
                if (f1.a._animation.index < f1.a._animation.frames - 2 && f1.a._animation.index > 1)
                {
                    if (G.ScaleCircle(f1.a._animation.head[f1.a._animation.index], f1.a.scale).radius + G.ScaleCircle(f2.a._animation.head[f2.a._animation.index], f2.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/)/1.5f )
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(90, 0), f2, 20);
                        else
                            hurtfighter(new Vector2(-90, 0), f2, 20);
                        f1.stats.succKicks++;
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
                            hurtfighter(new Vector2(60, 0), f1, 20);
                        else
                            hurtfighter(new Vector2(-60, 0), f1, 20);
                        f2.stats.succKicks++;
                    }
                }
            }
            if (f2.status == status.jumpattack)
            {
                if (G.ScaleCircle(f2.a._animation.body[f2.a._animation.index], f2.a.scale).radius + G.ScaleCircle(f1.a._animation.head[f1.a._animation.index], f1.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/))
                {
                    if (f1.status != status.block)
                    {
                        if (f2.fcaingright)
                            hurtfighter(new Vector2(10, 100), f1, 10);
                        else
                            hurtfighter(new Vector2(-10, 100), f1, 10);
                        f2.stats.succJAttack++;
                    }
                   

                }

            }
            if (f1.status == status.jumpattack)
            {
                if (G.ScaleCircle(f1.a._animation.body[f1.a._animation.index], f1.a.scale).radius + G.ScaleCircle(f2.a._animation.body[f2.a._animation.index], f2.a.scale).radius >= Vector2.Distance(f1.Pos/*+f1.a._animation.head[f1.a._animation.index].center*/, f2.Pos/*+ f2.a._animation.head[f2.a._animation.index].center*/) )
                {
                    if (f2.status != status.block)
                    {
                        if (f1.fcaingright)
                            hurtfighter(new Vector2(10, 100), f2, 10);
                        else
                            hurtfighter(new Vector2(-10, 100), f2, 10);
                        f1.stats.succJAttack++;
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
        private void hurtfighter(Vector2 attack, Fighter targert,float damage)
        {
            targert.status = status.hurt;
             targert.physics.calcpower(attack);
            targert.a.rot=Math.Atan2(-targert.physics.ma.Y,targert.physics.ma.X);
            targert.hp -= damage;
            targert.hp = Math.Max(0, targert.hp);
        }

        private void reset()
        {
            if(f1.hp==0||f2.hp==0)
            {
                this.f1 = new Fighter(Folders.ryu, f1.keys, new Vector2(800, 700),Color.White);
                this.f2 = new Fighter(Folders.ryu, new Botkeys(f1), new Vector2(1200, 700),Color.Yellow);
                game.ChangeState(G.menu);
            }
        }



    }
}
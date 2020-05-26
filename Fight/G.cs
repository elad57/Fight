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
    static class G
    {
        public static SpriteBatch sb;
        public static KeyboardState ks;
        public static GamePadCapabilities c;
        public static GamePadState pad;
        public static GameTime gameTime;
        public static float surface = 3072 * 1234;
        public static string stages = "stages/";
        public static Camera cam;
        public static Battle btl;
        public static PlayerKeys keys1;
        public static PlayerKeys keys2;
        public static Botkeys Botkeys;
        public static Dumbkeys dumbkeys;
        public static OnlineState connectionState;
        public static OnlineGame onlineGame;
        public static ContentManager content;
        public static menu menu;
        public static Viewport viewport;



        public static void init(GraphicsDevice gd, ContentManager Content)
        {

            sb = new SpriteBatch(gd);
            Game1.Event_Update += update;
        }
        public static void update()
        {
            ks = Keyboard.GetState();
            pad = GamePad.GetState(PlayerIndex.One);
        }
        public static float scale(float hieght,float width)
        {
            float x = hieght * width;
            float s = (float)(surface / 100);
            return s / x;
        }
        public static bool hitdetection(Rectangle rec, Circle cir)
        {
            Point a, b, c, d;
            a = rec.Location;
            b = a;
            b.X += rec.Width;
            c = b;
            c.Y += rec.Height;
            d = a;
            d.Y += rec.Height;
            //rectengal points

            if (distance(cir.center, a) <= cir.radius)
                return true;
            if (distance(cir.center, b) <= cir.radius)
                return true;
            if (distance(cir.center, c) <= cir.radius)
                return true;
            if (distance(cir.center, d) <= cir.radius)
                return true;
            return rec.Contains(cir.center);
        }

        private static float distance(Vector2 center, Point p)
        {
            float a = p.X - center.X;
            float b = p.Y - center.Y;

            return (float)Math.Sqrt(a * a + b * b);
        }
        public static Rectangle ScaleRectangle(Rectangle rec,Vector2 Scale,Vector2 pos)
        {
            rec.Location = new Point((int)(rec.Location.X + pos.X), (int)(rec.Location.Y + pos.Y));

            return new Rectangle(rec.Location.X, rec.Location.Y, (int)(rec.Width * Scale.X), (int)(rec.Height * Scale.Y));
        }
        public static Circle ScaleCircle(Circle cir, Vector2 Scale)
        {
            return new Circle(cir.center, cir.radius * Scale.X);
        }
        public static Rectangle fliphitbox(Rectangle frame,Rectangle hitbox)
        {
            int newhit = frame.Width - (hitbox.X + hitbox.Width);
            return new Rectangle(newhit, hitbox.Y, hitbox.Width, hitbox.Height);
        }
        
    }
}

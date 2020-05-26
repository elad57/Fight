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
    class Camera
    {
        #region Data
        public Matrix Mat { get; private set; }
        public List<Ifocus> Focus { get;  set; }
        public float Zoom { get; private set; }
        public Viewport View { get; private set; }
        public Vector2 Pos { get; private set; }
        public ContentManager cm;
        #endregion
        public Camera(Ifocus focus1, Ifocus focus2, float zoom, Viewport view)
        {
            Focus = new List<Ifocus>();
            Focus.Add(focus1);
            Focus.Add(focus2);
            Zoom = zoom;
            View = view;
            Pos = Focus[0].Pos;
        }
        public void UpdateMat(Ifocus f1,Ifocus f2)
        {
            // Focus = vectors;


            // Vector2 sum = new Vector2();
            // float Dis = 0;
            // sum = Focus[0].Pos+Focus[1].Pos;
            // sum /= 2;
            // Pos = Vector2.Lerp(Pos, sum, 0.03f);
            //// Zoom = MathHelper.Clamp(100f / Dis, 0.7f, 1.2f);
            // Mat = Matrix.CreateTranslation(-Pos.X, -Pos.Y, 0)*
            // /*Matrix.CreateScale(Zoom) **/ Matrix.CreateTranslation(View.Width / 2, View.Height / 2, 0);
            
            Vector2 firstcenter = new Vector2(f1.Pos.X, f1.Pos.Y);
            Vector2 seccondcenter = new Vector2(f2.Pos.X, f2.Pos.Y);
            Vector2 sum = (firstcenter + seccondcenter) / 2;
            float Dis = Vector2.Distance(firstcenter,seccondcenter);
            Zoom = MathHelper.Clamp(100f / Dis, 0.7f, 1.2f);
            sum.X += View.Width/2;
            Mat = Matrix.CreateScale(1,1,0) *
                Matrix.CreateTranslation(-sum.X, -sum.Y, 0) * Matrix.CreateTranslation(View.Width / 2, View.Height / 2, 0);


        }
        public void draw()
        {
            G.sb.Begin(SpriteSortMode.Deferred,null,null,null,null,null,Mat);
            G.sb.DrawString(cm.Load<SpriteFont>("font"), "", Pos, Color.Transparent);
            G.sb.End();
        }

       
    }
}

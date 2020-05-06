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
        public void UpdateMat(List<Ifocus> vectors)
        {
            Focus = vectors;
            Vector2 v = Vector2.Lerp(Pos, Focus[0].Pos, 0.03f);
            if (this != null)
            {
                v = Vector2.Lerp(new Vector2(0), new Vector2(0), 0.03f);

            }
            Vector2 sum = new Vector2();
            float Dis = 0;
            sum = v + Focus[0].Pos;
            sum /= 2;
            Dis = Vector2.Distance(v, Focus[0].Pos);
            Pos = Vector2.Lerp(Pos, sum, 0.03f);
            Zoom = MathHelper.Clamp(100f / Dis, 0.7f, 1.2f);
            Mat = Matrix.CreateTranslation(-Pos.X, -Pos.Y, 0)*
            Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(View.Width / 2, View.Height / 2, 0);
        }
        public void draw()
        {
            G.sb.Begin(SpriteSortMode.Deferred,null,null,null,null,null,Mat);
            G.sb.DrawString(cm.Load<SpriteFont>("font"), "", Pos, Color.Transparent);
            G.sb.End();
        }

       
    }
}

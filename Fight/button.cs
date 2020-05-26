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
    class button : Component
    {
        private MouseState currMouse;

        private SpriteFont font;

        private Boolean isHovering;

        private MouseState prevMouse;

        private Texture2D textureN;
        private Texture2D HoverTexture;


        public event EventHandler click;

        public Color PenColour { get; set; }
        public Vector2 Pos { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, textureN.Width, textureN.Height);
            }
        }

        public string text { get; set; }



        public button(Texture2D textureN,Texture2D HoverTexture,SpriteFont font)
        {
            this.textureN = maketransperant(textureN);
            this.HoverTexture = maketransperant(HoverTexture);
            this.font = font;
        }


        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            if(!isHovering)
            {
                sb.Draw(textureN, Rectangle, Color.White);
            }
            else
            {
                sb.Draw(HoverTexture, Rectangle, Color.White);
            }
            sb.End();
            sb.Begin();
            if(!string.IsNullOrEmpty(text))
            {
                var x=(Rectangle.X+(Rectangle.Width/2))-(font.MeasureString(text).X/2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(text).Y / 2);
                sb.DrawString(font, text, new Vector2(x, y), PenColour);
            }
            sb.End();
            
        }

        public override void update(GameTime gameTime)
        {
            prevMouse = currMouse;
            currMouse = Mouse.GetState();
            Vector2 view = new Vector2(1920-G.viewport.Width,1080-G.viewport.Height);
            var mouseRec = new Rectangle(currMouse.X+Rectangle.Width, currMouse.Y+Rectangle.Height+100, 1, 1);

            isHovering = false;

            if(mouseRec.Intersects(Rectangle))
            {
                isHovering = true;

                if(currMouse.LeftButton==ButtonState.Released&&prevMouse.LeftButton==ButtonState.Pressed)
                {
                    click?.Invoke(this, new EventArgs());
                }
            }

        }
        private Texture2D maketransperant(Texture2D texture)
        {
            Texture2D newt=texture;
            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
            Color trans = data[0];
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == trans)
                    data[i] = Color.Transparent;
            }
            newt.SetData<Color>(data);
            return newt;
        }
    }
}

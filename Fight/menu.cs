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
    class menu : state
    {
        List<Component> components;
        public menu(Game1 g,ContentManager content,GraphicsDevice graphics):base(content,graphics,g)
        {
            components = new List<Component>();
            button newgame = new button(content.Load<Texture2D>("blackbutton"),
                content.Load<Texture2D>("redbutton"), content.Load<SpriteFont>("font"));
            {
                newgame.text = "New Game";
                newgame.PenColour = Color.White;
                newgame.Pos=new Vector2((graphic.Viewport.Width / 2)-(newgame.Rectangle.Width/2), (graphic.Viewport.Height / 2) - (newgame.Rectangle.Height /2));
            }
            newgame.click+=newgameclick;
            button exitbutton = new button(content.Load<Texture2D>("blackbutton"),
                content.Load<Texture2D>("redbutton"), content.Load<SpriteFont>("font"));
            {
                exitbutton.text = "Exit";
                exitbutton.PenColour = Color.White;
                exitbutton.Pos = new Vector2(graphic.Viewport.Width / 2 - newgame.Rectangle.Width / 2, graphic.Viewport.Height / 2 - newgame.Rectangle.Height / 2+100);
            }
            exitbutton.click += exitclick;

            
            components.Add(newgame);
            components.Add(exitbutton);
        }
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(content.Load<Texture2D>("backgroundM"), new Vector2(0), Color.White);
            sb.End();
           foreach (Component c in components)
           {
                c.Draw(gameTime,sb);

           }
            
        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
           foreach(Component c in components)
            {
                c.update(gameTime);
            };
        }

        private void newgameclick(object sender,EventArgs args)
        {
            game.ChangeState(G.btl);
        }
        private void exitclick(object sender, EventArgs args)
        {
            game.Exit();
        }
    }
}

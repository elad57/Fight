using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fight
{
    public abstract class state
    {
        protected ContentManager content;
        protected GraphicsDevice graphic;
        protected Game1 game;

        public state(ContentManager content,GraphicsDevice graphics,Game1 game)
        {
            this.content = content;
            this.graphic = graphics;
            this.game = game;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch sb);
        public abstract void PostUpdate(GameTime gameTime);
        public abstract void Update(GameTime gameTime);

    }
}

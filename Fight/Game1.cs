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
    public delegate void DlgUpdate();
    public delegate void Dlgdraw();
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Battle btl;
        SpriteBatch spriteBatch;
        PlayerKeys keys1 = new PlayerKeys(Keys.A, Keys.D, Keys.W, Keys.S,Keys.K,Keys.J,Keys.L);
        PlayerKeys keys2 = new PlayerKeys(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.K, Keys.L, Keys.J);
        PadKeys pad = new PadKeys();
        public static event DlgUpdate Event_Update;
        public static event Dlgdraw Event_Draw;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            G.init(GraphicsDevice, Content);

            Thedict.Init(Content);
            Thedict.dic[Folders.ryu][status.hurt].speed = 0.1f;
            G.c = GamePad.GetCapabilities(PlayerIndex.One);

            Fighter f1 = new Fighter(Folders.ryu, keys1,new Vector2(800,500));
            Fighter f2 = new Fighter(Folders.ryu, new Botkeys(f1), new Vector2(1200,500));
            
         //fighters load
            if (G.c.IsConnected)
            {

                f1 = new Fighter(Folders.ryu,  pad,
                new Vector2(800));
        
            }//gamepad connection
            btl = new Battle(Content.Load<Texture2D>("stages/sumo"), f1, f2); //battle load
            G.btl = btl;
            //G.cam = new Camera(new Ifocus(new Vector2((f1.Pos.X + f2.Pos.X) / 2, (f1.Pos.Y + f2.Pos.Y) / 2),0), new Vector2(5), new Viewport());
            G.cam = new Camera(f1, f2, 1, new Viewport());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            G.gameTime = gameTime;
            Event_Update?.Invoke();
            
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //btl.drawbackground();


            //btl.drawfighters();

            Event_Draw?.Invoke();

            base.Draw(gameTime);
        }
    }
}

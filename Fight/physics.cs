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
    class Physics
    {
        public Vector2 Pos;
        public Vector2 ma { get;  private set; }
        Vector2 Gravity;
        Vector2 Normal;
        Vector2 horipower;
        public Physics(Vector2 Pos)
        {
            this.Pos = Pos;
            this.Gravity = new Vector2(0, 5);
            this.Normal = new Vector2(0, -5);
            this.ma = Gravity + Normal;
            horipower=new Vector2(0);


        }
        public void calcpower(Vector2 power)
        {
            ma = Gravity;
            if (Pos.Y >= 500)
            {
                ma += Normal;
                Pos.Y = 500;
                horipower.X = 0;

            }
            else
            {
                ma += horipower;
            }
            ma += power;
            horipower.X += power.X;
            
            

            

        }
        public void update()
        {
            
            Pos += ma;
            ma = Gravity;
            if (Pos.Y >= 500)
            {
                ma += Normal;
                Pos.Y = 500;
                horipower.X = 0;
            }
            else
            {
                ma += horipower;
                
                
            }


        }

        private void friction()
        {
            float dir=horipower.X/Math.Abs(horipower.X);
            horipower.X -= dir * (20);

            if(dir>0)
                horipower.X = Math.Max(horipower.X, 0);
            else
                horipower.X = Math.Min(horipower.X, 0) ;

        }



    }
}

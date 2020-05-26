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
    class Ifocus
    {
        public Vector2 Pos { get; }
        public float Rot { get; set; }

        public Ifocus(Vector2 pos,float rot)
        {
            this.Pos = pos;
            this.Rot = rot;
        }
    }
    
}

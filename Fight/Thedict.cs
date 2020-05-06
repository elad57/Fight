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
    static class Thedict
    {
        #region DATA


        public static Dictionary<Folders, Dictionary<status, animation>> dic;
        #endregion

        public static void Init(ContentManager cm)
        {
            dic = new Dictionary<Folders, Dictionary<status, animation>>();

            foreach (Folders folder in Enum.GetValues(typeof(Folders)))
            {
                Dictionary<status, animation> temp = new Dictionary<status, animation>();
                foreach (status state in Enum.GetValues(typeof(status)))
                {
                    try
                    {
                        temp.Add(state, new animation(folder, state,cm));
                        if(state==status.standing|| state == status.forwordstep||
                            state == status.backwordstep||state==status.block)
                        {
                            temp[state].isloopable = true;
                        }
                    }
                    catch { }
                }
                dic.Add(folder, temp);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fight
{
    class statistics
    {
        public double quickattack;
        public int succQAttack;
        public double kicks;
        public int succKicks;
        public double desfens;
        public int succDef;
        public double jumpattack;
        public int succJAttack;
        public statistics()
        {
             quickattack=0.000001;
             succQAttack=0;
             kicks=0.0000001;
             succKicks=0;
             desfens=0.000001;
             succDef=0;
            jumpattack = 0.000001;
            succJAttack = 0;
        }
    }
    
}

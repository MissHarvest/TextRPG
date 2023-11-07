using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        int lv = 1;
        public int Lv { get { return lv; } }
        
        string job = "전사";
        public string Class { get { return job; } }

        int atk = 10;
        public int Atk { get { return atk; } }

        int def = 1;
        public int Def { get { return def; } }
        
        int hp;
        public int Hp { get { return hp; } }

        int maxHp = 100;
        public int MaxHp { get { return maxHp; } }

        int gold = 0;
        public int Gold { get { return gold; } }

        public Player()
        {
            hp = maxHp;
        }
    }
}

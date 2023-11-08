using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Item
    {
        enum EStatus { HP, ATK, DEF };
        string[] _statusWord = { " 체력 ", "공격력", "방어력" };

        string _name;
        public string Name { get { return _name; } }

        public bool bEquip;
        EStatus _status;
        public string Status { get { return _statusWord[(int)_status]; } }

        int _val;
        public int Value { get { return _val; } }

        string _description;
        public string Description { get { return _description; } }

        int _price;
        public int Price { get { return _price; } }

        public Item(string name, string status, string description, Int32 price)
        {
            string[] msg = status.Split(',');
            for (int i = 0; i < msg.Length; ++i)
            {
                string[] data = msg[i].Split(':');
                _status = (EStatus)(int.Parse(data[0]));
                _val = int.Parse(data[1]);
            }

            _name = name;
            _description = description;
            _price = price;
        }
    }
}

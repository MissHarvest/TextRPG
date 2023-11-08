using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        int _gold = 100;
        public int Gold { get { return _gold; } }

        List<Item> _inventory = new List<Item>();
        public List<Item> Inventory { get { return _inventory; } }

        public Player()
        {
            hp = maxHp;

            _inventory.Add(new Item("목검", "1:3", "나무로 만들어진 검이다.", 10));
            _inventory.Add(new Item("나무 투구", "2:1", "나무로 만들어진 투구이다.", 5));
            _inventory.Add(new Item("목장갑", "0:10", "흔하게 생긴 장갑이다.", 3));
        }

        public void EquipItem(int index)
        {
            _inventory[index].bEquip = !_inventory[index].bEquip;
            int delta = _inventory[index].bEquip ? 1 : -1;
            switch (_inventory[index].Status)
            {
                case "체력":
                    maxHp += _inventory[index].Value * delta;                    
                    break;

                case "공격력":
                    atk += _inventory[index].Value * delta;
                    break;

                case "방어력":
                    def += _inventory[index].Value * delta;
                    break;
            }
        }

        public void SortInventory()
        {
            _inventory.Sort((x, y) =>
            {
                if (x.Name.Length > y.Name.Length) return -1;
                else if (x.Name.Length == y.Name.Length) return 0;
                else return 1;
            });
        }

        public bool Buy(Item item)
        {
            if (_gold < item.Price) return false;

            _inventory.Add(item);
            _gold -= item.Price;
            return true;
        }

        public void Sell(int index)
        {
            try
            {
                _gold += _inventory[index].Price;
                _inventory.RemoveAt(index);
            }
            catch(Exception e) // 배열 범위 초과
            {

            }
        }
    }
}

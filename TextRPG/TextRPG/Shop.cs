﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Shop
    {
        List<Item> _items = new List<Item>();
        public List<Item> Goods { get { return _items; } }

        public Shop()
        {
            _items.Add(new Item("목검", "1:3", "나무로 만들어진 검이다.", 10));
            _items.Add(new Item("나무 투구", "2:1", "나무로 만들어진 투구이다.", 5));
            _items.Add(new Item("목장갑", "0:10", "흔하게 생긴 장갑이다.", 3));
        }
    }
}

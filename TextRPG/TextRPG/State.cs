using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Scene
    {
        protected List<Scene> _next = new List<Scene>();
        public Scene Next { set { _next.Add(value); } }

        protected Scene _prev;
        public Scene Prev { set { _prev = value; } }

        protected string _name = "";
        public string Name { get { return _name; } }

        protected GameManager.EState _state;
        public GameManager.EState Statee { get { return _state; } }

        protected string[] _choices = { };
        public string[] Option { get { return _choices; } }

        protected string[] _display = { };
        public string[] Display { get { return _display; } }

        virtual public void HandleInput(GameManager game, ConsoleKey key) { }

        virtual public void Update(GameManager game) { }
    }

    class TitleScene : Scene
    {
        public TitleScene()
        {
            _name = "";
            _state = GameManager.EState.Title;
        }

        override public void HandleInput(GameManager game, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    game.ChangeState(_next[0]);
                    break;
            }
        }
    }

    class TownScene : Scene
    {
        public TownScene() 
        {
            _name = "마을";
            _state = GameManager.EState.Town;
            _choices = new string[] { "상태보기", "인벤토리", "상점" };
            // Scene status = new StatusScene(this);
            // GameManager._scene.Add(_name, this);
            // new StatusScene(this) > Add Dictionary
        }

        override public void HandleInput(GameManager game, ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.D0:
                    game.ChangeState(_prev);
                    break;

                case ConsoleKey.D1:
                    game.ChangeState(_next[0]);
                    break;

                case ConsoleKey.D2:
                    game.ChangeState(_next[1]);
                    break;

                case ConsoleKey.D3:
                    game.ChangeState(_next[2]);
                    break;
            }
        }
    }

    class StatusScene : Scene
    {
        public StatusScene()
        {
            _name = "능력치";
            _state = GameManager.EState.Status;
        }

        override public void HandleInput(GameManager game, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D0:
                    game.ChangeState(_prev);
                    break;
            }
        }

        public override void Update(GameManager game)
        {
            base.Update(game);
            Player player = game.Player;
            List<string> lines = new List<string>();

            lines.Add(new string($"Lv. {player.Lv}"));
            lines.Add(new string($"Chad ( {player.Class} )"));
            lines.Add(new string($"공격력 : {player.Atk}"));
            lines.Add(new string($"방어력 : {player.Def}"));
            lines.Add(new string($"체력 : {player.Hp} / {player.MaxHp}"));
            lines.Add(new string($"Gold : {player.Gold}"));

            _display = lines.ToArray();
        }
    }

    class InventoryScene : Scene
    {
        public InventoryScene()
        {
            _name = "능력치";
            _choices = new string[] { "장착관리", "아이템 정렬" };
            _state = GameManager.EState.Inventory;
        }

        public override void HandleInput(GameManager game, ConsoleKey key)
        {
            base.HandleInput(game, key);

            switch(key)
            {
                case ConsoleKey.D0:
                    game.ChangeState(_prev);
                    break;

                case ConsoleKey.D1:
                    game.ChangeState(_next[0]);
                    break;

                case ConsoleKey.D2:
                    game.Player.SortInventory();
                    game.ChangeState(this);
                    break;
            }
        }

        public override void Update(GameManager game)
        {
            base.Update(game);
            Player player = game.Player;

            List<string> lines = new List<string>();

            for (int i = 0; i < player.Inventory.Count; ++i)
            {
                Item item = player.Inventory[i];
                string itemName = item.Name;
                if (item.bEquip) itemName = itemName.Insert(0, "[E]");

                int len = Encoding.Default.GetBytes(item.Name).Length;
                
                int pad = 13 - len / 3 * 2 - len % 3;
                pad = item.bEquip ? pad - 3 : pad;
                string line = $" - {i + 1}.{itemName + "".PadRight(pad)}| {item.Status} + {item.Value,2} |";

                lines.Add(line);
                len = Encoding.Default.GetBytes(item.Description).Length;
                pad = 28 - len / 3 * 2 - len % 3;
                line = $"   ＊{item.Description}";
                lines.Add(line);
            }
            
            _display = lines.ToArray();
        }
    }

    class EquipScene : Scene
    {
        public EquipScene()
        {
            _name = "장착관리";
            _state = GameManager.EState.Equip;
        }

        public override void HandleInput(GameManager game, ConsoleKey key)
        {
            if (key < ConsoleKey.D0 || key >= ConsoleKey.D1 + _choices.Length) return;
            
            switch(key)
            {
                case ConsoleKey.D0:
                    game.ChangeState(_prev);
                    break;

                default:
                    game.Player.EquipItem((int)key - 49);
                    game.ChangeState(this);
                    break;
            }
        }

        public override void Update(GameManager game)
        {
            base.Update(game);
            Player player = game.Player;
            // Set Choice
            SetOption(player);

            // Set Display
            // 일단 인벤토리 목록을 보여준다. > 부위 별로 착용한 아이템을 보여준다?
            SetDisplay(player);
        }

        void SetDisplay(Player player)
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < player.Inventory.Count; ++i)
            {
                Item item = player.Inventory[i];
                string itemName = item.Name;
                if (item.bEquip) itemName = itemName.Insert(0, "[E]");
                
                int len = Encoding.Default.GetBytes(item.Name).Length;
                
                int pad = 13 - len / 3 * 2 - len % 3;
                pad = item.bEquip ? pad - 3 : pad;
                string line = $" - {i + 1}.{itemName + "".PadRight(pad)}| {item.Status} + {item.Value,2} |";
                
                lines.Add(line);
                len = Encoding.Default.GetBytes(item.Description).Length;
                pad = 28 - len / 3 * 2 - len % 3;
                line = $"   ＊{item.Description}";
                lines.Add(line);
            }

            _display = lines.ToArray();
        }

        void SetOption(Player player)
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < player.Inventory.Count; ++i)
            {
                Item item = player.Inventory[i];
                string line = $"{item.Name}";
                if (item.bEquip) line = line.Insert(0, "[E]");
                lines.Add(line);
            }
            _choices = lines.ToArray();
        }
    }

    class ShopScene : Scene
    {
        public Shop shop;
        public ShopScene()
        {
            _name = "상점";
            _choices = new string[] { "구입", "판매" };
            //shop = new Shop(); 잠깐 보류
            _state = GameManager.EState.Shop;
        }

        public override void HandleInput(GameManager game, ConsoleKey key)
        {
            base.HandleInput(game, key);

            // Set Display
            switch (key)
            {
                case ConsoleKey.D0:
                    game.ChangeState(_prev);
                    break;

                case ConsoleKey.D1:
                    game.ChangeState(_next[0]);
                    break;

                case ConsoleKey.D2:
                    game.ChangeState(_next[1]);
                    break;
            }
        }
    }

    class BuyScene : Scene
    {
        public BuyScene()
        {
            _name = "구입";
            // SetDisplay();
            _state = GameManager.EState.Buy;
        }

        public override void HandleInput(GameManager game, ConsoleKey key)
        {
            base.HandleInput(game, key);
            if (key < ConsoleKey.D0 || key >= ConsoleKey.D1 + _choices.Length) return;

            switch (key)
            {
                case ConsoleKey.D0:
                    game.ChangeState(_prev);
                    break;

                default:
                    Item item = game.Shop.Goods[(int)key - 49];
                    game.Player.Buy(item);
                    break;
            }
        }

        public override void Update(GameManager game)
        {
            base.Update(game);

            Shop shop = game.Shop;
            SetDisplay(shop);
            SetOption(shop);
        }

        void SetDisplay(Shop shop)
        {
            List<string> lines = new List<string>();
            int idx = 1;
            foreach (Item item in shop.Goods)
            {
                int len = Encoding.Default.GetBytes(item.Name).Length;
                int pad = 13 - len / 3 * 2 - len % 3;

                string line = $" - {idx++}.{item.Name + "".PadRight(pad)}| {item.Status} + {item.Value,2} |";

                lines.Add(line);
                len = Encoding.Default.GetBytes(item.Description).Length;
                pad = 28 - len / 3 * 2 - len % 3;
                line = $"   ＊{item.Description + "".PadRight(pad)} {item.Price,10} G";
                lines.Add(line);
            }
            _display = lines.ToArray();
        }

        void SetOption(Shop shop)
        {
            List<string> lines = new List<string>();
            foreach (Item item in shop.Goods)
            {
                string line = $"{item.Name}";
                lines.Add(line);
            }
            _choices = lines.ToArray();
        }
    }
    
    class SellScene : Scene
    {
        public SellScene()
        {
            _name = "판매";
            _state = GameManager.EState.Sell;
        }

        public override void HandleInput(GameManager game, ConsoleKey key)
        {
            base.HandleInput(game, key);
            if (key < ConsoleKey.D0 || key >= ConsoleKey.D1 + _choices.Length) return;

            switch (key)
            {
                case ConsoleKey.D0:
                    game.ChangeState(_prev);
                    break;

                default:
                    game.Player.Sell((int)key - 49);
                    game.ChangeState(this);
                    break;
            }
        }

        public override void Update(GameManager game)
        {
            base.Update(game);
            Player player = game.Player;

            SetDisplay(player);
            SetOption(player);
        }

        void SetDisplay(Player player)
        {
            List<string> lines = new List<string>();
            int idx = 1;
            foreach(Item item in player.Inventory)
            {
                int len = Encoding.Default.GetBytes(item.Name).Length;
                int pad = 13 - len / 3 * 2 - len % 3;

                string line = $" - {idx++}.{item.Name + "".PadRight(pad)}| {item.Status} + {item.Value,2} |";

                lines.Add(line);
                len = Encoding.Default.GetBytes(item.Description).Length;
                pad = 28 - len / 3 * 2 - len % 3;
                line = $"   ＊{item.Description + "".PadRight(pad)} {item.Price,10} G";
                lines.Add(line);
            }

            _display = lines.ToArray();
        }

        void SetOption(Player player)
        {
            List<string> lines = new List<string> ();
            foreach(Item item in player.Inventory)
            {
                string line = $"{item.Name}";
                lines.Add(line);
            }
            _choices = lines.ToArray();
        }
    }
}

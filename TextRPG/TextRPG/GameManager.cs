using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameManager
    {
        public enum EState { Title, Town, Status, Inventory, Equip, Sort, Shop, Buy, Sell} // Loading
        
        GameScene scene;        
        Shop shop;

        public Shop Shop { get { return  shop; } }
        public GameScene Scene { set { scene = value; } }
        public bool isPlay = false;

        Player player;

        public Player Player { get { return player; } }
        Scene _state;

        // public ?
        static public Dictionary<string, Scene> _scene = new Dictionary<string, Scene>();

        public GameManager()
        {   
            _scene.Add("Title", new TitleScene()); // v
            _scene.Add("Town", new TownScene()); // v
            _scene.Add("Status", new StatusScene()); 
            _scene.Add("Inventory", new InventoryScene());
            _scene.Add("Equip", new EquipScene());
            _scene.Add("Shop", new ShopScene());
            _scene.Add("Buy", new BuyScene());
            _scene.Add("Sell", new SellScene());

            // v 던전

            _scene["Title"].Next = _scene["Town"];
            
            _scene["Town"].Prev = _scene["Title"];
            _scene["Town"].Next = _scene["Status"];
            _scene["Town"].Next = _scene["Inventory"];
            _scene["Town"].Next = _scene["Shop"];

            _scene["Status"].Prev = _scene["Town"];

            _scene["Inventory"].Prev = _scene["Town"];
            _scene["Inventory"].Next = _scene["Equip"];

            _scene["Equip"].Prev = _scene["Inventory"];

            _scene["Shop"].Prev = _scene["Town"];
            _scene["Shop"].Next = _scene["Buy"];
            _scene["Shop"].Next = _scene["Sell"];

            _scene["Buy"].Prev = _scene["Shop"];
            _scene["Sell"].Prev = _scene["Shop"];

            player = new Player();
            shop = new Shop(); // 추후 제거 
        }

        public void RunGame()
        {
            ChangeState(_scene["Title"]);
            isPlay = true;
        }

        public void GetInput(ConsoleKey key)
        {
            _state.HandleInput(this, key);
            //try
            //{
            //    switch (key)
            //    {
            //        case ConsoleKey.D0:
            //            if (state == EState.Title)
            //            {
            //                isPlay = false;
            //            }
            //            else
            //            {
            //                ChangeState(preState);
            //            }
            //            break;

            //        case ConsoleKey.D1: // try ?
            //            if (state == EState.Equip)
            //            {
            //                player.EquipItem(0);
            //                ChangeState(EState.Equip);
            //            }
            //            else
            //            {
            //                ChangeState(curChoices[0]);
            //            }
            //            break;

            //        case ConsoleKey.D2:
            //            if (state == EState.Equip)
            //            {
            //                player.EquipItem(1);
            //                ChangeState(EState.Equip);
            //            }
            //            else
            //            {
            //                ChangeState(curChoices[1]);
            //            }
            //            break;

            //        case ConsoleKey.D3:
            //            if (state == EState.Equip)
            //            {
            //                player.EquipItem(2);
            //                ChangeState(EState.Equip);
            //            }
            //            else
            //            {
            //                ChangeState(curChoices[2]);
            //            }
            //            break;

            //        case ConsoleKey.D4:
            //            if (state == EState.Equip)
            //            {
            //                player.EquipItem(3);
            //                ChangeState(EState.Equip);
            //            }
            //            else
            //            {
            //                ChangeState(curChoices[3]);
            //            }
            //            break;

            //        case ConsoleKey.D5:
            //            if (state == EState.Equip)
            //            {
            //                player.EquipItem(4);
            //                ChangeState(EState.Equip);
            //            }
            //            else
            //            {
            //                ChangeState(curChoices[4]);
            //            }
            //            break;

            //        case ConsoleKey.Enter:
            //            if (state == EState.Title)
            //            {
            //                ChangeState(EState.Town);
            //            }
            //            break;

            //        default:

            //            break;
            //    }
            //}
            //catch (Exception e)
            //{

            //}
        }

        // ChangeScene . param = string . EState delete
        public void ChangeScene(string sceneName)
        {

        }

        public void ChangeState(Scene state)
        {
            _state = state;
            _state.Update(this); // player 가 매개변수 였음.

            switch (state.Statee)
            {
                case EState.Title:
                    scene.DrawTitleScene();
                    break;

                case EState.Town:
                    scene.Split();
                    scene.DrawMap("마을");
                    scene.ShowSelectList(_state.Option);
                    break;

                case EState.Status:
                    scene.DrawMap("능력치", _state.Display, 5, true);
                    scene.ShowSelectList(_state.Option);
                    break;

                case EState.Inventory:
                    scene.DrawMap("인벤토리", _state.Display, 1, true);
                    scene.ShowSelectList(_state.Option);
                    break;

                case EState.Equip:
                    scene.DrawMap("장착관리", _state.Display, 1, true);
                    scene.ShowSelectList(_state.Option);
                    break;

                case EState.Shop:
                    scene.DrawMap("상점", _state.Display, 1, true);
                    scene.ShowSelectList(_state.Option);
                    break;

                case EState.Buy:
                    scene.DrawMap("구입", _state.Display, 1, true);
                    scene.ShowSelectList(_state.Option);
                    break;

                case EState.Sell:
                    scene.DrawMap("판매", _state.Display, 1, true);
                    scene.ShowSelectList(_state.Option);
                    break;
            }
        }
    }
}

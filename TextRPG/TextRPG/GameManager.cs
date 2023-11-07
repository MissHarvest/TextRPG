using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameManager
    {
        public enum EState { Title, Town, Status, Inventory }
        EState state = EState.Title;
        EState preState = EState.Title;
        
        Dictionary<EState, string[]> choiceStringDic = new Dictionary<EState, string[]>();
        Dictionary<EState, EState[]> choiceStateDic = new Dictionary<EState, EState[]>();
        EState[] curChoices;
        GameScene scene;
        
        public GameScene Scene { set { scene = value; } }
        public bool isPlay = true;
        
        public GameManager()
        {
            choiceStringDic.Add(EState.Town, new string[] { "상태보기", "인벤토리" });
            choiceStateDic.Add(EState.Town, new EState[] {EState.Status, EState.Inventory });

            choiceStringDic.Add(EState.Status, new string[] { "상태보기", "인벤토리" });
            choiceStateDic.Add(EState.Status, new EState[] { EState.Status, EState.Inventory });


            // 인벤토리는 유동적인데 . . . 
            // choiceStringDic.Add(EState.Inventory, new string[] { "상태보기", "인벤토리" });
            // choiceStateDic.Add(EState.Inventory, new EState[] { EState.Status, EState.Inventory });
        }

        public void GetInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D0:
                    if(state == EState.Title)
                    {
                        isPlay = false;
                    }
                    else
                    {
                        ChangeState(preState);
                    }
                    break;

                case ConsoleKey.D1: // try ?
                    ChangeState(curChoices[0]);
                    break;

                case ConsoleKey.Enter:
                    if(state == EState.Title)
                    {
                        ChangeState(EState.Town);
                    }
                    break;

                default:

                    break;
            }
        }

        void ChangeState(EState state)
        {
            preState = this.state;
            this.state = state;

            switch (state)
            {
                case EState.Title:
                    scene.LoadScene(GameScene.EScene.Title);
                    break;

                case EState.Town:
                    scene.ShowSelectList(choiceStringDic[EState.Town]);
                    curChoices = choiceStateDic[EState.Town];
                    break;

                case EState.Status:
                    
                    break;
            }
        }
    }
}

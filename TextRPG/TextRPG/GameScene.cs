﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Point
    {
        int x = 0;
        int y = 0;
        string str = "";

        public Point(int x, int y, string str)
        {
            this.x = x;
            this.y = y;
            this.str = str;
        }

        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(str);
            Console.SetCursorPosition(52, 25);
        }

        public void Draw(string msg)
        {
            str = msg;
            Console.SetCursorPosition(x, y);
            Console.Write(msg);
            Console.SetCursorPosition(52, 25);
        }

        public void Clear()
        {
            byte[] buffer = Encoding.Default.GetBytes(str);
            int length = buffer.Length;

            for(int i = 0; i < length; ++i)
            {
                Console.SetCursorPosition(x + i, y);
                Console.Write(" ");
            }
        }

        public void Delete()
        {
            byte[] buffer = Encoding.Default.GetBytes(str);
            int length = buffer.Length;

            for (int i = 0; i < length; ++i)
            {
                Console.SetCursorPosition(x + i, y);
                Console.Write(" ");
            }
            str = "";
        }
    }

    internal class GameScene
    {
        public enum EScene { Title, Main };
        EScene curScene;

        List<Point> frame;

        Point title;
        Point comment;

        List<Point> boundary;
        Point mapName;
        List<Point> choices = new List<Point>();
        Point exit;

        bool bSplit = false;

        public GameScene()
        {
            curScene = EScene.Title;

            CreateFrame();
            CreateBoundary();
            CreateTitle();
            CreateComment();
            mapName = new Point(1, 1, "[마을]");
            choices.Add(new Point(3, 17, ""));
            choices.Add(new Point(3, 18, ""));
            choices.Add(new Point(3, 19, ""));
            choices.Add(new Point(3, 20, ""));

            exit = new Point(3, 23, "0. 뒤로가기 / 나가기");
        }

        void CreateFrame()
        {
            frame = new List<Point>();
            for (int i = 1; i < 50; ++i)
            {
                frame.Add(new Point(i, 0, "\u2501"));
                frame.Add(new Point(i, 25, "\u2501"));
            }

            for (int i = 1; i < 25; ++i)
            {
                frame.Add(new Point(0, i, "\u2503"));
                frame.Add(new Point(50, i, "\u2503"));
            }

            frame.Add(new Point(0, 0, "\u250F"));
            frame.Add(new Point(50, 0, "\u2513"));
            frame.Add(new Point(0, 25, "\u2517"));
            frame.Add(new Point(50, 25, "\u251B"));
        }

        void CreateBoundary()
        {
            boundary = new List<Point>();
            for(int i = 1; i < 50; ++i)
            {
                boundary.Add(new Point(i, 15, "\u2501"));
            }

            boundary.Add(new Point(0, 15, "\u2520"));
            boundary.Add(new Point(50, 15, "\u252B"));
        }

        void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.Black;
        }

        void CreateTitle()
        {
            title = new Point(10, 10, "Text RPG");
        }

        public void DrawMap(string mapName)
        {
            ClearTopBoundary();
            
            // 커서 위치 설정
            Console.SetCursorPosition(2, 1);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{mapName}]");
            ResetColor();
        }

        /// <summary>
        ///  Draw Top Boundary.
        /// </summary>
        /// <param name="mapName">
        ///     Show in Top Left with [] </param>
        /// <param name="content">
        ///     Top boundary is filled with content </param>
        /// <param name="cursorX">
        ///     Set Cursor's x position to print </param>
        /// <param name="space">
        ///     is True, Add Line between string </param>
        public void DrawMap(string mapName, string[] content, int cursorX = 1, bool space = false)
        {
            ClearTopBoundary();
            int y = 1;
            // 커서 위치 설정
            Console.SetCursorPosition(2, y++);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{mapName}]");
            ResetColor();

            //            
            for (int i = 0; i < content.Length; ++i)
            {
                if (space) y++;
                Console.SetCursorPosition(cursorX, y++);
                Console.Write(content[i]);                
            }
        }


        public void Split()
        {
            Console.Clear();
            DrawFrame();
            DrawBoundary();
        }

        void CreateComment()
        {
            comment = new Point(23, 15, "Press Enter To Start");
        }

        void DrawFrame()
        {
            for (int i = 0; i < frame.Count; ++i)
            {
                frame[i].Draw();
            }
        }

        void DrawBoundary()
        {
            for (int i = 0; i < boundary.Count; ++i)
            {
                boundary[i].Draw();
            }
            bSplit = true;
        }

        public void ShowSelectList(string[] selectList)
        {
            ClearBottomBoundary();

            for (int i = 0; i < selectList.Length; ++i)
            {
                choices[i].Draw((i + 1).ToString() + ". " + selectList[i]);
            }

            exit.Draw();
        }

        void ClearTopBoundary()
        {
            for(int i = 1; i < 50; ++i)
            {
                for(int j = 1; j < 14; ++j)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }
        }

        void ClearBottomBoundary()
        {
            for(int i = 1; i < 50; ++i)
            {
                for(int j = 16; j < 25; ++j)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }                
            }
        }

        public void DrawTitleScene(/* */)
        {
            Console.Clear();
            DrawFrame();
            title.Draw();
            comment.Draw();
        }
    }
}

using System;
using System.Net;


namespace Tetris
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gameSC = new GAMESCREEN();
            
            while (gameSC.RunGame)
            {
                var NewSC = new TETRISSCREEN(10, 20, true);
                var AcSC = new ACCClass(NewSC);
                var newBlock = new Block(NewSC, AcSC);
                int i = 0;
                Console.Clear();
                gameSC.renderScreen();

                while (!newBlock.Dead)
                {
                    newBlock.Move(i);
                    NewSC.Render();
                    for (int r = 0; r < 50000000; r++)
                    {
                        int a = 0;
                    }
                    NewSC.ClearBlock();
                    AcSC.Render();
                    i++;
                }
                NewSC.DeadRender();
                gameSC.GameOver();
            }

        }
    }
}

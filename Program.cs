using System;
using System.Net;


namespace Tetris
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gameScreen = new StaticScreen();
            while (gameScreen.CheckToRun()) // initially set to true, false when user enters 'n' after a game.
            {
                var NewSC = new GameScreen(10, 20, true);
                var AcSC = new StackScreen(NewSC);
                var newBlock = new Block(NewSC, AcSC);
                int i = 0;
                gameScreen.StaticRender();

                while (newBlock.blockAlive)
                {
                    newBlock.Move(i);
                    NewSC.Render();
                    for (int r = 0; r < 200000; r++) // controls speed
                    {
                        int a = 0;
                    }
                    NewSC.ClearBlock();
                    AcSC.Render();
                    i++;
                }
                NewSC.DeadRender();
                gameScreen.GameOver();
            }

        }
    }
}

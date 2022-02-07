using System;
using System.Threading;
using System.Threading.Tasks;

namespace BossFight_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            var player1 = new GameCharacter("Hero", 100, 20, 40);
            var player2 = new GameCharacter("Boss", 400, 20, 10);
            var playerTurn = true;

            while (player1.Alive && player2.Alive)
            {
                if (playerTurn)
                {
                    player1.Fight(player2);
                    playerTurn = false;
                }
                else
                {
                    player2.Fight(player1);
                    playerTurn = true;
                }
                //Thread.Sleep(700); //Utgått måte
                await Task.Delay(700);
            }
            Console.ReadLine();
        }

        
    }
}

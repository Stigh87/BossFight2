using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BossFight_2
{
    internal class GameCharacter
    {
        Random Random = new Random();
        private readonly string _name;
        private int _health;
        private readonly int _strength;
        private int _stamina;
        private readonly int _maxStamina;
        public bool Alive { get; private set; }

        public GameCharacter(string name, int health, int strength, int stamina)
        {
            _name = name;
            _health = health;
            _strength = strength;
            _stamina = stamina;
            _maxStamina = stamina;
            Alive = true;
        }

        public void Fight(GameCharacter opponent)
        {
            var rStrength = Random.Next(0, 30);
            var rCritChance = Random.Next(0, 4);
            var rCritMultiplier = Random.Next(2, 10);
            var critMultiplier = rCritChance == 0 ? rCritMultiplier : 1;
            int accumulatedDmg;
            ConsoleColor consoleColor = _name == "Hero" ? Console.ForegroundColor = ConsoleColor.Blue : Console.ForegroundColor = ConsoleColor.Red;

            if (_stamina <= 0)
            {
                Recharge();
            }
            else
            {

                if (_name == "Hero")
                {
                    accumulatedDmg = _strength * critMultiplier;
                    opponent._health -= accumulatedDmg;
                    _stamina -= 10;
                }

                else
                {
                    critMultiplier = 1;
                    accumulatedDmg = rStrength;
                    opponent._health -= accumulatedDmg;
                    _stamina -= 10;
                }
                Fight(opponent, critMultiplier, accumulatedDmg);
            }
        }

        private void Recharge()
        {
            //Stamina = _name == "Boss" ? 10 : 40;
            _stamina = _maxStamina;
            Console.WriteLine($"{_name} regenerate {_stamina} stamina");
        }

        private void Fight(GameCharacter opponent, int critMultiplier, int accumulatedDmg)
        {
            if (opponent._health < 0)
            {
                PrintOverkill(opponent, accumulatedDmg, critMultiplier);
                PrintWinner();
                opponent.Alive = false;
            }
            else if (opponent._health > 0)
            {
                PrintHit(opponent, accumulatedDmg, critMultiplier);
            }
            else if (opponent._health == 0)
            {
                PrintHit(opponent, accumulatedDmg, critMultiplier);
                PrintWinner();
                opponent.Alive = false;
            }
        }

        private void PrintHit(GameCharacter player, int accumulatedDmg, int critMultiplier = 1)
        {
            if (critMultiplier == 1)
            {
                Console.WriteLine(
                    $"{_name} hit {player._name} for {accumulatedDmg} damage. {player._name} got {player._health} health left.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"{_name} hit {player._name} for {accumulatedDmg} damage with a {critMultiplier}x critical multiplier. {player._name} got {player._health} health left.");
            }
        }

        private void PrintOverkill(GameCharacter player, int accumulatedDmg, int critMultiplier = 1)
        {
            var overkill = player._health * -1;
            if (critMultiplier == 1)
            {
                Console.WriteLine(
                    $"{_name} hit {player._name} for {accumulatedDmg} damage. With an overkill by {overkill}. {player._name} got 0 health left.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"{_name} hit {player._name} for {accumulatedDmg} damage and {critMultiplier}x critical multiplier. With an overkill by {overkill}. {player._name} got 0 health left.");
            }
        }
        private void PrintWinner()
        {
            ConsoleColor consoleColor = _name == "Hero" ? Console.ForegroundColor = ConsoleColor.Blue : Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"{_name} won the fight with {_health} health left!");
        }

    }
}

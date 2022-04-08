using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guessNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Guess number, 6 tries
            int tries = 0;
            bool correct = false;
            int guess = 0;
            Random rnd = new Random();
            int randNum = rnd.Next(1, 101);
            while (tries < 6 && !correct)
            {
                Console.WriteLine("Enter your guess:");
                guess = Convert.ToInt32(Console.ReadLine());
                tries++;
                correct = checkGuess(guess, randNum, tries);
            }
            if (!correct)
            {
                Console.WriteLine($"Unlucky! The number was {randNum}");
            }
            Console.ReadLine();
        }
        public static bool checkGuess(int guess, int randNum, int tries)
        {
            if (guess == randNum)
            {
                Console.WriteLine($"Correct! The number was {randNum}");
                return true;
            }
            if (guess < randNum)
            {
                Console.WriteLine($"Too low! {guess} is not the number, you have {6 - tries} tries remaining");
            }
            else
            {
                Console.WriteLine($"Too high! {guess} is not the number, you have {6 - tries} tries remaining");
            }
            return false;
        }
    }
}

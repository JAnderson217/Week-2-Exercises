using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Generate word to guess
            Console.WriteLine("Enter 1 for Random word or 2 to Enter a word: ");
            string wordToGuess = getWord(Console.ReadLine());
            int lives = 5;
            bool correct = false;
            string secretWord = new String('-', wordToGuess.Length);
            List<char> guesses = new List<char>();
            //while word not guessed and lives > 0, play game
            while (lives > 0 && !correct)
            {
                Console.WriteLine($"Secret word is: {secretWord}");
                Console.WriteLine($"You have {lives} lives remaining");
                guesses.Add(getGuess(guesses));
                Console.Write("Guesses: ");
                foreach(char c in guesses)
                {
                    Console.Write($"{c} ");
                }
                Console.WriteLine();
                secretWord = updateWord(secretWord, wordToGuess, guesses);
                lives = checkGuess(secretWord, guesses, lives);
                correct = checkWon(secretWord, wordToGuess, lives);
            }
            Console.ReadLine();
        }
        
        public static string getWord(string str)
        {
            //Get word to be guessed, either from .txt or word entered
            if (str.Equals("1"))
            {
                //read all lines from dictionary.txt, generate random word
                string[] lines = System.IO.File.ReadAllLines("dictionary.txt");
                Random random = new Random();
                return lines[random.Next(lines.Length)];
            }
            Console.WriteLine("Enter a word:");
            return Console.ReadLine();
        }
        public static char getGuess(List<char> guesses)
        {
            //Return char guess
            char guess = ' ';
            int count = 0;
            while (guess.Equals(' '))
            {
                Console.WriteLine("Please enter a letter to guess: ");
                guess = Console.ReadLine()[0];
                //search duplicate guess
                foreach (char c in guesses)
                {
                    if (c.Equals(guess))
                    {
                        Console.WriteLine("Already guessed!");
                        guess = ' ';
                        count++;
                    }
                }
                if (count==0) {
                    return guess;   
                }
            }
            //to have return type, will not be used
            return guess;
        }

        public static int checkGuess(String str, List<char> guesses, int lives)
        {
            //Check if guess correct, return 
            if (!str.Contains(guesses[guesses.Count - 1]))
            {
                lives--;
            }
            return lives;
        }

        public static string updateWord(string secret, string guess, List<char> guesses)
        {
            //split secret word into array to be compared
            string[] secretArray = new string[secret.Length];
            //compare guesses to word to guess, fill secret word with - and correct chars
            for (int i = 0; i < secret.Length; i++)
            {
                secretArray[i] = secret[i].ToString();
            }
            for (int i = 0; i < secretArray.Length; i++) 
            {
                foreach (char c in guesses)
                {
                    if (guess[i].Equals(c))
                    {
                        secretArray[i] = c.ToString();
                    }
                }
            }
            //return rejoined array into string 
            return String.Join("", secretArray);
        }
        public static bool checkWon(string secretWord, string wordToGuess, int lives)
        {
            //check status of game, if lives = 0, or word guessed then end
            if (secretWord.Equals(wordToGuess))
            {
                Console.WriteLine($"Well done! The word was {wordToGuess}");
                return true;
            }
            else if (lives == 0)
            {
                Console.WriteLine($"Unlucky! You're out of lives, the word was {wordToGuess}");
                return true;
            }
            return false;
        }
    }
}

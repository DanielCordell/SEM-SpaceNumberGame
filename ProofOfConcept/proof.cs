using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            /* e.g.
             * class Level { char[] potentialOperators; int[] numberRange; }
             * Level[] levels = new Level[];\
             * levels[4].potentialOperators; 
             */
            Random rand = new Random();
            int first = rand.Next(0, 10);
            int second = rand.Next(-2, 10);
            while (second == 0) second = rand.Next(-2, 10); // no second == 0 just in case div by 0, actual thing will have better checks.
            char[] operators = new char[] { '+', '-', '/', '*' };
            char op = operators[rand.Next(0, 4)];
            int result = 0;
            switch (op)
            {
                case '+': result = first + second; break;
                case '-': result = first - second; break;
                case '/': result = first / second; break;
                case '*': result = first * second; break;
            }
            int[] others = new int[] { rand.Next(0, 10), rand.Next(0, 10), rand.Next(0, 10), result };
            Console.WriteLine(first + " " + op.ToString() + " " + second + " = ?");
            for (int i = 0; i < 4; ++i)
            {
                Console.WriteLine(others[i]);
            }
            bool guessed = false;
            while (!guessed)
            {
                Console.Write("Enter your guess: ");
                int input = Int32.Parse(Console.ReadLine());
                guessed = input == result;
            }
            Console.WriteLine("You did it!");
        }
    }
}

using System;

namespace StringReducer
{
    class Program
    {
        private static char[] validCharacters = new char[] { 'a', 'b', 'c' };
        private static string printValidCharacters = new string(validCharacters);

        static void Main(string[] args)
        {
            Console.WriteLine("Redutor de Caracteres!");

            string input = getInputSequence();

            while (!string.IsNullOrEmpty(input))
            {
                while (!isValidCharacters(input))
                {
                    Console.WriteLine("\n\n\nDigite uma sequencia de caracteres validos! Caracteres aceitos: " + printValidCharacters + "\n\n\n");
                    input = getInputSequence();
                }

                string output = ReduceIfPossible(input);

                Console.WriteLine("\nResultado da redução: " + output + " tamanho: " + output.Length 
                                 + "\n Sequencia original: " + input + " tamanho: " + input.Length );

                input = getInputSequence();
            }
        }

        private static string getInputSequence()
        {
            Console.WriteLine("\nCaracteres aceitos: " + printValidCharacters);
            Console.WriteLine("Escreva a sequência de caracteres que deseja reduzir OU tecle Enter para sair: ");
            string input = Console.ReadLine();

            return input;
        }

        /// <summary>
        /// Validates if all characters on input are valid
        /// </summary>
        /// <param name="input">string to validate</param>
        /// <returns></returns>
        private static bool isValidCharacters(string input) 
        {
            foreach (char ch in input)
            {
                if (!printValidCharacters.Contains(ch))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Performs string reduction as long as possible
        /// </summary>
        /// <param name="input">string to reduce</param>
        /// <returns></returns>
        private static string ReduceIfPossible(string input)
        {
            int position = 1;
            string surrogate;
            while (CanReduce(input, out position, out surrogate))
            {
                input = input.Remove(position - 1, 2);
                input = input.Insert(position - 1, surrogate);
                Console.WriteLine(input);
            }
            return input;
        }

        /// <summary>
        /// Checks if it is possible to shorten the string
        /// </summary>
        /// <param name="input">string to verify</param>
        /// <param name="position">position to reduce</param>
        /// <param name="characterRemaining">character to insert on reduction</param>
        /// <returns></returns>
        private static bool CanReduce(string input, out int position, out string characterRemaining)
        {
            position = 1;
            characterRemaining = string.Empty;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] != input[i - 1])
                {
                    characterRemaining = getFirstRemainingCharacter(input[i], input[i - 1]);
                    position = i;
                    return true;
                }
            }
            return false;
        }
    

        /// <summary>
        /// Get the first character remaining inside character array validCharacters
        /// </summary>
        /// <param name="charOne"> Character One in use</param>
        /// <param name="charTwo"> Character Two in use</param>
        /// <returns></returns>
        private static string getFirstRemainingCharacter(char charOne, char charTwo)
        {
            string remaining = string.Empty;
            foreach(char ch in validCharacters)
            {
                if(ch != charOne && ch != charTwo)
                {
                    remaining = ch.ToString();
                    break;
                }
            }
            return remaining;
        }

    }
}

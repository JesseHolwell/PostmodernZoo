using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postmodern
{
    class Program
    {
        public static Stopwatch sw = new Stopwatch();

        static void Main(string[] args)
        {
            //INPUT
            sw.Start();

            Console.WriteLine("STARTING");
            Debug.WriteLine("STARTING");

            Random rand = new Random();
            List<int> dollars = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
            List<char> alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
            List<char> freeCharacters = new List<char>();
            Dictionary<string, int> answers = new Dictionary<string, int>();
            KeyValuePair<string, int> adelaide = new KeyValuePair<string, int>("ADELAIDE", 99);
            KeyValuePair<string, int> zoo = new KeyValuePair<string, int>("ZOO", 100);
            Dictionary<string, int> animals = new Dictionary<string, int>
            {
                { "BABOON", 62 },
                { "BILBY", 49 },
                { "CHEETAH", 85},
                { "CIVET", 66},
                { "EMU", 27},
                { "FENIC FOX", 69},
                { "FLAMINGO", 75},
                { "IGUANA", 70},
                { "JAGUAR", 81},
                { "KANGAROO", 106 },
                { "KOALA", 58 },
                { "MEERCAT", 72},
                { "ORYX", 52},
                { "OTTER", 60},
                { "QUOLL", 49},
                { "RED PANDA", 104 },
                { "SEALION", 83 },
                { "WALLABY", 74 }
            };


            //PROCESSING
            bool success = false;

            var totalAttempts = 0;

            Debug.WriteLine("Processing");
            while (!success && sw.ElapsedMilliseconds < 30000)
            {
                Stopwatch successwatch = new Stopwatch();
                successwatch.Start();

                Console.Write("Checking... ");

                freeCharacters = new List<char>(alphabet);
                answers.Clear();
                var check = true;

                Stopwatch answerswatch = new Stopwatch();
                answerswatch.Start();
                Debug.WriteLine(successwatch.ElapsedTicks + "\tanswersstart");

                //continue doing so until all are assigned values
                //for (int i = 0; i < alphabet.Count(); i++)
                while (answers.Count() != dollars.Count())
                {
                    var random = rand.Next(1, 27);

                    //if the random number has not been used yet
                    if (!answers.Values.Contains(random))
                    {
                        //assign a random dollar value to an alphabet character
                        answers.Add(freeCharacters[0].ToString(), random);
                        freeCharacters.Remove(freeCharacters[0]);
                    }
                }

                

                //check against the list of string/value pairs
                //success = Evaluate(answers, animals);
                
                Stopwatch checkcounter = new Stopwatch();
                checkcounter.Start();
                Debug.WriteLine(answerswatch.ElapsedTicks + "\tanswers");
                Debug.WriteLine(successwatch.ElapsedTicks + "\tcheckstart");

                foreach (var animal in animals)
                {
                    var counter = 0;

                    //TODO: do logic
                    foreach (var character in animal.Key)
                    {
                        //get the corresponding answer from the character
                        if (character != ' ')
                        {
                            counter += answers[character.ToString()];
                            //Debug.Write("\nAdding :" + character.ToString() + " " + answers[character.ToString()]);
                        }
                    }

                    //if the numbers add up
                    //Debug.WriteLine("Total: " + counter);
                    //Debug.WriteLine("Check: " + animal.Value);

                    if (counter != animal.Value)
                    {
                        check = false;
                        break;
                    }
                }

                success = check;
                totalAttempts++;
                Debug.WriteLine(checkcounter.ElapsedTicks + "\telapsed");
                Debug.WriteLine(successwatch.ElapsedTicks + "\tsuccess");

                //success = true;

                //NOTE: reshuffle may not be the best way to do it
                //potentially might be better to work through every single possible combination (26!)
            }

            if (success == true)
            {
                //if it does match...
                //OUTPUT
                Console.WriteLine("SUCCESS");
                Console.WriteLine("----------------------------------");
                Debug.WriteLine("SUCCESS");
                //the list of dollar value to alphabet characters list
                foreach (var answer in answers)
                    Console.WriteLine(answer.Key + " | " + answer.Value);

                //calculate the answer that is a separate bit of processing
                var south = "S34 54.";
                var first = "(KxExF+C)";
                var east = "E138 36.";
                var second = "(TxBxE+W)";

                Console.WriteLine("----------------------------------");
                Console.WriteLine(south + first + ", " + east + second);

                //algorithm that converts each alphabet character
                foreach (char c in first)
                    if (answers.Keys.Contains(c.ToString()))
                        first = first.Replace(c.ToString(), answers[c.ToString()].ToString());

                foreach (char c in second)
                    if (answers.Keys.Contains(c.ToString()))
                        second = second.Replace(c.ToString(), answers[c.ToString()].ToString());

                first = first.Replace("x", "*");
                second = second.Replace("x", "*");

                DataTable dt = new DataTable();
                var evalFirst = dt.Compute(first, "");
                var evalSecond = dt.Compute(second, "");

                //output that shit
                Console.WriteLine(south + first + ", " + east + second);
                Console.WriteLine(south + evalFirst + ", " + east + evalSecond);
                Debug.WriteLine(south + first + ", " + east + second);
                Debug.WriteLine(south + evalFirst + ", " + east + evalSecond);

            }
            else
            {
                Console.WriteLine("nothin good...");
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine("ELAPSED TIME: " + sw.Elapsed);
            Debug.WriteLine("ELAPSED TIME: " + sw.Elapsed);

            Console.WriteLine("Total attempts: " + totalAttempts);

            Console.ReadLine();
        }
    }
}

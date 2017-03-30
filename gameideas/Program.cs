using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameideas
{
    class Program
    {

        public static bool _comprehensiveLogging = true;
        public static int _initialPopulationSize = 25;

        /// <summary>
        /// Determines if two parents are capable of producing offspring
        /// </summary>
        /// <param name="parentOne"></param>
        /// <param name="parentTwo"></param>
        /// <returns></returns>
        public static bool CanReproduce(Person parentOne, Person parentTwo)
        {
            return parentOne.Gender != parentTwo.Gender;
        }

        public static Person CreateOffspring(Person parentOne, Person parentTwo)
        {
            if (parentOne.Gender == parentTwo.Gender)
            {
                Console.WriteLine("current parents are not capable of producing offspring");
                throw new Exception("parents are same gender - cannot reproduce");
            }
            Console.WriteLine("Breeding " + parentOne.FirstName + " " + parentOne.LastName + " with " + parentTwo.FirstName + " " + parentTwo.LastName);
            return new Person(parentOne, parentTwo);
        }

        /// <summary>
        /// Displays the population statistics header 
        /// </summary>
        public static void DisplayOutputHeader()
        {
            if (!_comprehensiveLogging)
            {
                Console.WriteLine(String.Format("{0, -3}  {1, -25} {2, -9} {3}",
                        "ID",
                        "Name",
                        "Gender",
                        "Age"));
                Console.WriteLine("----------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine(String.Format("{0, -3}  {1, -25} {2, -9} {3} {4, 7} {5,7} {6,7} {7,7} {8,7}",
                        "ID",
                        "Name",
                        "Gender",
                        "Age",
                        "Int",
                        "Str",
                        "Char",
                        "Appear",
                        "Repro"));
            }
        }

        /// <summary>
        /// Outputs attributes for a single person to the console
        /// </summary>
        /// <param name="personToDisplay"></param>
        public static void WriteUserDetailsToConsole(Person personToDisplay)
        {
            if (_comprehensiveLogging)
            {
                Console.WriteLine(String.Format("{0, -3}  {1, -25} {2, -9} {3} ", //{4, 7} {5,7} {6,7} {7,7} {8,7}",
                    personToDisplay.Id,
                    personToDisplay.FirstName + " " + personToDisplay.LastName,
                    personToDisplay.Gender,
                    personToDisplay.Age
                    //personToDisplay.Intelligence,
                    //personToDisplay.Strength,
                    //personToDisplay.Charisma,
                    //personToDisplay.Appearance,
                    //personToDisplay.Reproducability
                    ));
            }
            else
            {
                Console.WriteLine(String.Format("{0, -3}  {1, -25} {2, -9} {3}",
                    personToDisplay.Id + " ",
                    personToDisplay.FirstName + " " + personToDisplay.LastName,
                    personToDisplay.Gender,
                    personToDisplay.Age));
            }
        }

        #region Population Information


        public static Dictionary<string, double> GetPopulationStatistics(List<Person> population)
        {
            var results = new Dictionary<string, double>();
            var populationCount = Convert.ToDouble(population.Count);
            //gender            
            results.Add("Male", (population.Count(p => p.Gender == Gender.Male) / populationCount) * 100);
            results.Add("Female", (population.Count(p => p.Gender == Gender.Female) / populationCount) * 100);
            //age
            results.Add("0-9", (population.Count(p => p.Age <= 9) / populationCount) * 100);
            results.Add("10-18", (population.Count(p => p.Age > 9 && p.Age <= 18) / populationCount) * 100);
            results.Add("19-55", (population.Count(p => p.Age > 18 && p.Age <= 55) / populationCount) * 100);
            results.Add("55+", (population.Count(p => p.Age > 55) / populationCount) * 100);

            return results;
        }



        #endregion

        static void Main(string[] args)
        {
            int personCounter = 0;
            List<Person> population = new List<Person>();
            Person newPerson;

            DisplayOutputHeader();

            //create initial population
            for (int i = 0; i < _initialPopulationSize; i++)
            {
                System.Threading.Thread.Sleep(10);
                newPerson = new Person(personCounter);
                population.Add(newPerson);
                WriteUserDetailsToConsole(newPerson);

                personCounter++;
            }

            var populationStatistics = GetPopulationStatistics(population);
            Console.WriteLine("POPULATION STATISTICS");
            foreach (KeyValuePair<string, double> property in populationStatistics)
            {
                Console.WriteLine(property.Key + " - " + property.Value + "%");
            }


            var userCommand = Console.ReadLine();
            var parents = userCommand.ToString().Split('+').ToList();
            var parentOne = population.Where(p => p.Id == Convert.ToInt16(parents[0])).FirstOrDefault();
            var parentTwo = population.Where(p => p.Id == Convert.ToInt16(parents[1])).FirstOrDefault();
            if (CanReproduce(parentOne, parentTwo))
            {
                var child = CreateOffspring(parentOne, parentTwo);
            }
        }



    }

}

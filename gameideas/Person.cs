using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameideas
{
    public enum Gender { Male, Female };
    

    class Person
    {
        public List<string> AttributeNames = new List<string> { "Appearance", "Intelligence", "Charisma", "Strength" };

        public string FirstName;
        public string LastName;
        public Gender Gender;
        public int Id;
        public double Age;
        public List<Attribute> Attributes;
        public double Appearance;
        public double Intelligence;
        public double Charisma;
        public double Strength;
        public double Reproducability;

        public Person(int id = 0, double age = -1)
        {

            Id = id;
            Age = age == -1 ? new Random().Next(0, 100) : age;  //if age was not provided, create new random age between 0 and 100

            //todo: characteristics should be influenced by age (person of age 0 should not have high strength/intelligence)
            FirstName = GetFirstName();
            Gender = GetRandomGender();
            System.Threading.Thread.Sleep(10);
            LastName = GetLastName();
            System.Threading.Thread.Sleep(10);
            Attributes = new List<Attribute>();
            Attributes = InitializeAttributes();
            //Appearance = GetRandomAttributeValue(0, 100);
            //System.Threading.Thread.Sleep(10);
            //Intelligence = GetRandomAttributeValue(0, 100);
            //System.Threading.Thread.Sleep(10);
            //Charisma = GetRandomAttributeValue(0, 100);
            //System.Threading.Thread.Sleep(10);
            //Strength = GetRandomAttributeValue(0, 100);
            //Reproducability = ((Appearance * 0.89) + (Charisma * 0.8) + (Strength * 0.6) + (Intelligence * 0.85))/4;
            //Reproducability = (Appearance + Intelligence + Charisma + Strength) / 4;
            Reproducability = (Attributes.Sum(p => p.Value) / Attributes.Count);
        }

        /// <summary>
        /// Constructor for creating a new offspring
        /// </summary>
        /// <param name="parentOne"></param>
        /// <param name="parentTwo"></param>
        public Person(Person parentOne, Person parentTwo)
        {
            Age = 0;
            //Id = ;
            FirstName = GetFirstName();
            LastName = GetLastName();
            Gender = GetRandomGender();

            var random = new Random();
            var multiplier = random.NextDouble();
            //need method for creating combination of trait from parent (include chance of mutation)
            Intelligence = ((parentOne.Intelligence * multiplier) + (parentTwo.Intelligence * (1 - multiplier))) / 2;
            //multiplier = random.NextDouble();
        }

        

        private List<Attribute> InitializeAttributes()
        {
            var attributes = new List<Attribute>();

            foreach (var attribute in AttributeNames)
            {
                var attributeToAdd = new Attribute();
                attributeToAdd.Name = attribute;
                attributeToAdd.Value = GetRandomAttributeValue(0, 100);
                attributes.Add(attributeToAdd);
            }

            return attributes;
        }

        private string GetFirstName()
        {
            var firstNameFile = @"CSV_Database_of_First_Names.csv";
            return GetNameFromFile(firstNameFile);
        }

        private string GetLastName()
        {
            var lastNameFile = @"CSV_Database_of_Last_Names.csv";
            return GetNameFromFile(lastNameFile);
        }

        private Gender GetRandomGender()
        {
            Array enumValues = Enum.GetValues(typeof(Gender));
            return (Gender)enumValues.GetValue(new Random().Next(enumValues.Length));
        }

        private int GetRandomAttributeValue(int min, int max)
        {
            return new Random().Next(min, max);
        }

        /// <summary>
        /// Determines the number of lines in the provided file and returns a random entry from that file
        /// </summary>
        /// <param name="fileName">Name of file from which to return an entry</param>
        /// <returns></returns>
        private string GetNameFromFile(string fileName)
        {
            var lineCount = File.ReadLines(fileName).Count();
            var firstName = File.ReadLines(fileName).Skip(new Random().Next(0, lineCount - 1)).Take(1).First();
            return firstName;
        }

    }
}

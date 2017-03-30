using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameideas
{
    public  enum ClimateName { Desert, Tropical, Arid, Mild, Polar};

    class Geography
    {
        public string Name;
        public ClimateName Type;
        public double Survivability;
        public List<Person> Population;

        public Geography() { }
        
    }
}

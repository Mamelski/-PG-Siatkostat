using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siatkostat
{
    class Player
    {
        public int ID { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int Number { get; private set; }

        public int TeamID { get; private set; }

        public bool IsLibero { get; private set; }
    }
}

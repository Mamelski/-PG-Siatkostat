using System;

namespace Siatkostat.Data.DataModels
{
    public class Player
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Number { get; set; }

        public int TeamId { get; set; }

        public bool IsLibero { get; set; }
    }
}
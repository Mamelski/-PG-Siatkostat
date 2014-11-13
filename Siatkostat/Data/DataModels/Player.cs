using System;

namespace Siatkostat.Data.DataModels
{
    public class Player
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Number { get; set; }

        public string TeamId { get; set; }

        public bool IsLibero { get; set; }

        public string IsLiberoString
        {
            get
            {
                return IsLibero ? "tak" : "nie";
            }
        }
    }
}
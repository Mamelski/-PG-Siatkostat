using System.Collections.Generic;

namespace Siatkostat
{
    public class Log
    {
        private static Log instance;
        private static object thisLock = new object();

        public bool Attack = false;
        public bool serve = false;
        public bool rebound = false;
        public bool block = false;

        public List<string> Messages = new List<string>(); 

        private Log() { }

        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (thisLock)
                    {
                        if(instance == null)
                            instance = new Log();
                    }
                }
                return instance;
            }
        }
    }
}

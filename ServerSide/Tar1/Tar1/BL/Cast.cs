using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Tar1.DAL;

namespace Tar1.BL
{
    public class Cast
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string DateOfBirth { get; set; }
        public string Country { get; set; }
        public string photoURL { get; set; }


        public Cast()
        {
        }

        static public int Insert(Cast c)
        {
            DBService dbs = new DBService();
            return dbs.InsertCast(c);
        }

        static public List<Cast> Read()
        {
            DBService dbs = new DBService();
            return dbs.GetAllCast();

        }
    }
}



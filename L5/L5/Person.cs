using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace L5
{
    public class Person
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronimic { get; set; }

        public int Score { get; set; }
    }
}

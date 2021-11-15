using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace L5
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronimic { get; set; }

        public int Score { get; set; }

        public Command RemoveMe => new Command((person) =>
        {
            if (person is Person)
            {
                App.DataBase.Remove(person as Person).Wait();
            }
            if (person is int)
            {
                App.DataBase.Remove((int)person).Wait();
            }
        });

        public Command UpdateMe => new Command((person) =>
        {
            if (person is Person)
            {
                App.DataBase.UpdatePersonAsync(person as Person).Wait();
            }
        });
    }
}

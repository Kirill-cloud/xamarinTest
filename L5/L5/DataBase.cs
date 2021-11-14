using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L5
{
    public class DataBase
    {
        private readonly SQLiteAsyncConnection connection;
        public DataBase(string connectionString)
        {
            connection = new SQLiteAsyncConnection(connectionString);
            connection.CreateTableAsync<Person>();
        }

        public Task<List<Person>> GetPeopleAsync()
        {
            return connection.Table<Person>().ToListAsync();
        }

        public Task<int> AddPersonAsync(Person person)
        {
            return connection.InsertAsync(person);
        }
    }
}

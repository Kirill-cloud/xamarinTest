using SQLite;
using System;
using System.Collections;
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

        public Task<List<Person>> GetFiltredPeopleAsync()
        {
            return connection.QueryAsync<Person>(
                "SELECT * FROM Person WHERE Score > 200 ");
        }

        internal async Task<IEnumerable> GetLinqFiltredPeopleAsync()
        {
            return await connection.Table<Person>().Where(u => u.Score < 200).ToListAsync();
        }

        internal Task<int> Remove(int id)
        {
            return connection.Table<Person>().DeleteAsync(p => p.Id == id);
        }

        internal Task<int> Remove(Person person)
        {
            return connection.Table<Person>().DeleteAsync(p=> p.Id == person.Id);
        }

        public Task<int> UpdatePersonAsync(Person person)
        {
            return connection.UpdateAsync(person);
        }

        public Task<int> AddPersonAsync(Person person)
        {
            return connection.InsertAsync(person);
        }
    }
}

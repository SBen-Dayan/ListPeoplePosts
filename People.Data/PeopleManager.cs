using System.Data.SqlClient;

namespace People.Data
{
    public class PeopleManager
    {
        private readonly string _connectionString;

        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            using var reader = cmd.ExecuteReader();

            List<Person> people = new();
            while(reader.Read())
            {
                people.Add(new()
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }

        public void AddPeople(List<Person> people)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = BuildCommandText(people);
            for(int i = 0; i < people.Count; i++)
            {
                var current = people[i];
                cmd.Parameters.AddWithValue($"@firstName{i}", current.FirstName);
                cmd.Parameters.AddWithValue($"@lastName{i}", current.LastName);
                cmd.Parameters.AddWithValue($"@age{i}", current.Age);
            }
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        private static string BuildCommandText(List<Person> people)
        {
            string sql = "INSERT INTO People Values ";
            for(var i = 0; i < people.Count; i++)
            {
                sql += $"(@firstName{i}, @lastName{i}, @age{i})";
                if(i < people.Count - 1)
                {
                    sql += ", ";
                }
            }
            return sql;
        }
    }
}

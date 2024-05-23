using System.Data.SqlClient;
namespace Tassk2.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
    {
        private readonly string _connectionString;
        public GenericRepository(string c)
        {
            _connectionString = c;
        }

        public void Add(TEntity entity)
        {

            var tableName = typeof(TEntity).Name;

            var properties =
                    typeof(TEntity).GetProperties().Where(p => p.Name != "Id");
            var columnNames =
                string.Join(",", properties.Select(x => x.Name));
            var parameterNames =
                string.Join(",", properties.Select(y => "@" + y.Name));

            var query = $"insert into  {tableName} ({columnNames}) values({parameterNames})";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                foreach (var property in properties)
                {
                    command.Parameters.AddWithValue("@" + property.Name,
                        property.GetValue(entity));
                }
                command.ExecuteNonQuery();

            }

        }
        public void DeleteById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM " + typeof(TEntity).Name + " WHERE Id = " + id;
                command.ExecuteNonQuery();
            }
        }
        public TEntity? FindById(int id)
        {
            TEntity? entity = default;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + typeof(TEntity).Name + " WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entity = (TEntity)Activator.CreateInstance(typeof(TEntity)) ?? default;
                            var properties = typeof(TEntity).GetProperties();
                            foreach (var property in properties)
                            {
                                var value = reader[property.Name];
                                property.SetValue(entity, value is DBNull ? null : value);
                            }
                        }
                    }
                }
            }
            return entity;
        }

    }
}

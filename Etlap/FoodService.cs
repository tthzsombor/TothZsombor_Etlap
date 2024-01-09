using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF_Etlap
{
    public class FoodService
    {
        private MySqlConnection connection;
        public FoodService()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "etlapdb";
            this.connection = new MySqlConnection(builder.ConnectionString);
        }

        public List<Food> GetAll()
        {
            List<Food> foods = new List<Food>();

            OpenConnection();
            string sql = "SELECT * FROM etlap";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Food food = new Food();
                    food.Id = reader.GetInt32("id");
                    food.Name = reader.GetString("nev");
                    food.Description = reader.GetString("leiras");
                    food.Price = reader.GetInt32("ar");
                    food.Category = reader.GetString("kategoria");
                    foods.Add(food);
                }
            }
            CloseConnection();

            return foods;
        }

        public bool Create(Food newFood)
        {
            OpenConnection();
            string sql = "INSERT INTO etlap (nev, leiras, ar, kategoria) VALUES (@name, @description, @price, @category)";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@name", newFood.Name);
            command.Parameters.AddWithValue("@description", newFood.Description);
            command.Parameters.AddWithValue("@price", newFood.Price);
            command.Parameters.AddWithValue("@category", newFood.Category);

            int rowsAffected = command.ExecuteNonQuery();

            CloseConnection();

            return rowsAffected == 1;
        }

        public bool UpdateByPercent(int id, double percent, Food foodToUpdate)
        {
            OpenConnection();
            string sql = "UPDATE etlap SET ar = @price + (@price * (@percent / 100)) WHERE id = @id";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@price", foodToUpdate.Price);
            command.Parameters.AddWithValue("@percent", percent);
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();

            CloseConnection();

            return rowsAffected == 1;
        }

        public bool UpdateByForint(int id, int forint, Food foodToUpdate)
        {
            OpenConnection();
            string sql = "UPDATE etlap SET ar = @price + @forint WHERE id = @id";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@price", foodToUpdate.Price);
            command.Parameters.AddWithValue("@forint", forint);
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();

            CloseConnection();

            return rowsAffected == 1;
        }

        public bool UpdateByPercent(double percent)
        {
            OpenConnection();
            string sql = "UPDATE etlap SET ar = ar + (ar * (@percent / 100))";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@percent", percent);

            int rowsAffected = command.ExecuteNonQuery();

            CloseConnection();

            return rowsAffected == 1;
        }

        public bool UpdateByForint(int forint)
        {
            OpenConnection();
            string sql = "UPDATE etlap SET ar = ar + @forint";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@forint", forint);

            int rowsAffected = command.ExecuteNonQuery();

            CloseConnection();

            return rowsAffected == 1;
        }

        public bool Delete(int id)
        {
            OpenConnection();
            string sql = "DELETE FROM etlap WHERE id = @id";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();

            CloseConnection();

            return rowsAffected == 1;
        }

        private void OpenConnection()
        {
            if (this.connection.State == System.Data.ConnectionState.Closed)
            {
                this.connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (this.connection.State == System.Data.ConnectionState.Open)
            {
                this.connection.Close();
            }
        }
    }
}

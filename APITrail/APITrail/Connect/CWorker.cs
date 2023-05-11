using APITrail;
using Npgsql;
using System.Drawing;
using System.Net;
using System.Xml.Linq;
using TodoApi.Models;

namespace APITrail.Connect
{ 
    public static class CWorker
    {
       
        public static string AddWorker(Worker worker)
        {
            bool idExists = CheckIfIdExistsWorker(worker.Id);
            if (!idExists)
            {
                NpgsqlConnection con;
                using (con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("Connected");
                        string insertQuery = "INSERT INTO public.\"worker\" (w_id, \"Name\", address, \"BirthDate\", \"Telephone\", \"Pelephone\") " +
                                    "VALUES (@workerId, @name, @address, @birthDate, @telephone, @pelephone)";
                        using (var command = new NpgsqlCommand(insertQuery, con))
                        {
                            command.Parameters.AddWithValue("workerId", worker.Id);
                            command.Parameters.AddWithValue("name", worker.W_Name);
                            command.Parameters.AddWithValue("address", worker.Address ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("birthDate", worker.Birth_Date ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("telephone", worker.Telephone ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("pelephone", worker.Pelephone ?? (object)DBNull.Value);

                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine($"{rowsAffected} row(s) inserted.");
                            return "ok";
                        }

                    }
                    else
                    {
                        return "Can't connect to Database";


                    }
                }               

            }
            else
            {
                return "Id aleady exist";
         
            }
        }

        public static bool CheckIfIdExistsWorker( int id)
        {
            NpgsqlConnection con;
            using (con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connected");
                    string selectQuery = "SELECT COUNT(*) FROM public.Worker WHERE w_id = @id";

                    using (var command = new NpgsqlCommand(selectQuery, con))
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.CommandType = System.Data.CommandType.Text;
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0;
                    }
                }
                else
                {
                    Console.WriteLine("Can't connect to Database");
                    return false;

                }
            }
          
        }

        public static void Check()
        {
            NpgsqlConnection con;
            using (con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string schemaName = "public"; // Replace with your schema name
                    string query = $"SELECT table_name FROM information_schema.tables WHERE table_schema = '{schemaName}' AND table_type = 'BASE TABLE' AND has_table_privilege(table_schema || '.' || table_name, 'SELECT');";

                    using (var command = new NpgsqlCommand(query,con ))
                    {
                        Console.WriteLine(command);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tableName = reader.GetString(0);
                                Console.WriteLine(tableName);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Can't connect to Database");

                }
            }


        }
        public static Worker GetWorkerById(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();

                string query = $"SELECT * FROM public.worker WHERE w_id = @Id;";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Worker worker = new Worker
                            {
                                Id = reader.GetInt32(0),
                                W_Name = reader.GetString(1),
                                Address = reader.GetString(2),
                                Telephone = reader.GetInt32(3),
                                Pelephone = reader.GetInt32(4)
                            };

                            return worker;
                        }
                    }
                }
            }

            return null; // Return null if worker with the specified ID is not found
        }

    }
}

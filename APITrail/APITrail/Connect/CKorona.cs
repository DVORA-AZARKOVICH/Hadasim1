
using APITrail.Connect;
using Npgsql;
using TodoApi.Models;
namespace APITrail.Conect
{
    public static class CKorona
    {
        public static string AddKorona(Korona korona)
        {
            bool idExistsK = CheckIfIdExistsInKorona(korona.W_Id);
            bool idExistsW = CWorker.CheckIfIdExistsWorker(korona.W_Id);
            if (!idExistsK && idExistsW)
            {
                NpgsqlConnection con;
                using (con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("Connected");
                        string insertQuery = "INSERT INTO public.\"korona\" (w_id, \"positive\", \"negative\") " +
                                    "VALUES (@workerId, @positive, @negative)";
                        using (var command = new NpgsqlCommand(insertQuery, con))
                        {
                            command.Parameters.AddWithValue("workerId", korona.W_Id);
                            command.Parameters.AddWithValue("negative", korona.Positive);
                            command.Parameters.AddWithValue("positive", korona.Negative);

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
                if (idExistsK)
                    return "Id aleady exist";
                else
                    return "Id worker doesn't exist";

            }
        }

        public static bool CheckIfIdExistsInKorona(int id)
        {
            NpgsqlConnection con;
            using (con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connected");
                    string selectQuery = "SELECT COUNT(*) FROM public.korona WHERE w_id = @id";

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
        public static Korona GetKoronaById(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();

                string query = $"SELECT * FROM public.korona WHERE w_id = @Id;";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Korona korona = new Korona
                            {
                             
                                Positive = reader.GetDateTime(0),
                                Negative = reader.GetDateTime(1),
                                Id = reader.GetInt32(2),
                                W_Id = reader.GetInt32(3)

                            };

                            return korona;
                        }
                    }
                }
            }

            return null; // Return null if worker with the specified ID is not found
        }

    }
}

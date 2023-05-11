using APITrail.Connect;
using Npgsql;
using System.Data;
using TodoApi.Models;
namespace APITrail.Conect
{
    public static class CVaccination
    {

        public static string AddVaccination(Vaccination vaccination)
        {
            int idExistsV = CheckIfIdExistsInVaccination(vaccination.W_Id);
            bool idExistsW = CWorker.CheckIfIdExistsWorker(vaccination.W_Id);
            if (idExistsV<4 && idExistsW)
            {
                NpgsqlConnection con;
                using (con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("Connected");
                        string insertQuery = "INSERT INTO public.\"vaccination\" (w_id, \"moed\", \"creator\") " +
                                    "VALUES (@workerId, @moed, @creator)";
                        using (var command = new NpgsqlCommand(insertQuery, con))
                        {
                            command.Parameters.AddWithValue("workerId", vaccination.W_Id);
                            command.Parameters.AddWithValue("moed", vaccination.Moed);
                            command.Parameters.AddWithValue("creator", vaccination.Creator);

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
                if(!idExistsW)
                    return "Id Worker does not exist";
                else
                {
                    if(idExistsV==-1)
                    {
                        return "Can't connect to Database";
                    }
                    else
                    {
                        return "There are already 4 vaccination exist for this Id";
                    }
                    
                }

            }
        }
        public static int CheckIfIdExistsInVaccination(int id)
        {
            NpgsqlConnection con;
            using (con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connected");
                    string selectQuery = "SELECT COUNT(*) FROM public.vaccination WHERE w_id = @id";

                    using (var command = new NpgsqlCommand(selectQuery, con))
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.CommandType = System.Data.CommandType.Text;
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count;
                    }
                }
                else
                {
                    Console.WriteLine("Can't connect to Database");
                    return -1;

                }
            }

        }
        public static List<Vaccination> GetVaccinationsById(int id)
        {
            List<Vaccination> vaccinations = new List<Vaccination>();
            using (NpgsqlConnection con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();

                string query = $"SELECT * FROM public.vaccination WHERE w_id = @Id;";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Vaccination vaccination = new Vaccination
                            {
                                Id = reader.GetInt32(0),
                                W_Id = reader.GetInt32(1),
                                Moed = reader.GetDateTime(2),
                                Creator = reader.GetString(3)
                            };

                            vaccinations.Add(vaccination);
                        }
                    }
                }
            }

            return vaccinations; // Return null if vaccination with the specified ID is not found
        }

        public static Vaccination GetVaccinationById(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();

                string query = $"SELECT * FROM public.vaccination WHERE v_id = @Id;";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Vaccination vaccination = new Vaccination
                            {
                                Id = reader.GetInt32(0),
                                W_Id = reader.GetInt32(1),
                                Moed = reader.GetDateTime(2),
                                Creator = reader.GetString(3)
                            };

                            return vaccination;
                        }
                    }
                }
            }

            return null; // Return null if vaccination with the specified ID is not found
        }

        public static Vaccination GetVaccinationByIdWorker(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(@"server=localhost;port=5432;User Id=postgres;Password=d17520026a;Database=dev;Timeout = 1000;"))
            {
                con.Open();

                string query = $"SELECT * FROM public.vaccination WHERE w_id = @Id;";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Vaccination vaccination = new Vaccination
                            {
                                Id = reader.GetInt32(0),
                                W_Id = reader.GetInt32(1),
                                Moed = reader.GetDateTime(2),
                                Creator = reader.GetString(3)
                            };

                            return vaccination;
                        }
                    }
                }
            }

            return null; // Return null if vaccination with the specified ID is not found
        }

    }

}

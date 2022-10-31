using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using simpleCRUD.data;

namespace simpleCRUD.Models
{
    public class Publisher
    {
        private int id;
        private string name = "";

        public int ID 
        { 
            get { return id; }
            set { id = value ; }
        }
        public string Name 
        { 
            get { return name; }
            set { name = value; }
        }
        // public string Location { get; set; }

        public int AddNewPublisher(string name)
        {
            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();                
                query.CommandText  =
                    $"INSERT INTO publisher (Name) VALUES (@Name)";

                query.Parameters.AddWithValue("@Name", name);
                
                return query.ExecuteNonQuery();
                
            }   
        }

        public Publisher GetPublisher(string name)
        {
            Publisher publisher = new Publisher();

            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText       = $"SELECT Name FROM publisher WHERE Name = @Name";
                    
                    query.Parameters.AddWithValue("@Name", name);

                    var reader              = query.ExecuteReader();    

                    if (!reader.HasRows) return null;

                    while (reader.Read())
                    {
                        publisher.Name = reader.GetString(0);                    
                    }
                }

                connection.Close();         
            }                        

            return publisher;
        }

        public List<Publisher> GetAllPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();

            using (var connection = DataBase.DataBaseConnector())
            {   
                connection.Open();     

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText       = "SELECT * FROM publisher";                
                    MySqlDataReader reader  = query.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("There are no publishers...\n");

                        return null;
                    }

                    while (reader.Read())
                    {
                        Publisher publisher = new Publisher();
                        publisher.id        = reader.GetInt32(0);
                        publisher.name      = reader.GetString(1);                                                

                        publishers.Add(publisher);
                    }
                }
                
                connection.Close();           
            }   

            return publishers;
        }

        public int UpdatePublisher(int id, string value)
        {
            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText = 
                        @"UPDATE publisher SET Name = @Name WHERE ID = @ID;";

                    query.Parameters.AddWithValue("@Name", value);
                    query.Parameters.AddWithValue("@ID", id);

                    return query.ExecuteNonQuery();
                }
            }
        }

        public int DeletePublisher(int id)
        {
            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText = 
                        @"UPDATE FROM publisher WHERE ID = @ID;";
                    
                    query.Parameters.AddWithValue("@ID", id);

                    return query.ExecuteNonQuery();
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using simpleCRUD.data;

namespace simpleCRUD.Models
{
    public class Author
    {
        private int id;
        private string name = "";

        public int ID         
        { 
            get { return id; }
            set { id = value; }
        }
        public string Name 
        { 
            get { return name; }
            set { this.name = value; }
        }

        public void AddNewAuthor(string name)
        {
            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText  =
                        @"INSERT INTO author (Name) VALUES (@Name)";

                    query.Parameters.AddWithValue("@Name", name);
                    
                    if (query.ExecuteNonQuery() > 0)
                        Console.WriteLine("\nAuthor add to the shelf...");
                    else
                        Console.WriteLine("\nWe've got a problem...");
                }                
            }  
        }

        public Author GetAuthor(string name)
        {
            Author author = new Author();

            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText = 
                        @"SELECT ID, Name FROM author WHERE Name = @Name";

                    query.Parameters.AddWithValue("@Name", name);
                    
                    var reader = query.ExecuteReader();    

                    if (!reader.HasRows) return null;

                    while (reader.Read())
                    {
                        author.id   = reader.GetInt32(0);
                        author.Name = reader.GetString(1);                    
                    }
                }

                connection.Close();         
            }                        

            return author;
        }

        public List<Author> GetAllAuthors()
        {
            List<Author> authors = new List<Author>();

            using (var connection = DataBase.DataBaseConnector())
            {   
                connection.Open();     

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText       = "@SELECT * FROM author";
                    MySqlDataReader reader  = query.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("There are no Authors...\n");

                        return null;
                    }

                    Author author = new Author();
                        
                    while (reader.Read())
                    {
                        author.id   = reader.GetInt32(0);
                        author.name = reader.GetString(1);                        

                        authors.Add(author);
                    }
                }
                
                connection.Close();           
            }   

            return authors;        
        }
    }
}
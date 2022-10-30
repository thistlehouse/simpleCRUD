using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using simpleCRUD.data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace simpleCRUD.Models
{
    public class Book 
    {
        private int id;
        private string title = "";
        private bool isBestSeller;        
        public Author author        = new Author();
        public Publisher publisher  = new Publisher();        

        public int ID 
        { 
            get { return id ; }
            set { id = value; }         
        }
        public string Title 
        { 
            get { return title; }
            set { title = value; }
        }
        public bool IsBestSeller 
        { 
            get { return isBestSeller; } 
            set { isBestSeller = value; }
        }

        public int AddNewBook(Book book, string authorName, string publisherName)
        {            
            Author author       = new Author();
            Publisher publisher = new Publisher();

            author.AddNewAuthor(authorName);
            publisher.AddNewPublisher(publisherName);

            var authorId     = author.GetAuthor(authorName);
            var publisherId  = publisher.GetPublisher(publisherName);

            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText =
                        @"INSERT INTO (Title, IsBestSeller, AuthorID, PublisherID)"
                        + "VALUES (@Title, @IsBestSeller, @AuthorID, @PublisherID);";

                    query.Parameters.AddWithValue("@Title", book.Title);
                    query.Parameters.AddWithValue("@IsBestSeller", book.IsBestSeller);
                    query.Parameters.AddWithValue("@AuthorID", authorId);
                    query.Parameters.AddWithValue("@PublisherID", publisherId);

                    return query.ExecuteNonQuery();
                }
                
            }
        }

        public Book GetBook(string title)
        {
            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText =
                        @"SELECT book.ID, book.Title, book.IsBestSeller, author.Name as Author, publisher.Name as Publisher "
                        + "FROM book "
                        + "INNER JOIN author "
                        + "ON book.AuthorID = author.ID "
                        + "INNER JOIN publisher "
                        + "ON book.PublisherID = publisher.ID "
                        + "WHERE Title = @Title;";
                    
                    query.Parameters.AddWithValue("@Title", title);

                    MySqlDataReader reader  = query.ExecuteReader();
                    Book book               = new Book();

                    while (reader.Read())
                    {
                        book.id             = (int)reader["ID"];
                        book.title          = reader["Title"].ToString();
                        book.author.Name    = reader["Author"].ToString();
                        book.publisher.Name = reader["Publisher"].ToString();
                    }

                    return book;
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText =
                        @"SELECT book.ID, book.Title, book.IsBestSeller, author.Name as Author, publisher.Name as Publisher "
                        + "FROM book "
                        + "INNER JOIN author "
                        + "ON book.AuthorID = author.ID "
                        + "INNER JOIN publisher "
                        + "ON book.PublisherID = publisher.ID;";

                        MySqlDataReader reader  = query.ExecuteReader();
                        List<Book> books        = new List<Book>();                        

                        while (reader.Read())
                        {
                            Book book           = new Book();                            
                            book.id             = (int)reader["ID"];
                            book.Title          = reader["Title"].ToString();
                            book.isBestSeller   = Convert.ToBoolean(reader["IsBestSeller"]);
                            book.author.Name    = reader["Author"].ToString();
                            book.publisher.Name = reader["Publisher"].ToString();

                            books.Add(book);
                        }

                    return books;  
                }
            }                 
        }

        public int UpdateBook(int id, string value)
        {
            using (var connection = DataBase.DataBaseConnector())
            {
                connection.Open();

                using (MySqlCommand query = connection.CreateCommand())
                {
                    query.CommandText = 
                        @"UPDATE book SET Title = @Title WHERE ID = @ID;";

                    query.Parameters.AddWithValue("@Title", value);
                    query.Parameters.AddWithValue("@ID", id);

                    return query.ExecuteNonQuery();
                }
            }
        }
    }
}
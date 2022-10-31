using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using simpleCRUD.Models;

namespace simpleCRUD
{
    public class Menu
    {
        public void DisplayMenu()
        {
            Console.WriteLine("\n\t\t========= Fictitious Library =========\n");
            Console.WriteLine("1. Add");            
            Console.WriteLine("2. List");                        
            Console.WriteLine("3. Search");            
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete.");
            Console.Write("> ");
            
            DisplaySubMenu(ReadMenuOption());            
        }

        public int ReadMenuOption()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        public void DisplaySubMenu(int option)
        {
            switch(option)
            {
                case 1:
                {
                    AddSubMenu();          
                    
                    break;
                }

                case 2:
                {
                    ListSubMenu();
                   
                    break;
                }

                case 3:
                {
                    break;
                }

                case 4:
                {
                    UpdateSubMenu();
                    
                    break;
                }

                case 5:
                {
                    DeleteSubMenu();

                    break;
                }

                default:
                    break;
            }
        }

        public void ReturnBackMessage()
        {
            Console.WriteLine("\nReturn to main menu type > 0");
                    
            if (Convert.ToInt32(Console.ReadLine()) == 0)
                this.DisplayMenu();
        }

        public void AddSubMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Add new Book");
            Console.WriteLine("2. Add new Author");
            Console.WriteLine("3. Add new Publisher");
            Console.Write("> ");

            int opt = ReadMenuOption();

            switch (opt)
            {
                case 1:
                {
                    Console.Clear();
                    Console.WriteLine("==========\nAdd New Book\n");

                    var title           = "";
                    var isBestSeller    = 0;
                    var authorName      = "";
                    var publisherName   = "";

                    do
                    {
                        Console.Write("Type the Book's title: ");
                        title = Console.ReadLine();

                        Console.Write("Best Seller? (Type 0: false, Type 1: yes): ");
                        isBestSeller = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Type the Author's name: ");
                        authorName = Console.ReadLine();

                        Console.Write("Type the Publisher's name: ");
                        publisherName = Console.ReadLine();

                    }while (title == "" 
                        && (isBestSeller < 0 || isBestSeller > 1)
                        && authorName == ""
                        && publisherName == "");
                    
                    Book book           = new Book();
                    book.Title          = title;
                    book.IsBestSeller   = Convert.ToBoolean(isBestSeller);
                    var result          = book.AddNewBook(book, authorName, publisherName);

                    if (result > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("\nBook added to the shelf...");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nWe've got a problem...");
                    }

                    ReturnBackMessage();        

                    break;                 
                }

                case 2:
                {
                    Console.Clear();
                    Console.WriteLine("==========\nAdd New Author\n");
                    
                    var name = "";
                    
                    do
                    {
                        Console.Write("Type the author's name: ");
                        name = Console.ReadLine();

                    }while (name == "");

                    Author author   = new Author();                    
                    var result      = author.AddNewAuthor(name);

                    if (result > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Author added.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("We've got a problem...");
                    }

                    ReturnBackMessage();      

                    break;
                }

                case 3:
                {
                    Console.Clear();
                    Console.WriteLine("==========\nAdd New Publisher\n");
                    
                    var name = "";
                    
                    do
                    {
                        Console.Write("Type the publisher's name: ");
                        name = Console.ReadLine();

                    }while (name == "");

                    Publisher publisher     = new Publisher();                    
                    var result              = publisher.AddNewPublisher(name);

                    if (result > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Publisher added.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("We've got a problem...");
                    }

                    ReturnBackMessage();     

                    break;
                }

                default:
                    break;
            }
        }

        public void ListSubMenu()
        {
            Console.Clear();
            Console.WriteLine("1. List all Books");
            Console.WriteLine("2. List all Authors");
            Console.WriteLine("3. List all Publishers");
            Console.Write("> ");

            int opt = ReadMenuOption();

            switch(opt)
            {
                case 1:
                {   
                    ListAllBooks();
                    ReturnBackMessage();  

                    break;
                }

                case 2:
                {
                    ListAllAuthors();
                    ReturnBackMessage();

                    break;                    
                }

                case 3:
                {
                    ListAllPublishers();
                    ReturnBackMessage();

                    break;
                }

                default:
                    break;
            }

     
        }

        public void SearchSubMenu()
        {
            Console.Clear();
            Console.WriteLine("1. List all Books");
            Console.WriteLine("2. List all Authors");
            Console.WriteLine("3. List all Publishers");
            Console.Write("> ");

            int opt = ReadMenuOption();

            switch(opt)
            {
                case 1:
                {
                    Console.Clear();
                    Console.WriteLine("Search for a Book");
                    Console.Write("Type the Book's title: ");
                    
                    var bookTitle = Console.ReadLine();

                    if (bookTitle == null) Console.WriteLine("We've got a problem here.");

                    Book book   = new Book();
                    book        = book.GetBook(bookTitle);

                    if (book == null) Console.WriteLine("This book is not the shelf.");
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n======================");
                        Console.WriteLine("Title: " + book.Title);
                        Console.WriteLine("IsBestSeller: " + book.IsBestSeller);
                        Console.WriteLine("Author: " + book.author.Name);
                        Console.WriteLine("Publisher: " + book.publisher.Name);
                        Console.WriteLine("======================\n");
                    }

                    ReturnBackMessage();

                    break;
                }

                case 2:
                {
                    Console.Clear();
                    Console.WriteLine("Search for an author");
                    Console.Write("Type the author's name: ");
                    
                    var authorName = Console.ReadLine();

                    if (authorName == null) Console.WriteLine("We've got a problem here.");

                    Author author   = new Author();
                    author          = author.GetAuthor(authorName);

                    if (author == null) Console.WriteLine("This book is not the shelf.");
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n======================");
                        Console.WriteLine("Name: " + author.Name);                        
                        Console.WriteLine("======================\n");
                    }

                    ReturnBackMessage();

                    break;
                }

                case 3:
                {
                    Console.Clear();
                    Console.WriteLine("Search for a publisher");
                    Console.Write("Type the publisher's name: ");
                    
                    var publisherName = Console.ReadLine();

                    if (publisherName == null) Console.WriteLine("We've got a problem here.");

                    Publisher publisher = new Publisher();
                    publisher           = publisher.GetPublisher(publisherName);

                    if (publisher == null) Console.WriteLine("This book is not the shelf.");
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n======================");
                        Console.WriteLine("Publisher: " + publisher.Name);                        
                        Console.WriteLine("======================\n");
                    }

                    ReturnBackMessage();

                    break;
                }

                default:
                    break;
            }
        }

        public void DeleteSubMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Delete Book");
            Console.WriteLine("2. Delete Author");
            Console.WriteLine("3. Delete Publisher");
            Console.Write("> ");

            int opt = ReadMenuOption();

            switch(opt)
            {
                case 1:
                {
                    Console.Clear();
                    Console.WriteLine("==========\nDelete Book\n");        
                    ListAllBooks();
                    Console.Write("\n\nSelect Book's id: ");                    
                    var id = int.Parse(Console.ReadLine());
                    
                    Book book   = new Book();                    
                    var result  = book.DeleteBook(id);                    

                    if (result > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Book Deleted...");
                    }                        
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("We've got a problem...");
                    }

                    break;
                }

                case 2:
                {
                    Console.Clear();
                    Console.WriteLine("==========\nDelete Book\n");        
                    ListAllBooks();
                    Console.Write("\n\nSelect author's id: ");                    
                    var id = int.Parse(Console.ReadLine());
                    
                    Author author   = new Author();             
                    var result      = author.DeleteAuthor(id);                    

                    if (result > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Author Deleted...");
                    }                        
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("We've got a problem...");
                    }

                    break;
                }

                case 3:
                {
                                        Console.Clear();
                    Console.WriteLine("==========\nDelete Book\n");        
                    ListAllBooks();
                    Console.Write("\n\nSelect Book's id: ");                    
                    var id = int.Parse(Console.ReadLine());
                    
                    Publisher publisher     = new Publisher();                    
                    var result              = publisher.DeletePublisher(id);                    

                    if (result > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Publisher Deleted...");
                    }                        
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("We've got a problem...");
                    }

                    break;
                }
            }
        }
        
        public void UpdateSubMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Update Book");
            Console.WriteLine("2. Update Author");
            Console.WriteLine("3. Update Publisher");
            Console.Write("> ");

            int opt = ReadMenuOption();

            switch(opt)
            {
                case 1:
                {
                    Console.Clear();
                    Console.WriteLine("==========\nUpdate Book\n");        
                    ListAllBooks();
                    Console.Write("\n\nSelect Book's id: ");                    
                    var id = int.Parse(Console.ReadLine());
                    
                    Console.Write("Type new book's title: ");
                    var title = Console.ReadLine();
                    
                    Book book = new Book();
                    
                    var result  = book.UpdateBook(id, title);
                    var b       = book.GetBook(title);

                    if (result > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Book Updated...");
                    }                        
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("We've got a problem...");
                    }
                        
                    Console.WriteLine("New Book's title: " + b.Title);

                    break;
                }
            }
        }

        public void ListAllBooks()
        {            
            Console.WriteLine("========================\nList of All Books");
            
            Book book = new Book();
            var books = book.GetAllBooks();

            foreach (var b in books)
            {
                Console.WriteLine("========================");                        
                Console.WriteLine("ID: " + b.ID);
                Console.WriteLine("Title: " + b.Title);
                Console.WriteLine("Best Seller: " + b.IsBestSeller);
                Console.WriteLine("Author: " + b.author.Name);
                Console.WriteLine("Publisher: " + b.publisher.Name);
            }                        
        }

        public void ListAllAuthors()
        {            
            Console.WriteLine("========================\nList of All Authors");
            
            Author author = new Author();
            var authors = author.GetAllAuthors();

            foreach (var a in authors)
            {
                Console.WriteLine("========================");                                                
                Console.WriteLine("Author: " + a.Name);                        
            }      
        }

        public void ListAllPublishers()
        {            
            Console.WriteLine("========================\nList of All Publishers");
            
            Publisher publisher     = new Publisher();
            var publishers          = publisher.GetAllPublishers();

            foreach (var p in publishers)
            {
                Console.WriteLine("========================");                                                
                Console.WriteLine("Publisher: " + p.Name);                        
            }                    
        }
    }
}
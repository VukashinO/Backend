using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libary.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }

        public string Description { get; set; }
        public Author Author { get; set; }
        public int Pages { get; set; }
        public int Quantity { get; set; }

        public Book(int id, string isbn, string name, Genre genre, string description,
            Author author, int pages, int quantity)
        {
            ID = id;
            ISBN = isbn;
            Name = name;
            Genre = genre;
            Description = description;
            Author = author;
            Pages = pages;
            Quantity = quantity;
        }

        public void IncreaseQuantiy(int quantity)
        {
            Quantity += quantity;
        }
        public void RemoveCopy()
        {
            if (Quantity > 0) Quantity--;
            Console.WriteLine("there is no more copyes");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PR19
{

    class Book
    {
        public string autor;
        public string name = "Null";
        public int pageCount = 0;
        public string publ = "Null";
        public int publYear = 0;

        public Book(string autor, string name, int pageCount, string publ, int publYear)
        {
            this.autor = autor;
            this.name = name;
            this.pageCount = pageCount;
            this.publ = publ;
            this.publYear = publYear;
        }
        public void addBook()
        {
            StreamWriter logWr = new StreamWriter(@"C:\books\books.txt", true);
            logWr.WriteLine($"{autor}|{name}|{pageCount}|{publ}|{publYear}");
            logWr.Close();
        }

        public void Print()
        {
            Console.WriteLine($"Информация о книге:\n" +
                $"Автор книги: {autor}\n" +
                $"Название книги: {name}\n" +
                $"Кол-во страниц: {pageCount}\n" +
                $"Издательство: {publ}\n" +
                $"Год издательства:{publYear}\n");
        }

        public void NewAutor(string autor)
        {
            this.autor = autor;
        }
        ~Book()
        {
            Console.WriteLine("Объект удалён");
        }
    }

    class TextBook : Book
    {
        public int Class;
        public TextBook(string autor, string name, int pageCount, string publ, int publYear, int Class) : base(autor, name, pageCount, publ, publYear)
        {
            this.autor = autor;
            this.name = name;
            this.pageCount = pageCount;
            this.publ = publ;
            this.publYear = publYear;
            this.Class = Class;
        }
        public new void addBook()
        {
            StreamWriter logWr = new StreamWriter(@"C:\books\books.txt", true);
            logWr.WriteLine($"{autor}|{name}|{pageCount}|{publ}|{publYear}|{Class}");
            logWr.Close();
        }
    }
        
}

using System;
using System.IO;
using System.Collections.Generic;

namespace PR19
{
    class Program
    {
        static StreamReader booksTXT = new StreamReader(@"C:\books\books.txt");
        static List<Book> books = new List<Book>();

        //__| Читаем файл в объекты класса и обратно в файл
        static void fileToClass()
        {
            try
            {
                string inputTXT;
                while ((inputTXT = booksTXT.ReadLine()) != null)
                {
                    string[] bufferString = inputTXT.Split('|');
                    if (bufferString.Length == 5)
                    {
                        Book bufBook = new(bufferString[0], bufferString[1], int.Parse(bufferString[2]), bufferString[3], int.Parse(bufferString[4]));
                        books.Add(bufBook);
                    }
                    else if(bufferString.Length == 6)
                    {
                        TextBook bufBook = new(bufferString[0], bufferString[1], int.Parse(bufferString[2]), bufferString[3], int.Parse(bufferString[4]), int.Parse(bufferString[5]));
                        books.Add(bufBook);
                    }
                }
                booksTXT.Close();
            }
            catch(FormatException)
            {
                Console.WriteLine("Книги отсутствуют в библиотеке или произошла ошибка при их чтении");
                PressButton();
            }
        }
        static void ClassToFile()
        {
            StreamWriter txtWrite = new StreamWriter(@"C:\books\books.txt", false);
            foreach (Book i in books)
            {

                txtWrite.WriteLine($"{i.autor}|{i.name}|{i.pageCount}|{i.publ}|{i.publYear}");
            }
            txtWrite.Close();
        }

        //__| Поиск книг по разным параметрам:
        static void SearchByAutor(string autor)
        {
            Console.WriteLine($"Результаты поиска по автору \"{autor}\":\n");
            bool hasFinded = false;
            foreach(Book i in books)
            {
                if(i.autor.ToLower() == autor.ToLower())
                {
                    i.Print();
                    hasFinded = true;
                }
            }
            if(hasFinded == false)
            {
                Console.WriteLine($"Книги не были найдены.");
            }
            PressButton();
        }
        static void SearchByName(string name)
        {
            Console.WriteLine($"Результаты поиска по названию \"{name}\":\n");
            bool hasFinded = false;
            foreach (Book i in books)
            {
                if (i.name.ToLower() == name.ToLower())
                {
                    i.Print();
                    hasFinded = true;
                }
            }
            if (hasFinded == false)
            {
                Console.WriteLine($"Книги не были найдены.");
            }
            PressButton();
        }
        static void SearchByName(out bool hasFinded, out int index)
        {
            Console.WriteLine("Введите название книги");
            string name = Console.ReadLine();
            hasFinded = false;
            index = 0;
            foreach (Book i in books)
            {
                if (name.ToLower() == i.name.ToLower())
                {
                    index = books.IndexOf(i);
                    hasFinded = true;
                }
            }
        }
        static void SearchByPageCount(int PageCount)
        {
            Console.WriteLine($"Результаты поиска по количеству страниц \"{PageCount}\":\n");
            bool hasFinded = false;
            foreach (Book i in books)
            {
                if (i.pageCount == PageCount)
                {
                    i.Print();
                    hasFinded = true;
                }
            }
            if (hasFinded == false)
            {
                Console.WriteLine($"Книги не были найдены.");
            }
            PressButton();
        }
        static void SearchByPubl(string Publ)
        {
            Console.WriteLine($"Результаты поиска по изданию \"{Publ}\":\n");
            bool hasFinded = false;
            foreach (Book i in books)
            {
                if (i.publ.ToLower() == Publ.ToLower())
                {
                    i.Print();
                    hasFinded = true;
                }
            }
            if (hasFinded == false)
            {
                Console.WriteLine($"Книги не были найдены.");
            }
            PressButton();
        }
        static void SearchByYear(int year)
        {
            Console.WriteLine($"Результаты поиска по году издания \"{year}\":\n");
            bool hasFinded = false;
            foreach (Book i in books)
            {
                if (i.publYear == year)
                {
                    i.Print();
                    hasFinded = true;
                }
            }
            if (hasFinded == false)
            {
                Console.WriteLine($"Книги не были найдены.");
            }
            PressButton();
        }

        //__| Сортировка книг по разным параметрам:
        static bool SortChar(char[] ibook, char[] jbook, int i)
        {
            try
            {
                if ((int)ibook[i] > (int)jbook[i])
                    return true;
                else if ((int)ibook[i] < (int)jbook[i])
                    return false;
                else
                    return (SortChar(ibook, jbook, i + 1));
            }
            catch (Exception)
            {
                return false;
            }
        }
        static void Swap(bool change, int i, int j)
        {
            if (change)
            {
                var temp = books[i];
                books[i] = books[j];
                books[j] = temp;
            }
        }

        static void SortByAutor()
        {
            for(int i = 0; i<books.Count; i++)
                for(int j = i; j<books.Count; j++)
                {
                    char[] ibook = books[i].autor.ToCharArray();
                    char[] jbook = books[j].autor.ToCharArray();
                    int k = 0;
                    Swap(SortChar(ibook, jbook, k), i, j);
                }
            ClassToFile();
        }
        static void SortByName()
        {
            for (int i = 0; i < books.Count; i++)
                for (int j = i; j < books.Count; j++)
                {
                    char[] ibook = books[i].name.ToCharArray();
                    char[] jbook = books[j].name.ToCharArray();
                    int k = 0;
                    Swap(SortChar(ibook, jbook, k), i, j);
                }
            ClassToFile();
        }
        static void SortByPageCount()
        {
            for (int i = 0; i < books.Count; i++)
                for (int j = i; j < books.Count; j++)
                {
                    Swap(books[i].pageCount < books[j].pageCount, i, j);
                }
            ClassToFile();
        }
        static void SortByPubl()
        {
            for (int i = 0; i < books.Count; i++)
                for (int j = i; j < books.Count; j++)
                {
                    char[] ibook = books[i].publ.ToCharArray();
                    char[] jbook = books[j].publ.ToCharArray();
                    int k = 0;
                    Swap(SortChar(ibook, jbook, k), i, j);
                }
            ClassToFile();
        }
        static void SortByYear()
        {
            for (int i = 0; i < books.Count; i++)
                for (int j = i; j < books.Count; j++)
                {
                    Swap(books[i].publYear < books[j].publYear, i, j);
                }
            ClassToFile();
        }

        //__| Выбор варианта действия
        static void Switch()
        {
            Console.Clear();
            int answer;
            do
            {
                Console.Write("__ Домашняя Библиотека __\n" +
                    "1 - Список добавленной литературы\n" +
                    "2 - Изменить данные о книги\n" +
                    "3 - Добавить книгу\n" +
                    "4 - Добавить учебник\n" +
                    "5 - Удалить книгу/учебник\n" +
                    "6 - Найти книгу по параметру\n" +
                    "7 - Сортировать книги по параметру:\n" +
                    "0 - Выйти из программы\n" +
                    "Выбор: ");
                try
                {
                    answer = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    answer = 9;
                }
                switch (answer)
                {
                    case 1:
                        Console.Clear();
                        PrintList(books);
                        break;
                    case 2:
                        SwitchChange();
                        break;
                    case 3:
                        Console.Clear();
                        AddBook();
                        break;
                    case 4:
                        Console.Clear();
                        AddTextBook();
                        break;
                    case 5:
                        Console.Clear();
                        DeleteBook();
                        break;
                    case 6:
                        SwitchSearch();
                        break;
                    case 7:
                        SwitchSort();
                        break;
                    case 0:
                        //Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
            while (answer != 0);
        }
        static void SwitchSearch()
        {
            Console.Clear();
            int answer;
            do
            {
                Console.Write("--==(0) Найти книгу по параметру (0)==--\n" +
                    "1 - По автору\n" +
                    "2 - По названию\n" +
                    "3 - По количеству страниц\n" +
                    "4 - По издателю\n" +
                    "5 - По году издания\n" +
                    "0 - Выйти в главное меню\n" +
                    "Выбор: ");
                try
                {
                    answer = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    answer = 9;
                }
                switch (answer)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Введите автора искомой книги: ");
                        var autor = Console.ReadLine();
                        SearchByAutor(autor);
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Введите название искомой книги: ");
                        var name = Console.ReadLine();
                        SearchByName(name);
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("Введите кол-во страниц искомой книги: ");
                        var count = int.Parse(Console.ReadLine());
                        SearchByPageCount(count);
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("Введите издание искомой книги: ");
                        var publ = Console.ReadLine();
                        SearchByPubl(publ);
                        break;
                    case 5:
                        Console.Clear();
                        Console.Write("Введите автора искомой книги: ");
                        var year = int.Parse(Console.ReadLine());
                        SearchByYear(year);
                        break;
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
            while (answer != 0);
        }
        static void SwitchChange()
        {
            Console.Clear();
            int answer;
            do
            {
                Console.Write("--==(0) Изменить параметр книги (0)==--\n" +
                    "1 - Изменить автора книги\n" +
                    "2 - Изменить название книги\n" +
                    "3 - Изменить издательство (и) количество страниц\n" +
                    "0 - Выйти в главное меню\n" +
                    "Выбор: ");
                try
                {
                    answer = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    answer = 9;
                }
                switch (answer)
                {
                    case 1:
                        Console.Clear();
                        ChangeAutor();
                        break;
                    case 2:
                        Console.Clear();
                        ChangeName();
                        break;
                    case 3:
                        Console.Clear();
                        ChangePubl();
                        break;
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
            while (answer != 0);
        }
        static void SwitchSort()
        {
            Console.Clear();
            int answer;
            do
            {
                Console.Write("--==(0) Сортировать книги по параметру (0)==--\n" +
                     "1 - По автору\n" +
                     "2 - По названию\n" +
                     "3 - По количеству страниц\n" +
                     "4 - По издателю\n" +
                     "5 - По году издания\n" +
                     "0 - Выйти в главное меню\n" +
                     "Выбор: ");
                try
                {
                    answer = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    answer = 9;
                }
                Console.Clear();
                switch (answer)
                {
                    case 1:
                        SortByAutor();
                        break;
                    case 2:
                        SortByName();
                        break;
                    case 3:
                        SortByPageCount();
                        break;
                    case 4:
                        SortByPubl();
                        break;
                    case 5:
                        SortByYear();
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
            }
            while (answer != 0);
        }

        //__| нажмите кнопку, чтобы выйти в главное меню
        static void PressButton()
        {
            Console.Write("\nНажмите любую кнопку для выхода в меню выбора...");
            Console.ReadKey();
            Console.Clear();
        }

        //__| Вывод списка литературы существующей
        static void PrintList(List<Book> books)
        {
            //foreach (Book i in books)
            //{
            //    Console.Write($"{books.IndexOf(i)+1}) ");
            //    i.Print();
            //}
            for(int i = 0; i < books.Count; i++)
            {
                Console.Write($"{i + 1}) ");
                books[i].Print();
            }
            PressButton();
        }

        //__| Добавить/удалить книгу
        static void AddBook()
        {
            try
            {
                Console.WriteLine("Напишите автора книги:");
                var autor = Console.ReadLine();
                Console.WriteLine("Напишите название книги:");
                var name = Console.ReadLine();
                Console.WriteLine("Напишите кол-во страниц книги:");
                var pageCount = int.Parse(Console.ReadLine());
                Console.WriteLine("Напишите название издательства книги:");
                var publ = Console.ReadLine();
                Console.WriteLine("Напишите год издания книги:");
                var publYear = int.Parse(Console.ReadLine());
                var bufbook = new Book(autor, name, pageCount, publ, publYear);
                bufbook.addBook();
                books.Add(bufbook);
            }
            catch (FormatException)
            {
                Console.WriteLine("Произошла ошибка при заполнении данных. Убедитесь что данные введены верно");
            }
            PressButton();
        }
        static void AddTextBook()
        {
            try
            {
                Console.WriteLine("Напишите автора книги:");
                var autor = Console.ReadLine();
                Console.WriteLine("Напишите название книги:");
                var name = Console.ReadLine();
                Console.WriteLine("Напишите кол-во страниц книги:");
                var pageCount = int.Parse(Console.ReadLine());
                Console.WriteLine("Напишите название издательства книги:");
                var publ = Console.ReadLine();
                Console.WriteLine("Напишите год издания книги:");
                var publYear = int.Parse(Console.ReadLine());
                Console.WriteLine("Напишите предусмотренный класс обучения книги:");
                var Class = int.Parse(Console.ReadLine());
                var bufbook = new TextBook(autor, name, pageCount, publ, publYear, Class);
                bufbook.addBook();
                books.Add(bufbook);
            }
            catch (FormatException)
            {
                Console.WriteLine("Произошла ошибка при заполнении данных. Убедитесь что данные введены верно");
            }
            PressButton();
        }
        static void DeleteBook()
        {
            bool hasFinded;
            int index;
            SearchByName(out hasFinded, out index);

            if (hasFinded == true)
            {
                books.RemoveAt(index);
                ClassToFile();
                Console.WriteLine("Книга успешно удалена.");
            }
            else
                Console.WriteLine("Такой книги не найдено!");

            PressButton();
        }

        //__| Изменить данные книги
        static void ChangeName()
        {
            bool hasFinded;
            int Index;
            SearchByName(out hasFinded, out Index);

            if (hasFinded == true)
            {
                Console.WriteLine("Введите новое название книги:");
                var newName = Console.ReadLine();
                books[Index].name = newName;
                ClassToFile();
                Console.WriteLine("Книга успешно изменена.");
            }
            else
                Console.WriteLine("Такой книги не найдено!");

            PressButton();

        }
        static void ChangeAutor()
        {
            bool hasFinded;
            int Index;
            SearchByName(out hasFinded, out Index);

            if (hasFinded == true)
            {
                Console.WriteLine("Введите нового автора книги:");
                var newAutor = Console.ReadLine();
                books[Index].NewAutor(newAutor);
                ClassToFile();
                Console.WriteLine("Книга успешно изменена.");
            }
            else
                Console.WriteLine("Такой книги не найдено!");

            PressButton();
        }
        static void ChangePubl()
        {
            bool hasFinded;
            int Index;
            SearchByName(out hasFinded, out Index);
            if (hasFinded == true)
            {
                Console.WriteLine("Введите нового издателя книги:");
                var newPubl = Console.ReadLine();
                Console.WriteLine("Введите изменённое кол-во страниц (этот пункт можно пропустить)");
                var bufString = Console.ReadLine();
                int newCount;
                if (int.TryParse(bufString, out newCount))
                    ChangePublProcess(Index, newPubl, newCount);
                else
                    ChangePublProcess(Index, newPubl);
            }
            else
                Console.WriteLine("Такой книги не найдено!");
            PressButton();

        }
        static void ChangePublProcess(int Index, string newPubl)
        {
            books[Index].publ = newPubl;
            ClassToFile();
            Console.WriteLine("Книга успешно изменена.");
        }
        static void ChangePublProcess(int Index, string newPubl, int newCount)
        {
            books[Index].publ = newPubl;
            books[Index].pageCount = newCount;
            ClassToFile();
            Console.WriteLine("Книга успешно изменена.");
        }

        static void Main(string[] args)
        {
            fileToClass();
            Switch();
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Group { get; set; }
    public double AverageGrade { get; set; }
}

class Program
{
    static List<Student> students = new List<Student>();
    static int nextId = 1;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("=== МЕНЕДЖЕР СТУДЕНТОВ ===");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Показать всех студентов");
            Console.WriteLine("3. Найти студента");
            Console.WriteLine("4. Удалить студента");
            Console.WriteLine("5. Сохранить в JSON");
            Console.WriteLine("6. Загрузить из JSON");
            Console.WriteLine("7. Сортировать по среднему баллу");
            Console.WriteLine("0. Выход");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;

                case "2":
                    ShowStudents();
                    break;

                case "3":
                    FindStudent();
                    break;

                case "4":
                    DeleteStudent();
                    break;

                case "5":
                    SaveToJson();
                    break;

                case "6":
                    LoadFromJson();
                    break;

                case "7":
                    SortStudents();
                    break;

                case "0":
                    return;

                default:
                    Console.Clear();

                    Console.Write("Неизвестная команда.");

                    Thread.Sleep(850);
                    Console.Clear();
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Student student = new Student();

        student.Id = nextId++;

        Console.Clear();

        Console.Write("Введите ФИО: ");
        student.FullName = Console.ReadLine();

        Console.Write("Введите группу: ");
        student.Group = Console.ReadLine();

        Console.Write("Введите средний балл: ");
        student.AverageGrade = double.Parse(Console.ReadLine());

        students.Add(student);

        Console.Clear();

        Console.Write("Студент добавлен.");

        Thread.Sleep(2000);
        Console.Clear();
    }

    static void ShowStudents()
    {
        if (students.Count == 0)
        {
            Console.Clear();

            Console.Write("Список студентов пуст.");

            Thread.Sleep(2000);
            Console.Clear();
            return;
        }

        Console.Clear();

        foreach (Student student in students)
        {
            #region
            if (student.Id > 99999)
            {
                Console.Write($"ID: {student.Id} | ");
            }
            else if (student.Id > 9999)
            {
                Console.Write($"ID: 0{student.Id} | ");
            }
            else if (student.Id > 999)
            {
                Console.Write($"ID: 00{student.Id} | ");
            }
            else if (student.Id > 99)
            {
                Console.Write($"ID: 000{student.Id} | ");
            }
            else
            {
                Console.Write($"ID: 0000{student.Id} | ");
            }
            #endregion
            Console.WriteLine(
                $"ФИО: {student.FullName} | " +
                $"Группа: {student.Group} | " +
                $"Средний балл: {student.AverageGrade}\n"
            );
        }

        Thread.Sleep(5000);
    }

    static void FindStudent()
    {
        Console.Clear();

        Console.Write("Введите имя или группу: ");
        string search = Console.ReadLine();

        List<Student> result = students
            .Where(s =>
                s.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                s.Group.Contains(search, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (result.Count == 0)
        {
            Console.Clear();

            Console.Write("Студенты не найдены.");

            Thread.Sleep(2000);
            Console.Clear();
            return;
        }

        Console.Clear();

        foreach (Student student in result)
        {
            Console.WriteLine(
                $"ID: {student.Id} | " +
                $"ФИО: {student.FullName} | " +
                $"Группа: {student.Group} | " +
                $"Средний балл: {student.AverageGrade}\n"
            );
        }
        Thread.Sleep(3000);
    }

    static void DeleteStudent()
    {
        Console.Write("Введите ID студента: ");
        int id = int.Parse(Console.ReadLine());

        Student student = students.FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            Console.Clear();

            Console.Write("Студент не найден.");

            Thread.Sleep(2000);
            Console.Clear();
            return;
        }

        students.Remove(student);

        Console.Clear();

        Console.Write("Студент удалён.");

        Thread.Sleep(2000);
        Console.Clear();
    }

    static void SaveToJson()
    {
        string json = JsonSerializer.Serialize(
            students,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );

        File.WriteAllText("students.json", json);

        Console.Clear();

        Console.Write("Данные сохранены в students.json");

        Thread.Sleep(2000);
        Console.Clear();
    }

    static void LoadFromJson()
    {
        if (!File.Exists("students.json"))
        {
            Console.Clear();

            Console.Write("Файл students.json не найден.");

            Thread.Sleep(2000);
            Console.Clear();
            return;
        }

        string json = File.ReadAllText("students.json");

        students = JsonSerializer.Deserialize<List<Student>>(json);

        if (students == null)
        {
            students = new List<Student>();
        }

        if (students.Count > 0)
        {
            nextId = students.Max(s => s.Id) + 1;
        }

        Console.Clear();

        Console.Write("Данные загружены.");

        Thread.Sleep(2000);
        Console.Clear();
    }

    static void SortStudents()
    {
        students = students
            .OrderByDescending(s => s.AverageGrade)
            .ToList();

        Console.Clear();

        Console.Write("Студенты отсортированы по среднему баллу.");

        Thread.Sleep(2000);
        Console.Clear();
    }
}

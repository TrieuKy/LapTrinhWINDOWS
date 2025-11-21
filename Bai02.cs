using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace QuanLyHocSinh
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Student(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student(1, "An", 14),
                new Student(2, "Binh", 16),
                new Student(3, "Anh", 18),
                new Student(4, "Cuong", 20),
                new Student(5, "Dung", 15),
                new Student(6, "Tien", 17)
            };
            Console.WriteLine("\nDanh sach tat ca hoc sinh:");
            PrintList(students);

            Console.WriteLine("\nDanh sach hoc sinh co tuoi tu 15 den 18");
            var studentsAge15to18 = students.Where(s => s.Age >= 15 && s.Age <= 18).ToList();
            PrintList(studentsAge15to18);

            Console.WriteLine("\nHoc sinh co ten bat dau bang chu A");
            var studentsNameA = students.Where(s => s.Name.StartsWith("A")).ToList();
            PrintList(studentsNameA);

            Console.Write("\nTong tuoi cua tat ca hoc sinh:");
            int totalAge = students.Sum(s => s.Age);
            Console.WriteLine($"{totalAge}");

            Console.WriteLine("\nHoc sinh co tuoi lon nhat:");
            int maxAge = students.Max(s => s.Age);
            var oldestStudents = students.Where(s => s.Age == maxAge).ToList();
            PrintList(oldestStudents);

            Console.WriteLine("\nDanh sach tuoi tang dan cua hoc sinh:");
            var sortedStudents = students.OrderBy(s => s.Age).ToList();
            PrintList(sortedStudents);
            Console.ReadKey();
        }
        static void PrintList(List<Student> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Khon co hoc sinh nao");
                return;
            }
            Console.WriteLine("{0,-5} {1,-20} {2,-5}", "ID", "Ten", "Tuoi");
            Console.WriteLine(new string('-', 35));

            foreach (var sv in list)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-5}", sv.Id, sv.Name, sv.Age);
            }
        }
    }
}



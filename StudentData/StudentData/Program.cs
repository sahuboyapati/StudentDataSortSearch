using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentdataSortandSearch
{
    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }

        public override string ToString()
        {
            return $"{Name}\t{Class}";
        }

        public void PrintStudentData(List<Student> students)
        {
            Console.WriteLine("Name\tClass");
            Console.WriteLine("----------------");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            string choice;
            string filePath = "C:\\Users\\Boyapati Sahaja\\OneDrive\\Desktop\\PracticeProject3\\StudentData.txt";

            if (File.Exists(filePath))
            {
                bool isHeaderLine = true;
                Student data = new Student();
                List<Student> students = new List<Student>();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Read the school name and ignore it for now
                    string schoolName = reader.ReadLine();
                    Console.WriteLine($"School Name: {schoolName}");
                    // Read and process the student data
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (isHeaderLine)
                        {
                            isHeaderLine = false;
                            continue;
                        }

                        string[] values = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (values.Length == 2)
                        {
                            string name = values[0];
                            string studentClass = values[1];

                            Student student = new Student
                            {
                                Name = name,
                                Class = studentClass
                            };

                            students.Add(student);
                        }
                        else
                        {
                            Console.WriteLine("Invalid data format in the file.");
                        }
                    }
                }

                // Print the unsorted data
                Console.WriteLine("Unsorted Student Data:");
                data.PrintStudentData(students);
                do
                {
                    Console.WriteLine("Choose the operations to be done on student data");
                    Console.WriteLine("1.Sorting");
                    Console.WriteLine("2.Searching");
                    int op = int.Parse(Console.ReadLine());
                    switch (op)
                    {
                        case 1:
                            {
                                students = students.OrderBy(s => s.Name).ToList();
                                Console.WriteLine("\nSorted Student Data by Name:");
                                data.PrintStudentData(students);
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Enter the name to be searched");
                                string searchName = Console.ReadLine();
                                Student foundStudent = students.Find(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                                if (foundStudent != null)
                                {
                                    Console.WriteLine($"\nFound Student: {foundStudent}");
                                }
                                else
                                {
                                    Console.WriteLine($"\nStudent with name '{searchName}' not found.");
                                }

                                break;

                            }
                        default:
                            {
                                Console.WriteLine("Invalid");
                                break;
                            }
                    }
                    Console.WriteLine("Do you want to continue? press y/n");
                    choice = Console.ReadLine();
                } while (choice == "y");
            }


            // Search for a student by name

            else
            {
                Console.WriteLine("File not found.");
            }

            Console.ReadKey();

        }
    }
}
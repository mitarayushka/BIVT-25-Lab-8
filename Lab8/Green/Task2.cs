using System.Collections;
using System.Xml.Linq;

namespace Lab8.Green
{
    public class Task2
    {
        public class Human
        {
            // поля
            private string _name; // Перенесли свойства Name и Surname, а также связанные с ними поля в класс Human.
            private string _surname;

            // свойства 
            public string Name => _name;
            public string Surname => _surname;

            // конструктор
            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            public void Print() // для вывода информации о необходимых полях структуры
            {
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Surname: {Surname}");
            }
        }
        public class Student : Human
        {
            // поля
            private int[] _marks;
            private static int _excellentStudent; //  поле, подсчитывающее общее количество отличников. 

            // свойства

            public int[] Marks => _marks.ToArray();
            public double AverageMark => _marks.Average(); // среднее значение оценок студента

            public bool IsExcellent // возвращает значение, все ли оценки по предметам "4" и выше
            {
                get
                {
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] < 4) return false; // // Нашли оценку ниже 4 - не отличник
                    }
                    return true;
                }
            }
            // конструктор
            public static int ExcellentAmount // свойство только для чтения поля общего количества отличников.
            {
                get
                {
                    return _excellentStudent;
                }
            }
            public Student(string name, string surname) : base(name, surname) 
            {
                
                _marks = new int[4];
            }
            public void Exam(int mark) // заменяет оценку по предмету новой оценкой
            {

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        break;
                    }
                    ;
                }
                if (IsExcellent)
                    _excellentStudent++;
            }
            public static void SortByAverageMark(Student[] array) // для сортировки массива структур в порядке убывания среднего балла
            {
                Array.Sort(array, (a, b) => b.AverageMark.CompareTo(a.AverageMark));
            }
            public void Print() // для вывода информации о необходимых полях структуры
            {
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Surname: {Surname}");
                Console.WriteLine($"Marks: {Marks}");
                Console.WriteLine($"Average mark: {AverageMark}");
                Console.WriteLine($"Is excellent: {IsExcellent}");
            }
        }
    }
}

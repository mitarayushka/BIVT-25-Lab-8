using System;

namespace Lab8.White
{
    public class Task3
    {
        // Поля с измененным уровнем доступа (protected)
        protected string _name;
        protected string _surname;
        protected int[] _marks;
        protected int _skips;

        // Конструктор
        public Student(string name, string surname, int[] marks, int skips)
        {
            _name = name;
            _surname = surname;
            _marks = marks;
            _skips = skips;
        }

        // Защищенный конструктор для копирования
        protected Student(Student other)
        {
            _name = other._name;
            _surname = other._surname;
            _marks = (int[])other._marks.Clone();
            _skips = other._skips;
        }

        // Метод для добавления оценки (используется в WorkOff)
        public void AddMark(int mark)
        {
            int[] newMarks = new int[_marks.Length + 1];
            _marks.CopyTo(newMarks, 0);
            newMarks[_marks.Length] = mark;
            _marks = newMarks;
        }

        // Метод для замены оценки (используется в WorkOff)
        public void ReplaceMark(int oldMark, int newMark)
        {
            for (int i = 0; i < _marks.Length; i++)
            {
                if (_marks[i] == oldMark)
                {
                    _marks[i] = newMark;
                    return;
                }
            }
        }

        public virtual void Print()
        {
            Console.WriteLine($"Студент: {_name} {_surname}, Пропуски: {_skips}");
            Console.Write("Оценки: ");
            foreach (var m in _marks) Console.Write($"{m} ");
            Console.WriteLine();
        }
    }

    public class Undergraduate : Student
    {
        // Конструктор 1: принимает имя и фамилию
        public Undergraduate(string name, string surname) : base(name, surname, new int[0], 0)
        {
        }

        // Конструктор 2: принимает объект студента
        public Undergraduate(Student student) : base(student)
        {
        }

        // Метод отработки пропусков
        public void WorkOff(int mark)
        {
            if (_skips > 0)
            {
                _skips--;
                AddMark(mark);
            }
            else
            {
                ReplaceMark(2, mark);
            }
        }

        // Переопределение метода Print
        public override void Print()
        {
            Console.WriteLine($"Бакалавр: {_name} {_surname}, Пропуски: {_skips}");
            Console.Write("Оценки: ");
            foreach (var m in _marks) Console.Write($"{m} ");
            Console.WriteLine();
        }
    }

    public class Task3
    {
        public static void Main(string[] args)
        {
            // Создание студента
            Student s = new Student("Иван", "Иванов", new int[] { 4, 3, 2 }, 2);
            
            // Создание бакалавра через конструктор с именем
            Undergraduate u1 = new Undergraduate("Петр", "Петров");
            
            // Создание бакалавра через конструктор с объектом студента
            Undergraduate u2 = new Undergraduate(s);

            u1.Print();
            u2.Print();

            // Отработка пропуска у u2 (было 2 пропуска, стало 1, добавлена оценка 4)
            u2.WorkOff(4);
            Console.WriteLine("После WorkOff(4) у u2:");
            u2.Print();

            // Отработка пропуска у u1 (пропусков 0, заменяем 2 на 5)
            u1.AddMark(2); // Добавим двойку для теста замены
            u1.Print();
            u1.WorkOff(5); 
            Console.WriteLine("После WorkOff(5) у u1:");
            u1.Print();
        }
    }
}

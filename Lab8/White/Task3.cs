using System;
using System.Linq;

namespace Lab8.White
{
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            protected int[] _marks;
            protected int _missedLessons;

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _missedLessons = 0;
            }

            protected Student(Student other)
            {
                _name = other._name;
                _surname = other._surname;
                _marks = (int[])other._marks.Clone();
                _missedLessons = other._missedLessons;
            }

            public void Lesson(int mark)
            {
                if (mark == 2)
                {
                    _missedLessons++;
                }
            }

            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks => _marks;
            public int MissedLessons => _missedLessons;
        }

        public class Undergraduate : Student
        {
            public Undergraduate(string name, string surname) : base(name, surname)
            {
            }

            public Undergraduate(Student other) : base(other)
            {
            }

            public void WorkOff(int mark)
            {
                if (_missedLessons > 0)
                {
                    _missedLessons--;
                }
                else
                {
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] == 2)
                        {
                            _marks[i] = mark;
                            break;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Студент: {_name} {_surname}, Пропуски: {_missedLessons}");
                Console.WriteLine("Оценки: " + string.Join(", ", _marks));
            }
        }

        public static void Main(string[] args)
        {
            Undergraduate ug = new Undergraduate("Иван", "Иванов");
            ug.Lesson(2);
            ug.Lesson(2);
            
            ug.Print();

            ug.WorkOff(4);
            ug.WorkOff(3);
            
            ug.Print();

            ug.Lesson(2);
            ug.WorkOff(5);
            
            ug.Print();
        }
    }
}

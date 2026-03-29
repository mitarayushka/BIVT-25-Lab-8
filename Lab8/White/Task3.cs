using System;


    public class Task3
    {
        
        public class Student
        {
            
            private string _name;
            private string _surname;
            protected int[] _grades;      
            protected int _misses;         

            
            public string Name => _name;
            public string Surname => _surname;
            public int[] Grades => _grades;
            public int Misses => _misses;

            
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _grades = new int[0]; // Пустой массив оценок
                _misses = 0;
            }

            
            protected Student(Student other)
            {
                if (other != null)
                {
                    _name = other._name;
                    _surname = other._surname;
                    _misses = other._misses;

                    // Копируем массив оценок
                    _grades = new int[other._grades.Length];
                    for (int i = 0; i < other._grades.Length; i++)
                    {
                        _grades[i] = other._grades[i];
                    }
                }
                else
                {
                    _name = "";
                    _surname = "";
                    _grades = new int[0];
                    _misses = 0;
                }
            }

            // Метод для добавления оценки
            public void Lesson(int grade)
            {
                // Увеличиваем массив оценок
                int[] newGrades = new int[_grades.Length + 1];
                for (int i = 0; i < _grades.Length; i++)
                {
                    newGrades[i] = _grades[i];
                }
                newGrades[_grades.Length] = grade;
                _grades = newGrades;
            }

            // Метод для добавления пропуска
            public void Miss()
            {
                _misses++;
            }

            // Метод для вывода информации
            public virtual void Print()
            {
                Console.WriteLine($"Студент: {_name} {_surname}");
                Console.WriteLine($"Пропуски: {_misses}");
                Console.Write("Оценки: ");
                if (_grades.Length > 0)
                {
                    for (int i = 0; i < _grades.Length; i++)
                    {
                        Console.Write(_grades[i] + " ");
                    }
                }
                else
                {
                    Console.Write("нет оценок");
                }
                Console.WriteLine();
            }
        }

        // Класс-наследник Undergraduate
        public class Undergraduate : Student
        {
            
            public Undergraduate(string name, string surname) : base(name, surname)
            {
            }

            // Конструктор, принимающий объект студента 
            public Undergraduate(Student student) : base(student)
            {
            }

            // Метод для отработки пропуска 
            public void WorkOff(int mark)
            {
                if (_misses > 0)
                {
                    // Если есть пропуски - уменьшаем их количество и добавляем оценку
                    _misses--;
                    Lesson(mark);
                }
                else
                {
                    // Если пропусков нет - ищем оценку 2 и заменяем её на новую
                    for (int i = 0; i < _grades.Length; i++)
                    {
                        if (_grades[i] == 2)
                        {
                            _grades[i] = mark;
                            return; // Заменили первую найденную двойку и выходим
                        }
                    }
                    // Если двоек нет, просто добавляем новую оценку
                    Lesson(mark);
                }
            }

            
            public override void Print()
            {
                
                Console.WriteLine($"Имя: {Name} {Surname}");
                Console.WriteLine($"Пропуски: {_misses}");
                Console.Write("Оценки: ");
                if (_grades.Length > 0)
                {
                    for (int i = 0; i < _grades.Length; i++)
                    {
                        Console.Write(_grades[i] + " ");
                    }
                }
                else
                {
                    Console.Write("нет оценок");
                }
                Console.WriteLine();
                Console.WriteLine("=");
            }
        }
    }


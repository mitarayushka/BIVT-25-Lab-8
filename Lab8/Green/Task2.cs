using static Lab8.Green.Task2.Human;

namespace Lab8.Green
{
    public class Task2
    {
        public class Human
        {
            // Поля
            protected string _name;
            protected string _surname;

            // Cвойства
            public string Name => _name;
            public string Surname => _surname;

            // Конструктор
            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            public virtual void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_surname);
            }
        }
        public class Student : Human
        {
            // Поля 
            private int[] _marks;
            private int _count;

            private static int _count_otl;

            // Свойства
            public int[] Marks => _marks.ToArray();
            public static int ExcellentAmount => _count_otl;
            public double AverageMark
            {
                get
                {

                    if (_marks == null || _marks.Length == 0)
                        return 0;

                    double average = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        average += _marks[i];
                    }
                    average /= _marks.Length;
                    return average;
                }
            }
            public bool IsExcellent
            {
                get
                {

                    if (_marks == null || _marks.Length == 0)
                        return false;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        //if (_marks[i] >= 4)
                        //{
                        //    _count_otl++;
                        //}
                        if (_marks[i] < 4)
                            return false;

                    }
                    return true;

                }
            }


            // Конструктор
            public Student(string name, string surname) : base(name, surname)
            {
                _count_otl = 0;
                _marks = new int[4];
            }

            // Методы

            public void Exam(int mark)
            {
                if (mark < 2 || mark > 5)
                    return;
                //if (_count <= _marks.Length)
                //{
                //    _marks[_count++] = mark;
                //}
                if (_count >= 4)
                {
                    return;
                }
                bool wasExcellent = IsExcellent;
                _marks[_count] = mark;
                _count++;
                if (!wasExcellent && IsExcellent && _count >= 4)
                {
                    _count_otl++;
                }
            }
        
            public static void SortByAverageMark(Student[] array)
            {

                if (array == null || array.Length == 0)
                    return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].AverageMark < array[j + 1].AverageMark)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine(string.Join(" ", _marks));
                Console.WriteLine(AverageMark);
                Console.WriteLine(IsExcellent);

            }
        } 
    }
}

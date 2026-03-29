

namespace Lab8.White
{
    public class Task3
    {
        public class Student
        {
            //поля
            private string _name;
            private string _surname;
            protected int _skipped;
            protected int[] _marks;

            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Skipped => _skipped;


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
                    average = average / _marks.Length;
                    return average;
                }
            }
            //конструктор
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;

            }


            //принимает объект студента и копирует его значения в соответствующие поля.
            protected Student(Student student)
            {
                _name = student._name;
                _surname = student._surname;
                _skipped = student._skipped;
                _marks = student._marks;
            }



            //метод
            public void Lesson(int mark)
            {
                if (mark == 0)
                {
                    _skipped++;
                }
                else
                {
                    //если массив не сущ создаем пустой
                    if (_marks == null)
                        _marks = new int[0];

                    Array.Resize(ref _marks, _marks.Length + 1);
                    _marks[_marks.Length - 1] = mark;
                }

            }
            public static void SortBySkipped(Student[] array)
            {
                if (array == null || array.Length == 0)
                    return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].Skipped < array[j].Skipped)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_surname);
                Console.WriteLine(_skipped);
                Console.WriteLine(_marks);
                Console.WriteLine(AverageMark);
            }

        }
        public class Undergraduate : Student
        {

            public Undergraduate(string name, string surname) : base(name, surname)
            {

            }

            public Undergraduate(Student student) : base(student)
            {

            }

            //получить новую оценку по предмету вместо одного пропуска. Если пропусков нет, то заменить
            //имеющуюся оценку 2 на новую оценку
            public void WorkOff(int mark)
            {
                if (_skipped > 0)
                {
                    //есть пропуски, заменяем один пропуск на оценку
                    _skipped--;
                    Lesson(mark);

                }
                else

                {
                    // пропусков нет, ищем оценку 2 и заменяем её
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] == 2)
                        {
                            _marks[i] = mark;
                            return;


                        }
                    }
                }

            }
            public new void Print()
            {

                Console.WriteLine(_skipped);
                Console.WriteLine(_marks);
                Console.WriteLine(WorkOff);

            }

        }


    }
}

using System.Xml.Linq;

namespace Lab8.White
{
    public class Task3
    {
        public class Student
        {
            //Приватные поля
            private string _name;
            private string _surname;
            //Защищенные поля 
            protected int _skipped;
            protected int[] _marks;

            //Свойства 
            public string Name => _name;
            public string Surname => _surname;
            public int Skipped => _skipped;

            //Средний балл 
            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;
                    double w = 0;
                    for (int i = 0; i < _marks.Length; i++) w += _marks[i];
                    w /= _marks.Length;
                    return w;
                }
            }
            //Конструктор 
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }
            //Защищенный конструктор
            protected Student(Student student)
            {
                _name = student._name;
                _surname = student._surname;
                _skipped = student._skipped;
                _marks = student._marks;
            }
            //Методы
            public void Lesson(int mark)
            {
                if (mark == 0) _skipped++;
                else
                {
                    if (_marks == null) _marks = new int[0];
                    //увеличиваем размер массива и добавляем оценку в конец
                    Array.Resize(ref _marks, _marks.Length + 1);
                    _marks[_marks.Length - 1] = mark;
                }
            }
            //Сортирует массив студентов по убыванию количества пропусков
            public static void SortBySkipped(Student[] array)
            {
                if (array == null || array.Length == 0) return;
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
                return;
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
            public void WorkOff(int mark)
            {
                //если есть пропуск, тогда отрабатываем один пропуск
                if (_skipped > 0)
                {
                    _skipped--; //уменьшаем количество пропусков
                    Lesson(mark); //добавляем новую оценку 
                }
                else
                {
                    //пропусков нет, тогда ищем двойку в массиве оценок и меняем её
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] == 2)
                        {
                            _marks[i] = mark; //замена двойки на новую оценку
                            return; //выходим после первой замены
                        }
                    }
                }
            }
            public new void Print()
            {
                return;
            }
        }
    }
}

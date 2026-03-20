namespace Lab8.White
{
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            protected double[] _marks;
            protected int _skipped;

            public string Name => _name;
            public string Surname => _surname;
            public int Skipped => _skipped;

        
            public double AverageMark
            {
                get
                {
                    if (_marks.Length == 0) return 0;

                    double sum= 0;
                    for(int i = 0; i < _marks.Length; i++)
                    {
                        sum += _marks[i];
                    }
                    return sum / _marks.Length;
                }
            }
            protected Student(Student other)
            {
                _name = other._name;
                _surname = other._surname;
                _skipped = other._skipped;
                _marks = new double[other._marks.Length];
                for (int i = 0; i< other._marks.Length; i++)
                {
                    _marks[i]= other._marks[i];
                }
            }
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks  = new double[0];
                _skipped = 0;

            }
            public void Lesson( int mark)
            {
                if (mark == 0)
                {
                    _skipped++;  
                }
                else
                {
                    int currentLength = _marks.Length;
                    Array.Resize(ref _marks, currentLength + 1);
                    _marks[currentLength] = mark;  
                }
            }
            public static void SortBySkipped(Student[] array)
            {
                for ( int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Skipped < array[j + 1].Skipped)
                        {
                            Student temp = array[j];
                            array[j] = array[j+1];
                            array[j+1]=temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Name: {_name}");
                Console.WriteLine($"Surname: {_surname}");
                Console.WriteLine($"Skipped: {_skipped}");
                Console.WriteLine($"AverageMark: {AverageMark}");
                Console.Write("Marks:");
                if ( _marks.Length == 0)
                {
                    Console.WriteLine("No marks");
                }
                else
                {
                    for(int i = 0; i < _marks.Length; i++)
                    {
                        Console.WriteLine($"{i+1} Mark: {_marks[i]}");
                    }
                }
                Console.WriteLine("------------------------");


            }
        }
        public class Undergraduate : Student
        {
            public Undergraduate( string name,string surname): base(name,surname){}
            public Undergraduate(Student student): base(student){}
            public void WorkOff(int mark)
            {
                if (_skipped > 0){
                    _skipped--;
                    Lesson(mark);
                    
                }
                else 
                {
                    for(int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] ==2)
                        {
                            _marks[i]=mark;
                            return;
                        }
                    }
                }
            }
        }
    }
}
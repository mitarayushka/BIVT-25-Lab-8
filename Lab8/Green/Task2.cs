namespace Lab8.Green
{
    public class Task2
    {
        public class Student : Human
        {
            private static int _excellentAmount = 0;
            private int[] _marks;

            public static int ExcellentAmount
            {
                get => _excellentAmount;
            }
            public int[] Marks
            {
                get
                {
                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        copy[i] = _marks[i];
                    }
                    return copy;
                }
            }
            public double AverageMark
            {
                get
                {
                    double sum = 0;
                    int count = 0;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] != 0)
                        {
                            sum += _marks[i];
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return sum / count;
                    }
                }
            }
            public bool IsExcellent
            {
                get
                {
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] < 4)
                            return false;
                    }
                    
                    return true;
                }
            }
            
            
            public Student(string name, string surname) : base(name, surname)
            {
                _marks = new int[4];
            }
            
            public void Exam(int mark)
            {
                bool isExcellent = true;
                for (int i = 0; i < _marks.Length; i++) 
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        
                        break;
                    }
                }

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] < 4)
                    {
                        isExcellent = false;
                    }
                }

                if (isExcellent)
                {
                    _excellentAmount++;
                }
                
                
            }
            
            public static void SortByAverageMark(Student[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].AverageMark < array[j + 1].AverageMark)
                        {
                            Student temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            
            public new void Print()
            {
                return;
            }
        }
        public class Human
        {
            private string _name;
            private string _surname;
    
            public string Name => _name;
            public string Surname => _surname;

            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            public void Print()
            {
                return;
            }
        }
    }
}

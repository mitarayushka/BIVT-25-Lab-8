namespace Lab8.Green
{
    public class Task5
    {
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            public string Name => _name;
            public string Surname => _surname;
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
                        if (_marks[i] >= 2 && _marks[i] <= 5)
                        {
                            sum += _marks[i];
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        return 0;
                    }

                    return sum / count;
                }
            }
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
            }
            public void Exam(int mark)
            {
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        if (mark >= 2 && mark <= 5)
                        {
                            _marks[i] = mark;
                        }
                        break;
                    }
                }
            }
            
            public void Print()
            {
                return;
            }
        }
        
        public class Group
        {
            private string _name;
            protected Student[] _students;
            
            
            public string Name => _name;
            public Student[] Students
            {
                get
                {
                    Student[] copy = new Student[_students.Length];
                    for (int i = 0; i < _students.Length; i++)
                    {
                        copy[i] = _students[i];
                    }
                    return copy;
                }
            }
            public virtual double AverageMark
            {
                get
                {
                    if (_students.Length == 0)
                        return 0;
            
                    double sum = 0;
                    int count = 0;
            
                    // Считаем сумму средних баллов всех студентов
                    for (int i = 0; i < _students.Length; i++)
                    {
                        sum += _students[i].AverageMark;
                        count++;
                    }
            
                    return sum / count;
                }
            }
            
            public Group(string name)
            {
                _name = name;
                _students = new Student[0];
            }
            
            // Метод для добавления ОДНОГО студента
            public void Add(Student student)
            {
                Student[] newArray = new Student[_students.Length + 1];
                
                for (int i = 0; i < _students.Length; i++)
                {
                    newArray[i] = _students[i];
                }
                newArray[_students.Length] = student;
                
                _students = newArray;
            }
            // Метод для добавления НЕСКОЛЬКИХ студентов
            public void Add(Student[] students)
            {
                Student[] newArray = new Student[_students.Length + students.Length];
                
                for (int i = 0; i < _students.Length; i++)
                {
                    newArray[i] = _students[i];
                }
                
                for (int i = 0; i < students.Length; i++)
                {
                    newArray[_students.Length + i] = students[i];
                }
                
                _students = newArray;
            }
            public static void SortByAverageMark(Group[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].AverageMark < array[j + 1].AverageMark)
                        {
                            Group temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                return;
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string name): base(name)
            {
                

            }
            public override double AverageMark
            {
                get
                {
                    if (_students.Length == 0)
                        return 0;
            
                    double sum = 0;
                    double count = 0;
                    
                    for (int i = 0; i < _students.Length; i++)
                    {
                        for (int j = 0; j < _students[i].Marks.Length; j++)
                        {
                            if (_students[i].Marks[j] == 5)
                            {
                                sum += _students[i].Marks[j] * 1;
                                count += 1;
                            }
                            else if (_students[i].Marks[j] == 4)
                            {
                                sum += _students[i].Marks[j] * 1.5;
                                count += 1.5;
                            }
                            else if (_students[i].Marks[j] == 3)
                            {
                                sum += _students[i].Marks[j] * 2;
                                count += 2;
                            }
                            else if (_students[i].Marks[j] == 2)
                            {
                                sum += _students[i].Marks[j] * 2.5;
                                count += 2.5;
                            }
                            
                        }
                        
                        
                    }
            
                    return sum / count;
                }
            }
        }

        public class SpecialGroup : Group
        {
            public SpecialGroup(string name) : base(name)
            {
                
            }
            public override double AverageMark
            {
                get
                {
                    if (_students.Length == 0)
                        return 0;
            
                    double sum = 0;
                    double count = 0;
                    
                    for (int i = 0; i < _students.Length; i++)
                    {
                        for (int j = 0; j < _students[i].Marks.Length; j++)
                        {
                            if (_students[i].Marks[j] == 5)
                            {
                                sum += _students[i].Marks[j] * 1;
                                count += 1;
                            }
                            else if (_students[i].Marks[j] == 4)
                            {
                                sum +=   _students[i].Marks[j] * 0.75;
                                count += 0.75;
                            }
                            else if (_students[i].Marks[j] == 3)
                            {
                                sum += _students[i].Marks[j] * 0.5;
                                count += 0.5;
                            }
                            else if (_students[i].Marks[j] == 2)
                            {
                                sum += _students[i].Marks[j] * 0.25;
                                count += 0.25;
                            }
                        }
                    }
            
                    return sum / count;
                }
            }
        }
    }
}

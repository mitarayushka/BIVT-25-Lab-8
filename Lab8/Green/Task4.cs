namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _isExpelled;
            private int _id;
            
            private static int _nextid;
            
            public string Name => _name;
            public string Surname => _surname; 
            public int ID => _id;
            
            
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
            public bool IsExpelled => _isExpelled;
            public double AverageMark
            {
                get
                {
                    double sum = 0;
                    int count = 0;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > 0)
                        {
                            sum += _marks[i];
                            count++;
                        }
                    }

                    if (count == 0)
                        return 0;

                    return sum / count;
                }
            }

            static Student()
            {
                _nextid = 1;
            }
            
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _isExpelled = false;
                _id = _nextid;
                _nextid ++;
            }

            public void Restore()
            {
                _isExpelled = false;
            }
            
            public void Exam(int mark)
            {
                if (_isExpelled)
                {
                    return;
                }

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;

                        if (mark == 2)
                            _isExpelled = true;

                        break;
                    }
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
            public void Print()
            {
                Console.WriteLine("Имя: " + _name);
                Console.WriteLine("Фамилия: " + _surname);
                Console.WriteLine("Отчислен: " + _isExpelled);
                Console.WriteLine("Оценки: " + _marks[0] + " " + _marks[1] + " " + _marks[2]);
                Console.WriteLine("Средний балл: " + AverageMark);
                Console.WriteLine();
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
                    {
                        if (students[j].ID > students[j + 1].ID)
                        {
                            Student temp = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = temp;
                        }
                    }
                }
            }

            public static Student[] Expel(ref Student[] students)
            {
                Student[] isExcpelleledStudents;
                Student[] isNotExpelledStudents;
                int countIsExpelled = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled)
                    {
                        countIsExpelled++;
                    }
                }
                isExcpelleledStudents = new Student[countIsExpelled];
                isNotExpelledStudents = new Student[students.Length - countIsExpelled];
                int j = 0;
                int k = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled)
                    {
                        isExcpelleledStudents[j] = students[i];
                        j++;
                    }
                    else
                    {
                        isNotExpelledStudents[k] = students[i];
                        k++;
                    }
                }
                students = isNotExpelledStudents;
                return isExcpelleledStudents;
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                if (!restored.IsExpelled)
                {
                    return;
                }
                
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].ID == restored.ID)
                    {
                        return;
                    }
                }
                
                restored.Restore();
                
                Student[] newArray = new Student[students.Length + 1];
                
                int insertIndex = 0;
                
                while (insertIndex < students.Length && students[insertIndex].ID < restored.ID)
                {
                    newArray[insertIndex] = students[insertIndex];
                    insertIndex++;
                }
                
                newArray[insertIndex] = restored;
                
                for (int i = insertIndex; i < students.Length; i++)
                {
                    newArray[i + 1] = students[i];
                }
                
                students = newArray;
            }
        }
    }
}

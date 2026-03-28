using System;

namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            private static int _nextId;
            private int _id;
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _isExpelled;

            public int ID => _id;
            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        copy[i] = _marks[i];
                    }
                    return copy;
                }
            }
            public bool IsExpelled => _isExpelled;

            static Student()
            {
                _nextId = 1;
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _isExpelled = false;
                _id = _nextId++;
            }

            public double AverageMark
            {
                get
                {
                    if (_marks == null) return 0;

                    int sum = 0;
                    int count = 0;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] != 0)
                        {
                            sum += _marks[i];
                            count++;
                        }
                    }
                    if (count == 0) return 0;

                    return (double)sum / count;
                }
            }

            public void Exam(int mark)
            {
                if (_marks == null) return;
                if (_isExpelled) return;

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        if (mark == 2)
                        {
                            _isExpelled = true;
                        }
                        break;
                    }
                }
            }

            public void Restore()
            {
                _isExpelled = false;
            }

            public static void SortByAverageMark(Student[] array)
            {
                if (array == null) return;

                int n = array.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
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
                Console.WriteLine($"ID: {_id}, Имя: {_name}, Фамилия: {_surname}, Средний балл: {AverageMark}, Отчислен: {_isExpelled}");
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                if (students == null) return;

                int n = students.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
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
                if (students == null) return new Student[0];

                int expelledCount = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null && students[i].IsExpelled)
                    {
                        expelledCount++;
                    }
                }

                Student[] expelled = new Student[expelledCount];
                int expelledIndex = 0;

                int activeCount = students.Length - expelledCount;
                Student[] active = new Student[activeCount];
                int activeIndex = 0;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null)
                    {
                        if (students[i].IsExpelled)
                        {
                            expelled[expelledIndex] = students[i];
                            expelledIndex++;
                        }
                        else
                        {
                            active[activeIndex] = students[i];
                            activeIndex++;
                        }
                    }
                }

                students = active;
                return expelled;
            }

            
            public static void Restore(ref Student[] students, Student restored)
            {
                if (students == null || restored == null) return;

                
                restored.Restore();

                
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null && students[i].ID == restored.ID)
                    {
                        return; 
                    }
                }

                 
                Student[] newArray = new Student[students.Length + 1];
                int index = 0;
                bool inserted = false;

                
                for (int i = 0; i < students.Length; i++)
                {
                    
                    if (!inserted && students[i] != null && restored.ID < students[i].ID)
                    {
                        newArray[index] = restored;
                        index++;
                        inserted = true;
                    }

                    if (students[i] != null)
                    {
                        newArray[index] = students[i];
                        index++;
                    }
                }

                
                if (!inserted)
                {
                    newArray[index] = restored;
                }

                students = newArray;
            }
        }
    }
}

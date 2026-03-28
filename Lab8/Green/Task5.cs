using System;

namespace Lab8.Green
{
    public class Task5
    {
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
            }

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
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}, Средний балл: {AverageMark}");
            }
        }

        public class Group
        {
            private string _name;
            private Student[] _students;

            public Group(string name)
            {
                _name = name;
                _students = new Student[0];
            }

            public string Name => _name;

            public Student[] Students
            {
                get
                {
                    if (_students == null) return null;
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
                    if (_students == null || _students.Length == 0) return 0;

                    double sum = 0;
                    int count = 0;

                    for (int i = 0; i < _students.Length; i++)
                    {
                        double studentAvg = _students[i].AverageMark;
                        if (studentAvg > 0)
                        {
                            sum += studentAvg;
                            count++;
                        }
                    }

                    if (count == 0) return 0;
                    return sum / count;
                }
            }

            public void Add(Student student)
            {
                if (_students == null)
                    _students = new Student[0];

                Student[] newArray = new Student[_students.Length + 1];
                for (int i = 0; i < _students.Length; i++)
                {
                    newArray[i] = _students[i];
                }
                newArray[_students.Length] = student;
                _students = newArray;
            }

            public void Add(Student[] students)
            {
                if (students == null) return;
                if (_students == null)
                    _students = new Student[0];

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
                if (array == null) return;

                int n = array.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
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
                Console.WriteLine($"Группа: {_name}, Средний балл: {AverageMark}, Студентов: {_students?.Length ?? 0}");
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string name) : base(name)
            {
            }

            public override double AverageMark
            {
                get
                {
                    Student[] students = Students;
                    if (students == null || students.Length == 0) return 0;

                    double totalWeightedSum = 0;
                    double totalWeight = 0;

                    for (int i = 0; i < students.Length; i++)
                    {
                        int[] marks = students[i].Marks;
                        if (marks == null) continue;

                        for (int j = 0; j < marks.Length; j++)
                        {
                            if (marks[j] != 0)
                            {
                                
                                double weight = 3.5 - marks[j] * 0.5;
                                totalWeightedSum += marks[j] * weight;
                                totalWeight += weight;
                            }
                        }
                    }

                    if (totalWeight == 0) return 0;
                    return totalWeightedSum / totalWeight;
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
                    Student[] students = Students;
                    if (students == null || students.Length == 0) return 0;

                    double totalWeightedSum = 0;
                    double totalWeight = 0;

                    for (int i = 0; i < students.Length; i++)
                    {
                        int[] marks = students[i].Marks;
                        if (marks == null) continue;

                        for (int j = 0; j < marks.Length; j++)
                        {
                            if (marks[j] != 0)
                            {
                                
                                double weight = (marks[j] - 1) * 0.25;
                                totalWeightedSum += marks[j] * weight;
                                totalWeight += weight;
                            }
                        }
                    }

                    if (totalWeight == 0) return 0;
                    return totalWeightedSum / totalWeight;
                }
            }
        }
    }
}

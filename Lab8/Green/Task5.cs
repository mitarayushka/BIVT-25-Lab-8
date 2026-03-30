namespace Lab8.Green
{
    public class Task5
    {
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private int _markCounter;

            public string Name => _name ?? string.Empty;
            public string Surname => _surname ?? string.Empty;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return new int[0];
                    var result = new int[_markCounter];
                    Array.Copy(_marks, result, _markCounter);
                    return result;
                }
            }

            public double AverageMark
            {
                get
                {
                    if (_marks == null || _markCounter == 0) return 0;

                    double sum = 0;
                    for (int i = 0; i < _markCounter; i++)
                    {
                        sum += _marks[i];
                    }
                    return sum / _markCounter;
                }
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[10];
                _markCounter = 0;
            }

            public void Exam(int mark)
            {
                if (mark < 2 || mark > 5) return;
                if (_markCounter < _marks.Length)
                {
                    _marks[_markCounter++] = mark;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {_name}");
                Console.WriteLine($"Surname: {_surname}");
                Console.WriteLine($"AverageMark: {AverageMark:F2}");
            }
        }
        public class Group
        {
            protected string _name;
            protected Student[] _students;
            protected int _studentCount;

            public string Name => _name ?? string.Empty;

            public Student[] Students => _students.ToArray();


            public virtual double AverageMark
            {
                get
                {
                    if (_studentCount == 0) return 0;
                    double sum = 0;
                    int validStudents = 0;

                    for (int i = 0; i < _studentCount; i++)
                    {
                        double avg = _students[i].AverageMark;
                        if (avg > 0)
                        {
                            sum += avg;
                            validStudents++;
                        }
                    }

                    if (validStudents == 0) return 0;
                    return sum / validStudents;
                }
            }

            public Group(string name)
            {
                _name = name;
                _students = new Student[20];
                _studentCount = 0;
            }

            public void Add(Student student)
            {
                if (_studentCount < _students.Length)
                {
                    _students[_studentCount++] = student;
                }
            }

            public void Add(Student[] students)
            {
                if (students == null) return;
                foreach (var student in students)
                {
                    Add(student);
                }
            }

            public static void SortByAverageMark(Group[] array)
            {
                if (array == null || array.Length == 0) return;

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

            public virtual void Print()
            {
                Console.WriteLine($"Group: {_name}");
                Console.WriteLine($"Average mark: {AverageMark:F2}");
                Console.WriteLine($"Students count: {_studentCount}");
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string goup) : base(goup) { }
                                
            public override double AverageMark
            {
                get
                {
                    double totalMarks = 0;
                    double totalWeigth = 0;

                    for (int i = 0; i < Students.Length; i++)
                    {
                        if (Students[i].Marks.Length == 0) continue;
                        for (int j = 0; j < Students[i].Marks.Length; j++)
                        {
                            if (Students[i].Marks[j] == 5)
                            {
                                totalMarks += Students[i].Marks[j] * 1.0;
                                totalWeigth += 1.0;
                            }

                            if (Students[i].Marks[j] == 4)
                            {
                                totalMarks += Students[i].Marks[j] * 1.5;
                                totalWeigth += 1.5;
                            }

                            if (Students[i].Marks[j] == 3)
                            {
                                totalMarks += Students[i].Marks[j] * 2.0;
                                totalWeigth += 2.0;
                            }

                            if (Students[i].Marks[j] == 2)
                            {
                                totalMarks += Students[i].Marks[j] * 2.5;
                                totalWeigth += 2.5;
                            }
                        }
                    }
                    return totalMarks / totalWeigth;
                }
            }
        }

        public class SpecialGroup : Group
        {
            public SpecialGroup(string goup) : base(goup) { }
            
            public override double AverageMark
            {
                get
                {
                    double totalMarks = 0;
                    double totalWeigth = 0;

                    for (int i = 0; i < Students.Length; i++)
                    {
                        for (int j = 0; j < Students[i].Marks.Length; j++)
                        {
                            if (Students[i].Marks[j] == 5)
                            {
                                totalMarks += Students[i].Marks[j] * 1.0;
                                totalWeigth += 1.0;
                            }

                            if (Students[i].Marks[j] == 4)
                            {
                                totalMarks += Students[i].Marks[j] * 0.75;
                                totalWeigth += 0.75;
                            }

                            if (Students[i].Marks[j] == 3)
                            {
                                totalMarks += Students[i].Marks[j] * 0.5;
                                totalWeigth += 0.5;
                            }

                            if (Students[i].Marks[j] == 2)
                            {
                                totalMarks += Students[i].Marks[j] * 0.25;
                                totalWeigth += 0.25;
                            }
                        }
                    }
                    return totalMarks / totalWeigth;
                }
            }
        }
    }
}

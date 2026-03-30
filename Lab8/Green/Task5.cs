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
            public int[] Marks => _marks.ToArray();

            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;

                    double average = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        average += _marks[i];
                    }
                    average /= _marks.Length;
                    return average;
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
            private Student[] _students;
            public string Name => _name;
            public Student[] Students => _students.ToArray();

            public virtual double AverageMark
            {
                get
                {
                    if (_students == null || _students.Length == 0) return 0;

                    double summ = 0;
                    int count = 0;

                    for (int i = 0; i < _students.Length; i++)
                    {
                        summ += _students[i].AverageMark * 5;
                        count += 5;
                    }

                    return summ / count;
                }
            }

            public Group(string name)
            {
                _name = name;
                _students = new Student[0];
            }

            public void Add(Student student)
            {
                Student[] newArray = new Student[_students.Length + 1];

                for (int i = 0; i < _students.Length; i++)
                {
                    newArray[i] = _students[i];
                }

                newArray[newArray.Length - 1] = student;
                _students = newArray;
            }

            public void Add(Student[] students)
            {
                if (students == null || students.Length == 0)
                    return;

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
                if (array == null || array.Length == 0)
                {
                    return;
                }

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i].AverageMark < array[j].AverageMark)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
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

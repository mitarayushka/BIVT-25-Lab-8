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
                _name = name == null ? "" : name;
                _surname = surname == null ? "" : surname;
                _marks = new int[5];
            }

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }

            public int[] Marks
            {
                get
                {
                    if (_marks == null)
                        return new int[0];

                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                        copy[i] = _marks[i];
                    return copy;
                }
            }

            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0)
                        return 0;

                    int sum = 0;
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

                    return (double)sum / count;
                }
            }

            public void Exam(int mark)
            {
                if (_marks == null)
                    return;

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        return;
                    }
                }
            }

            public void Print()
            {
                System.Console.WriteLine($"{Name} {Surname} {AverageMark}");
            }
        }

        public class Group
        {
            private string _name;
            private Student[] _students;

            public Group(string name)
            {
                _name = name == null ? "" : name;
                _students = new Student[0];
            }

            public string Name { get { return _name; } }
            public Student[] Students { get { return _students; } }

            public virtual double AverageMark
            {
                get
                {
                    if (_students == null || _students.Length == 0)
                        return 0;

                    double sum = 0;
                    for (int i = 0; i < _students.Length; i++)
                        sum += _students[i].AverageMark;

                    return sum / _students.Length;
                }
            }

            public void Add(Student elem)
            {
                if (_students == null)
                    _students = new Student[0];

                Student[] newArr = new Student[_students.Length + 1];
                for (int i = 0; i < _students.Length; i++)
                    newArr[i] = _students[i];
                newArr[newArr.Length - 1] = elem;
                _students = newArr;
            }

            public void Add(Student[] array)
            {
                if (array == null)
                    return;

                if (_students == null)
                    _students = new Student[0];

                Student[] newArr = new Student[_students.Length + array.Length];
                for (int i = 0; i < _students.Length; i++)
                    newArr[i] = _students[i];
                for (int i = 0; i < array.Length; i++)
                    newArr[_students.Length + i] = array[i];
                _students = newArr;
            }

            public static void SortByAverageMark(Group[] array)
            {
                if (array == null)
                    return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        double left = array[j] == null ? -1 : array[j].AverageMark;
                        double right = array[j + 1] == null ? -1 : array[j + 1].AverageMark;

                        if (left < right)
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
                System.Console.WriteLine($"{Name} {AverageMark}");
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string name) : base(name)
            {
            }

            private double GetWeight(int mark)
            {
                if (mark == 5) return 1;
                if (mark == 4) return 1.5;
                if (mark == 3) return 2;
                if (mark == 2) return 2.5;
                return 0;
            }

            public override double AverageMark
            {
                get
                {
                    Student[] students = Students;
                    if (students == null || students.Length == 0)
                        return 0;

                    double sum = 0;
                    double weightSum = 0;

                    for (int i = 0; i < students.Length; i++)
                    {
                        int[] marks = students[i].Marks;
                        if (marks == null)
                            continue;

                        for (int j = 0; j < marks.Length; j++)
                        {
                            if (marks[j] == 0)
                                continue;

                            double weight = GetWeight(marks[j]);
                            sum += marks[j] * weight;
                            weightSum += weight;
                        }
                    }

                    if (weightSum == 0)
                        return 0;

                    return sum / weightSum;
                }
            }
        }

        public class SpecialGroup : Group
        {
            public SpecialGroup(string name) : base(name)
            {
            }

            private double GetWeight(int mark)
            {
                if (mark == 5) return 1;
                if (mark == 4) return 0.75;
                if (mark == 3) return 0.5;
                if (mark == 2) return 0.25;
                return 0;
            }

            public override double AverageMark
            {
                get
                {
                    Student[] students = Students;
                    if (students == null || students.Length == 0)
                        return 0;

                    double sum = 0;
                    double weightSum = 0;

                    for (int i = 0; i < students.Length; i++)
                    {
                        int[] marks = students[i].Marks;
                        if (marks == null)
                            continue;

                        for (int j = 0; j < marks.Length; j++)
                        {
                            if (marks[j] == 0)
                                continue;

                            double weight = GetWeight(marks[j]);
                            sum += marks[j] * weight;
                            weightSum += weight;
                        }
                    }

                    if (weightSum == 0)
                        return 0;

                    return sum / weightSum;
                }
            }
        }
    }
}

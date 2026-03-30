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
                        return;
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
                    {
                        return 0;
                    }

                    double totalSum = 0;
                    int totalCount = 0;

                    for (int i = 0; i < _students.Length; i++)
                    {
                        totalSum += _students[i].AverageMark;
                        totalCount++;
                    }

                    return totalSum / totalCount;
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
                {
                    return;
                }

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
                    if (students.Length == 0)
                    {
                        return 0;
                    }

                    double totalWeightedSum = 0;
                    double totalWeight = 0;

                    for (int i = 0; i < students.Length; i++)
                    {
                        int[] marks = students[i].Marks;
                        double weightedSum = 0;
                        double weightSum = 0;

                        for (int j = 0; j < marks.Length; j++)
                        {
                            int mark = marks[j];
                            
                            if (mark >= 2 && mark <= 5)
                            {
                                double weight;
                                if (mark == 5)
                                {
                                    weight = 1;
                                }
                                else if (mark == 4)
                                {
                                    weight = 1.5;
                                }
                                else if (mark == 3)
                                {
                                    weight = 2;
                                }
                                else
                                {
                                    weight = 2.5;
                                }

                                weightedSum += mark * weight;
                                weightSum += weight;
                            }
                        }

                        if (weightSum > 0)
                        {
                            totalWeightedSum += weightedSum;
                            totalWeight += weightSum;
                        }
                    }

                    if (totalWeight == 0)
                    {
                        return 0;
                    }
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
                    if (students.Length == 0)
                    {
                        return 0;
                    }

                    double totalWeightedSum = 0;
                    double totalWeight = 0;

                    for (int i = 0; i < students.Length; i++)
                    {
                        int[] marks = students[i].Marks;
                        double weightedSum = 0;
                        double weightSum = 0;

                        for (int j = 0; j < marks.Length; j++)
                        {
                            int mark = marks[j];
                            if (mark >= 2 && mark <= 5)
                            {
                                double weight;
                                if (mark == 5)
                                {
                                    weight = 1;
                                }
                                else if (mark == 4)
                                {
                                    weight = 0.75;
                                }
                                else if (mark == 3)
                                {
                                    weight = 0.5;
                                }
                                else
                                {
                                    weight = 0.25;
                                }

                                weightedSum += mark * weight;
                                weightSum += weight;
                            }
                        }

                        if (weightSum > 0)
                        {
                            totalWeightedSum += weightedSum;
                            totalWeight += weightSum;
                        }
                    }

                    if (totalWeight == 0)
                    {
                        return 0;
                    }
                    
                    return totalWeightedSum / totalWeight;
                }
            }
        }
    }
}

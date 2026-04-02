using System.Collections;

namespace Lab8.White
{
    public class Task3
    {
        public class Student
        {
            protected string name;
            protected string surname;
            protected List<int> _marks;
            protected int _skipped;

            public string Name => name;
            public string Surname => surname;
            public int Skipped => _skipped;
            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Count == 0)
                        return 0;

                    double sum = 0;
                    foreach (int mark in _marks)
                    {
                        sum += mark;
                    }
                    return sum / _marks.Count;
                }
            }

            public Student(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                _marks = new List<int>();
                _skipped = 0;
            }

            // 受保护的复制构造函数
            protected Student(Student other)
            {
                if (other == null) return;

                this.name = other.name;
                this.surname = other.surname;
                this._marks = new List<int>(other._marks);
                this._skipped = other._skipped;
            }

            public void Lesson(int mark)
            {
                if (mark == 0)
                {
                    _skipped++;
                }
                else
                {
                    _marks.Add(mark);
                }
            }

            public static void SortBySkipped(Student[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Skipped < array[j + 1].Skipped)
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
                Console.WriteLine($"Name: {name}, Surname: {surname}, Skipped: {_skipped}, AverageMark: {AverageMark:F2}");
            }
        }

        public class Undergraduate : Student
        {
            // 构造函数：接受姓名并传递给基类
            public Undergraduate(string name, string surname) : base(name, surname)
            {
            }

            // 构造函数：接受 Student 对象并传递给基类的复制构造函数
            public Undergraduate(Student student) : base(student)
            {
            }

            // 方法：用新成绩代替一个缺勤
            public void WorkOff(int mark)
            {
                if (_skipped > 0)
                {
                    // 如果有缺勤，减少一次缺勤并添加新成绩
                    _skipped--;
                    Lesson(mark);
                }
                else
                {
                    // 如果没有缺勤，查找并替换第一个2分成绩
                    for (int i = 0; i < _marks.Count; i++)
                    {
                        if (_marks[i] == 2)
                        {
                            _marks[i] = mark;
                            return;
                        }
                    }
                    // 如果没有找到2分成绩，不做任何操作
                }
            }

            // 重写 Print 方法，显示本科生信息
            public new void Print()
            {
                Console.WriteLine($"Undergraduate - Name: {name}, Surname: {surname}, Skipped: {_skipped}, AverageMark: {AverageMark:F2}");
            }
        }
    }
}

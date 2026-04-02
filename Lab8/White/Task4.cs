using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8.White
{
    public class Task4
    {
        // Human 类
        public class Human
        {
            private string name;
            private string surname;

            public string Name => name;
            public string Surname => surname;

            public Human(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
            }

            // 虚方法 Print
            public virtual void Print()
            {
                Console.WriteLine($"Name: {name}, Surname: {surname}");
            }
        }

        // Participant 继承自 Human
        public class Participant : Human
        {
            private List<double> scores;

            // 静态字段：统计参与者总数
            private static int _count;

            // 静态只读属性
            public static int Count => _count;

            public double[] Scores
            {
                get
                {
                    if (scores == null || scores.Count == 0)
                        return Array.Empty<double>();

                    // 返回副本以实现隔离
                    return scores.ToArray();
                }
            }

            public double TotalScore
            {
                get
                {
                    if (scores == null || scores.Count == 0)
                        return 0;

                    double sum = 0;
                    foreach (double score in scores)
                    {
                        sum += score;
                    }
                    return sum;
                }
            }

            // 静态构造函数：初始化总数为 0
            static Participant()
            {
                _count = 0;
            }

            // 构造函数：接受姓名
            public Participant(string name, string surname) : base(name, surname)
            {
                scores = new List<double>();
                _count++;
            }

            public void PlayMatch(double result)
            {
                if (scores == null)
                    scores = new List<double>();

                scores.Add(result);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            // 重写 Print 方法
            public override void Print()
            {
                Console.WriteLine($"Name: {Name}, Surname: {Surname}, TotalScore: {TotalScore}");
            }
        }
    }
}

using System;

 public class Task4
    {
        public class Human
        {
            protected string _name;
            protected string _surname;

            public string Name => _name;
            public string Surname => _surname;

            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            public virtual void Print()
            {
                Console.WriteLine($"Имя: {_name}");
                Console.WriteLine($"Фамилия: {_surname}");
            }
        }

        public class Participant : Human
        {
            private double[] _scores;
            private int _scoresCount;
            private static int _count;

            public static int Count => _count;

            public double[] Scores
            {
                get
                {
                    double[] result = new double[_scoresCount];
                    for (int i = 0; i < _scoresCount; i++)
                    {
                        result[i] = _scores[i];
                    }
                    return result;
                }
            }

            public double TotalScore
            {
                get
                {
                    double sum = 0;
                    for (int i = 0; i < _scoresCount; i++)
                    {
                        sum += _scores[i];
                    }
                    return sum;
                }
            }

            static Participant()
            {
                _count = 0;
            }

            public Participant(string name, string surname) : base(name, surname)
            {
                _scores = new double[100];
                _scoresCount = 0;
                _count++;
            }

            public void PlayMatch(double result)
            {
                if (_scoresCount < _scores.Length)
                {
                    _scores[_scoresCount] = result;
                    _scoresCount++;
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public override void Print()
            {
                base.Print();
                Console.WriteLine($"Очки за матчи: {TotalScore}");
            }
        }
    }


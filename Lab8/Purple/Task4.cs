using System;
using System.Linq;

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _hasTime;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0.0;
                _hasTime = false;
            }

            public void Run(double time)
            {
                if (_hasTime) return;
                _time = time;
                _hasTime = true;
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null || array.Length <= 1)
                {
                    return;
                }

                int n = array.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (array[j].Time > array[j + 1].Time)
                        {                       
                            Sportsman temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {_time:F4} сек");
            }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }
            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group other)
            {
                _name = other.Name;
                _sportsmen = new Sportsman[other.Sportsmen.Length];
                for (int i = 0; i < other.Sportsmen.Length; i++)
                {
                    _sportsmen[i] = other.Sportsmen[i];
                }
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null)
                {
                    _sportsmen = new Sportsman[1];
                    _sportsmen[0] = sportsman;
                    return;
                }
                Sportsman[] newSportsmen = new Sportsman[_sportsmen.Length + 1];
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    newSportsmen[i] = _sportsmen[i];
                }
                newSportsmen[_sportsmen.Length] = sportsman;
                _sportsmen = newSportsmen;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || sportsmen.Length == 0)
                {
                    return;
                }

                Sportsman[] newSportsmen = new Sportsman[_sportsmen.Length + sportsmen.Length];

                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    newSportsmen[i] = _sportsmen[i];
                }

                for (int i = 0; i < sportsmen.Length; i++)
                {
                    newSportsmen[_sportsmen.Length + i] = sportsmen[i];
                }

                _sportsmen = newSportsmen;
            }

            public void Add(Group group)
            {
                Add(group.Sportsmen);
            }

            public void Sort()
            {
                Sportsman.Sort(_sportsmen);
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                int menCount = 0;
                int womenCount = 0;

                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        menCount++;
                    }
                    else if (_sportsmen[i] is SkiWoman)
                    {
                        womenCount++;
                    }
                }

                men = new Sportsman[menCount];
                women = new Sportsman[womenCount];

                int menIndex = 0;
                int womenIndex = 0;

                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        men[menIndex] = _sportsmen[i];
                        menIndex++;
                    }
                    else if (_sportsmen[i] is SkiWoman)
                    {
                        women[womenIndex] = _sportsmen[i];
                        womenIndex++;
                    }
                }
            }

            public void Shuffle()
            {
                Split(out var men, out var women);
                Sportsman.Sort(men);
                Sportsman.Sort(women);

                int total = _sportsmen.Length;
                var shuffled = new Sportsman[total];
                int mIdx = 0, wIdx = 0, k = 0;

                bool nextIsMan = true;
                if (men.Length > 0 && women.Length > 0)
                {
                    if (women[0].Time < men[0].Time) nextIsMan = false;
                }
                else if (women.Length > 0) nextIsMan = false;

                while (k < total)
                {
                    if (nextIsMan && mIdx < men.Length)
                    {
                        shuffled[k++] = men[mIdx++];
                        if (wIdx < women.Length) nextIsMan = false;
                    }
                    else if (!nextIsMan && wIdx < women.Length)
                    {
                        shuffled[k++] = women[wIdx++];
                        if (mIdx < men.Length) nextIsMan = true;
                    }
                    else
                    {
                        if (mIdx < men.Length) shuffled[k++] = men[mIdx++];
                        else if (wIdx < women.Length) shuffled[k++] = women[wIdx++];
                    }
                }
                _sportsmen = shuffled;
            }

            public static Group Merge(Group group1, Group group2)
            {
                var result = new Group("Финалисты");
                result.Add(group1);
                result.Add(group2);
                result.Sort();
                return result;
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {_name}");
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i].Print();
                }
            }
        }
    }
}

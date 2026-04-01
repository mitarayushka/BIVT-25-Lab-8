using System.ComponentModel.Design;

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            //поля

            private string _name;
            private string _surname;
            private double _time;
            private bool _installed = false;

            //свойства

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            //конструктор

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            //методы

            public void Run(double time)
            {
                if (_installed)
                {
                    return;
                }
                _time = time;
                _installed = true;
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null || array.Length <= 1)
                {
                    return;
                }
                int i = 1, j = 2;
                while (i < array.Length)
                {
                    if (i == 0 || array[i - 1].Time < array[i].Time)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        (array[i], array[i - 1]) = (array[i - 1], array[i]);
                        i--;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Time}");
            }
        }





        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname){}
            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname){}
            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }







        public class Group
        {
            //поля

            private string _name;
            private Sportsman[] _sportsmen;

            //свойства

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            //конструкторы

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group other)
            {
                _name = other.Name;
                if (other.Sportsmen == null)
                {
                    _sportsmen = new Sportsman[0];
                    return;
                }
                int n = other.Sportsmen.Length;
                _sportsmen = new Sportsman[n];
                for (int i = 0; i < n; i++)
                {
                    _sportsmen[i] = other.Sportsmen[i];
                }

            }

            //методы

            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[^1] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || sportsmen.Length == 0)
                {
                    return;
                }
                int n = _sportsmen.Length;
                Array.Resize(ref _sportsmen, _sportsmen.Length + sportsmen.Length);
                for(int i = 0; i < sportsmen.Length; i++)
                {
                    _sportsmen[n + i] = sportsmen[i];
                }
            }

            public void Add(Group other)
            {
                if (other._sportsmen == null || other._sportsmen.Length == 0)
                {
                    return;
                }
                int n = _sportsmen.Length;
                Array.Resize(ref _sportsmen, _sportsmen.Length + other._sportsmen.Length);
                for(int i = 0; i < other._sportsmen.Length; i++)
                {
                    _sportsmen[n + i] = other._sportsmen[i];
                }
            }

            public void Sort()
            {
                if (_sportsmen == null || _sportsmen.Length <= 1)
                {
                    return;
                }
                int i = 1;
                int j = 2;
                while (i < _sportsmen.Length)
                {
                    if (i == 0 || _sportsmen[i - 1].Time < _sportsmen[i].Time)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        (_sportsmen[i], _sportsmen[i - 1]) = (_sportsmen[i - 1], _sportsmen[i]);
                        i--;
                    }
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group res = new Group("Финалисты");
                res.Add(group1);
                res.Add(group2);
                res.Sort();
                return res;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                if (_sportsmen == null || _sportsmen.Length == 1)
                {
                    men = new Sportsman[0];
                    women = new Sportsman[0];
                    return;
                }
                (men, women) = (new Sportsman[0], new Sportsman[0]);
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        Array.Resize(ref men, men.Length + 1);
                        men[^1] = _sportsmen[i];
                    }
                    else if (_sportsmen[i] is SkiWoman)
                    {
                        Array.Resize(ref women, women.Length + 1);
                        women[^1] = _sportsmen[i];
                    }
                }

            }


            public void Shuffle()
            {
                _sportsmen = new Sportsman[0];
                int minLen;
                Sportsman[] men; Sportsman[] women;
                Split(out men, out women);
                Group groupMen = new Group("men");
                groupMen.Add(men);
                groupMen.Sort();
                Group groupWomen = new Group("women");
                groupWomen.Add(women);
                groupWomen.Sort();


                if ((groupWomen.Sportsmen == null || groupWomen.Sportsmen.Length == 0 ) &&  (groupMen.Sportsmen == null || groupMen.Sportsmen.Length == 0))
                {
                    return;
                }
                else if (groupWomen.Sportsmen == null || groupWomen.Sportsmen.Length == 0)
                {
                    Add(groupMen);
                }
                else if(groupMen.Sportsmen == null || groupMen.Sportsmen.Length == 0)
                {
                    Add(groupWomen);
                }
                else
                {
                    if (groupWomen.Sportsmen[0].Time <= groupMen.Sportsmen[0].Time)
                    {
                        if (groupWomen.Sportsmen.Length >= groupMen.Sportsmen.Length)
                        {
                            minLen = groupMen.Sportsmen.Length;
                            for (int i = 0; i < minLen; i++)
                            {
                                Add(groupWomen.Sportsmen[i]);
                                Add(groupMen.Sportsmen[i]);
                            }
                            for (int i = minLen; i < groupWomen.Sportsmen.Length; i++)
                            {
                                Add(groupWomen.Sportsmen[i]);
                            }
                        }
                        else
                        {
                            minLen = groupWomen.Sportsmen.Length;
                            for (int i = 0; i < minLen; i++)
                            {
                                Add(groupWomen.Sportsmen[i]);
                                Add(groupMen.Sportsmen[i]);
                            }
                            for (int i = minLen; i < groupMen.Sportsmen.Length; i++)
                            {
                                Add(groupMen.Sportsmen[i]);
                            }
                        }
                    }

                    else
                    {
                        if (groupWomen.Sportsmen.Length >= groupMen.Sportsmen.Length)
                        {
                            minLen = groupMen.Sportsmen.Length;
                            for (int i = 0; i < minLen; i++)
                            {
                                Add(groupMen.Sportsmen[i]);
                                Add(groupWomen.Sportsmen[i]);
                            }
                            for (int i = minLen; i < groupWomen.Sportsmen.Length; i++)
                            {
                                Add(groupWomen.Sportsmen[i]);
                            }
                        }
                        else
                        {
                            minLen = groupWomen.Sportsmen.Length;
                            for (int i = 0; i < minLen; i++)
                            {
                                Add(groupMen.Sportsmen[i]);
                                Add(groupWomen.Sportsmen[i]);
                            }
                            for (int i = minLen; i < groupMen.Sportsmen.Length; i++)
                            {
                                Add(groupMen.Sportsmen[i]);
                            }
                        }
                    }
                }
            }


            public void Print()
            {
                Console.WriteLine($"{Name}");
                foreach (var s in _sportsmen)
                {
                    s.Print();
                }
                Console.WriteLine();
            }
        }

        
    }
}
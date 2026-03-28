using static Lab8.Purple.Task4;

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool isRan;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string Name, string Surname)
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname)) throw new Exception("Null constructor");
                _name = Name;
                _surname = Surname;
                _time = 0;
                isRan = false;
            }

            public void Run(double time)
            {
                if (!isRan)
                {
                    _time = time;
                    isRan = true;
                }
            }
            public static void Sort(Sportsman[] array)
            {
                Sportsman[] sorted = new Sportsman[array.Length];
                array.OrderBy(x => x.Time).ToArray();
                Array.Copy(sorted, array, array.Length);
            }

            public void Print()
            {
                Console.WriteLine("Лыжник" +
                    $"Имя:{_name}" +
                    $"Фамилия:{_surname}" +
                    $"Время:{_time}");
            }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string Name, string Surname) : base(Name, Surname) { }
            public SkiMan(string Name, string Surname, double Time) : base(Name, Surname)
            {
                Run(Time);
            }
        }
        public class SkiWoman : Sportsman
        {
            public SkiWoman(string Name, string Surname) : base(Name, Surname) { }
            public SkiWoman(string Name, string Surname, double Time) : base(Name, Surname) 
            {
                Run(Time);
            }
        }

        public class Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public Group(string Name)
            {
                if (string.IsNullOrWhiteSpace(Name)) throw new Exception("Null constructor");
                _name = Name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group group)
            {
                if (group.Sportsmen.Length != 0)
                {
                    _name = group.Name;
                    _sportsmen = (Sportsman[])group._sportsmen.Clone();
                }
            }
            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }
            public void Add(Sportsman[] sportsmen)
            {
                for (int x = 0; x < sportsmen.Length; x++)
                    Add(sportsmen[x]);
            }
            public void Add(Group group)
            {   
                for (int x = 0; x < group.Sportsmen.Length; x++)
                    Add(group.Sportsmen[x]);
            }
            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                int m = 0, w = 0;
                foreach (Sportsman el in _sportsmen)
                    if (el is SkiMan)
                        m++;
                w = _sportsmen.Length - m;
                men = new Sportsman[m]; women = new Sportsman[w];
                m = 0; w = 0;
                foreach (Sportsman el in _sportsmen)
                    if (el is SkiMan)
                        men[m++] = el;
                    else
                        women[w++] = el;
            }
            public void Shuffle()
            {
                Sportsman[] sorted = new Sportsman[_sportsmen.Length];
                _sportsmen.OrderBy(x => x.Time).ToArray();
                Array.Copy(sorted, _sportsmen, _sportsmen.Length);
            }
            public void Sort()
            {
                Sportsman temp;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    for (int j = 0; j < _sportsmen.Length - i - 1; j++)
                    {
                        if (_sportsmen[j].Time > _sportsmen[j + 1].Time)
                        {
                            temp = _sportsmen[j];
                            _sportsmen[j] = _sportsmen[j + 1];
                            _sportsmen[j + 1] = temp;
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {
                Group result = new Group("Финалисты");

                Sportsman[] merged = new Sportsman[group1._sportsmen.Length + group2._sportsmen.Length];

                int i = 0;
                int j = 0;
                int k = 0;

                while (i < group1._sportsmen.Length && j < group2._sportsmen.Length)
                {
                    if (group1._sportsmen[i].Time <= group2._sportsmen[j].Time)
                    {
                        merged[k++] = group1._sportsmen[i++];
                    }
                    else
                    {
                        merged[k++] = group2._sportsmen[j++];
                    }
                }

                while (i < group1._sportsmen.Length)
                {
                    merged[k++] = group1._sportsmen[i++];
                }

                while (j < group2._sportsmen.Length)
                {
                    merged[k++] = group2._sportsmen[j++];
                }

                result.Add(merged);

                return result;
            }
            public void Print()
            {
                Console.WriteLine($"Имя группы:{_name}" +
                    $"Количество участников:{_sportsmen.Length}" +
                    $"Участники:");
                foreach (Sportsman el in _sportsmen)
                {
                    Console.WriteLine($"{el.Surname} {el.Name} время:{el.Time}");
                }
            }
        }


    }
}
using static Lab8.Purple.Task4;

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            protected string _name;
            protected string _surname;
            protected double _time;
            protected bool _flag;
            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _flag = true;
            }
            public void Run(double time)
            {
                if (_flag)
                {
                    _flag = false;
                    _time = time;
                }
            }
            public static void Sort(Sportsman[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
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
                Console.WriteLine($"Name:{_name}");
                Console.WriteLine($"Surname:{_surname}");
                Console.WriteLine($"Time:{_time}");
            }
        }
        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base (name, surname)
            {
            }
            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run( time);
            }
        }
        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname)
            {
            }
            public SkiWoman(string name, string surname,double time) : base(name, surname)
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
            public Group(Group group)
            {
                _name = group.Name;
                _sportsmen = group.Sportsmen.ToArray();
            }
            public void Add(Sportsman sportsman)
            {
                Sportsman[] newArray = new Sportsman[_sportsmen.Length + 1];
                Array.Copy(_sportsmen, newArray, _sportsmen.Length);
                newArray[_sportsmen.Length] = sportsman;
                _sportsmen = newArray;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || sportsmen.Length == 0) return;

                Sportsman[] newArray = new Sportsman[_sportsmen.Length + sportsmen.Length];
                Array.Copy(_sportsmen, newArray, _sportsmen.Length);
                Array.Copy(sportsmen, 0, newArray, _sportsmen.Length, sportsmen.Length);
                _sportsmen = newArray;
            }
            public void Add(Group group)
            {
                Add(group.Sportsmen);
            }
            public void Sort()
            {
                if (_sportsmen == null) return;

                // Сортировка пузырьком
                for (int i = 0; i < _sportsmen.Length - 1; i++)
                {
                    for (int j = 0; j < _sportsmen.Length - 1 - i; j++)
                    {
                        if (_sportsmen[j].Time > _sportsmen[j + 1].Time)
                        {
                            Sportsman temp = _sportsmen[j];
                            _sportsmen[j] = _sportsmen[j + 1];
                            _sportsmen[j + 1] = temp;
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {
                Group result = new Group("Финалисты");

                int i = 0, j = 0;

                while (i < group1.Sportsmen.Length && j < group2.Sportsmen.Length)
                {
                    if (group1.Sportsmen[i].Time <= group2.Sportsmen[j].Time)
                    {
                        result.Add(group1.Sportsmen[i]);
                        i++;
                    }
                    else
                    {
                        result.Add(group2.Sportsmen[j]);
                        j++;
                    }
                }

                while (i < group1.Sportsmen.Length)
                {
                    result.Add(group1.Sportsmen[i]);
                    i++;
                }

                while (j < group2.Sportsmen.Length)
                {
                    result.Add(group2.Sportsmen[j]);
                    j++;
                }

                return result;
            }
            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                int menCount = 0;
                int womenCount = 0;

                foreach (var sportsman in _sportsmen)
                {
                    if (sportsman is SkiWoman) womenCount++;
                    else if (sportsman is SkiMan) menCount++;
                }
                men = new Sportsman[menCount];
                women = new Sportsman[womenCount];

                int menIndex = 0;
                int womenIndex = 0;

                foreach (var sportsman in _sportsmen)
                {
                    if (sportsman is SkiWoman w)
                    {
                        women[womenIndex++] = w;
                    }
                    else if (sportsman is SkiMan m)
                    {
                        men[menIndex++] = m;
                    }
                }
            }
            public void Shuffle()
            {
                if (_sportsmen.Length < 2 || _sportsmen == null) return;
                Split(out var men, out var women);
                Sportsman.Sort(men);
                Sportsman.Sort(women);
                string now = "w";
                if (men[0].Time < women[0].Time) now = "m";
                int iman = 0;
                int iwoman = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (now == "m" && iman < men.Length)
                    {
                        _sportsmen[i] = men[iman++];
                        now = "w";
                    }
                    else if (now == "w" && iwoman < women.Length)
                    {
                        _sportsmen[i] = women[iwoman++];
                        now = "m";
                    }
                    else if (iman < men.Length)
                    {
                        _sportsmen[i] = men[iman++];
                    }
                    else if (iwoman < women.Length)
                    {
                        _sportsmen[i] = women[iwoman++];
                    }
                }
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

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
            }

            public void Run(double time)
            {
                _time = time;
            }
            public static void Sort(Sportsman[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].Time > array[j].Time)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Participant: {Name} {Surname}");
                Console.WriteLine($"Time: {Time}");
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
            public Group(Group group)
            {
                _name = group.Name;
                _sportsmen = (Sportsman[])group.Sportsmen.Clone();
            }

            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || sportsmen.Length == 0)
                    return;

                foreach (Sportsman sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }
            public void Add(Group group)
            {
                if (group.Sportsmen == null || group.Sportsmen.Length == 0)
                    return;

                Add(group.Sportsmen);
            }
            public void Sort()
            {
                if (_sportsmen == null || _sportsmen.Length == 0)
                    return;

                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    for (int j = 1; j < _sportsmen.Length; j++)
                    {
                        if (_sportsmen[j - 1].Time > _sportsmen[j].Time)
                        {
                            (_sportsmen[j - 1], _sportsmen[j]) = (_sportsmen[j], _sportsmen[j - 1]);
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {
                Group finalists = new Group("Финалисты");

                finalists.Add(group1);
                finalists.Add(group2);
                finalists.Sort();

                return finalists;
            }
            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                int menCount = 0; int womenCount = 0;
                foreach (var sportsman in _sportsmen)
                {
                    if (sportsman is SkiMan man) menCount++;
                    else if (sportsman is SkiWoman woman) womenCount++;
                }
                men = new Sportsman[menCount]; women = new Sportsman[womenCount];
                menCount = 0; womenCount = 0;

                foreach(var sportsman in _sportsmen)
                {
                    if (sportsman is SkiMan man) men[menCount++] = man;
                    else if (sportsman is SkiWoman woman) women[womenCount++] = woman;
                }
            }
            public void Shuffle()
            {
                if (_sportsmen == null || _sportsmen.Length < 2) return;

                Split(out var men, out var women);

                Sportsman.Sort(men);
                Sportsman.Sort(women);

                bool nextIsMan = false;
                if (men[0].Time <= women[0].Time)
                {
                    nextIsMan = true;
                }

                int menIndex = 0; int womenIndex = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (nextIsMan && menIndex < men.Length)
                    {
                        _sportsmen[i] = men[menIndex++];
                        nextIsMan = false;
                    }
                    else if (womenIndex < women.Length)
                    {
                        _sportsmen[i] = women[womenIndex++];
                        nextIsMan = true;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Group {Name}:");
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i].Print();
                }
            }
        }
    }
}

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
                if (_time == 0)
                {
                    _time = time;
                }
            }
            public void Print()
            {
                Console.WriteLine(_name);

                Console.WriteLine(_surname);

                Console.WriteLine(_time);
            }
            public static void Sort(Sportsman[] array)
            {
                if (array == null || array.Length == 0) { return; }
                var s = array.OrderBy(_sportman => _sportman.Time).ToArray();
                for (int i = 0; i < s.Length; i++)
                {
                    array[i] = s[i];
                }
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
                _sportsmen = new Sportsman[group._sportsmen.Length];
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i] = group._sportsmen[i];
                }
            }
            public void Add(Sportsman chel)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = chel;
            }
            public void Add(Sportsman[] cheliki)
            {
                foreach (Sportsman chel in cheliki)
                {
                    Add(chel);
                }
            }
            public void Add(Group group)
            {
                if (group.Sportsmen == null || group.Sportsmen.Length == 0) return;
                Add(group.Sportsmen);
            }
            public void Sort()
            {
                if (_sportsmen == null || _sportsmen.Length == 0) return;
                var sor = _sportsmen.OrderBy(s => s.Time).ToArray();
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i] = sor[i];
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group final = new Group("Финалисты");
                final.Add(group1);
                final.Add(group2);
                final.Sort();
                return final;
            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_sportsmen);
            }
            
            public  void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = new Sportsman[0];
                women = new Sportsman[0];
                if (_sportsmen == null) return;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiWoman )
                    {
                        Array.Resize(ref women,  women.Length +1);
                        women[^1] = _sportsmen[i];
                    }
                    if (_sportsmen[i] is SkiMan)
                    {
                        Array.Resize(ref men, men.Length + 1);
                        men[^1] = _sportsmen[i];
                    }
                }
            }
            public void Shuffle()
            {
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    Console.WriteLine(_sportsmen[i].Name + " " + _sportsmen[i].Time);
                }
                Sportsman[] men;
                Sportsman[] women;
                Split(out men, out women);
                Sportsman.Sort(men);
                Sportsman.Sort(women);
                int ind = 0;
                Array.Resize(ref _sportsmen, Math.Min(men.Length, women.Length)*2);
                if (women[0].Time <= men[0].Time)
                {
                    for (int i = 0; i < Math.Min(men.Length, women.Length); i++)
                    {
                        _sportsmen[ind] = women[i];
                        _sportsmen[ind+1] = men[i];
                        ind += 2;
                    }
                }
                else if (men[0].Time < women[0].Time)
                {
                    for (int i = 0; i < Math.Min(men.Length, women.Length); i++)
                    {
                        _sportsmen[ind] = men[i];
                        _sportsmen[ind + 1] = women[i];
                        ind += 2;
                    }
                }
                Console.WriteLine();
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    Console.WriteLine(_sportsmen[i].Name + " " + _sportsmen[i].Time);
                }

            }
        }
        public class SkiMan : Sportsman
        {
            public  SkiMan(string name, string surname) : base(name, surname) { }
            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }
        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public  SkiWoman(string name, string surname, double time) : base(name, surname) 
            { 
                Run(time);
            }
        }
        

    }
}
using System.Net.Http.Headers;

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _hasRun;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name,string surname)
            {
                _name = name;
                _surname = surname;
                _hasRun = false;
            }
            public void Run(double time)
            {
                if (_hasRun || time <= 0)return;   
                _time = time;
                _hasRun = true;
            }
            public void Print()
            {
                System.Console.WriteLine("___________Sportsman___________");
                System.Console.WriteLine($"Name: {_name}; Surname: {_surname}");
                System.Console.WriteLine($"HasRun: {_hasRun}; Time: {_time}");
            }
            public static void Sort(Sportsman[] array)
            {
                int l =1;
                while (l < array.Length)
                {
                    if (l==0 || array[l-1].Time<=array[l].Time) l++;
                    else
                    {
                        (array[l-1],array[l])=(array[l],array[l-1]);
                        l--;
                    }
                }
            }
        }
        public class SkiMan : Sportsman
        {
            public SkiMan(string name,string surname) : base(name,surname){}
            public SkiMan(string name,string surname,double time) : base(name, surname)
            {
                Run(time);
            }
        }
        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name,string surname) : base(name,surname){}
            public SkiWoman(string name,string surname,double time) : base(name, surname)
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
            public Group (string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group group)
            {
                if (group._sportsmen == null) return;
                _name = group._name;
                _sportsmen = new Sportsman[0];
                Add (group._sportsmen);
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null)
                {
                    Array.Resize(ref _sportsmen,1);
                    _sportsmen[0] = sportsman;
                }
                else
                {
                    Array.Resize(ref _sportsmen,_sportsmen.Length+1);
                    _sportsmen[^1] = sportsman;
                }
            }
            public void Add(Sportsman[] sportsmans)
            {
                if (sportsmans == null) return;
                foreach (var sportsman in sportsmans)
                    Add(sportsman);
            }
            public void Add(Group group)
            {
                var sportsmans = group._sportsmen;
                Add(sportsmans);
            }
            public  void  Sort()
            {
                int l = 1;
                while (l < _sportsmen.Length)
                {
                    if (l == 0 || _sportsmen[l-1].Time <= _sportsmen[l].Time) l++;
                    else
                    {
                        (_sportsmen[l-1],_sportsmen[l]) = (_sportsmen[l],_sportsmen[l-1]);
                        l--;
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {

                var group = new Group("Финалисты");

                int len1 = group1._sportsmen.Length,len2 = group2._sportsmen.Length;
                Array.Resize(ref group._sportsmen, len1 + len2);
                for (int i = 0; i < len1; i++)
                    group._sportsmen[i] = group1._sportsmen[i];
                
                for (int i =0;i<len2;i++)
                    group._sportsmen[len1+i] = group2._sportsmen[i];
        
                group._sportsmen = group._sportsmen.OrderBy(s => s.Time).ToArray();
                return group;
            }
            public void Print()
            {
                System.Console.WriteLine("___________Group____________");
                System.Console.WriteLine($"Name: {_name}");
                System.Console.WriteLine("Sportsmans: ");
                foreach (var sportsman in _sportsmen)
                    System.Console.Write($"{sportsman.Name,10}");
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = _sportsmen.Where(s=> s is SkiMan).ToArray();
                women = _sportsmen.Where(s => s is SkiWoman).ToArray();
            }
            public void Shuffle()
            {
                Sportsman.Sort(_sportsmen);
                Split(out Sportsman[] men, out Sportsman[] women);
                
                int m =0, w = 0, l = 0;
                bool FirstIsMan = _sportsmen[0] is SkiMan;

                while (m<men.Length && w < women.Length)
                {
                    if (FirstIsMan)
                    {
                        _sportsmen[l++] = men[m++];
                        FirstIsMan = !FirstIsMan;
                    }
                    else
                    {
                        _sportsmen[l++] = women[w++];
                        FirstIsMan = !FirstIsMan;
                    }
                }

                while (m<men.Length)
                    _sportsmen[l++] = men[m++];

                while (w<women.Length)
                    _sportsmen[l++] = women[w++];
            }
        }
    }
}
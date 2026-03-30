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
                if (_time == 0) _time = time;
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                var sorted = array.OrderBy(s => s.Time).ToArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = sorted[i];
            }

            public void Print()
            {
                Console.WriteLine($"{Surname} {Name}: {Time}");
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
                _name = group._name;
                _sportsmen = group._sportsmen != null
                    ? (Sportsman[])group._sportsmen.Clone()
                    : new Sportsman[0];
            }

            public void Add(Sportsman sportsman)
            {
                if (sportsman == null) return;
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                foreach (var s in sportsmen)
                    Add(s);
            }

            public void Add(Group group)
            {
                if (group?._sportsmen != null)
                    Add(group._sportsmen);
            }

            public void Sort()
            {
                if (_sportsmen == null) return;
                Sportsman.Sort(_sportsmen);
            }

            public static Group Merge(Group group1, Group group2)
            {
                var arr1 = group1?._sportsmen ?? new Sportsman[0];
                var arr2 = group2?._sportsmen ?? new Sportsman[0];

                var merged = new Sportsman[arr1.Length + arr2.Length];
                int i = 0, j = 0, k = 0;

                while (i < arr1.Length && j < arr2.Length)
                {
                    if (arr1[i].Time <= arr2[j].Time)
                        merged[k++] = arr1[i++];
                    else
                        merged[k++] = arr2[j++];
                }
                while (i < arr1.Length) merged[k++] = arr1[i++];
                while (j < arr2.Length) merged[k++] = arr2[j++];

                var result = new Group("\u0424\u0438\u043D\u0430\u043B\u0438\u0441\u0442\u044B");
                result._sportsmen = merged;
                return result;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = _sportsmen?.Where(s => s is SkiMan).ToArray() ?? new Sportsman[0];
                women = _sportsmen?.Where(s => s is SkiWoman).ToArray() ?? new Sportsman[0];
            }

            public void Shuffle()
            {
                if (_sportsmen == null || _sportsmen.Length == 0) return;
                Sort();
                Split(out var men, out var women);

                bool menFirst = men.Length > 0 && (women.Length == 0 || men[0].Time <= women[0].Time);

                var result = new Sportsman[_sportsmen.Length];
                int mi = 0, wi = 0, ri = 0;

                while (mi < men.Length || wi < women.Length)
                {
                    if (menFirst)
                    {
                        if (mi < men.Length) result[ri++] = men[mi++];
                        if (wi < women.Length) result[ri++] = women[wi++];
                    }
                    else
                    {
                        if (wi < women.Length) result[ri++] = women[wi++];
                        if (mi < men.Length) result[ri++] = men[mi++];
                    }
                }

                _sportsmen = result;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                if (_sportsmen == null) return;
                foreach (var s in _sportsmen)
                    s.Print();
            }
        }
    }
}

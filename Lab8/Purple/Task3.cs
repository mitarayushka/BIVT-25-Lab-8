using System;
using System.Linq;

namespace Lab8.Purple
{
    public class Task3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _topPlace;
            private double _totalMark;
            private int _countOfMarks;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Marks
            {
                get
                {
                    var copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int[] Places
            {
                get
                {
                    var copy = new int[_places.Length];
                    Array.Copy(_places, copy, _places.Length);
                    return copy;
                }
            }

            public int TopPlace => _topPlace;
            public double TotalMark => _totalMark;

            public int Score => _places.Sum();

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _countOfMarks = 0;
                _topPlace = 0;
                _totalMark = 0;
            }

            public void Evaluate(double result)
            {
                if (_countOfMarks >= 7) return;
                _marks[_countOfMarks++] = result;
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                int n = participants.Length;

                for (int judge = 0; judge < 7; judge++)
                {
                    int[] ids = Enumerable.Range(0, n).ToArray();
                    Array.Sort(ids, (x, y) => participants[y]._marks[judge].CompareTo(participants[x]._marks[judge]));

                    for (int place = 0; place < n; place++)
                        participants[ids[place]]._places[judge] = place + 1;
                }

                for (int i = 0; i < n; i++)
                {
                    participants[i]._topPlace = participants[i]._places.Min();
                    participants[i]._totalMark = participants[i]._marks.Sum();
                }

                Array.Sort(participants, (a, b) => a._places[6].CompareTo(b._places[6]));
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Array.Sort(array, (a, b) =>
                {
                    if (a.Score != b.Score) return a.Score.CompareTo(b.Score);
                    if (a.TopPlace != b.TopPlace) return a.TopPlace.CompareTo(b.TopPlace);
                    return b.TotalMark.CompareTo(a.TotalMark);
                });
            }

            public void Print()
            {
                Console.WriteLine($"{Surname} {Name}: {Score}");
            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;
            private int _evaluateIndex;

            public Participant[] Participants => _participants;
            public double[] Moods => _moods;

            public Skating(double[] moods)
            {
                _moods = new double[7];
                for (int i = 0; i < 7 && i < moods.Length; i++)
                    _moods[i] = moods[i];
                _participants = new Participant[0];
                _evaluateIndex = 0;
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (_participants == null || _evaluateIndex >= _participants.Length || marks == null) return;
                for (int j = 0; j < _moods.Length && j < marks.Length; j++)
                    _participants[_evaluateIndex].Evaluate(marks[j] * _moods[j]);
                _evaluateIndex++;
            }

            public void Add(Participant p)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = p;
            }

            public void Add(Participant[] ps)
            {
                if (ps == null) return;
                foreach (var p in ps)
                    Add(p);
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += (i + 1) / 10.0;
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] *= (1.0 + 0.01 * (i + 1));
            }
        }
    }
}

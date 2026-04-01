using System.Runtime.CompilerServices;

namespace Lab8.Purple
{
    public class Task3
    {
        public struct Participant
        {

            //поля 

            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _topPlace;
            private double _totalMark;
            private int _judgsNumber;
            private bool _isSkate;

            //свойства

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return new double[0];
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }
            public int[] Places => _places;
            public int TopPlace => _topPlace;

            public double TotalMark => _totalMark;

            public int Score => _places.Sum();
            internal bool IsSkate => _isSkate;

            //конструктор

            public Participant(string name, string surname)
            {
                _surname = surname;
                _name = name;
                _marks = new double[7];
                _places = new int[7];
                _topPlace = int.MaxValue;
                _judgsNumber = 0;
                _totalMark = 0;
                _isSkate = false;

            }

            //методы

            public void Evaluate(double result)
            {
                if (_judgsNumber < _marks.Length)
                {
                    _marks[_judgsNumber] = result;
                    _judgsNumber++;
                    if (_judgsNumber == _marks.Length)
                        _isSkate = true;
                }
                else
                {
                    _isSkate = true;
                }
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                int n = participants.Length;
                int judges = 7;

                // сброс данных перед расчётом
                for (int i = 0; i < n; i++)
                {
                    participants[i]._topPlace = int.MaxValue;
                    participants[i]._totalMark = 0;
                    for (int j = 0; j < judges; j++)
                        participants[i]._places[j] = 0;
                }
                int[] indices = new int[n];
                for (int judge = 0; judge < 7; judge++)
                {
                    for (int i = 0; i < n; i++)
                        indices[i] = i;

                    int idx = 1;
                    int jdx = 2;

                    while (idx < n)
                    {
                        if (idx == 0 || participants[indices[idx]]._marks[judge] <= participants[indices[idx - 1]]._marks[judge])
                        {
                            idx = jdx;
                            jdx++;
                        }
                        else
                        {
                            (indices[idx], indices[idx - 1]) = (indices[idx - 1], indices[idx]);
                            idx--;
                        }
                    }
                    for (int place = 0; place < n; place++)
                    {
                        int originalIdx = indices[place];
                        participants[originalIdx]._places[judge] = place + 1;
                        
                        if (place + 1 < participants[originalIdx]._topPlace)
                        {
                            participants[originalIdx]._topPlace = place + 1;
                        }
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        sum += participants[i]._marks[j];
                    }
                    participants[i]._totalMark = sum;
                }
                int a = 1;
                int b = 2;
                
                while (a < n)
                {
                    if (a == 0 || participants[a]._places[6] >= participants[a - 1]._places[6])
                    {
                        a = b;
                        b++;
                    }
                    else
                    {
                        (participants[a - 1], participants[a]) = (participants[a], participants[a - 1]);
                        a--;
                        }
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;

                Array.Sort(array, (x, y) =>
                {
                    int cmp = x.Score.CompareTo(y.Score);
                    if (cmp != 0) return cmp;

                    cmp = x.TopPlace.CompareTo(y.TopPlace);
                    if (cmp != 0) return cmp;

                    // totalMark по убыванию
                    return y.TotalMark.CompareTo(x.TotalMark);
                });
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Score}");
            }
        }
        
        public abstract class Skating
        {
            private Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;
            public double[] Moods
            {
                get
                {
                    if(_moods == null)
                    {
                        return new double[0];
                    }
                    double[] copy = new double[_moods.Length];
                    Array.Copy(_moods, copy, _moods.Length);
                    return copy;
                }
            }

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                if(moods == null || moods.Length != 7)
                {
                    _moods = new double[7];
                    for (int i = 0; i < 7; i++) _moods[i] = 1.0;
                }
                else {
                    _moods = new double[7];
                    Array.Copy(moods, _moods, _moods.Length);
                }
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (marks == null || marks.Length != 7 || _participants == null)
                {
                    return;
                }
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].IsSkate == false)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        return;
                    }
                }
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0)
                {
                    return;
                }
                foreach (Participant participant in participants)
                    Add(participant);
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods){}

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (i + 1) / 10.0;
                }
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods){}
            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += _moods[i] * (i + 1) / 100.0;
                }
            }
        }
    }
}
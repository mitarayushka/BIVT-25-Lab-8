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
            private int _index = 0;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks => (double[])_marks.Clone();
            public int[] Places => (int[])_places.Clone();
            public int TopPlace => _topPlace;
            public double TotalMark => _totalMark;
            public int Score
            {
                get
                {
                    int sum = 0;

                    for (int i = 0; i < _places.Length; i++)
                    {
                        sum += _places[i];
                    }

                    return sum;
                }
            }

            public Participant (string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _topPlace = 0;
                _totalMark = 0;
                _index = 0;
            }

            public void Evaluate(double result)
            {
                if (_index < _marks.Length)
                {
                    _marks[_index++] = result;
                    _totalMark += result;
                }
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                int n = participants.Length;

                for (int judge = 0; judge < 7; judge++)
                {
                    int[] order = new int[n];
                    for (int i = 0; i < n; i++)
                        order[i] = i;

                    for (int i = 0; i < n - 1; i++)
                    {
                        for (int j = 0; j < n - 1 - i; j++)
                        {
                            int a = order[j];
                            int b = order[j + 1];

                            if (participants[a]._marks[judge] < participants[b]._marks[judge])
                                (order[j], order[j + 1]) = (order[j + 1], order[j]);
                        }
                    }

                    int place = 1;
                    participants[order[0]]._places[judge] = place;

                    for (int rank = 1; rank < n; rank++)
                    {
                        double prev = participants[order[rank - 1]]._marks[judge];
                        double cur  = participants[order[rank]]._marks[judge];

                        if (cur < prev)
                            place = rank + 1;

                        participants[order[rank]]._places[judge] = place;
                    }
                }

                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (participants[j]._places[6] > participants[j + 1]._places[6])
                            (participants[j], participants[j + 1]) = (participants[j + 1], participants[j]);
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    int best = participants[i]._places[0];
                    for (int k = 1; k < 7; k++)
                        if (participants[i]._places[k] < best)
                            best = participants[i]._places[k];

                    participants[i]._topPlace = best;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Score > array[j + 1].Score)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        } else if (array[j].Score == array[j + 1].Score)
                        {
                            if (array[j].TopPlace > array[j + 1].TopPlace)
                            {
                                (array[j], array[j + 1]) = (array[j + 1], array[j]);
                            } else if (array[j].TopPlace == array[j + 1].TopPlace)
                            {
                                if (array[j].TotalMark < array[j + 1].TotalMark)
                                {
                                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                                }
                            }
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine();
            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;
            private int _index;

            public Participant[] Participants => _participants ?? Array.Empty<Participant>();
            public double[] Moods => _moods ?? Array.Empty<double>();

            public Skating (double[] moods)
            {
                if (moods == null || moods.Length != 7) return;

                _participants = Array.Empty<Participant>();
                _moods = new double[7];
                _index = 0;

                for (int i = 0; i < moods.Length; i++)
                {
                    _moods[i] = moods[i];
                }

                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (marks == null || marks.Length != 7) return;
                if (_participants == null || _participants.Length == 0) return;

                for (int i = 0; i < marks.Length; i++)
                {
                    _participants[_index].Evaluate(marks[i] * _moods[i]);
                }

                _index++;
            }

            public void Add (Participant participant)
            {
                if (_participants == null) return;

                Participant[] array = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                {
                    array[i] = _participants[i];
                }

                array[array.Length - 1] = participant;
                _participants = array;
            }

            public void Add (Participant[] participants)
            {
                if (participants == null || _participants == null) return;

                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating (double[] moods) : base(moods) {}
            protected override void ModificateMood () {
                if (_moods == null) return;

                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += (double)(i + 1) / 10;
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating (double[] moods) : base(moods) {}

            protected override void ModificateMood ()
            {
                if (_moods == null) return;

                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += _moods[i] * (i + 1) / 100;
            }
        }
    }
}

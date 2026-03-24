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
            private bool hasPerfomance;

            private int _countMarks = 0;

            public string Name => _name;
            public string Surname => _surname;
            internal bool HasPerfomance => hasPerfomance;
            public double[] Marks
            {
                get
                {
                    double[] copyArray = new double[_marks.Length];
                    Array.Copy(_marks, copyArray, _marks.Length);
                    return copyArray;
                }
            }

            public int[] Places
            {
                get
                {
                    int[] copyArray = new int[_places.Length];
                    Array.Copy(_places, copyArray, _places.Length);
                    return copyArray;
                }
            }

            public int TopPlace => _places.Min();
            public double TotalMark => _marks.Sum();
            public int Score => _places.Sum();

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _topPlace = 0;
                _totalMark = 0;
                hasPerfomance = false;
            }

            public void Evaluate(double result)
            {
                hasPerfomance = true;
                _marks[_countMarks] = result;
                _countMarks++;
            }


            //referee 1    2   n     places[]
            //parp_1.marks   [2,5; 4,5 ...]   0  => 1  => logic by marks
            //parp_2.marks   [1,1; 2,1 ...]   0  => 1  => logic by marks
            public static void SetPlaces(Participant[] participants)
            {
                for (int referee = 0; referee < 7; referee++)
                {
                    for (int partp = 0; partp < participants.Length; partp++)
                    {
                        //Прибавим 1, так как места у каждого судьи начинает с 1 и заканчиваются 7
                        participants[partp]._places[referee] += 1;
                        for (int j = 0; j < participants.Length; j++)
                        {
                            if (participants[partp]._marks[referee] < participants[j]._marks[referee])
                                participants[partp]._places[referee] += 1;
                        }
                    }
                }

                var sortedArray = participants.OrderBy(x => x._places[^1]).ToArray();
                for (int i = 0; i < participants.Length; i++)
                    participants[i] = sortedArray[i];

            }

            public static void Sort(Participant[] participants)
            {
                var sortedArray = participants.OrderBy(x => x.Score).ThenBy(x => x.TopPlace).ThenByDescending(x => x.TotalMark).ToArray();
                for (int i = 0; i < participants.Length; i++)
                {
                    participants[i] = sortedArray[i];
                }
            }

            public void Print()
            {
                Console.WriteLine(string.Join(" ", _places));
                Console.WriteLine(TopPlace);
                Console.WriteLine(TopPlace);
                Console.WriteLine(Score);
            }
        }
        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;
            public double[] Moods => _moods;


            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                if (moods != null)
                {
                    for (int i = 0; i < moods.Length; i++)
                    {
                        _moods[i] = moods[i];
                    }
                }
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (marks == null) return;
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (!_participants[i].HasPerfomance)
                    {
                        for (int j = 0; j < marks.Length; j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        break;
                    }
                }
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || _participants == null) return;

                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods)
            {
            }

            protected override void ModificateMood()
            {
                if (_moods == null) return;
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (i + 1) / 10.0;
                }
            }
        }



        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods)
            {
            }

            protected override void ModificateMood()
            {
                if (_moods == null) return;
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += _moods[i] * (i + 1) / 100.0;
                }
            }
        }
    }
}

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

            public string Name => _name;
            public string Surname => _surname;
            public int TopPlace => _topPlace;
            public double TotalMark => _totalMark;

            public double[] Marks
            {
                get
                {
                    if (_marks == null) return new double[7];
                    double[] Marks = new double[_marks.Length];
                    Array.Copy(_marks, Marks, Marks.Length);
                    return Marks;
                }
            }

            public int[] Places
            {
                get
                {
                    if (_places == null) return new int[7];
                    int[] Places = new int[_places.Length];
                    Array.Copy(_places, Places, Places.Length);
                    return Places;
                }
            }

            public int Score => _places.Sum();

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _topPlace = 0;
                _totalMark = 0;
            }

            public void Evaluate(double result)
            {
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = result;
                        break;
                    }
                }
            }

            public static void SetPlaces(Participant[] participants)
            {
                int n = participants.Length;
                var ans = participants;
                for (int i = 0; i < 7; i++)
                {
                    ans = (from j in ans orderby j.Marks[i] descending select j).ToArray();
                    for (int j = 0; j < n; j++)
                    {
                        ans[j]._places[i] = j + 1;
                        if (ans[j]._topPlace == 0 || ans[j]._topPlace > j + 1) ans[j]._topPlace = j + 1;
                        ans[j]._totalMark += ans[j].Marks[i];
                    }
                }
                for (int i = 0; i < n; i++) participants[i] = ans[i];
            }

            public static void Sort(Participant[] participants)
            {
                Participant[] sortedParticipants = new Participant[participants.Length];
                sortedParticipants = participants.OrderBy(x => x.Score).ThenByDescending(x => x._totalMark).ToArray();
                for (int i = 0; i < participants.Length; i++)
                {
                    participants[i] = sortedParticipants[i];
                }
            }

            public void Print()
            {
                Console.WriteLine(
                    $"Name: {Name}, Surname: {Surname}, Marks: {string.Join(",", Marks)}, Places: {string.Join(",", _places)}");
            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;
            protected int _index;

            public Participant[] Participants => _participants;
            public double[] Moods
            {
                get
                {
                    if (_moods == null) return new double[0];
                    double[] moods = new double[_moods.Length];
                    Array.Copy(_moods, moods, _moods.Length);
                    return moods;
                }

            }
            
            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _index = 0;
                _moods = new double[moods.Length];
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] = moods[i];
                }
                
                ModificateMood();
            }
            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {

                for (int i = 0; i < 7; i++)
                {
                    _participants[_index].Evaluate(marks[i] * _moods[i]);
                }
                _index++;
               
            }
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participant)
            {
                for (int i = 0; i < participant.Length; i++)
                {
                    Add(participant[i]);
                }
            }

        }
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) {}
            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] += (double)(i + 1) / 10;
                }
            }


        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] += _moods[i]*(i + 1) / 100;
                }
            }
        }

    }
}

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
            private int _totalMark;
            private int _markIndex;
            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks => (double[]) _marks.Clone();
            public int[] Places => (int[]) _places.Clone();
            public int TopPlace => _places.Min();
            public double TotalMark => _marks.Sum();
            public int Score => _places.Sum();
            public Participant (string name,string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _markIndex = 0;
            }
            public void Evaluate(double result)
            {
                if (_markIndex>=_marks.Length) return;
                if (result < 0) return;
                _marks[_markIndex] = result;
                _markIndex++;
            }
            public  static  void SetPlaces(Participant[] participants)
            {
                if (participants== null) return;

                int cols = participants.Length;
                int rows = participants[0]._marks.Length;
                double[,] matrix = new double[rows,cols];
                for (int i = 0; i < cols; i++)
                    for (int j = 0; j < rows; j++)
                        matrix[j,i] = participants[i]._marks[j];

                for (int i = 0; i <rows; i++)
                {
                    int[] index = new int [cols];
                    for (int j =0;j<cols;j++)
                        index[j] = j;

                    int l =1;
                    while (l < cols)
                    {
                        if (l == 0 || matrix[i,l-1]>=matrix[i,l]) l++;
                        else
                        {
                            (matrix[i,l-1],matrix[i,l]) =(matrix[i,l],matrix[i,l-1]);
                            (index[l-1],index[l]) = (index[l],index[l-1]) ;
                            l--;
                        }
                    }

                    for (int j = 0; j < cols; j++)
                        participants[index[j]]._places[i] = j+1;
                }            
            }
            public static void Sort(Participant[] array)
            {
                Array.Sort(array, CompareParticipants);
            }

            private static int CompareParticipants(Participant a, Participant b)
            {
                int scoreCompare = a.Score.CompareTo(b.Score);
                if (scoreCompare != 0)
                    return scoreCompare;

                int topPlaceCompare = a.TopPlace.CompareTo(b.TopPlace);
                if (topPlaceCompare != 0)
                    return topPlaceCompare;

                return b.TotalMark.CompareTo(a.TotalMark);
            }

            public void Print()
            {
                System.Console.WriteLine("____________Participant____________");
                System.Console.WriteLine($"Name: {_name}; Surname: {_surname}");
                System.Console.WriteLine("Marks: ");
                System.Console.WriteLine(string.Join(" ",_marks));
                System.Console.WriteLine("Places: ");
                System.Console.WriteLine($"{string.Join(" ", _places),5}");
                System.Console.WriteLine($"TopPlace: {_topPlace}; TotalMark: {_totalMark}");
            }
        }

        public abstract class Skating
        {
            private Participant[] _participants;
            private int _nextParticipantIndex;
            protected double[] _moods;

            public Participant[] Participants=> _participants;
            public double[] Moods => (double[])_moods.Clone();

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                _nextParticipantIndex = 0;
                moods.CopyTo(_moods,0);
                ModificateMood();
            }

            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (marks==null || marks.Length!=7) return;
                if (_nextParticipantIndex >= _participants.Length) return;

                for (int j = 0; j < marks.Length; j++)
                    _participants[_nextParticipantIndex].Evaluate(marks[j] * _moods[j]);

                _nextParticipantIndex++;
            }
            public void Add (Participant participant)
            {
                Array.Resize(ref _participants,_participants.Length+1);
                _participants[^1] = participant;
            }
            public void Add (Participant[] participants)
            {
                if (participants==null) return;
                foreach(var participant in participants)
                    Add(participant);
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) {}
            protected override void ModificateMood() 
            {
                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += (i + 1) / 10.0;
            }
        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base (moods) {}
            protected override void ModificateMood() 
            {
                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += _moods[i] * ((i + 1) / 100.0);
            }
        }
    }
}
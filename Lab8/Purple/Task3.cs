using System.Diagnostics;

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
            private int _topplace;
            private double _totalmark;
            private int _nummark;
            private bool _yes;




            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks => _marks.ToArray();
            public int[] Places => _places.ToArray();
            internal bool Yes => _yes;

            public int TopPlace => _topplace;
            public double TotalMark => _totalmark;
            internal int Numark => _nummark;

            public int Score
            {
                get
                {
                    if (_places == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        sum += _places[i];
                    }
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _nummark = 0;
                _totalmark = 0;
                _topplace = int.MaxValue;
                _yes = false;

            }

            public void Evaluate(double result)
            {
                if (_nummark < 7)
                {
                    _marks[_nummark] = result;
                    _nummark++;
                    _totalmark += result;
                }
                _yes = true;

            }


            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                for (int sud = 0; sud < 7; sud++)
                {
                    for (int i = 0; i < participants.Length - 1; i++)
                    {
                        for (int j = 0; j < participants.Length - i - 1; j++)
                        {
                            if (participants[j]._marks[sud] < participants[j + 1]._marks[sud])
                            {
                                (participants[j], participants[j + 1]) = (participants[j + 1], participants[j]);
                            }

                        }
                    }
                    for (int i = 0; i < participants.Length; i++)
                    {
                        participants[i]._places[sud] = i + 1;
                        if (sud == 0 || (i + 1) < participants[i]._topplace)
                        {
                            participants[i]._topplace = i + 1;
                        }
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Score > array[j + 1].Score)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                        else if (array[j].Score == array[j + 1].Score)
                        {
                            if (array[j].TopPlace > array[j + 1].TopPlace)
                            {
                                (array[j], array[j + 1]) = (array[j + 1], array[j]);
                            }
                            else if (array[j].TopPlace == array[j + 1].TopPlace)
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
                Console.WriteLine(_name);
                Console.WriteLine(_surname);
                Console.WriteLine(Score);
                Console.WriteLine(TotalMark);

            }





        }
        public abstract class Skating
        {
            private Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;
            public double[] Moods => _moods.ToArray();

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                Array.Copy(moods, _moods, _moods.Length);
                ModificateMood();

            }
            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Yes == false)
                    {
                        for (int j = 0; j < _moods.Length; j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        break;
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
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }




        }
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    Console.WriteLine(_moods[i]);
                    _moods[i] += (double)(i + 1) / 10;
                    Console.WriteLine(_moods[i]);
                }
            }

        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += _moods[i] * (double)(i + 1) / 100;
                }
            }
        }
    }
}

        
    

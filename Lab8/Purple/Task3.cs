using System;

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
            private int _marksCount;

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
                    for (int i = 0; i < 7; i++)
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
                _topPlace = 0;
                _totalMark = 0;
                _marksCount = 0;
            }

            public void Evaluate(double result)
            {
                if (_marksCount < 7)
                {
                    _marks[_marksCount] = result;
                    _totalMark += result;
                    _marksCount++;
                }
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                int n = participants.Length;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        participants[i]._places[j] = 0;
                    }
                    participants[i]._topPlace = 0;
                }

                for (int judge = 0; judge < 7; judge++)
                {

                    int[] indices = new int[n];
                    for (int i = 0; i < n; i++) indices[i] = i;
                    for (int i = 0; i < n - 1; i++)
                    {
                        for (int j = 0; j < n - i - 1; j++)
                        {
                            if (participants[indices[j]]._marks[judge] < participants[indices[j + 1]]._marks[judge])
                            {
                                int temp = indices[j];
                                indices[j] = indices[j + 1];
                                indices[j + 1] = temp;
                            }
                        }
                    }

                    for (int rank = 0; rank < n; rank++)
                    {
                        int idx = indices[rank];
                        int place = rank + 1;
                        participants[idx]._places[judge] = place;

                        if (judge == 0 || place < participants[idx]._topPlace)
                        {
                            participants[idx]._topPlace = place;
                        }
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;

                for (int i = 1; i < array.Length; i++)
                {
                    Participant current = array[i];
                    int j = i - 1;

                    while (j >= 0 && (array[j].Score > current.Score || (array[j].Score == current.Score && array[j].TopPlace > current.TopPlace) || (array[j].Score == current.Score && array[j].TopPlace == current.TopPlace && array[j].TotalMark < current.TotalMark)))
                    {
                        j--;
                    }

                    if (j + 1 != i)
                    {
                        
                        Participant temp = current; 
                        for (int k = i; k > j + 1; k--)
                        {
                            array[k] = array[k - 1]; 
                        }
                        array[j + 1] = temp;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Score}");
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

                for (int i = 0; i < 7 && i < moods.Length; i++)
                {
                    _moods[i] = moods[i];
                }
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Add(Participant participant)
            {
                if (_participants == null)
                {
                    _participants = new Participant[1];
                    _participants[0] = participant;
                    return;
                }

                Participant[] newParticipants = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                {
                    newParticipants[i] = _participants[i];
                }

                newParticipants[_participants.Length] = participant;
                _participants = newParticipants;
            }


            public void Add(Participant[] participants)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }

            public void Evaluate(double[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                { 
                    bool hasEmpty = false;
                    double[] participantMarks = _participants[i].Marks;

                    for (int j = 0; j < 7; j++)
                    {
                        if (participantMarks[j] == 0)
                        {
                            hasEmpty = true;
                            break;
                        }
                    }

                    if (hasEmpty)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            double adjusted = marks[j] * _moods[j];
                            _participants[i].Evaluate(adjusted);
                        }
                        break;
                    }
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
                    _moods[i] += (i + 1) / 10.0;
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
                    _moods[i] *= (1.0 + (i + 1) / 100.0);
                }
            }
        }
    }
}

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
            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks => (double[])_marks.Clone();
            public int[] Places=> (int[])_places.Clone();

            public int TopPlace
            {
                get
                {
                    int mn = int.MaxValue;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        if (mn > _places[i])
                        {
                            mn = _places[i];
                        }
                    }
                    return mn;
                }
            }
            public double TotalMark
            {
                get
                {
                    double sum = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        sum += Marks[i];
                    }
                    return sum;
                }
            }
            public int Score
            {
                get
                {
                    int score = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        score += _places[i];
                    }
                    return score;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
            }
            int count = 0;
            public void Evaluate(double result)
            {
                _marks[count] = result;
                count++;
            }
            public static void SetPlaces(Participant[] participants)
            {
                double[,] array = new double[participants.Length, 7];
                // Заполняем массив оценками участников
                for (int i = 0; i < participants.Length; i++)
                {
                    for (int j = 0; j < participants[i]._marks.Length; j++)
                    {
                        array[i, j] = participants[i]._marks[j];
                    }
                }
                for (int i = 0; i < 7; i++)
                {
                    int[] places = new int[participants.Length];
                    double[] orig = new double[participants.Length];
                    double[] sorted = new double[participants.Length];
                    for (int j = 0; j < participants.Length; j++)
                    {
                        orig[j] = array[j, i];
                        sorted[j] = array[j, i];
                    }
                    // Сортировка пузырьком
                    for (int j = 0; j < sorted.Length - 1; j++)
                    {
                        for (int m = 0; m < sorted.Length - 1 - j; m++)
                        {
                            if (sorted[m] < sorted[m + 1])
                            {
                                double temp = sorted[m];
                                sorted[m] = sorted[m + 1];
                                sorted[m + 1] = temp;
                            }
                        }
                    }
                    //сопоставляем значение отсортированой и исходной таблиц, чтобы раставить места
                    for (int j = 0; j < participants.Length; j++)
                    {
                        for (int k = 0; k < participants.Length; k++)
                        {
                            if (orig[j] == sorted[k])
                            {
                                places[j] = k + 1;
                            }
                        }
                    }
                    for (int j = 0; j < participants.Length; j++)
                    {
                        array[j, i] = places[j];
                    }
                }
                for (int i = 0; i < participants.Length; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        participants[i]._places[j] = Convert.ToInt32(array[i, j]);
                    }
                }

            }
            public static void Sort(Participant[] array)
            {
                // Сортировка пузырьком
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Score > array[j + 1].Score)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Score == array[j + 1].Score)
                        {
                            int a1 = 0;
                            int a2 = 0;
                            for (int k = 0; k < 7; k++)
                            {
                                if (array[j].Places[k] > array[j + 1].Places[k])
                                {
                                    a1++;
                                }
                                if (array[j].Places[k] < array[j + 1].Places[k])
                                {
                                    a2++;
                                }
                                if (a1 < a2)
                                {
                                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                                }
                            }
                            if (array[j].TotalMark < array[j + 1].TotalMark)
                            {
                                (array[j], array[j + 1]) = (array[j + 1], array[j]);
                            }
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name:{_name}");
                Console.WriteLine($"Surname:{_surname}");
                Console.WriteLine($"Score:{Score}");
                Console.WriteLine($"TopPlace:{TopPlace}");
                Console.WriteLine($"TotalMark:{TotalMark}");
            }
        }
        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;
            protected int _count;
            public Participant[] Participants => _participants;
            public double[] Moods =>(double[])_moods.Clone();
            internal int Count => _count;

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                _moods = (double[])moods.Clone();
                ModificateMood();
                _count = 0;
            }
            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (marks == null || marks.Length != 7 || _count >= _participants.Length) return;
                for (int i = 0; i < marks.Length; i++)
                    _participants[_count].Evaluate(marks[i] * _moods[i]);
                _count++;
            }
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }
            public void Add(Participant[] participant)
            {
                if (participant == null || participant.Length == 0) return;

                foreach (var i in participant)
                    Add(i);
            }
        }
        public class FigureSkating: Skating
        {
            public FigureSkating(double[] moods): base( moods)
            {
            }
            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] +=(double)( i+1) / 10;
                }
            }
        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base( moods)
            {
            }
            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                    _moods[i] += (double)_moods[i] / 100*(i + 1);
            }
        }
    }
}

namespace Lab8.Green
{
    public class Task4
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;

            public Participant(string name, string surname)
            {
                _name = name == null ? "" : name;
                _surname = surname == null ? "" : surname;
                _jumps = new double[3];
            }

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }

            public double[] Jumps
            {
                get
                {
                    if (_jumps == null)
                        return new double[0];

                    double[] copy = new double[_jumps.Length];
                    for (int i = 0; i < _jumps.Length; i++)
                        copy[i] = _jumps[i];
                    return copy;
                }
            }

            public double BestJump
            {
                get
                {
                    if (_jumps == null || _jumps.Length == 0)
                        return 0;

                    double best = 0;
                    for (int i = 0; i < _jumps.Length; i++)
                    {
                        if (_jumps[i] > best)
                            best = _jumps[i];
                    }

                    return best;
                }
            }

            public void Jump(double result)
            {
                if (_jumps == null)
                    return;

                for (int i = 0; i < _jumps.Length; i++)
                {
                    if (_jumps[i] == 0)
                    {
                        _jumps[i] = result;
                        return;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null)
                    return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                System.Console.WriteLine($"{Name} {Surname} {BestJump}");
            }
        }

        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;

            protected Discipline(string name)
            {
                _name = name == null ? "" : name;
                _participants = new Participant[0];
            }

            public string Name { get { return _name; } }
            public Participant[] Participants { get { return _participants; } }

            protected int Count
            {
                get
                {
                    if (_participants == null)
                        return 0;

                    return _participants.Length;
                }
            }

            protected Participant GetParticipant(int index)
            {
                return _participants[index];
            }

            protected void SetParticipant(int index, Participant participant)
            {
                _participants[index] = participant;
            }

            public void Add(Participant elem)
            {
                if (_participants == null)
                    _participants = new Participant[0];

                Participant[] newArr = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                    newArr[i] = _participants[i];
                newArr[newArr.Length - 1] = elem;
                _participants = newArr;
            }

            public void Add(Participant[] array)
            {
                if (array == null)
                    return;

                if (_participants == null)
                    _participants = new Participant[0];

                Participant[] newArr = new Participant[_participants.Length + array.Length];
                for (int i = 0; i < _participants.Length; i++)
                    newArr[i] = _participants[i];
                for (int i = 0; i < array.Length; i++)
                    newArr[_participants.Length + i] = array[i];
                _participants = newArr;
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int index);

            public void Print()
            {
                System.Console.WriteLine($"{Name} {Count}");
            }
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump")
            {
            }

            public override void Retry(int index)
            {
                if (index < 0 || index >= Count)
                    return;

                Participant participant = GetParticipant(index);
                double best = participant.BestJump;

                Participant updated = new Participant(participant.Name, participant.Surname);
                if (best != 0)
                    updated.Jump(best);

                SetParticipant(index, updated);
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High jump")
            {
            }

            public override void Retry(int index)
            {
                if (index < 0 || index >= Count)
                    return;

                Participant participant = GetParticipant(index);
                double[] jumps = participant.Jumps;
                if (jumps == null || jumps.Length == 0)
                    return;

                int last = -1;
                for (int i = 0; i < jumps.Length; i++)
                {
                    if (jumps[i] != 0)
                        last = i;
                }

                if (last < 0)
                    return;

                Participant updated = new Participant(participant.Name, participant.Surname);
                for (int i = 0; i < last; i++)
                {
                    if (jumps[i] != 0)
                        updated.Jump(jumps[i]);
                }

                SetParticipant(index, updated);
            }
        }
    }
}

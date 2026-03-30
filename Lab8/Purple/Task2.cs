using System;

namespace Lab8.Purple
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _target;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks=> (int[])_marks.Clone();
           

            public int Result
            {
                get
                {
                    if (_distance == 0 || _marks == null) return 0;

                    int sum = _marks.Sum() - _marks.Min() - _marks.Max();
                    int distancePoints = 60 + (_distance - _target) * 2;
                    int total = sum + distancePoints;

                    if (total< 0)
                    {
                        total = 0;
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _target = 0;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                _distance = distance;
                _target = target;

                if (marks != null)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[i] = marks[i];
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Result < array[j + 1].Result)
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
                Console.WriteLine($"{Name} {Surname}: {Result}");
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }

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

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name} ({Standard}м):");
                for (int i = 0; i < _participants.Length; i++)
                {
                    _participants[i].Print();
                }
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}

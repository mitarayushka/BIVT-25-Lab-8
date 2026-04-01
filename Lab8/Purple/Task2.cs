namespace Lab8.Purple
{
    public class Task2
    {
        public struct Participant
        {
            //поля
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _target;
            private bool _isJump;

            //свойства

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return new int[0];
                    int[] copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }
            internal bool IsJump => _isJump;
            public int Result
            {
                get
                {
                    if(_isJump == false)
                    {
                        return 0;
                    }
                    if (_marks == null)
                        return 0;
                    int mn = int.MaxValue, mx = int.MinValue;
                    int sum = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        sum += _marks[i];
                        mn = Math.Min(mn, _marks[i]);
                        mx = Math.Max(mx, _marks[i]);
                    }
                    sum -= (mn + mx);
                    sum += (_distance - _target) * 2 + 60;
                    if (sum < 0)
                        sum = 0;
                    return sum;
                }
            }

            //конструктор

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _isJump = false;
                _target = 0;
            }

            //методы

            public void Jump(int distance, int[] marks, int target)
            {
                _target = target;
                _distance = distance;
                if (marks == null || marks.Length != 5) return;
                for (int i = 0; i < 5; i++) _marks[i] = marks[i];
                _isJump = true;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;
                int i = 1;
                int j = 2;
                while (i < array.Length)
                {
                    if (i == 0 || array[i].Result <= array[i - 1].Result)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                       (array[i - 1], array[i]) = (array[i], array[i - 1]);
                       i--;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {Result}");
            }
        }
        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants
            {
                get
                {
                    if(_participants == null)
                    {
                        return new Participant[0];
                    }
                    return _participants;
                }
            }

            public SkiJumping(string name, int standard, Participant[] participants)
            {
                _name = name;
                _standard = standard;
                _participants = participants;
                if (participants == null)
                {
                    _participants = new Participant[0];
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
                int n = _participants.Length;
                Array.Resize(ref _participants, n + participants.Length);
                for(int i = n; i < _participants.Length; i++)
                {
                    _participants[i] = participants[i - n];
                }
            }
            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if(_participants[i].IsJump == false)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        return;
                    }
                }
            }
            public void Print()
            {
                System.Console.WriteLine($"Name: {_name}, Standard: {_standard}");
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100, new Participant[0]){}
        }
        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150, new Participant[0]){}
        }
    }
}

namespace Lab8.Purple
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private bool _hasJump;
            private int[] _marks;
            private int _target;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks => (int[])_marks.Clone();
            public int Result
            {
                get
                {
                    if (!_hasJump) return 0;
                    int marks = _marks.Sum() - _marks.Min() - _marks.Max();
                    int jump = 60 + (_distance - _target) * 2;
                    return marks + jump;
                }
            }
            public Participant(string name,string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _hasJump = false;
            }

            public  void  Jump(int  distance,  int[] marks, int target)
            {
                if (distance < 0) return;
                if (marks == null || marks.Length!=5 || _hasJump) return;
                for (int i =0;i<5;i++)
                    if (marks[i]<0 || marks[i]>20) return;

                _distance = distance;
                _target = target;
                marks.CopyTo(_marks,0);
                _hasJump = true;
            }
            public static void Sort(Participant[] array)
            {
                if (array.Length<2 || array == null) return;
                int l = 1;
                while (l < array.Length)
                {
                    if (l ==0 || array[l-1].Result >= array[l].Result) l++;
                    else
                    {
                        (array[l-1],array[l]) = (array[l],array[l-1]);
                        l--;
                    }
                }
            }

            public void Print ()
            {
                System.Console.WriteLine("__________Participant___________");
                System.Console.WriteLine($"Name: {_name}; Surname: {_surname}");
                System.Console.WriteLine($"Distance: {_distance}");
                System.Console.WriteLine($"Marks: {string.Join(" ",_marks)} ");
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            private int _participantsIndex;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name,int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
                _participantsIndex = 0;
            }

            public void Add (Participant participant)
            {
                Array.Resize(ref _participants,_participants.Length+1);
                _participants[^1] = participant;
            }
            public void Add (Participant[] participants)
            {
                if (participants == null) return;
                foreach(var participant in participants)
                    Add(participant);
            }
            public void Jump(int distance, int[] marks)
            {
                if (_participantsIndex==_participants.Length) return;
                _participants[_participantsIndex++].Jump(distance,marks,_standard);
            }

            public void Print()
            {
                System.Console.WriteLine("__________SkiJumping___________");
                System.Console.WriteLine($"Name: {Name}; Standard: {Standard};");
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base ("100m",100)
            {
            }
        }
        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base ("150m",150)
            {
            }
        }
    }
}
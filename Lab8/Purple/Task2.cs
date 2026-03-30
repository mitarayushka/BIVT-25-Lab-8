using System;
using System.Linq;

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
            private int _result;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    if (_marks == null) return new int[5];
                    var copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int Result => _result;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _distance = 0;
                _result = 0;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null) return;
                _distance = distance;
                _marks = (int[])marks.Clone();

                var sorted = (int[])marks.Clone();
                Array.Sort(sorted);

                int styleSum = 0;
                for (int i = 1; i < sorted.Length - 1; i++)
                    styleSum += sorted[i];

                _result = Math.Max(0, styleSum + 60 + 2 * (distance - target));
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                var sorted = array.OrderByDescending(p => p.Result).ToArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = sorted[i];
            }

            public void Print()
            {
                Console.WriteLine($"{Surname} {Name}: {Result}");
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            private int _jumpIndex;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
                _jumpIndex = 0;
            }

            public void Add(Participant p)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = p;
            }

            public void Add(Participant[] ps)
            {
                if (ps == null) return;
                foreach (var p in ps)
                    Add(p);
            }

            public void Jump(int distance, int[] marks)
            {
                if (_participants == null || _jumpIndex >= _participants.Length) return;
                _participants[_jumpIndex].Jump(distance, marks, _standard);
                _jumpIndex++;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                if (_participants == null) return;
                foreach (var p in _participants)
                    p.Print();
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

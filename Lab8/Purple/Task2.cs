using System;
using System.Numerics;

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
            private double _totalScore;
            private int _target;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks => (int[])_marks.Clone();
            internal int Target => _target;
            public int Result
            {
                get
                {
                    if (_marks == null || _distance == 0) return 0;
                    int[] sortedMarks = new int[5];
                    Array.Copy(_marks, sortedMarks, 5);
                    Array.Sort(sortedMarks);

                    int sumMarks = sortedMarks[1] + sortedMarks[2] + sortedMarks[3];

                    int distancePoints = 60 + (_distance - _target) * 2;
                    _result = sumMarks + distancePoints;
                    if (_result < 0) _result = 0;
  
                    return _result;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _result = 0;
                _target= 0;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5) return;
                _marks = (int[])marks.Clone();
                _distance = distance;
                _target = target;
            }
            public static void Sort(Participant[] array)
            {
                // Сортировка пузырьком
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
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
                Console.WriteLine($"Name:{_name}");
                Console.WriteLine($"Surname:{_surname}");
                Console.WriteLine($"Result:{Result}");
            }
        }
        public abstract class SkiJumping
        {
            protected private string _name;
            protected private int _standard;
            protected  int _count;
            protected private Participant[] _participants;
            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;
            public SkiJumping(string name, int distance)
            {
                _name = name;
                _standard = distance;
                _participants = new Participant[0];
                _count = 0;
            }
            public void Add(Participant participants)
            {

                Participant[] newArray = new Participant[_participants.Length + 1];
                Array.Copy(_participants, newArray, _participants.Length);
                newArray[_participants.Length] = participants;
                _participants = newArray;
            }
            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                foreach (var participant in participants)
                {
                    Add(participant);
                }
            }
            public void Jump(int distance, int[] marks)
            {
                if (marks == null || marks.Length == 0 || _count >= _participants.Length) return;

                _participants[_count].Jump(distance, marks, _standard);
                _count += 1;
            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_standard);
                foreach (var participant in _participants)
                {
                    participant.Print();
                }
            } 
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100)
            {
            }
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150)
            {
            }
        }
    }
}

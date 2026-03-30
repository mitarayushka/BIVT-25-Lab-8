using System;
using System.Linq;

namespace Lab8.Purple
{
    public class Task1
    {
        // КЛАСС УЧАСТНИКА
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpCounter;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Coefs => (double[])_coefs.Clone();

            public int[,] Marks => (int[,])_marks.Clone();

            public double TotalScore
            {
                get
                {
                    if (_coefs == null || _marks == null) return 0;

                    double total = 0;

                    for (int jump = 0; jump < 4; jump++)
                    {
                        int[] jumpMarks = new int[7];
                        for (int judge = 0; judge < 7; judge++)
                        {
                            jumpMarks[judge] = _marks[jump, judge];
                        }

                        int sum = jumpMarks.Sum() - jumpMarks.Max() - jumpMarks.Min();                  

                        total += sum * _coefs[jump];
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                _marks = new int[4, 7];
                _jumpCounter = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs != null && coefs.Length == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        _coefs[i] = coefs[i];
                    }
                }
            }

            public void Jump(int[] marks)
            {
                if (_jumpCounter >= 4) return;
                if (marks == null || marks.Length != 7) return;

                for (int i = 0; i < 7; i++)
                {
                    _marks[_jumpCounter, i] = marks[i];
                }
                _jumpCounter++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
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
                Console.WriteLine($"Участник: {Name} {Surname}");
                Console.WriteLine($"Итоговый результат: {TotalScore:F2}");
            }
        }

        // КЛАСС СУДЬЯ
        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _markIndex;

            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = new int[marks.Length];
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
                _markIndex = 0;
            }

            public int CreateMark()
            {
                int mark = _marks[_markIndex];
                _markIndex = (_markIndex + 1) % _marks.Length;
                return mark;
            }

            public void Print()
            {
                Console.WriteLine($"Судья: {_name}");
            }
        }

        // КЛАСС СОРЕВНОВАНИЯ
        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;
            private int _participantCount;

            public Judge[] Judges
            {
                get
                {
                    if (_judges == null) return new Judge[0];
                    Judge[] copy = new Judge[_judges.Length];
                    for (int i = 0; i < _judges.Length; i++)
                    {
                        copy[i] = _judges[i];
                    }
                    return copy;
                }
            }

            public Participant[] Participants
            {
                get
                {
                    if (_participants == null) return new Participant[0];
                    Participant[] copy = new Participant[_participantCount];
                    for (int i = 0; i < _participantCount; i++)
                    {
                        copy[i] = _participants[i];
                    }
                    return copy;
                }
            }

            public Competition(Judge[] judges)
            {
                _judges = new Judge[judges.Length];
                for (int i = 0; i < judges.Length; i++)
                {
                    _judges[i] = judges[i];
                }
                _participants = new Participant[10];
                _participantCount = 0;
            }

            public void Evaluate(Participant jumper)
            {
                int[] marks = new int[7];
                for (int judge = 0; judge < _judges.Length; judge++)
                {
                    marks[judge] = _judges[judge].CreateMark();
                }
                jumper.Jump(marks); 
            }

            public void Add(Participant jumper)
            {
                if (_participantCount >= _participants.Length)
                {
                    Participant[] newArray = new Participant[_participants.Length * 2];
                    for (int i = 0; i < _participantCount; i++)
                    {
                        newArray[i] = _participants[i];
                    }
                    _participants = newArray;
                }

                _participants[_participantCount] = jumper;
                _participantCount++;
                Evaluate(jumper);
            }

            public void Add(Participant[] jumpers)
            {
                for (int i = 0; i < jumpers.Length; i++)
                {
                    Add(jumpers[i]);
                }
            }
            public void Sort()
            {
                if (_participantCount <= 1) return;

                for (int i = 0; i < _participantCount - 1; i++)
                {
                    for (int j = 0; j < _participantCount - i - 1; j++)
                    {
                        if (_participants[j].TotalScore < _participants[j + 1].TotalScore)
                        {
                            Participant temp = _participants[j];
                            _participants[j] = _participants[j + 1];
                            _participants[j + 1] = temp;
                        }
                    }
                }
            }
        }
    }
}

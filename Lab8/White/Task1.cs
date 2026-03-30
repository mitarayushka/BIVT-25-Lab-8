namespace Lab8.White
{
    public class Task1
    {
        public class Participant
        {
            private string _surname;
            private string _club;
            private double _firstjump;
            private double _secondjump;

            private  static double  _standard;
            private static  int _jumpers;
            private static int _disqualified;



            public string Surname => _surname;
            public string Club => _club;
            public double FirstJump => _firstjump;
            public double SecondJump => _secondjump;

            public double JumpSum => _firstjump + _secondjump;
            

            public  static int Jumpers=>_jumpers;
            public static int Disqualified => _disqualified;
            public Participant(string surname, string club)
            {
                _surname = surname;
                _club = club;
                _firstjump = 0;
                _secondjump = 0;
                _jumpers++;
                
                
            }
            static Participant()
            {
                _disqualified = 0;
                _jumpers = 0;
                _standard = 5;

            }



            public void Jump(double result)
            {
                if (_firstjump == 0)
                {
                    _firstjump = result;
                }
                else if (_secondjump == 0)
                {
                    _secondjump = result;
                }

            }
            public static void Disqualify(ref Participant[] participants)
            {
                if (participants == null || participants.Length==0)
                    return;
                int count = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].FirstJump >= _standard  &&  participants[i].SecondJump>=_standard)
                    {
                        count++;
                    }
                    else
                    {
                        _disqualified++;
                        _jumpers--;
                    }
                }
                Participant[] participants1 = new Participant[count];
                int k = 0;

                for (int i=0; i < participants.Length; i++)
                {
                    if (participants[i].FirstJump >= _standard && participants[i].SecondJump >= _standard)
                    {
                        participants1[k] = participants[i];
                        k++;
                    }
                }
                participants = participants1;

            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0)
                    return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].JumpSum < array[j].JumpSum)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }

                }
            }
            public void Print()
            {
                Console.WriteLine(_surname);
                Console.WriteLine(_club);
                Console.WriteLine(_firstjump);
                Console.WriteLine(_secondjump);
                Console.WriteLine(JumpSum);
            }
        }


    }
}

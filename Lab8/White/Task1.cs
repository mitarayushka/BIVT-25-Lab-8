namespace Lab8.White
{
    public class Task1
    {
        public class Participant
        {
            private string _surname;
            private string _club;
            private double _firstJump;
            private double _secondJump;

            private static double _standard;
            private static int _jumpers;
            private static int _disqualified;

            
            public static double Standard => _standard;
            public static int Jumpers => _jumpers;
            public static int Disqualified => _disqualified;

            
            public string Surname => _surname;
            public string Club => _club;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump;
            public double JumpSum => _firstJump + _secondJump;

           
            public Participant(string surname, string club)
            {
                _surname = surname;
                _club = club;
                _firstJump = 0;
                _secondJump = 0;
                _jumpers++; // Увеличиваем счетчик активных участников
            }

            
            static Participant()
            {
                _standard = 5;
                _jumpers = 0;
                _disqualified = 0;
            }

            
            public void Jump(double result)
            {
                if (_firstJump == 0)
                {
                    _firstJump = result;
                }
                else if (_secondJump == 0)
                {
                    _secondJump = result;
                }
            }

            
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].JumpSum < array[j + 1].JumpSum)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            
            public static void Disqualify(ref Participant[] participants)
            {
                if (participants == null || participants.Length == 0)
                    return;

                int count = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].FirstJump >= _standard && participants[i].SecondJump >= _standard)
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

                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].FirstJump >= _standard && participants[i].SecondJump >= _standard)
                    {
                        participants1[k] = participants[i];
                        k++;
                    }
                }
                participants = participants1;
            }

            
            public void Print()
            {
                Console.WriteLine($"Фамилия: {_surname}");
                Console.WriteLine($"Клуб: {_club}");
                Console.WriteLine($"Первый прыжок: {_firstJump}");
                Console.WriteLine($"Второй прыжок: {_secondJump}");
                Console.WriteLine($"Сумма: {JumpSum}");
            }
        }
    }
}

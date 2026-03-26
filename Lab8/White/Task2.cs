namespace Lab8.White
{
    public class Task2
    {
        public class Participant
        {
            //создаем поля
            private string _name;
            private string _surname;
            private double _firstjump;
            private double _secondjump;


            private static readonly double _standard;
            



            //свойства 
            public string Name => _name;
            public string Surname => _surname;
            public double FirstJump => _firstjump;
            public double SecondJump => _secondjump;
            

            public double BestJump
            {
                get
                {
                    if (_firstjump == 0 && _secondjump == 0)
                        return 0;
                    else if (_firstjump != 0 && _secondjump == 0)
                        return FirstJump;
                    else if (_secondjump != 0 && _firstjump == 0)
                        return SecondJump;
                    else
                        return Math.Max(_firstjump, _secondjump);

                }
            }

            //участника IsPassed – прошел он норматив или нет.
            public bool IsPassed
            {
                get
                {
                    return BestJump>=_standard; 
                }
            }

            static Participant()
            {
                _standard = 3.0;
            }

            //конструктор из 2 строк поля
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _firstjump=0;
                _secondjump=0;

            }
            //конструктор из 4 строк поля
            public Participant(string name, string surname, double firstjump, double secondjump)
            {
                _name = name;
                _surname = surname;
                _firstjump = firstjump;
                _secondjump = secondjump;
            }



            //метод
            public static Participant[] GetPassed(Participant[] participants)
            {
                if (participants == null || participants.Length == 0)
                    return new Participant[0];
                int count = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].IsPassed)      
                        count++;
                }
                Participant[] result = new Participant[count];
                
                int k = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].IsPassed)
                    {
                        result[k++]=participants[i];
                    }
                }
                return result;

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
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0)
                    return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].BestJump < array[j].BestJump)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);

                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_surname);
                Console.WriteLine(_firstjump);
                Console.WriteLine(_secondjump);
                Console.WriteLine(BestJump);
            }
        }
    }
}

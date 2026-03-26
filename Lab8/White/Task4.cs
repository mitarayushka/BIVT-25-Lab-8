namespace Lab8.White
{
    public class Task4
    {
        public class Human
        {
             //поля
            private string _name;
            private string _surname;
            


            //свойства
            public string Name => _name;
            public string Surname => _surname;
            
            public Human(string name, string surname)
            {
                //проинициализировать все нестатик поля
                _name = name;
                _surname = surname;
                

            }
            public  virtual void Print() 
            {
                Console.WriteLine(_name);
                Console.WriteLine(_surname);
            }

        }
        public class Participant: Human
        {
            //поля 
            private double[] _scores;

            private static int _count;
            //свойства

            public static int Count => _count;




            public double[] Scores
            {
                get
                {
                    if (_scores == null || _scores.Length == 0)
                        return new double[0];


                    double[] copy = new double[_scores.Length];
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        copy[i] = _scores[i];
                    }
                    return copy;


                }

            }
            public double TotalScore
            {
                get
                {
                    if (_scores == null || _scores.Length == 0)
                        return 0;
                    double sum = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        sum += _scores[i];
                    }
                    return sum;
                }
            }



            // Статический конструктор 
            static Participant()
            {
                _count = 0;
            }



            //конструктор
            public Participant(string name, string surname) : base(name, surname)
            {
                _scores = new double[0];
                _count++;
            }


            public void PlayMatch(double result)
            {

                if (result == 0 || result == 0.5 || result == 1)
                {
                    if (_scores == null)
                        _scores = new double[0];
                    Array.Resize(ref _scores, _scores.Length + 1);
                    _scores[_scores.Length - 1] = result;
                }


            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0)
                    return;

                // Пузырьковая сортировка
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].TotalScore < array[j].TotalScore)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
            public  override void Print()
            {
                base.Print();
                Console.WriteLine(Count );
                Console.WriteLine(_scores);
                Console.WriteLine(TotalScore);
            }
        }

    }
}

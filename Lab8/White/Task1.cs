namespace Lab8.White
{
    public class Task1
    {
        public class Participant
        {
            //Поля
            private string _surname;
            private string _club;
            private double _firstjump;
            private double _secondjump;

            //Статические поля 
            private static int _jumpers;
            private static int _disqualified;
            private static double _standard;

            //Свойства
            public string Surname => _surname;
            public string Club => _club;
            public double FirstJump => _firstjump;
            public double SecondJump => _secondjump;
            public double JumpSum => _firstjump + _secondjump;

            //Статические свойства 
            public static int Jumpers => _jumpers;
            public static int Disqualified => _disqualified;
            
            //Конструктор
            public Participant(string surname, string club)
            {
                _surname = surname;
                _club = club;
                _firstjump = 0;
                _secondjump = 0;
                _jumpers++;
            }
            //Статический конструктор 
            static Participant()
            {
                _jumpers = 0;
                _disqualified = 0;
                _standard = 5;
            }
            //Методы
            public void Jump(double result)
            {
                if (_firstjump == 0) _firstjump = result;   //если первый прыжок ещё не задан
                else if (_secondjump == 0) _secondjump = result;  //иначе если второй ещё не задан
            }

            public static void Disqualify(ref Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                int count = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].FirstJump >= _standard && participants[i].SecondJump >= _standard) count++;
                    else
                    {
                        _disqualified++;
                        _jumpers--;
                    }
                }
                //Создаём массив только для квалифицированных участников
                Participant[] parti = new Participant[count];
                int s = 0;

                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].FirstJump >= _standard && participants[i].SecondJump >= _standard)
                    {
                        parti[s] = participants[i];
                        s++;
                    }
                }
                //Заменяем исходный массив новым
                participants = parti;
            }

            //Пузырьком сортируем массив участников по убыванию суммы двух прыжков
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
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
                return;
            }
        }
    }
}

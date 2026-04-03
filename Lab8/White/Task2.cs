namespace Lab8.White
{
    public class Task2
    {
        public class Participant
        {
            //Поля
            private string _name;
            private string _surname;
            private double _firstjump;
            private double _secondjump;

            //Статическое поле, где значение задается один раз и не может быть изменено
            private static readonly double _standard; 

            //Свойства
            public string Name => _name;
            public string Surname => _surname;
            public double FirstJump => _firstjump;
            public double SecondJump => _secondjump;


            public double BestJump
            {
                get
                {
                    if (_firstjump == 0 && _secondjump == 0) return 0; //нет ни одного прыжка
                    else if (_firstjump != 0 && _secondjump == 0) return FirstJump; //только первый прыжок
                    else if (_secondjump != 0 && _firstjump == 0) return SecondJump; //только второй прыжок
                    else return Math.Max(_firstjump, _secondjump); //максимум, если есть оба
                }
            }
            //Свойство, прошёл ли участник норматив
            public bool IsPassed
            {
                get
                {
                    return BestJump >= _standard;
                }
            }
            //Статический конструктор 
            static Participant()
            {
                _standard = 3.0; //норматив
            }
            //Конструктор 
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _firstjump = 0;
                _secondjump = 0;
            }
            //Конструктор для результатов сразу двух прыжков
            public Participant(string name, string surname, double firstjump, double secondjump)
            {
                _name = name;
                _surname = surname;
                _firstjump = firstjump;
                _secondjump = secondjump;
            }
            //Методы
            public static Participant[] GetPassed(Participant[] participants)
            {
                //Защита от null/пустого массива (возвращаем пустой массив)
                if (participants == null || participants.Length == 0) return new Participant[0];
                // Подсчёт количества прошедших норматив
                int s = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].IsPassed) s++;
                }
                //Создаем новый массив нужного размера
                Participant[] result = new Participant[s];
                int x = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].IsPassed)
                    {
                        result[x++] = participants[i];
                    }
                }
                return result;
            }
            public void Jump(double result)
            {
                if (_firstjump == 0) _firstjump = result; //если первый прыжок ещё не задан
                else if (_secondjump == 0) _secondjump = result;  //иначе если второй ещё не задан
            }
            //Сортируем массив участников по убыванию лучшего прыжка
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
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
                return;
            }
        }
    }
}

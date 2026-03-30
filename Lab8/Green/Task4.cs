namespace Lab8.Green
{
    public class Task4

    {
        public struct Participant
        {
            // поля
            private string _name;
            private string _surname;
            private double[] _jumps;

            //  свойства
            public string Name => _name;
            public string Surname => _surname;
            public double[] Jumps => _jumps.ToArray();
            public double BestJump => _jumps.Max();
            public void UpdateJumps(double[] jumps) // полностью заменяет массив результатов прыжков участника (_jumps)
                                                    // на новый массив, переданный в параметре jumps
            {
                _jumps = jumps;
            }

            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3]; // массив из трех попыток
            }
            public void Jump(double result) // заполняет результат очередного
                                            // прыжка в массиве данными.
            {
                for (int i = 0; i < _jumps.Length; i++)
                {
                    if (_jumps[i] == 0) // если прыжка не было, то заполняем место
                    {
                        _jumps[i] = result; // заполняем элемент массива значением прыжка
                        break;
                    }
                }
            }
            public static void Sort(Participant[] array) // по убыванию лучшего результата спортсмена
            {
                Array.Sort(array, (a, b) => b.BestJump.CompareTo(a.BestJump)); // от большего к меньшему
            }
            public void Print()
            {
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Surname: {Surname}");
                Console.WriteLine($"Jumps: {Jumps}");
                Console.WriteLine($"Best jump: {BestJump}");
            }

        }
        public abstract class Discipline
        {
            // поля
            private string _name;
            private Participant[] _participants;

            // свойства
            public string Name => _name;
            public Participant[] Participants => _participants;

            // конструктор
            protected Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
            }

            public void Add(Participant participant) // добавляет одного и несколько новых участников в массив

            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants) // метод для добавления массива участников в существующий список; принимает массив participants — новых участников для добавления
            
                {
                int startIdx = _participants.Length; // длина массива
                Array.Resize(ref _participants, startIdx + participants.Length);// сразу после последнего
                for (int i = 0; i < participants.Length; i++)
                {
                    _participants[startIdx + i] = participants[i]; // добавляем новых участников
                }
            }

            public void Sort() // в убывающем порядке (от лучшего к худшему).
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int index); //  позволяет выбранному участнику соревнований прыгнуть ещё раз(с определенным условием)

            public void Print() { }
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Long Jump") { } // конструктор класса LongJump; вызывает конструктор базового класса Discipline


            public override void Retry(int index) // переопределяет абстрактный метод Retry из базового класса Discipline
            {
                double bestJump = Participants[index].BestJump; 
                Participants[index].UpdateJumps(new[] { bestJump, 0d, 0d });
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High Jump") { }  

            public override void Retry(int index) // переопределяет абстрактный метод Retry из Discipline
            {
                double[] jumps = Participants[index].Jumps;
                jumps[2] = 0.0;

                Participants[index].UpdateJumps(jumps);
            }
        }
    }
}

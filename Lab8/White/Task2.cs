namespace Lab8.White
{
    public class Task2
    {
        public class Participant
        {
            // ПОЛЯ
            protected static int _standard;
            private string _name;
            private string _surname;
            private double _firstJump;
            private double _secondJump;

            //СВОЙСТВА
            public bool isPassed => BestJump >= _standard;
            public string Name => _name;
            public string Surname => _surname;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump; 

            public double BestJump
            {
                get
                {
                    return Math.Max(_firstJump,_secondJump);
                }
            }

            // КОНСТРУКТОР
            static Participant()
            {
                _standard = 3;
            }
            public Participant(string name,string surname,double firstJump, double secondJump)
            {
                _name = name;
                _surname = surname;
                _firstJump = firstJump;
                _secondJump = secondJump;
            }
            public static Participant[] GetPassed(Participant[] participants)
            {
                int passed = 0;  
                foreach (var participant in participants)
                {
                    if (participant.BestJump >= _standard)
                    {
                        passed++;
                    }
                }
                Participant[] newArray = new Participant[passed];
                int index = 0;
                foreach( var participant in participants)
                {
                    if(participant.BestJump >= _standard)
                    {
                        newArray[index] = participant;
                        index++;
                    }
                }
                return newArray;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;
                for ( int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {
                            // 5. INTERCAMBIO
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
        }
    }
}
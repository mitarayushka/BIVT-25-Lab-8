using System.Linq.Expressions;

namespace Lab8.White
{
    public class Task1
    {
        public class Participant
        {
             // Поля
            private static int _standard;
            private static int  _jumpers;
            private static int  _desqualified;
            private string _surname;
            private string _club;
            private double _firstJump;
            private double _secondJump;
            private bool _firstJumpSet;     // Necesario para saber si ya se estableció
            private bool _secondJumpSet;    // Necesario para saber si ya se estableció
            // Свойства
            public static int Jumpers => _jumpers;
            public static int Desqualified => _desqualified;
            public string Surname => _surname;
            public string Club => _club;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump;
            public double JumpSum
            {
                get
                {
                    double sum = _firstJump + _secondJump;
                    return sum;
                }
            }
            // Конструстор
            static Participant()
            {
                _standard = 5;
            }
            public Participant(string surname,string club)
            {
                _surname = surname;
                _club = club;
                _firstJump = 0;
                _secondJump = 0;
                _firstJumpSet = false;      // Inicialmente no hay primer salto
                _secondJumpSet = false;     // Inicialmente no hay segundo salto
            }
            public static void Desqualify(ref Participant [] participants)
            {
                if (participants.Length == 0)
                {
                    Console.WriteLine("There are no participants");
                }
 
                foreach (var participant in participants)
                {
                    if(participant.FirstJump >= _standard && participant.SecondJump >= _standard)
                    {
                        _jumpers++;

                    }
                    else
                    {
                        _desqualified++;
                            
                    }
                }
                int index = 0;
                Participant [] newArray = new Participant[_jumpers];
                foreach (var participant in participants)
                {
                    if(participant.FirstJump >= _standard && participant.SecondJump >= _standard)
                    {
                        newArray[index] = participant;
                        index++;
                    }
                }
                participants = newArray;
            }
            public void Jump(double result)
            {
                if (!_firstJumpSet)
                {
                    _firstJump = result;
                    _firstJumpSet = true;
                }
                else if (!_secondJumpSet)
                {
                    _secondJump = result;
                    _secondJumpSet = true;
                }
            }
            public static void Sort(Participant[] array)
            {
                Array.Sort(array, (a, b) => b.JumpSum.CompareTo(a.JumpSum));
            }
            public void Print()
            {
                Console.WriteLine($"Suername: {_surname}");
                Console.WriteLine($"Club: {_club}");
                Console.WriteLine($"First jump: {_firstJump:F2}");
                Console.WriteLine($"Second jump: {_secondJump:F2}");
                Console.WriteLine($"Sum: {JumpSum:F2}");
                Console.WriteLine("------------------------");
            }
        }
    }
}
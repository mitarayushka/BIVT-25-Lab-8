namespace Lab8.White
{
    public class Task1
    {
        public class Participant
        {
            private string surname;
            private string club;
            private double firstJump;
            private double secondJump;
            private bool firstJumpSet;
            private bool secondJumpSet;

            private static double _standard;
            private static int _jumpers;
            private static int _disqualified;

            public string Surname => surname;
            public string Club => club;
            public double FirstJump => firstJump;
            public double SecondJump => secondJump;
            public double JumpSum => firstJump + secondJump;

            public static int Jumpers => _jumpers;
            public static int Disqualified => _disqualified;

            static Participant()
            {
                _standard = 5;
                _jumpers = 0;
                _disqualified = 0;
            }

            public Participant(string surname, string club)
            {
                this.surname = surname;
                this.club = club;
                firstJump = 0;
                secondJump = 0;
                firstJumpSet = false;
                secondJumpSet = false;
                _jumpers++;
            }

            public void Jump(double result)
            {
                if (!firstJumpSet)
                {
                    firstJump = result;
                    firstJumpSet = true;
                }
                else if (!secondJumpSet)
                {
                    secondJump = result;
                    secondJumpSet = true;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
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
                if (participants == null) return;

                int validCount = 0;

                // 统计通过标准的参与者数量（两个跳都达到或超过标准）
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null &&
                        participants[i].firstJump >= _standard &&
                        participants[i].secondJump >= _standard)
                    {
                        validCount++;
                    }
                    else if (participants[i] != null)
                    {
                        _disqualified++;
                        _jumpers--;
                    }
                }

                // 创建新数组
                Participant[] newArray = new Participant[validCount];
                int index = 0;

                // 填充通过标准的参与者
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null &&
                        participants[i].firstJump >= _standard &&
                        participants[i].secondJump >= _standard)
                    {
                        newArray[index++] = participants[i];
                    }
                }

                participants = newArray;
            }

            public void Print()
            {
                Console.WriteLine($"Surname: {surname}, Club: {club}, FirstJump: {firstJump}, SecondJump: {secondJump}, JumpSum: {JumpSum}");
            }
        }
    }
}

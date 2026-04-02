namespace Lab8.White
{
    public class Task2
    {
        public class Participant
        {
            private string name;
            private string surname;
            private double firstJump;
            private double secondJump;
            private bool firstJumpSet;
            private bool secondJumpSet;

            // 静态只读字段 - 不变的标准
            private static readonly double _standard;

            public string Name => name;
            public string Surname => surname;
            public double FirstJump => firstJump;
            public double SecondJump => secondJump;
            public double BestJump => Math.Max(firstJump, secondJump);

            // 属性：判断是否通过标准（根据最好成绩）
            public bool IsPassed => BestJump >= _standard;

            // 静态构造函数：初始化标准为 3
            static Participant()
            {
                _standard = 3;
            }

            // 构造函数：只接受姓名
            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                firstJump = 0;
                secondJump = 0;
                firstJumpSet = false;
                secondJumpSet = false;
            }

            // 新构造函数：接受姓名和两次跳跃成绩
            public Participant(string name, string surname, double firstJump, double secondJump)
            {
                this.name = name;
                this.surname = surname;
                this.firstJump = firstJump;
                this.secondJump = secondJump;
                this.firstJumpSet = true;
                this.secondJumpSet = true;
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
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            // 静态方法：返回通过标准的参与者数组
            public static Participant[] GetPassed(Participant[] participants)
            {
                if (participants == null)
                {
                    return new Participant[0];
                }

                // 统计通过的人数
                int passedCount = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].IsPassed)
                    {
                        passedCount++;
                    }
                }

                // 创建结果数组并填充
                Participant[] result = new Participant[passedCount];
                int index = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].IsPassed)
                    {
                        result[index] = participants[i];
                        index++;
                    }
                }

                return result;
            }

            public void Print()
            {
                Console.WriteLine($"Name: {name}, Surname: {surname}, FirstJump: {firstJump}, SecondJump: {secondJump}, BestJump: {BestJump}");
            }
        }
    }
}

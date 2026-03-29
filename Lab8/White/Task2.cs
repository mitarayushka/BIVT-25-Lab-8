using System;
using System.Collections.Generic;


    public class Task2
    {
        public class Participant
        {
           
            private string _name;
            private string _surname;
            private double _firstJump;
            private double _secondJump;

            
            private static readonly double _standard;

            
            public string Name => _name;
            public string Surname => _surname;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump;

            
            public double BestJump
            {
                get
                {
                    if (_firstJump > _secondJump)
                        return _firstJump;
                    else
                        return _secondJump;
                }
            }

            // Свойство для определения статуса участника (прошел норматив или нет)
            public bool IsPassed
            {
                get
                {
                    return BestJump >= _standard;
                }
            }

            // Статический конструктор (устанавливает норматив = 3)
            static Participant()
            {
                _standard = 3;
            }

            
            public Participant(string name, string surname, double firstJump, double secondJump)
            {
                _name = name;
                _surname = surname;
                _firstJump = firstJump;
                _secondJump = secondJump;
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
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            
            public static Participant[] GetPassed(Participant[] participants)
            {
                if (participants == null || participants.Length == 0)
                    return new Participant[0];

                
                int passedCount = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].IsPassed)
                    {
                        passedCount++;
                    }
                }

                
                Participant[] passedParticipants = new Participant[passedCount];
                int index = 0;

                
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].IsPassed)
                    {
                        passedParticipants[index] = participants[i];
                        index++;
                    }
                }

                return passedParticipants;
            }

            
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} | Прыжки: {_firstJump}, {_secondJump} | Лучший: {BestJump} | Норматив пройден: {(IsPassed ? "Да" : "Нет")}");
            }
        }
    }


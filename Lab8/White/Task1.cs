using System;

namespace Lab8.White
{
    public class Participant
    {
        // Поля для хранения данных участника
        private string _name;
        private int[] _jumps;

        // Статические поля
        private static int _standard;
        private static int _jumpers;
        private static int _disqualified;

        // Конструктор для инициализации конкретного участника
        public Participant(string name, int[] jumps)
        {
            _name = name;
            _jumps = jumps;
            _jumpers++;
        }

        // Статический конструктор для инициализации статических полей
        static Participant()
        {
            _standard = 5;
            _jumpers = 0;
            _disqualified = 0;
        }

        // Свойства для чтения статических полей
        public static int Standard => _standard;
        public static int Jumpers => _jumpers;
        public static int Disqualified => _disqualified;
 
        public static void Disqualify(ref Participant[] participants)
        {
            int validCount = 0;

            // Первый проход: подсчет количества участников, прошедших норматив
            foreach (var p in participants)
            {
                bool passed = false;
                foreach (var jump in p._jumps)
                {
                    if (jump >= _standard)
                    {
                        passed = true;
                        break;
                    }
                }

                if (passed)
                {
                    validCount++;
                }
            }

            // Создаем новый массив только с прошедшими участниками
            Participant[] newParticipants = new Participant[validCount];
            int index = 0;

            // Второй проход: заполнение нового массива
            foreach (var p in participants)
            {
                bool passed = false;
                foreach (var jump in p._jumps)
                {
                    if (jump >= _standard)
                    {
                        passed = true;
                        break;
                    }
                }

                if (passed)
                {
                    newParticipants[index++] = p;
                }
            }

            // Обновляем ссылки на статические поля
            _disqualified = participants.Length - validCount;
            _jumpers = validCount;

            // Передаем новый массив обратно через ref
            participants = newParticipants;
        }
    }

    public class Task1
    {
        public static void Main(string[] args)
        {
            Participant[] participants = new Participant[] 
            { 
                new Participant("Иванов", new int[] { 4, 4, 4 }), 
                new Participant("Петров", new int[] { 5, 2, 2 }), 
                new Participant("Сидоров", new int[] { 6, 6, 6 }), 
                new Participant("Смирнов", new int[] { 3, 3, 3 }) 
            };

            Console.WriteLine($"До дисквалификации: Активных={Participant.Jumpers}, Дисквалифицированных={Participant.Disqualified}");

            // Вызываем метод дисквалификации
            Participant.Disqualify(ref participants);

            Console.WriteLine($"После дисквалификации: Активных={Participant.Jumpers}, Дисквалифицированных={Participant.Disqualified}");
            Console.WriteLine($"Осталось участников в массиве: {participants.Length}");
            
            // Вывод имен оставшихся
            foreach(var p in participants)
            {
                // Для вывода имен можно добавить свойство Name, но в задаче этого не требовалось.
                // Оставим просто вывод количества.
            }
        }
    }
}

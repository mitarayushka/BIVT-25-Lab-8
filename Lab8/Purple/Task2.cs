namespace Lab8.Purple
{
  public class Task2
  {
    public struct Participant
    {
      private string _name;
      private string _surname;
      private int _distance;
      private int[] _marks;
      private int _target;

      public string Name => _name;
      public string Surname => _surname;
      public int Distance => _distance;
      public int[] Marks
      { get
        {
          int[] answer = new int[5];
          for (int i = 0; i < 5; i++)
            answer[i] = _marks[i];
          return answer;
        }
      }
      public int Result
      { get
        {
          int answer = 60, S = 0, mx = int.MinValue, mn = int.MaxValue;
          for (int i = 0; i < _marks.Length; i++)
          {
            (mx, mn) = (Math.Max(_marks[i], mx), Math.Min(_marks[i], mn));
            S += _marks[i];
          }
          S = S - (mx + mn);
          answer = Math.Max(0, answer + S + (_distance - _target) * 2);
          return answer;
        }
      }

      public Participant(string name, string surname)
      {
        _name = name;
        _surname = surname;
        _distance = 0;
        _marks = new int[5] { 0, 0, 0, 0, 0 };
        _target = 120;
      }
      public void Jump(int distance, int[] marks, int target)
      {
        _distance = distance;
        for (int i = 0; i < marks.Length; i++)
          _marks[i] = marks[i];
        _target = target;
      }
      public static void Sort(Participant[] array)
      {
        for (int i = 0; i < array.Length; i++)
          for (int j = 0; j < array.Length - i - 1; j++)
            if (array[j].Result < array[j + 1].Result)
              (array[j], array[j + 1]) = (array[j + 1], array[j]);
      }
      public void Print()
      {
        Console.Write($"Name:{_name}  Surname:{_surname}  Distance:{_distance}\nMarks:");
        for (int i = 0; i < _marks.Length; i++)
          Console.Write(_marks[i] + " ");
        Console.WriteLine();
      }
    }

    public abstract class SkiJumping
    {
      private string _name;
      private int _standard;
      private Participant[] _participants;
      private int _counter;

      public string Name => _name;
      public int Standard => _standard;
      public Participant[] Participants => _participants;

      public SkiJumping(string name, int standard)
      {
        _name = name;
        _standard = standard;
        _participants = new Participant[0];
        _counter = 0;
      }

      public void Add(Participant participant)
      {
        Participant[] array = new Participant[_participants.Length + 1];
        for (int i = 0; i < _participants.Length; i++) array[i] = _participants[i];
        array[array.Length - 1] = participant;
        _participants = array;
      }
      public void Add(Participant[] participant)
      {
        for (int i = 0; i < participant.Length; i++)
          Add(participant[i]);
      }
      public void Jump(int distance, int[] marks)
      {
        _participants[_counter].Jump(distance, marks, _standard);
        _counter++;
      }
      public void Print()
      {
        Console.WriteLine($"Name {_name}  Standard {_standard}");
      }
    }

    public class JuniorSkiJumping : SkiJumping
    {
      public JuniorSkiJumping() : base("100m", 100) { }
    }

    public class ProSkiJumping : SkiJumping
    {
      public ProSkiJumping() : base("150m", 150) { }
    }

  }
}

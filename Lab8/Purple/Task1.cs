namespace Lab8.Purple
{
  public class Task1
  {
    public class Participant
    {
      private string _name;
      private string _surname;
      private double[] _coefs;
      private int[,] _marks;

      public string Name => _name;
      public string Surname => _surname;
      public double[] Coefs
      { get
        {
          double[] answer = new double[4];
          for (int i = 0; i < _coefs.Length; i++)
            answer[i] = _coefs[i];
          return answer;
        }
      }
      public int[,] Marks
      { get
        {
          int[,] answer = new int[4, 7];
          for (int i = 0; i < answer.GetLength(0); i++)
            for (int j = 0; j < answer.GetLength(1); j++)
              answer[i, j] = _marks[i, j];
          return answer;
        }
      }
      public double TotalScore
      { get
        {
          double answer = 0;
          for (int i = 0; i < _marks.GetLength(0); i++)
          {
            double S = 0, mx = int.MinValue, mn = int.MaxValue;
            for (int j = 0; j < _marks.GetLength(1); j++)
            {
              (mx, mn) = (Math.Max(_marks[i, j], mx), Math.Min(_marks[i, j], mn));
              S += _marks[i, j];
            }
            S = S - (mx + mn);
            S = S * _coefs[i];
            answer += S;
          }
          return answer;
        }
      }

      public Participant(string name, string surname)
      {
        _name = name;
        _surname = surname;
        _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
        _marks = new int[4, 7];

      }
      public void SetCriterias(double[] coefs)
      {
        for (int i = 0; i < coefs.Length; i++)
          _coefs[i] = coefs[i];
      }
      int counter = 0;
      public void Jump(int[] marks)
      {
        for (int i = 0; i < marks.Length; i++)
          _marks[counter, i] = marks[i];
        counter++;
        if (counter >= 4) counter = 0;
      }
      public static void Sort(Participant[] array)
      {
        for (int i = 0; i < array.Length; i++)
          for (int j = 0; j < array.Length - i - 1; j++)
            if (array[j].TotalScore < array[j + 1].TotalScore)
              (array[j], array[j + 1]) = (array[j + 1], array[j]);
      }
      public void Print()
      {
        Console.Write($"Name:{_name}  Surname:{_surname}\nCoefs:");
        for (int i = 0; i < _coefs.Length; i++)
          Console.Write(_coefs[i] + " ");
        Console.Write("\nMarks:");
        for (int i = 0; i < _marks.GetLength(0); i++)
        {
          for (int j = 0; j < _marks.GetLength(1); j++)
            Console.Write(_marks[i, j] + " ");
          Console.WriteLine();
        }
        Console.WriteLine();
      }
    }

    public class Judge
    {
      private string _name;
      private int[] _marks;
      private int _counter;

      public string Name => _name;
      public Judge(string name, int[] marks)
      {
        _name = name;
        _marks = (int[])marks.Clone();
        _counter = 0;
      }
      public int CreateMark()
      {
        return _marks[_counter++ % _marks.Length];
      }
      public void Print()
      {
        Console.WriteLine("Name " + _name);
      }
    }

    public class Competition
    {
      private Judge[] _judges;
      private Participant[] _participants;

      public Judge[] Judges => _judges;
      public Participant[] Participants => _participants;
      public Competition(Judge[] judges)
      {
        _judges = (Judge[])judges.Clone();
        _participants = new Participant[0];
      }
      public void Evaluate(Participant jumper)
      {
        int[] array = new int[_judges.Length];
        for (int i = 0; i < _judges.Length; i++)
          array[i] = _judges[i].CreateMark();
        jumper.Jump(array);
      }
      public void Add(Participant participant)
      {
        Evaluate(participant);
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
      public void Sort()
      {
        Participant.Sort(_participants);
      }
    }
  }
}

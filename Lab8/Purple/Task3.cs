using static Lab8.Purple.Task1;

namespace Lab8.Purple
{
  public class Task3
  {
    public struct Participant
    {
      private string _name;
      private string _surname;
      private double[] _marks;
      private int[] _places;
      private double _totalMark;

      public string Name => _name;
      public string Surname => _surname;
      public double[] Marks => _marks.ToArray();
      public int[] Places => _places.ToArray();
      public int TopPlace => _places.Min();
      public double TotalMark
      { get
        {
          _totalMark = 0;
          for (int i = 0; i < _marks.Length; i++)
            _totalMark += _marks[i];
          _totalMark = Math.Round(_totalMark, 7);
          return _totalMark;
        }
      }
      public int Score { get { return _places.Sum(); } }

      public Participant(string name, string surname)
      {
        _name = name;
        _surname = surname;
        _marks = new double[7] { 0, 0, 0, 0, 0, 0, 0 };
        _places = new int[7];
        _totalMark = 0;
      }

      int counterMarks = 0;
      public void Evaluate(double result)
      {
        _marks[counterMarks++] = result;
      }
      public static void SetPlaces(Participant[] participants)
      {
        for (int i = 0; i < 7; i++)
        {
          participants = participants.OrderBy(p => p.Marks[i]).ToArray();
          for (int j = 0; j < participants.Length; j++)
            participants[j]._places[i] = participants.Length - j;
        }
      }
      private static bool flag(Participant p1, Participant p2)
      {
        bool swap = false;
        if (p1.Score < p2.Score)
          swap = true;
        else if (p1.Score == p2.Score && p1.TopPlace < p2.TopPlace)
          swap = true;
        else if (p1.Score == p2.Score && p1.TopPlace == p2.TopPlace && p1.TotalMark > p2.TotalMark)
          swap = true;
        return swap;
      }
      public static void Sort(Participant[] array)
      {
        for (int i = 0; i < array.Length; i++)
          for (int j = i; j > 0 && flag(array[j], array[j - 1]); j--)
            (array[j], array[j - 1]) = (array[j - 1], array[j]);
      }
      public void Print()
      {
        Console.Write($"------------\nName:{_name}  Surname:{_surname}\nMarks:");
        for (int i = 0; i < _marks.Length; i++)
          Console.Write(_marks[i] + " ");
        Console.Write("\nPlaces:");
        for (int i = 0; i < _places.Length; i++)
          Console.Write(_places[i] + " ");
        Console.WriteLine($"\nTotalMark:{_totalMark}\n------------");
      }
    }

    public abstract class Skating
    {
      protected Participant[] _participants;
      protected double[] _moods;
      private int _counter;

      public Participant[] Participants => _participants;
      public double[] Moods => _moods;
      public Skating(double[] moods)
      {
        _moods = (double[])moods.Clone();
        _participants = new Participant[0];
        _counter = 0;
        ModificateMood();
      }
      protected abstract void ModificateMood();
      public void Evaluate(double[] marks)
      {
        for (int i = 0; i < marks.Length; i++)
          _participants[_counter].Evaluate(marks[i] * _moods[i]);
        _counter++;
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
    }

    public class FigureSkating : Skating
    {
      public FigureSkating(double[] moods) : base(moods) { }
      protected override void ModificateMood()
      {
        for (int i = 0; i < _moods.Length; i++)
          _moods[i] += ((i + 1) / 10.0);
      }
    }

    public class IceSkating : Skating
    {
      public IceSkating(double[] moods) : base(moods) { }
      protected override void ModificateMood()
      {
        for (int i = 0; i < _moods.Length; i++)
          _moods[i] += (_moods[i] * (i + 1) / 100.0);
      }
    }

  }
}

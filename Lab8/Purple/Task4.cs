using static Lab8.Purple.Task4;

namespace Lab8.Purple
{
  public class Task4
  {
    public class Sportsman
    {
      private string _name;
      private string _surname;
      private double _time;

      public string Name => _name;
      public string Surname => _surname;
      public double Time => _time;

      public Sportsman(string name, string surname)
      {
        _name = name;
        _surname = surname;
        _time = 0;
      }
      public void Run(double time)
      {
        _time = time;
      }
      public static void Sort(Sportsman[] array)
      {
        array = array.OrderBy(a => a.Time).ToArray();
      }
      public void Print()
      {
        Console.Write($"Name:{_name}  Surname:{_surname}  Time:{_time}");
      }
    }

    public class SkiMan : Sportsman
    {
      public SkiMan(string name, string surname) : base(name, surname) { }
      public SkiMan(string name, string surname, double time) : base(name, surname)
      { Run(time); }
    }
    public class SkiWoman : Sportsman
    {
      public SkiWoman(string name, string surname) : base(name, surname) { }
      public SkiWoman(string name, string surname, double time) : base(name, surname)
      { Run(time); }
    }

    public class Group
    {
      private string _name;
      private Sportsman[] _sportsmen;

      public string Name => _name;
      public Sportsman[] Sportsmen => _sportsmen;

      public Group(string name)
      {
        _name = name;
        _sportsmen = new Sportsman[0];
      }
      public Group(Group group)
      {
        _name = group.Name;
        _sportsmen = new Sportsman[group._sportsmen.Length];
        Array.Copy(group._sportsmen, _sportsmen, group._sportsmen.Length);
      }
      public void Add(Sportsman sportsman)
      {
        Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
        _sportsmen[_sportsmen.Length - 1] = sportsman;
      }
      public void Add(Sportsman[] sportsman)
      {
        for (int i = 0; i < sportsman.Length; i++)
          Add(sportsman[i]);
      }
      public void Add(Group group)
      {
        Sportsman[] sportsman = group._sportsmen;
        for (int i = 0; i < sportsman.Length; i++)
          Add(sportsman[i]);
      }
      public void Sort()
      {
        for (int i = 0; i < _sportsmen.Length; i++)
          for (int j = 0; j < _sportsmen.Length - i - 1; j++)
            if (_sportsmen[j].Time > _sportsmen[j + 1].Time)
              (_sportsmen[j], _sportsmen[j + 1]) = (_sportsmen[j + 1], _sportsmen[j]);
      }
      public static Group Merge(Group group1, Group group2)
      {
        Group answer = new Group("Финалисты");
        answer.Add(group1);
        answer.Add(group2);
        answer.Sort();
        return answer;
      }
      public void Split(out Sportsman[] men, out Sportsman[] women)
      {
        men = new Sportsman[0];
        women = new Sportsman[0];
        for (int i = 0; i < _sportsmen.Length; i++)
        {
          if (_sportsmen[i] is SkiMan)
          {
            Array.Resize(ref men, men.Length + 1);
            men[men.Length - 1] = _sportsmen[i];
          }
          else if (_sportsmen[i] is SkiWoman)
          {
            Array.Resize(ref women, women.Length + 1);
            women[women.Length - 1] = _sportsmen[i];
          }
        }
      }
      public void Shuffle()
      {
        Sort();
        Split(out Sportsman[] men, out Sportsman[] women);
        men = men.OrderBy(m => m.Time).ToArray();
        women = women.OrderBy(w => w.Time).ToArray();
        int count = 0, countMen = 0, countWomen = 0;
        if (men[0].Time < women[0].Time)
          while (count < _sportsmen.Length)
          {
            if (countMen < men.Length)
              _sportsmen[count++] = men[countMen++];
            if (countWomen < women.Length)
              _sportsmen[count++] = women[countWomen++];
          }
        else
          while (count < _sportsmen.Length)
          {
            if (countMen < women.Length)
              _sportsmen[count++] = women[countMen++];
            if (countWomen < men.Length)
              _sportsmen[count++] = men[countWomen++];
          }
      }
      public void Print()
      {
        Console.Write($"Name:{_name}");
      }
    }

  }
}

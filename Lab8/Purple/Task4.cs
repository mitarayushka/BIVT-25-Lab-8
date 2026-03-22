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
	_time = .0;
      }

      public void Run(double time)
      {
	if (_time <= .0001)
	  _time = time;
      }

      public void Print()
      {
	Console.Write($"Name: {Name}\nSurname: {Surname}\nTime: {Time}\n\n");
      }

      public static void Sort(Sportsman[] array)
      {
	array = array.OrderBy((x => x.Time)).ToArray();
      }
    }

    public class SkiMan : Sportsman
    {
      public SkiMan(string name, string surname) : base (name, surname) {}
      public SkiMan(string name, string surname, double time) : base (name, surname) {Run(time);}
    }

    public class SkiWoman : Sportsman
    {
      public SkiWoman(string name, string surname) : base (name, surname) {}
      public SkiWoman(string name, string surname, double time) : base (name, surname) {Run(time);}
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
	_sportsmen = (Sportsman[])group.Sportsmen.Clone();
      }

      private void Append(ref Sportsman[] s, Sportsman sportsman)
      {
	Sportsman[] buff = new Sportsman[s.Length+1];
	for (int i = 0; i < s.Length; i++)
	{
	  buff[i] = s[i];
	}
	buff[s.Length] = sportsman;
	s = buff;
      }

      public void Add(Sportsman sportsman)
      {
	Append(ref _sportsmen, sportsman);
	//_sportsmen = (Sportsman[])_sportsmen.OrderBy((s => s.Time)).ToArray().Clone();
	//Array.Sort(_sportsmen);
	//Sort();
      }

      public void Add(Sportsman[] sportsmen)
      {
	foreach (Sportsman sportsman in sportsmen)
	{
	  Append(ref _sportsmen, sportsman);
	}
	//sportsmen = (Sportsman[])_sportsmen.OrderBy((s => s.Time)).ToArray().Clone();
	//Array.Sort(_sportsmen);
	//Sort();
      }

      public void Add(Group group)
      {
	foreach (Sportsman sportsman in group.Sportsmen)
	{
	  Append(ref _sportsmen, sportsman);
	}
	//sportsmen = (Sportsman[])_sportsmen.OrderBy((s => s.Time)).ToArray().Clone();
	//Array.Sort(_sportsmen);
	//Sort();
      }

      public void Sort()
      {
	_sportsmen = _sportsmen.OrderBy((x => x.Time)).ToArray();
      }

      public static Group Merge(Group group1, Group group2)
      {
	Group temp = (Group)group1.MemberwiseClone();
	temp.Add(group2);
	temp._name = "Финалисты";
	temp.Sort();
	return temp;
      }

      public void Print()
      {
	Console.Write($"Name: {Name}\nMembers:\n\n");
	foreach (Sportsman s in Sportsmen)
	{
	  s.Print();
	}
      }

      public void Split(out Sportsman[] men, out Sportsman[] women)
      {
	men = new Sportsman[0];
	women = new Sportsman[0];

	foreach (Sportsman s in _sportsmen)
	{
	  if (s is SkiMan)
	  {
	    men = men.Append(s).ToArray();
	  }
	  else
	  {
	    women = women.Append(s).ToArray();
	  }
	}
      }

      public void Shuffle()
      {
	Sort();
	Split(out Sportsman[] men, out Sportsman[] women);
	Sportsman[] s1, s2;
	if (men.Length == 0 || women.Length == 0) return;
	if (men[0].Time > women[0].Time)
	{
	  s1 = women.ToArray();
	  s2 = men.ToArray();
	}
	else
	{
	  s1 = men.ToArray();
	  s2 = women.ToArray();
	}
	int done = 0;
	int done1 = 0;
	int done2 = 0;
	while (done < _sportsmen.Length)
	{
	  if (done1 < s1.Length)
	  {
	    _sportsmen[done++] = s1[done1++];
	  }
	  if (done2 < s2.Length)
	  {
	    _sportsmen[done++] = s2[done2++];
	  }
	}
      }
    }
  }
}

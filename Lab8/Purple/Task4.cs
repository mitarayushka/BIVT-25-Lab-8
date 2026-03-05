namespace Lab7.Purple
{
  public class Task4
  {
    public struct Sportsman
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
    }

    public struct Group
    {
      private string _name;
      private Sportsman[] _sportsmen;

      public string Name => _name;
      public Sportsman[] Sportsmen => _sportsmen;
      
      //public Sportsman[] Sportsmen => _sportsmen.ToArray();
      
      /*
      public string Name => _name;
      public Sportsman[] Sportsmen { get {
	Sportsman[] s = new Sportsman[_sportsmen.Length];
	for (int i = 0; i < _sportsmen.Length; i++){
	  s[i] = _sportsmen[i];
	}
	return s;
        }
      }
      */

      public Group(string name)
      {
	_name = name;
	_sportsmen = new Sportsman[0];
      }
      public Group(Group group)
      {
	_name = group.Name;
	_sportsmen = (Sportsman[])group.Sportsmen.Clone();
	Sort();
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
	int pos = 1;
	while (pos < _sportsmen.Length)
	{
	  if (_sportsmen[pos].Time >= _sportsmen[pos-1].Time)
	  {
	    pos++;
	  }
	  else
	  {
	    (_sportsmen[pos], _sportsmen[pos-1]) = (_sportsmen[pos-1], _sportsmen[pos]);
	    if (pos > 1)
	    {
	      pos--;
	    }
	  }
	}
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
    }
  }
}

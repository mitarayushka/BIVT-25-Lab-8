namespace Lab8.Purple
{
  public class Task1
  {
    public class Judge
    {
      private string _name;
      private int[] _marks;

      private int _lastMark;

      public string Name => _name;

      public Judge(string name, int[] marks)
      {
	_name = name;
	_marks = (int[])marks.Clone();
	_lastMark = -1;
      }

      public int CreateMark()
      {
	return _marks[++_lastMark%_marks.Length];
      }

      public void Print()
      {
	Console.Write($"Name: {Name}");
      }
    }

    public class Competition
    {
      private Judge[] _judges;
      private Participant[] _participants;

      public Judge[] Judges => _judges;
      public Participant[] Participants => _participants;

      public Competition(Judge[] js)
      {
	_judges = (Judge[])js.Clone();
	_participants = new Participant[0];
      }

      public void Evaluate(Participant jumper)
      {
	int[] marks = new int[_judges.Length];
	for (int i = 0; i < _judges.Length; i++)
	{
	  marks[i] = _judges[i].CreateMark();
	}
	jumper.Jump(marks);
      }

      public void Add(Participant p)
      {
	Evaluate(p);
	_participants = _participants.Append(p).ToArray();
      }

      public void Add(Participant[] ps)
      {
	foreach (Participant p in ps)
	{
	  Evaluate(p);
	  _participants = _participants.Append(p).ToArray();
	}
      }

      public void Sort()
      {
	Participant.Sort(_participants);
      } 

    }

    public class Participant
    {
      private string _name; /* basic fields */
      private string _surname;
      private double[] _coefs;
      private int[,] _marks;
      private double _totalScore;

      private int _jumpsDone;

      public string Name => _name; /* getters */
      public string Surname => _surname;
      public double[] Coefs => (double[])_coefs.Clone();
      public int[,] Marks => (int[,])_marks.Clone();
/* TotalScore optimized for calculating only when nessesary, not every time we ask for it */
      public double TotalScore => _totalScore == -1.0 ? _calculateTotalScore() : _calculateTotalScore(); 
/* setters with updating _totalScore field function */
      public void SetCriterias(double[] coefs) { _coefs = (double[])coefs.Clone(); if (_jumpsDone >= 4) _calculateTotalScore(); } 
      public void Jump(int[] marks){
	if (_jumpsDone >= 4) return;
	for (int i = 0; i < marks.Length; i++){
	  _marks[_jumpsDone, i] = marks[i];
	}
	_jumpsDone++;
	if (_jumpsDone >= 4)
	  _calculateTotalScore();
      }

/* operators overload for overall struct usage improvements and Array.Sort availability */
      //public static bool operator <(Participant left, Participant right) => left.TotalScore < right.TotalScore;
      //public static bool operator >(Participant left, Participant right) => left.TotalScore > right.TotalScore;
      //public static bool operator <=(Participant left, Participant right) => left.TotalScore <= right.TotalScore;
      //public static bool operator >=(Participant left, Participant right) => left.TotalScore >= right.TotalScore;

      public Participant(string _name, string _surname)
      {
	this._name = _name;
	this._surname = _surname;
	_coefs = new double[4] {2.5, 2.5, 2.5, 2.5};
	_marks = new int[4, 7];
	_totalScore = -1.0; //violation of incapsulation principe :(
	_jumpsDone = 0;
      }

/* private helper for optimizing _totalScore calculation */
      private double _calculateTotalScore() 
      {
	double sum = 0.0;
	for (int j = 0; j < 4; j++){
	  int worstInd = 0;
	  int bestInd = 0;
	  for (int i = 0; i < 7; i++){
	    if (_marks[j, i] < _marks[j, worstInd]) worstInd = i;
	  }
	  for (int i = 0; i < 7; i++){
	    if (_marks[j, i] > _marks[j, bestInd] && i != worstInd) bestInd = i;
	  }
	  for (int i = 0; i < 7; i++){
	    if (i != bestInd && i != worstInd){
	      sum+= _marks[j, i] * _coefs[j];
	    }
	  }
	}
	_totalScore = sum;
	return sum;
      }

      public void Print()
      {
	Console.Write($"Name: {Name}\nSurname: {Surname}\nTotal score: {TotalScore}\n\n");
      }

      public static void Sort(Participant[] array) { Array.Sort(array, (left, right) => right.TotalScore.CompareTo(left.TotalScore)); } 
	/* old implementation replaced by standart one for speed improvements by using comparator delegate
	int n = array.Length;

	int k = 1;
	while (k < n){
	  if (k == 0 || array[k].TotalScore < array[k - 1].TotalScore) 
	    k++;

	  else{
	    (array[k], array[k - 1]) = (array[k - 1], array[k]);
	    k--;
	  }
	}
	
      }
      */
    }
  }
}

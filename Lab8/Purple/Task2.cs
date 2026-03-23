namespace Lab8.Purple
{
  public class Task2
  {
    public struct Participant
    {
      private string _name; /* basic fields */
      private string _surname;
      private int[] _marks;
      private int _distance;
      private int _result;
      private int _target;

      public string Name => _name; /* getters */
      public string Surname => _surname;
      public int[] Marks => (int[])_marks.Clone();
      public int Distance => _distance;
      /* Result optimized for calculating only when nessesary, not every time we ask for it */
      public int Result => _result == -1 ? _calculateResult() : _calculateResult(); 
      /* setters with updating _totalScore field function */

      public void Jump(int distance, int[] marks, int target){
	_marks = (int[])marks.Clone();
	_distance = distance;
	_target = target;
	_calculateResult();
      }

      public Participant(string _name, string _surname)
      {
	this._name = _name;
	this._surname = _surname;
	_marks = new int[5];
	_result = -1; //violation of incapsulation principe :(
	_target = 120;
      }

      /* private helper for optimizing _totalScore calculation */
      private int _calculateResult() 
      {
	int sum = 0;
	int worstInd = 0;
	int bestInd = 0;
	for (int i = 0; i < 5; i++){
	  if (_marks[i] < _marks[worstInd]) worstInd = i;
	}
	for (int i = 0; i < 5; i++){
	  if (_marks[i] > _marks[bestInd] && i != worstInd) bestInd = i;
	}
	for (int i = 0; i < 5; i++){
	  if (i != bestInd && i != worstInd){
	    sum += _marks[i];
	  }
	}
	sum = Math.Max(0, sum + 60 + (Distance-_target)*2);
	_result = sum;
	return sum;
      }

      public void Print()
      {
	Console.Write($"Name: {Name}\nSurname: {Surname}\nResult: {Result}\n\n");
      }

      public static void Sort(Participant[] array) { Array.Sort(array, (left, right) => right.Result.CompareTo(left.Result)); } 
    }

    public abstract class SkiJumping
    {
      private string _name;
      private int _standard;
      private Participant[] _participants;

      private int _jumped;

      public string Name => _name;
      public int Standard => _standard;
      public Participant[] Participants => _participants;
    
      public SkiJumping(string name, int standard)
      {
	_name = name;
	_standard = standard;
	_participants = new Participant[0];
	_jumped = 0;
      }

      public void Add(Participant p)
      {
	_participants = _participants.Append(p).ToArray();
      }

      public void Add(Participant[] ps)
      {
	foreach (Participant p in ps)
	{
	  _participants = _participants.Append(p).ToArray();
	}
      }

      public void Jump(int distance, int[] marks)
      {
	_participants[_jumped].Jump(distance, marks, Standard);
	_jumped++;
      }

      public void Print()
      {
	Console.Write($"SkiJumping:\n|Name: {Name}\n|Standard: {Standard}\nParticipants count: {Participants.Length}\n");
      }

    }

    public class ProSkiJumping : SkiJumping
    {
      public ProSkiJumping() : base("150m", 150){}
    }

    public class JuniorSkiJumping : SkiJumping
    {
      public JuniorSkiJumping() : base("100m", 100){}
    }
  }
}

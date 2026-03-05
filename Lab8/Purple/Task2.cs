namespace Lab7.Purple
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

      public string Name => _name; /* getters */
      public string Surname => _surname;
      public int[] Marks => (int[])_marks.Clone();
      public int Distance => _distance;
      /* Result optimized for calculating only when nessesary, not every time we ask for it */
      public int Result => _result == -1 ? _calculateResult() : _calculateResult(); 
      /* setters with updating _totalScore field function */

      public void Jump(int distance, int[] marks){
	_marks = (int[])marks.Clone();
	_distance = distance;
	_calculateResult();
      }

      public Participant(string _name, string _surname)
      {
	this._name = _name;
	this._surname = _surname;
	_marks = new int[5];
	_result = -1; //violation of incapsulation principe :(
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
	sum = Math.Max(0, sum + 60 + (Distance-120)*2);
	_result = sum;
	return sum;
      }

      public void Print()
      {
	Console.Write($"Name: {Name}\nSurname: {Surname}\nResult: {Result}\n\n");
      }

      public static void Sort(Participant[] array) { Array.Sort(array, (left, right) => right.Result.CompareTo(left.Result)); } 
    }
  }
}

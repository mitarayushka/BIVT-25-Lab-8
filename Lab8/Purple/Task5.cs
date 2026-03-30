using static Lab8.Purple.Task5;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab8.Purple
{
  public class Task5
  {
    public struct Response
    {
      private string _animal;
      private string _characterTrait;
      private string _concept;

      public string Animal => _animal;
      public string CharacterTrait => _characterTrait;
      public string Concept => _concept;
      public Response(string animal, string characterTrait, string concept)
      {
        _animal = animal;
        _characterTrait = characterTrait;
        _concept = concept;
      }
      public int CountVotes(Response[] responses, int questionNumber)
      {
        int count = 0;
        for (int i = 0; i < responses.Length; i++)
        {
          if ((questionNumber == 1) && (responses[i].Animal != null))
            count++;
          else if ((questionNumber == 2) && (responses[i].CharacterTrait != null))
            count++;
          else if ((questionNumber == 3) && (responses[i].Concept != null))
            count++;
        }
        return count;
      }
      public void Print()
      {
        Console.Write($"Animal:{_animal}  CharacterTrait:{_characterTrait}  Concept:{_concept}");
      }
    }
    public struct Research
    {
      private string _name;
      private Response[] _responses;

      public string Name => _name;
      public Response[] Responses => _responses;
      public Research(string name)
      {
        _name = name;
        _responses = new Response[0];
      }
      public void Add(string[] answers)
      {
        Array.Resize(ref _responses, _responses.Length + 1);
        _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
      }
      public string[] GetTopResponses(int question)
      {
        Dictionary<string, int> question1 = new Dictionary<string, int>();
        Dictionary<string, int> question2 = new Dictionary<string, int>();
        Dictionary<string, int> question3 = new Dictionary<string, int>();
        for (int i = 0; i < _responses.Length; i++)
        {
          if ((_responses[i].Animal != null) && (!question1.ContainsKey(_responses[i].Animal)))
            question1[_responses[i].Animal] = 1;
          else if (_responses[i].Animal != null)
            question1[_responses[i].Animal]++;
        }
        for (int i = 0; i < _responses.Length; i++)
        {
          if ((_responses[i].CharacterTrait != null) && (!question2.ContainsKey(_responses[i].CharacterTrait)))
            question2[_responses[i].CharacterTrait] = 1;
          else if (_responses[i].CharacterTrait != null)
            question2[_responses[i].CharacterTrait]++;
        }
        for (int i = 0; i < _responses.Length; i++)
        {
          if ((_responses[i].Concept != null) && (!question3.ContainsKey(_responses[i].Concept)))
            question3[_responses[i].Concept] = 1;
          else if (_responses[i].Concept != null)
            question3[_responses[i].Concept]++;
        }
        question1 = question1.OrderByDescending(q => q.Value).ToDictionary<string, int>();
        question2 = question2.OrderByDescending(q => q.Value).ToDictionary<string, int>();
        question3 = question3.OrderByDescending(q => q.Value).ToDictionary<string, int>();

        Dictionary<string, int> q = new Dictionary<string, int>();
        if (question == 1) q = question1;
        else if (question == 2) q = question2;
        else if (question == 3) q = question3;
        string[] answer = new string[Math.Min(5, q.Count)];

        int count = 0;
        foreach (var i in q)
        {
          answer[count++] = i.Key;
          if (count == 5) break;
        }
        return answer;
      }
      public void Print()
      {
        Console.Write($"Name:{_name}  \nResponses:");
        for (int i = 0; i < _responses.Length; i++)
          Console.Write(_responses[i] + " ");
        Console.WriteLine();
      }
    }

    public class Report
    {
      private Research[] _researches;
      private static int _counter;

      public Research[] Researches => _researches;
      static Report()
      {
        _counter = 1;
      }
      public Report()
      {
        _researches = new Research[0];
      }
      public Research MakeResearch()
      {
        var answer = new Research($"No_{_counter++}_{DateTime.Today.Month}/{DateTime.Today.Year % 100}");
        Research[] array = new Research[_researches.Length + 1];
        for (int i = 0; i < _researches.Length; i++) array[i] = _researches[i];
        array[array.Length - 1] = answer;
        _researches = array;
        return answer;
      }
      public (string, double)[] GetGeneralReport(int question)
      {
        int k = 0;
        var dict = new Dictionary<string, int>();

        for (int i = 0; i < _researches.Length; i++)
        {
          Response[] array = _researches[i].Responses;

          for (int j = 0; j < array.Length; j++)
          {
            string resp = "";

            if (question == 1)
              resp = array[j].Animal;
            else if (question == 2)
              resp = array[j].CharacterTrait;
            else if (question == 3)
              resp = array[j].Concept;

            if (resp == null) continue;

            k++;
            if (dict.ContainsKey(resp)) dict[resp] += 1;
            else dict[resp] = 1;
          }
        }

        return dict.Select(x => (x.Key, x.Value * 100.0 / k)).ToArray();
      }
    }

  }
}

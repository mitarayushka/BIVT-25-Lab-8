using System;
using System.Xml.Linq;
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

                if (questionNumber < 1 || questionNumber > 3)
                    return 0;

                string currentValue = "";
                switch (questionNumber)
                {
                    case 1:
                        currentValue = Animal;
                        break;
                    case 2:
                        currentValue = CharacterTrait;
                        break;
                    case 3:
                        currentValue = Concept;
                        break;
                }

                foreach (Response response in responses)
                {
                    string responseValue = "";
                    switch (questionNumber)
                    {
                        case 1:
                            responseValue = response.Animal;
                            break;
                        case 2:
                            responseValue = response.CharacterTrait;
                            break;
                        case 3:
                            responseValue = response.Concept;
                            break;
                    }

                    if (currentValue == responseValue)
                    {
                        count++;
                    }
                }

                return count;
            }
            public void Print()
            {
                Console.WriteLine($"Animal:{_animal}");
                Console.WriteLine($"CharacterTrait:{_characterTrait}");
                Console.WriteLine($"Concept:{_concept}");
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
                if (answers.Length != 3 || answers == null) return;

                Response[] newArray = new Response[_responses.Length + 1];
                for (int i = 0; i < _responses.Length; i++)
                {
                    newArray[i] = _responses[i];
                }
                Response newResponse = new Response(answers[0], answers[1], answers[2]);
                newArray[_responses.Length] = newResponse;
                _responses = newArray;

            }
            public string[] GetTopResponses(int question)
            {
                if (_responses == null || _responses.Length == 0) return new string[0];
                string[] allAnswers = new string[0];
                switch (question)
                {
                    case 1:
                        for (int i = 0; i < _responses.Length; i++)
                        {
                            Array.Resize(ref allAnswers, allAnswers.Length + 1);
                            allAnswers[allAnswers.Length - 1] = _responses[i].Animal;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < _responses.Length; i++)
                        {
                            Array.Resize(ref allAnswers, allAnswers.Length + 1);
                            allAnswers[allAnswers.Length - 1] = _responses[i].CharacterTrait;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < _responses.Length; i++)
                        {
                            Array.Resize(ref allAnswers, allAnswers.Length + 1);
                            allAnswers[allAnswers.Length - 1] = _responses[i].Concept;
                        }
                        break;
                }

                string[] unique = allAnswers.Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int[] amounts = new int[unique.Length];

                for (int i = 0; i < allAnswers.Length; i++)
                {
                    for (int j = 0; j < unique.Length; j++)
                    {
                        if (allAnswers[i] == unique[j])
                        {
                            amounts[j]++;
                        }
                    }
                }

                for (int i = 0; i < unique.Length; i++)
                {
                    for (int j = 0; j < unique.Length - i - 1; j++)
                    {
                        if (amounts[j] < amounts[j + 1])
                        {
                            (unique[j], unique[j + 1]) = (unique[j + 1], unique[j]);
                            (amounts[j], amounts[j + 1]) = (amounts[j + 1], amounts[j]);
                        }
                    }
                }

                int resultSize = Math.Min(5, amounts.Length);
                string[] result = new string[resultSize];
                Array.Copy(unique, result, resultSize);
                return result;
            }
            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(string.Join(" ", Responses));
            }
        }
        public class Report
        {
            private Research[] _researches;
            private static int _num;
            public Research[] Researches => _researches;
            static Report()
            {
                _num = 1;
            }
            public Report()
            {
                _researches = new Research[0];
            }
            public Research MakeResearch() {
                var newRes = new Research($"No_{_num++}_{DateTime.Now.Month}/{DateTime.Now.Year}");
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = newRes;
                return newRes;
            }
            public (string, double)[] GetGeneralReport(int question)
            {
                string ans = null;
                string[] all_ans = new string[0];
                int k = 0;
                for (int i = 0; i < _researches.Length; i++)
                {
                    Response[] responses = _researches[i].Responses; 
                    for (int j = 0; j < responses.Length; j++)
                    {
                        if (question == 1) ans = responses[j].Animal;
                        if (question == 2) ans = responses[j].CharacterTrait;
                        if (question == 3) ans = responses[j].Concept;
                        if (string.IsNullOrEmpty(ans)) continue;
                        Array.Resize(ref all_ans, all_ans.Length + 1);
                        all_ans[k++] = ans;

                    }
                }
                string[] no_repeat = all_ans.Distinct().ToArray();
                int[] repeat_counts = new int[no_repeat.Length];
                int m = 0;
                (string, double)[] result = new (string, double)[no_repeat.Length];
                foreach (string s in no_repeat)
                {
                    repeat_counts[m++] = all_ans.Count(x => x == s);
                }
                for (int i = 0; i < no_repeat.Length; i++)
                {
                    result[i] = (no_repeat[i], (double)repeat_counts[i] / all_ans.Length * 100.0);
                }
                return result;
            }
        }
    }
}

using System.Diagnostics.CodeAnalysis;
using System.Runtime;
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
                switch (questionNumber)
                {
                    case 1:
                        foreach (var response in responses)
                        {
                            if (response._animal == _animal)
                                count++;
                        }

                        break;
                   case 2:
                       foreach (var response in responses)
                       {
                            if (response._concept == _concept)
                                count++;
                       }
                       break;
                  case 3:
                        foreach (var response in responses)
                        {
                            if (response._characterTrait == _characterTrait)
                                count++;
                        }

                        break;
                }
                return count;
            }

            public void Print()
            {
                Console.WriteLine($"{Animal}, {CharacterTrait}, {Concept}");
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

            private static string GetAnswer(Response response, int questionNumber) => questionNumber switch
            {
                1 => response.Animal,
                2 => response.CharacterTrait,
                3 => response.Concept,
                _ => string.Empty
            };
            
            public string[] GetTopResponses(int question)
            {
                return _responses.Select(r => GetAnswer(r, question))// берем все ответы, получаем ответы на конкретный вопрос
                    .Where(a => a != null && a != "")// 2️⃣ убираем пустые ответы
                    .GroupBy(answer => answer)// 3️⃣ группируем одинаковые ответы
                    .Select(g => new// 4️⃣ создаем объекты с ответом и счетчиком
                    {
                        Answer = g.Key,// сам ответ
                        Count = g.Count()// сколько раз встретился
                    })
                    .OrderByDescending(x => x.Count)// 5️⃣ сортируем по убыванию популярности
                    .Select(x => x.Answer)// 6️⃣ берем только ответы (без счетчиков)
                    .Take(5)// 7️⃣ берем первые 5 (топ-5)
                    .ToArray();// 8️⃣ превращаем в массив
            }
            
            public void Print()
            {
                Console.WriteLine($"Исследование: {Name}");
                Console.WriteLine("Ответы:");
                for (int i = 0; i < _responses.Length; i++)
                {
                    _responses[i].Print();
                }
            }
        }

        public class Report
        {
            private Research[] _researches;
            static int _number;
            
            public Research[] Researches => _researches;

            static Report()
            {
                _number = 1;
            }

            public Report()
            {
                _researches = new Research[0];
            }

            public Research MakeResearch()
            {
                string date = DateTime.Now.ToString("MM/yy");
                Research research = new Research($"No_{_number++}_{date}");
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = research;
                return research;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                var answers = new List<string>();
    
                foreach (var research in _researches)
                {
                    foreach (var response in research.Responses)
                    {
                        string answer = question switch
                        {
                            1 => response.Animal,
                            2 => response.CharacterTrait,
                            _ => response.Concept
                        };
            
                        if (answer != null && answer != "")
                            answers.Add(answer);
                    }
                }
                
                var frequency = new Dictionary<string, int>();
                foreach (string answer in answers)
                {
                    if (frequency.ContainsKey(answer))
                    {
                        int count = frequency[answer];
                        frequency[answer] = count + 1;
                    }
                    else
                    {
                        frequency[answer] = 1;
                    }
                }
                
                (string, double)[] result = new (string, double)[frequency.Count];
                int index = 0;
                int total = answers.Count;
    
                foreach (var kvp in frequency)
                {
                    double percentage = (double)kvp.Value / total * 100;
                    result[index++] = (kvp.Key, percentage);
                }
    
                return result;
            }
            
        }
    }
}

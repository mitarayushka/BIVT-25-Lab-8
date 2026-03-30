using System;
using System.Linq;

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
                if (responses == null) return 0;

                string current = questionNumber == 1 ? _animal
                    : questionNumber == 2 ? _characterTrait
                    : _concept;

                if (string.IsNullOrEmpty(current)) return 0;

                return responses.Count(r =>
                {
                    string other = questionNumber == 1 ? r._animal
                        : questionNumber == 2 ? r._characterTrait
                        : r._concept;
                    return current == other;
                });
            }

            public void Print()
            {
                Console.WriteLine($"{_animal}, {_characterTrait}, {_concept}");
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
                if (answers == null || answers.Length < 3) return;
                var response = new Response(answers[0], answers[1], answers[2]);
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = response;
            }

            public string[] GetTopResponses(int question)
            {
                return _responses
                    .Select(r => question == 1 ? r.Animal
                        : question == 2 ? r.CharacterTrait
                        : r.Concept)
                    .Where(a => !string.IsNullOrEmpty(a))
                    .GroupBy(a => a)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => g.Key)
                    .ToArray();
            }

            public void Print()
            {
                Console.WriteLine(_name);
                if (_responses == null) return;
                foreach (var r in _responses)
                    r.Print();
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
                var now = DateTime.Now;
                string name = $"No_{_counter}_{now.Month:D2}/{now.Year % 100:D2}";
                _counter++;

                var research = new Research(name);
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = research;
                return research;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                if (_researches == null) return new (string, double)[0];

                var allAnswers = _researches
                    .SelectMany(r => r.Responses)
                    .Select(r => question == 1 ? r.Animal
                        : question == 2 ? r.CharacterTrait
                        : r.Concept)
                    .Where(a => !string.IsNullOrEmpty(a))
                    .ToArray();

                if (allAnswers.Length == 0) return new (string, double)[0];

                double total = allAnswers.Length;

                return allAnswers
                    .GroupBy(a => a)
                    .OrderByDescending(g => g.Count())
                    .Select(g => (g.Key, g.Count() / total * 100.0))
                    .ToArray();
            }
        }
    }
}

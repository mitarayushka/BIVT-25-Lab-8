namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Proffecion prof1 = new Proffecion("elictric", 100);
            Teacher teach1 = new Teacher("math teacher", 35, "math", true);
            Proffecion hiddenTeacher = new Teacher("inf teacher", 70, "informatics");

            Proffecion[] proffecions = new Proffecion[] {prof1, teach1, hiddenTeacher};
            
            for (int i = 0; i < proffecions.Length; i++) // первый способ
            {
                if (proffecions[i] is Teacher)
                {
                    Teacher t = proffecions[i] as Teacher;
                    Console.WriteLine(t.Sub);
                }
            }
            
            for (int i = 0; i < proffecions.Length; i++) // второй способ
            {
                Teacher t = proffecions[i] as Teacher;
                if (t != null)
                {
                    Console.WriteLine(t.Sub);
                }
            }
            
        }
    }

    public abstract class Proffecion
    {
        protected string _name;
        protected int _salary;
        
        public string Name => _name;
        public abstract string AbstractProparty { get; }

        public Proffecion(string name, int salary)
        {
            _name = name;
            _salary = salary;
        }

        public virtual void Greetings()
        {
            Console.WriteLine("I don't know my proffecion");
        }

        public void Print()
        {
            Console.WriteLine("Proffecion: " + _name + " salary: " +  _salary);
        }

        public abstract void Bye();
    }

    public class Teacher : Proffecion
    {
        private string _sub;
        private bool _hasClass;
        
        public string Sub => _sub;
        
        public Teacher(string name, int salary, string sub) : base(name, salary)
        {
            _salary *= 2;
            _sub = sub;
            _hasClass = false;
        }

        public Teacher(string name, int salary, string sub, bool hasClass) : this(name, salary, sub)
        {
            _hasClass = hasClass;
        }
        
        public override void Greetings() // override переопределение
        {
            Console.WriteLine("I am a teacher");
        }
        
        public new void Print() // скрытие new 
        {
            Console.WriteLine("Proffecion: " + _name + " salary: " +  _salary);
        }

        public override void Bye()
        {
            Console.WriteLine("Bye");
        }

        public override string AbstractProparty
        {
            
        }
    }
    
    
}
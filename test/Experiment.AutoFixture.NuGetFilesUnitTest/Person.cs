namespace Jwc.Experiment
{
    public class Person
    {
        private readonly string _name;
        private readonly int _age;

        public Person()
        {
        }

        public Person(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }
        }
    }
}
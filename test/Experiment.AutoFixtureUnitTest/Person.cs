namespace Jwc.Experiment.AutoFixture
{
    public class Person
    {
        private readonly string name;
        private readonly int age;

        public Person()
        {
        }

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
        }
    }
}
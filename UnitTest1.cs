using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NavtorAssignment
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup");
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
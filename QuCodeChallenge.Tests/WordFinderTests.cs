namespace QuCodeChallenge.Tests
{
    [TestClass]
    public class WordFinderTests
    {
        [TestMethod]
        public void ChallengeExample_Test()
        {
            var matrix = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var wordstream = new string[] { "cold", "wind", "snow", "chill" };
            var wordFinder = new WordFinder(matrix);

            var expected = new string[] { "cold", "wind", "chill" };
            var actual = wordFinder.Find(wordstream);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void WhenRepeatedInputs_ShowOnlyOnceInOutput_Test()
        {
            var matrix = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var wordstream = new string[] { "cold", "cold", "cold", "wind", "snow", "chill" };
            var wordFinder = new WordFinder(matrix);

            var expected = new string[] { "cold", "wind", "chill" };
            var actual = wordFinder.Find(wordstream)
                .Where(r => r == "cold");

            Assert.IsTrue(actual.Count() == 1, "Result should not contains repeated cold word.");
        }

        [TestMethod]
        public void WhenExistsMostCommonInput_ShouldBeFistInOutput_Test()
        {
            var matrix = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy", "chill" };
            var wordstream = new string[] { "cold", "wind", "snow", "chill" };
            var wordFinder = new WordFinder(matrix);

            var expected = "chill";
            var actual = wordFinder.Find(wordstream)
                .First();

            Assert.AreEqual(expected, actual, "chill should be the first word because it's repeated.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenRowsAreDifferentLenghts_ThrowException_Test()
        {
            var matrix = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "thisisareallylongline" };
            
            new WordFinder(matrix);
        }
    }
}
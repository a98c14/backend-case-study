using System;
using TextSummarization;
using Xunit;

namespace TextSummarizationTests
{
    public class TextSummarizerTests
    {
        [Fact]
        public void TestCosineSimilarityReturnsCorrectResult()
        {
            var v1 = new double[] { 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            var v2 = new double[] { 0, 0, 1, 1, 1, 1, 0, 0, 0 };
            var v3 = new double[] { 0, 0, 0, 1, 0, 0, 1, 1, 1 };
            var res1 = TextSummarizer.CosineSimilarity(v1, v2);
            var res2 = TextSummarizer.CosineSimilarity(v1, v3);
            Assert.True(res1 == 0.5);
            Assert.True(res2 == 0.25);
        }
    }
}

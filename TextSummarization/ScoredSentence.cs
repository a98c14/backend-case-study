namespace TextSummarization
{
    public class ScoredSentence
    {
        public int    Index    { get; set;}
        public double Score    { get; set; }
        public string Sentence { get; set; }

        public ScoredSentence(int index, double score, string sentence)
        {
            Index = index;
            Score = score;
            Sentence = sentence;
        }
    }
}

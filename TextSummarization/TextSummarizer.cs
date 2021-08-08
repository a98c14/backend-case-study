using System;
using System.Collections.Generic;
using System.Linq;

namespace TextSummarization
{
    public static class TextSummarizer
    {
        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public static double Length(double[] v)
        {
            var sum = 0.0;
            for (int i = 0; i < v.Length; i++)
                sum += Math.Pow(v[i], 2);

            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Calculates cosine similarity between two vectors
        /// </summary>
        public static double CosineSimilarity(double[] v1, double[] v2)
        {
            var dot = v1.Zip(v2, (a, b) => a * b).Sum();
            var len1 = Length(v1);
            var len2 = Length(v2);
            return dot / (len1 * len2);
        }

        public static List<string> GetWords(string sentence)
            => sentence.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(x => x.ToLower()).ToList();

        public static double SentenceSimilarity(string a, string b)
        {
            var wordsA = GetWords(a);
            var wordsB = GetWords(b);
            var allWords = wordsA.Concat(wordsB).Distinct().ToList();
            var v1 = new double[allWords.Count];
            var v2 = new double[allWords.Count];

            foreach (var word in wordsA)
                v1[allWords.IndexOf(word)] += 1;

            foreach (var word in wordsB)
                v2[allWords.IndexOf(word)] += 1;

            return 1 - CosineSimilarity(v1, v2);
        }

        /// <summary>
        /// Generates a similarity matrix that shows how similar each sentence is to each other
        /// </summary>
        public static double[,] BuildSimilarityMatrix(string[] sentences)
        {
            var matrix = new double[sentences.Length, sentences.Length];

            for(int i = 0; i < sentences.Length; i++)
            {
                for(int j = 0; j < sentences.Length; j++)
                {
                    if(i==j)
                        continue;

                    matrix[i, j] = SentenceSimilarity(sentences[i], sentences[j]);
                }
            }

            return matrix;
        }
    }
}

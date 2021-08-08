using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextSummarization
{
    public class Program
    {
        private const int PrintCellWidth = 7;

        // Summary sentence length
        private const int SummarySentenceCount = 3;

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^üçşğöıa-zÜÇŞĞÖİA-Z0-9 ]+", " ", RegexOptions.Compiled);
        }

        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("No input file is specified!");
                return;
            }

            var path = args[0];
            var text = File.ReadAllText(path);

            // Get sentences from input
            var sentences = Regex.Split(text, @"(?<=[\.!\?])\s+");

            // Generate similarity matrix using tokenized sentences
            var similarityMatrix = TextSummarizer.BuildSimilarityMatrix(sentences);
            // PrintSimilaritMatrix(similarityMatrix);
            
            // Using the similarity matrix generate a score for each sentence by summing up the total similarity
            var scoreSentences = new List<ScoredSentence>();
            for(int i = 0; i < sentences.Length; i++)
            {
                var score = 0.0;
                for(int j = 0; j < sentences.Length; j++)
                    score += similarityMatrix[i, j];

                scoreSentences.Add(new ScoredSentence(i, score, sentences[i]));
            }

            // Order by index again to get a sentence structure similar to inital text
            var result = string.Join(" ", scoreSentences
                .OrderBy(x => x.Score)
                .Take(SummarySentenceCount)
                .OrderBy(x => x.Index)      
                .Select(x => x.Sentence));

            Console.WriteLine(result);
        }

        /// <summary>
        /// Displays similarity matrix in a structured format
        /// </summary>
        public static void PrintSimilaritMatrix(double[,] similarityMatrix)
        {
            Console.Write($"{"",PrintCellWidth:0.##}");
            for (int i = 0; i < similarityMatrix.GetLength(0); i++)
                Console.Write($"{i,PrintCellWidth:0.##}");
            Console.Write("\n\n");
            for (int i = 0; i < similarityMatrix.GetLength(0); i++)
            {
                Console.Write($"{i,PrintCellWidth:0.##}:");
                for (int j = 0; j < similarityMatrix.GetLength(1); j++)
                    Console.Write($"{similarityMatrix[i, j],PrintCellWidth:0.##}");
                Console.Write("\n\n");
            }
        }
    }

}

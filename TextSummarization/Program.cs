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
        
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^üçşğöıa-zÜÇŞĞÖİA-Z0-9 ]+", " ", RegexOptions.Compiled);
        }

        static void Main(string[] args)
        {
            ParseArgs(args, out var text, out var summaryLength);

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
                .Take(summaryLength)
                .OrderBy(x => x.Index)      
                .Select(x => x.Sentence));

            Console.WriteLine(result);
        }

        public static bool ParseArgs(string[] args, out string text, out int length)
        {
            text = "";
            length = 0;

            if (args.Length == 0)
            {
                Console.WriteLine("No input file is specified!");
                return false;
            }

            var path = args[0];
            try
            {
                text = File.ReadAllText(path);
            }
            catch
            {
                Console.WriteLine("Could not find file in specified path!");
                return false;
            }

            var summarySentenceCount = 3;
            if (args.Length > 1)
            {
                if (int.TryParse(args[1], out var summaryLength))
                    summarySentenceCount = summaryLength;
                else
                    Console.WriteLine("Could not parse summary length! Summary will be default length of 3");
            }
            length = summarySentenceCount;
            return true;
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

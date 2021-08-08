using Newtonsoft.Json;
using ReceiptParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReceiptParser
{
    class Program
    {
        /// <summary>
        /// // Calculates the average distance between sections
        /// </summary>
        /// <param name="orderedSections">OCR Sections</param>
        /// <returns>Average distance between sections</returns>
        static double CalculateLineLength(List<OCRSection> orderedSections)
        {
            var distances = new List<int>();
            for (int i = 0; i < orderedSections.Count - 1; i++)
            {
                var dist = orderedSections[i + 1].BL.Y - orderedSections[i].BL.Y;
                distances.Add(dist);
            }
            return distances.Average();
        }

        /// <summary>
        /// // Groups sections by their y values to generate lines
        /// </summary>
        /// <param name="orderedSections">OCR Sections</param>
        /// <param name="lineLength">Average distance between lines</param>
        /// <returns>Sections grouped by their y values</returns>
        static List<Line> GenerateLines(List<OCRSection> orderedSections, double lineLength)
        {
            var index = 0;
            var lines = new List<Line>{ new Line(0) };

            for (int i = 0; i < orderedSections.Count; i++)
            {
                lines[index].Add(orderedSections[i]);

                // If we are last element don't check for distance to next
                if(i == orderedSections.Count - 1)
                    break;

                var dist = orderedSections[i + 1].BL.Y - orderedSections[i].BL.Y;
                if (dist > lineLength)
                {
                    index++;
                    lines.Add(new Line(index));
                }
            }

            return lines;
        }

        static void Main(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "response.json");
            var json = File.ReadAllText(path);
            var sections = JsonConvert.DeserializeObject<OCRSection[]>(json);
            var ordered = sections.Skip(1).OrderBy(x => x.BL.Y).ToList();
            var lineLength = CalculateLineLength(ordered);
            var lines = GenerateLines(ordered, lineLength);

            Console.WriteLine($"{"Line", 8} | {"Description"}");
            foreach (var line in lines)
            {
                line.SortSectionsByX();
                Console.WriteLine(line);
            }
        }
    }
}

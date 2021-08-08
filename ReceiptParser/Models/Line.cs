using System.Collections.Generic;
using System.Text;

namespace ReceiptParser.Models
{
    public class Line
    {
        public int Index { get; init; }
        public List<OCRSection> Sections { get; set; }

        public Line(int index)
        {
            Index = index;
            Sections = new List<OCRSection>();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{"Line " + (Index + 1), 8} | ");
            foreach (var section in Sections)
                stringBuilder.Append($"{section.Description} ");

            return stringBuilder.ToString();
        }

        public void Add(OCRSection ocrSection)
        {
            Sections.Add(ocrSection);
        }

        /// <summary>
        /// Sorts the sections by their X coordinate, used to fix ordering issues for display
        /// </summary>
        public void SortSectionsByX() =>
            Sections.Sort((a,b) => a.BL.X > b.BL.X ? 1 : -1);
    }
}

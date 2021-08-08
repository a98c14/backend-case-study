namespace ReceiptParser.Models
{
    public class OCRSection
    {
        public string Locale       { get; set; }
        public string Description  { get; set; }
        public Bounds BoundingPoly { get; set; }

        public Vec2 BL => BoundingPoly.BL;
        public Vec2 TR => BoundingPoly.TR;

        public override string ToString()
        {
            return $"BL:({BL.X, 4}, {BL.Y, 4}), TR:({TR.X, 4}, {TR.Y, 4}), Desc: {Description, 30}";
        }
    }
}

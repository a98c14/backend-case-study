using System.Linq;

namespace ReceiptParser.Models
{
    public class Bounds
    {
        public int MinX => Vertices.Min(v => v.X);
        public int MinY => Vertices.Min(v => v.Y);

        public int MaxX => Vertices.Max(v => v.X);
        public int MaxY => Vertices.Max(v => v.Y);

        public Vec2 BL => new() { X = MinX, Y = MinY };
        public Vec2 TR => new() { X = MaxX, Y = MaxY };

        public Vec2[] Vertices;
    }
}

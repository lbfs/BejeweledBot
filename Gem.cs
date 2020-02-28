using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BejeweledBot
{
    public enum Gemtype { Unknown = 0, White = 1, Red = 2, Purple = 3, Green = 4, Blue = 5, Orange = 6, Yellow = 7 };

    class Gem
    {
        public Gemtype Type { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Gem(Gemtype type, int x, int y)
        {
            this.Type = type;
            this.X = x;
            this.Y = y;
        }
    }
}

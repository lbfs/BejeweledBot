using System;
using System.Collections.Generic;
using System.Drawing;

namespace BejeweledBot
{
    static class ColorClassifier
    {
        private static Dictionary<Color, Gemtype> matches = new Dictionary<Color, Gemtype>()
        {
            { Color.FromArgb(0, 0, 0), Gemtype.Unknown },
            { Color.FromArgb(240, 240, 240), Gemtype.White },
            { Color.FromArgb(246, 24, 53), Gemtype.Red },
            { Color.FromArgb(237, 15, 238),  Gemtype.Purple },
            { Color.FromArgb(14, 161, 33), Gemtype.Green },
            { Color.FromArgb(15, 131, 251), Gemtype.Blue },
            { Color.FromArgb(238, 170, 33), Gemtype.Orange },
            { Color.FromArgb(251, 240, 34), Gemtype.Yellow }
        };

        static private double EuclideanColorDifference(Color A, Color B)
        {
            return Math.Sqrt(Math.Pow((A.R - B.R), 2) + Math.Pow((A.G - B.G), 2) + Math.Pow((A.B - B.B), 2));
        }

        static public Gemtype FindClosestTypeFromColor(Color color)
        {
            Gemtype type = Gemtype.Unknown;
            double lowest = double.MaxValue;

            foreach (KeyValuePair<Color, Gemtype> match in matches)
            {
                double difference = EuclideanColorDifference(color, match.Key);
                if (difference < lowest)
                {
                    type = match.Value;
                    lowest = difference;
                }
            }

            return type;
        }

        /* FIX
        static public Color FindColorByType(Gemtype type)
        {
            return matches[type];
        }
        */
    }
}

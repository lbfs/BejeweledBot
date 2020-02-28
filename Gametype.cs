using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BejeweledBot
{
    enum Gametype { Classic = 0, Lightning = 1, Zen = 2, Ice = 3 }

    class GametypeConfiguration
    {
        public Gametype Type { get; private set; }
        public Point BoardLocation { get; private set; }
        public (Point Location, Color Color) ActiveGame { get; }
        public (Point Location, Color Color) ResetGame { get; private set; }
        public Size BlockSize { get; private set;  }

        private GametypeConfiguration() {}

        public GametypeConfiguration(Gametype gametype)
        {
            switch (gametype)
            {
                case Gametype.Classic:
                    BoardLocation = new Point(334, 47);
                    ActiveGame = (new Point(198, 699), Color.FromArgb(69, 0, 39));
                    ResetGame = (new Point(579, 687), Color.FromArgb(82, 0, 34)); // Repair
                    Type = Gametype.Classic;
                    break;
                case Gametype.Lightning:
                    BoardLocation = new Point(334, 83);
                    ActiveGame = (new Point(198, 699), Color.FromArgb(179, 69, 137));
                    ResetGame = (new Point(591, 712), Color.FromArgb(82, 0, 34));
                    Type = Gametype.Lightning;
                    break;
                case Gametype.Zen:
                    BoardLocation = new Point(334, 47);
                    ActiveGame = (new Point(198, 699), Color.FromArgb(68, 0, 38));
                    ResetGame = (new Point(591, 712), Color.FromArgb(82, 0, 34)); // Unknown
                    Type = Gametype.Zen;
                    break;
                case Gametype.Ice:
                    BoardLocation = new Point(334, 73);
                    ActiveGame = (new Point(198, 699), Color.FromArgb(89, 0, 17));
                    ResetGame = (new Point(591, 712), Color.FromArgb(82, 0, 34)); // Unknown
                    break;
                default:
                    throw new ArgumentException("Invalid gametype parameter passed to GametypeConfiguration constructor.");
            }

            BlockSize = new Size(82, 82);
        }
    }
}
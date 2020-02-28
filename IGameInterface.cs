using System.Drawing;

namespace BejeweledBot
{
    interface IGameInterface
    {
        void PushGameMessage(string message);
        void UpdateGameImage(Bitmap bitmap);
        Point FetchGameWindowCoordinates();
    }
}

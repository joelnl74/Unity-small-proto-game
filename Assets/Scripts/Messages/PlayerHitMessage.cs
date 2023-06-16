using Messaging.Interfaces;

namespace Game
{
    public class PlayerHitMessage : IMessage
    {
        public int lives;
    }
}

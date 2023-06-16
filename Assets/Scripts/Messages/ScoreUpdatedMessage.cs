using Messaging.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ScoreUpdatedMessage : IMessage
    {
        public int score;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Basics
{
    internal class MultiplayerGame
    {
        static int playerCount = 0;
        public static void AddPlayer()
        {
            playerCount++;
        }
        public static int GetPlayerCount() { return playerCount; }
        public static void RemovePlayer()
        {
            if (playerCount != 0)
            {
                playerCount--;
            }


        }
    }
}

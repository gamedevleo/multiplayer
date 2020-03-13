using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace UnityServer
{
    public class InputManager
    {
        public enum Keys
        {
            None,
            W,
            A,
            S,
            D
        }

        public static void TryToMove(int connectionID, Keys key)
        {
            Vector3 tmpPosition = GameManager.playerList[connectionID].position;

            if (key == Keys.None) return;

            Player player = GameManager.playerList[connectionID];

            if (key == Keys.W)
            {
                tmpPosition.X += GameManager.playerSpeed * ConvertRotationSin(player.rotation);
                tmpPosition.Z += GameManager.playerSpeed * ConvertRotationCos(player.rotation);
            }
            else if (key == Keys.S)
            {
                tmpPosition.X -= GameManager.playerSpeed * ConvertRotationSin(player.rotation);
                tmpPosition.Z -= GameManager.playerSpeed * ConvertRotationCos(player.rotation);
            }
            else if (key == Keys.A)
            {
                tmpPosition.X -= GameManager.playerSpeed * ConvertRotationCos(player.rotation);
                tmpPosition.Z += GameManager.playerSpeed * ConvertRotationSin(player.rotation);
            }
            else if (key == Keys.D)
            {
                tmpPosition.X += GameManager.playerSpeed * ConvertRotationCos(player.rotation);
                tmpPosition.Z -= GameManager.playerSpeed * ConvertRotationSin(player.rotation);
            }
            GameManager.playerList[connectionID].position = tmpPosition;

            NetworkSend.SendPlayerMove(connectionID, GameManager.playerList[connectionID].position.X, GameManager.playerList[connectionID].position.Y, GameManager.playerList[connectionID].position.Z);

        }

        public static float ConvertRotationSin(float rotation)
        {
            return (float)Math.Round(Math.Sin(rotation * (Math.PI / 180)), 4);
        }

        public static float ConvertRotationCos(float rotation)
        {
            return (float)Math.Round(Math.Cos(rotation * (Math.PI / 180)), 4);
        }


    }
}

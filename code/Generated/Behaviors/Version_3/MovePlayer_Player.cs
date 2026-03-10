// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class MovePlayer_Player : MonoBehaviour
    {
        void Update()
        {
            if (UserAlgorithms.IsPlayerActive())
            {
                UserAlgorithms.UpdatePlayer(GameObject.Find("Player"));
            }
        }
    }
}

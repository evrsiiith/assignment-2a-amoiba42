// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
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

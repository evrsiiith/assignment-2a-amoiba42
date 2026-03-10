// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
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

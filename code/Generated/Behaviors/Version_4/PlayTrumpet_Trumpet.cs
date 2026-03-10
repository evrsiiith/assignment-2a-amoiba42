// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class PlayTrumpet_Trumpet : MonoBehaviour
    {
        void Update()
        {
            if (UserAlgorithms.IsTrumpetHovered())
            {
                UserAlgorithms.PlayTrumpet(GameObject.Find("Trumpet"));
            }
        }
    }
}

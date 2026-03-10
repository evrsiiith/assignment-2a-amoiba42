// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class PlayTrumpet_Trumpet : MonoBehaviour
    {
        void Update()
        {
            if (UserAlgorithms.IsTrumpetHovered())
            {
                UserAlgorithms.PlayTrumpet();
            }
        }
    }
}

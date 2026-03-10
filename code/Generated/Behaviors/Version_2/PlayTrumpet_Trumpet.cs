// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class PlayTrumpet_Trumpet : MonoBehaviour
    {
        void Update()
        {
            if ((TrumpetStateStorage.Get(GameObject.Find("Trumpet")) == TrumpetStateEnum.NotPlayed && UserAlgorithms.IsTrumpetHovered()))
            {
                UserAlgorithms.PlayTrumpet(GameObject.Find("Trumpet"));
            }
        }
    }
}

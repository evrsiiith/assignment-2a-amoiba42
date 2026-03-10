// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class UnlockChest_Chest : MonoBehaviour
    {
        void Update()
        {
            if ((TrumpetStateStorage.Get(GameObject.Find("Trumpet")) == TrumpetStateEnum.Played && UserAlgorithms.AllBooksDestroyed()))
            {
                UserAlgorithms.OpenChest(GameObject.Find("Chest"));
            }
        }
    }
}

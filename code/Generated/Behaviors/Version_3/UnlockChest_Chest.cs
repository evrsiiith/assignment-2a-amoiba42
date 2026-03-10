// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class UnlockChest_Chest : MonoBehaviour
    {
        void Update()
        {
            if ((ChestStateStorage.Get(GameObject.Find("Chest")) == ChestStateEnum.Locked && TrumpetStateStorage.Get(GameObject.Find("Trumpet")) == TrumpetStateEnum.Played && book_brownStateStorage.Get(GameObject.Find("book_brown")) == book_brownStateEnum.Destroyed))
            {
                UserAlgorithms.OpenChest(GameObject.Find("Chest"));
            }
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class UnlockChest_Chest : MonoBehaviour
    {
        void Update()
        {
            if (UserAlgorithms.ShouldOpenChest(GameObject.Find("Chest")))
            {
                UserAlgorithms.OpenChest(GameObject.Find("Chest"));
            }
        }
    }
}

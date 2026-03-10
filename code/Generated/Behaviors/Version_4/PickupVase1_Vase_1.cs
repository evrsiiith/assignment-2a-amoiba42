// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class PickupVase1_Vase_1 : MonoBehaviour
    {
        void Update()
        {
            if ((Vase_1StateStorage.Get(GameObject.Find("Vase_1")) == Vase_1StateEnum.Idle && UserAlgorithms.IsRotatableGrabbed(GameObject.Find("Vase_1"))))
            {
                UserAlgorithms.StartRotatingObject(GameObject.Find("Vase_1"));
            }
        }
    }
}

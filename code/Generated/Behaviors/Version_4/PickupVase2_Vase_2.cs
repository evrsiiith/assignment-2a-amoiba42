// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class PickupVase2_Vase_2 : MonoBehaviour
    {
        void Update()
        {
            if ((Vase_2StateStorage.Get(GameObject.Find("Vase_2")) == Vase_2StateEnum.Idle && UserAlgorithms.IsRotatableGrabbed(GameObject.Find("Vase_2"))))
            {
                UserAlgorithms.StartRotatingObject(GameObject.Find("Vase_2"));
            }
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class ReleaseVase1_Vase_2 : MonoBehaviour
    {
        void Update()
        {
            if ((Vase_2StateStorage.Get(GameObject.Find("Vase_2")) == Vase_2StateEnum.Rotating && UserAlgorithms.IsRotatableReleased(GameObject.Find("Vase_2"))))
            {
                UserAlgorithms.StopRotatingObject(GameObject.Find("Vase_2"));
            }
        }
    }
}

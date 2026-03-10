// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class ReleaseVase1_Vase_1 : MonoBehaviour
    {
        void Update()
        {
            if ((Vase_1StateStorage.Get(GameObject.Find("Vase_1")) == Vase_1StateEnum.Rotating && UserAlgorithms.IsRotatableReleased(GameObject.Find("Vase_1"))))
            {
                UserAlgorithms.StopRotatingObject(GameObject.Find("Vase_1"));
            }
        }
    }
}

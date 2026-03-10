// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class PickupStatue_LionStatue : MonoBehaviour
    {
        void Update()
        {
            if ((LionStatueStateStorage.Get(GameObject.Find("LionStatue")) == LionStatueStateEnum.Idle && UserAlgorithms.IsRotatableGrabbed(GameObject.Find("LionStatue"))))
            {
                UserAlgorithms.StartRotatingObject(GameObject.Find("LionStatue"));
            }
        }
    }
}

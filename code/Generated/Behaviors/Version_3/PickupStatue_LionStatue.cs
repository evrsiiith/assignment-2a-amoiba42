// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class PickupStatue_LionStatue : MonoBehaviour
    {
        void Update()
        {
            if ((LionStatueStateStorage.Get(GameObject.Find("LionStatue")) == LionStatueStateEnum.Idle && UserAlgorithms.IsStatueGrabbed(GameObject.Find("LionStatue"))))
            {
                UserAlgorithms.StartRotatingStatue(GameObject.Find("LionStatue"));
            }
        }
    }
}

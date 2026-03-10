// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class ReleaseStatue_LionStatue : MonoBehaviour
    {
        void Update()
        {
            if ((LionStatueStateStorage.Get(GameObject.Find("LionStatue")) == LionStatueStateEnum.Rotating && UserAlgorithms.IsRotatableReleased(GameObject.Find("LionStatue"))))
            {
                UserAlgorithms.StopRotatingObject(GameObject.Find("LionStatue"));
            }
        }
    }
}

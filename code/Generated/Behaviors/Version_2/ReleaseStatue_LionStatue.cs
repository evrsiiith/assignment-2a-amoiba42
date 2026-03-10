// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class ReleaseStatue_LionStatue : MonoBehaviour
    {
        void Update()
        {
            if ((LionStatueStateStorage.Get(GameObject.Find("LionStatue")) == LionStatueStateEnum.Rotating && UserAlgorithms.IsStatueReleased(GameObject.Find("LionStatue"))))
            {
                UserAlgorithms.StopRotatingStatue(GameObject.Find("LionStatue"));
            }
        }
    }
}

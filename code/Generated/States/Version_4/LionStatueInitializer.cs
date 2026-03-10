// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class LionStatueInitializer : MonoBehaviour
    {
        public LionStatueStateEnum initialState = LionStatueStateEnum.Idle;

        void Awake()
        {
            LionStatueStateStorage.Register(gameObject, initialState);
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class StatueInitializer : MonoBehaviour
    {
        public StatueStateEnum initialState = StatueStateEnum.Idle;

        void Awake()
        {
            StatueStateStorage.Register(gameObject, initialState);
        }
    }
}

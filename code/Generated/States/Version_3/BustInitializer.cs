// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class BustInitializer : MonoBehaviour
    {
        public BustStateEnum initialState = BustStateEnum.Idle;

        void Awake()
        {
            BustStateStorage.Register(gameObject, initialState);
        }
    }
}

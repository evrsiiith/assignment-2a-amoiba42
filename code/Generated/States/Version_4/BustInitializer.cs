// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
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

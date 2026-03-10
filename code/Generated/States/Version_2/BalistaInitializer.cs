// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class BalistaInitializer : MonoBehaviour
    {
        public BalistaStateEnum initialState = BalistaStateEnum.Idle;

        void Awake()
        {
            BalistaStateStorage.Register(gameObject, initialState);
        }
    }
}

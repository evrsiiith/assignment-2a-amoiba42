// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class DoorInitializer : MonoBehaviour
    {
        public DoorStateEnum initialState = DoorStateEnum.Closed;

        void Awake()
        {
            DoorStateStorage.Register(gameObject, initialState);
        }
    }
}

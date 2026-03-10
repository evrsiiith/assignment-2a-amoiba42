// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class FloorInitializer : MonoBehaviour
    {
        public FloorStateEnum initialState = FloorStateEnum.Idle;

        void Awake()
        {
            FloorStateStorage.Register(gameObject, initialState);
        }
    }
}

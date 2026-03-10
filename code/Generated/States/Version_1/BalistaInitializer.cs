// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class BalistaInitializer : MonoBehaviour
    {
        public BalistaStateEnum initialState = BalistaStateEnum.Ready;

        void Awake()
        {
            BalistaStateStorage.Register(gameObject, initialState);
        }
    }
}

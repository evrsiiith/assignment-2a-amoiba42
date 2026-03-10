// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class BoxInitializer : MonoBehaviour
    {
        public BoxStateEnum initialState = BoxStateEnum.Ready;

        void Awake()
        {
            BoxStateStorage.Register(gameObject, initialState);
        }
    }
}

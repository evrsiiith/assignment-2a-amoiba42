// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class bInitializer : MonoBehaviour
    {
        public bStateEnum initialState = bStateEnum.Present;

        void Awake()
        {
            bStateStorage.Register(gameObject, initialState);
        }
    }
}

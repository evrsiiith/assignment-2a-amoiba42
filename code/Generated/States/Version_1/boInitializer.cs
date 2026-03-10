// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class boInitializer : MonoBehaviour
    {
        public boStateEnum initialState = boStateEnum.Present;

        void Awake()
        {
            boStateStorage.Register(gameObject, initialState);
        }
    }
}

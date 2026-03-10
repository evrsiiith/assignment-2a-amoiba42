// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class booInitializer : MonoBehaviour
    {
        public booStateEnum initialState = booStateEnum.Present;

        void Awake()
        {
            booStateStorage.Register(gameObject, initialState);
        }
    }
}

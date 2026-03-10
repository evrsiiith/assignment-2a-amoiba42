// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class ChestInitializer : MonoBehaviour
    {
        public ChestStateEnum initialState = ChestStateEnum.Locked;

        void Awake()
        {
            ChestStateStorage.Register(gameObject, initialState);
        }
    }
}

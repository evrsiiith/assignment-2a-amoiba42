// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
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

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class PlayerInitializer : MonoBehaviour
    {
        public PlayerStateEnum initialState = PlayerStateEnum.Active;

        void Awake()
        {
            PlayerStateStorage.Register(gameObject, initialState);
        }
    }
}

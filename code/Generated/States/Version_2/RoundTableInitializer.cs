// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class RoundTableInitializer : MonoBehaviour
    {
        public RoundTableStateEnum initialState = RoundTableStateEnum.Idle;

        void Awake()
        {
            RoundTableStateStorage.Register(gameObject, initialState);
        }
    }
}

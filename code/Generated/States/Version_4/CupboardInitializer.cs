// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class CupboardInitializer : MonoBehaviour
    {
        public CupboardStateEnum initialState = CupboardStateEnum.Closed;

        void Awake()
        {
            CupboardStateStorage.Register(gameObject, initialState);
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
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

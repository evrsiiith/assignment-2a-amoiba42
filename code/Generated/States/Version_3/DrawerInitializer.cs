// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class DrawerInitializer : MonoBehaviour
    {
        public DrawerStateEnum initialState = DrawerStateEnum.Closed;

        void Awake()
        {
            DrawerStateStorage.Register(gameObject, initialState);
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
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

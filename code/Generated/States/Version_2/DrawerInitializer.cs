// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
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

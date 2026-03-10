// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public static class DrawerStateAPI
    {
        public static bool Closed(GameObject obj) => DrawerStateStorage.IsClosed(obj);
        public static bool Open(GameObject obj) => DrawerStateStorage.IsOpen(obj);

        public static void SetClosed(GameObject obj) => DrawerStateStorage.SetClosed(obj);
        public static void SetOpen(GameObject obj) => DrawerStateStorage.SetOpen(obj);
    }
}

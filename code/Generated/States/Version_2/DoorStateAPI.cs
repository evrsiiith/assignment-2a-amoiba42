// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public static class DoorStateAPI
    {
        public static bool Closed(GameObject obj) => DoorStateStorage.IsClosed(obj);
        public static bool Open(GameObject obj) => DoorStateStorage.IsOpen(obj);

        public static void SetClosed(GameObject obj) => DoorStateStorage.SetClosed(obj);
        public static void SetOpen(GameObject obj) => DoorStateStorage.SetOpen(obj);
    }
}

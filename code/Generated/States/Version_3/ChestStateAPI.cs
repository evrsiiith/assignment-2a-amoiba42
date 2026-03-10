// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public static class ChestStateAPI
    {
        public static bool Locked(GameObject obj) => ChestStateStorage.IsLocked(obj);
        public static bool Open(GameObject obj) => ChestStateStorage.IsOpen(obj);

        public static void SetLocked(GameObject obj) => ChestStateStorage.SetLocked(obj);
        public static void SetOpen(GameObject obj) => ChestStateStorage.SetOpen(obj);
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public static class CupboardStateAPI
    {
        public static bool Closed(GameObject obj) => CupboardStateStorage.IsClosed(obj);
        public static bool Open(GameObject obj) => CupboardStateStorage.IsOpen(obj);

        public static void SetClosed(GameObject obj) => CupboardStateStorage.SetClosed(obj);
        public static void SetOpen(GameObject obj) => CupboardStateStorage.SetOpen(obj);
    }
}

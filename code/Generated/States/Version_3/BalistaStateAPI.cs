// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public static class BalistaStateAPI
    {
        public static bool Idle(GameObject obj) => BalistaStateStorage.IsIdle(obj);
        public static bool Loaded(GameObject obj) => BalistaStateStorage.IsLoaded(obj);
        public static bool Fired(GameObject obj) => BalistaStateStorage.IsFired(obj);

        public static void SetIdle(GameObject obj) => BalistaStateStorage.SetIdle(obj);
        public static void SetLoaded(GameObject obj) => BalistaStateStorage.SetLoaded(obj);
        public static void SetFired(GameObject obj) => BalistaStateStorage.SetFired(obj);
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public static class boStateAPI
    {
        public static bool Present(GameObject obj) => boStateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => boStateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => boStateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => boStateStorage.SetDestroyed(obj);
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public static class bStateAPI
    {
        public static bool Present(GameObject obj) => bStateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => bStateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => bStateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => bStateStorage.SetDestroyed(obj);
    }
}

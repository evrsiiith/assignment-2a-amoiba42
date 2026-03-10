// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public static class booStateAPI
    {
        public static bool Present(GameObject obj) => booStateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => booStateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => booStateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => booStateStorage.SetDestroyed(obj);
    }
}

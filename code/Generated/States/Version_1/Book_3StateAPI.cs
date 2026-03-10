// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public static class Book_3StateAPI
    {
        public static bool Present(GameObject obj) => Book_3StateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => Book_3StateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => Book_3StateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => Book_3StateStorage.SetDestroyed(obj);
    }
}

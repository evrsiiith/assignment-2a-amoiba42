// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public static class Book_1StateAPI
    {
        public static bool Present(GameObject obj) => Book_1StateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => Book_1StateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => Book_1StateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => Book_1StateStorage.SetDestroyed(obj);
    }
}

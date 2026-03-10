// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public static class book_brownStateAPI
    {
        public static bool Present(GameObject obj) => book_brownStateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => book_brownStateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => book_brownStateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => book_brownStateStorage.SetDestroyed(obj);
    }
}

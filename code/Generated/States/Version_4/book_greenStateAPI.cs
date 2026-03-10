// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public static class book_greenStateAPI
    {
        public static bool Present(GameObject obj) => book_greenStateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => book_greenStateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => book_greenStateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => book_greenStateStorage.SetDestroyed(obj);
    }
}

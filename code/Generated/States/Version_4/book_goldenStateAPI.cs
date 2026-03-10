// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public static class book_goldenStateAPI
    {
        public static bool Present(GameObject obj) => book_goldenStateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => book_goldenStateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => book_goldenStateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => book_goldenStateStorage.SetDestroyed(obj);
    }
}

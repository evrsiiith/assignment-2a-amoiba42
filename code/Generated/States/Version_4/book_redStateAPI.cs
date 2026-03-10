// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public static class book_redStateAPI
    {
        public static bool Present(GameObject obj) => book_redStateStorage.IsPresent(obj);
        public static bool Destroyed(GameObject obj) => book_redStateStorage.IsDestroyed(obj);

        public static void SetPresent(GameObject obj) => book_redStateStorage.SetPresent(obj);
        public static void SetDestroyed(GameObject obj) => book_redStateStorage.SetDestroyed(obj);
    }
}

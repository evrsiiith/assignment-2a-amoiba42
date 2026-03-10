// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public static class StatueStateAPI
    {
        public static bool Idle(GameObject obj) => StatueStateStorage.IsIdle(obj);
        public static bool Rotating(GameObject obj) => StatueStateStorage.IsRotating(obj);

        public static void SetIdle(GameObject obj) => StatueStateStorage.SetIdle(obj);
        public static void SetRotating(GameObject obj) => StatueStateStorage.SetRotating(obj);
    }
}

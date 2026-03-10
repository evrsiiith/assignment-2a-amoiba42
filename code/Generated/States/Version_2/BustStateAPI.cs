// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public static class BustStateAPI
    {
        public static bool Idle(GameObject obj) => BustStateStorage.IsIdle(obj);
        public static bool Rotating(GameObject obj) => BustStateStorage.IsRotating(obj);

        public static void SetIdle(GameObject obj) => BustStateStorage.SetIdle(obj);
        public static void SetRotating(GameObject obj) => BustStateStorage.SetRotating(obj);
    }
}

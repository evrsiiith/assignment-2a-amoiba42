// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public static class LionStatueStateAPI
    {
        public static bool Idle(GameObject obj) => LionStatueStateStorage.IsIdle(obj);
        public static bool Rotating(GameObject obj) => LionStatueStateStorage.IsRotating(obj);

        public static void SetIdle(GameObject obj) => LionStatueStateStorage.SetIdle(obj);
        public static void SetRotating(GameObject obj) => LionStatueStateStorage.SetRotating(obj);
    }
}

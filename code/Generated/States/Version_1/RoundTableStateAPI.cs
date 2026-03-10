// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public static class RoundTableStateAPI
    {
        public static bool Idle(GameObject obj) => RoundTableStateStorage.IsIdle(obj);
        public static bool Playing(GameObject obj) => RoundTableStateStorage.IsPlaying(obj);

        public static void SetIdle(GameObject obj) => RoundTableStateStorage.SetIdle(obj);
        public static void SetPlaying(GameObject obj) => RoundTableStateStorage.SetPlaying(obj);
    }
}

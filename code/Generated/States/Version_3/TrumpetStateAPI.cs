// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public static class TrumpetStateAPI
    {
        public static bool NotPlayed(GameObject obj) => TrumpetStateStorage.IsNotPlayed(obj);
        public static bool Played(GameObject obj) => TrumpetStateStorage.IsPlayed(obj);

        public static void SetNotPlayed(GameObject obj) => TrumpetStateStorage.SetNotPlayed(obj);
        public static void SetPlayed(GameObject obj) => TrumpetStateStorage.SetPlayed(obj);
    }
}

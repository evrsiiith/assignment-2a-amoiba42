// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_2
{
    public static class TrumpetStateStorage
    {
        private static Dictionary<GameObject, TrumpetStateEnum> stateTable = new();

        public static event Action<GameObject, TrumpetStateEnum> OnStateChanged;

        public static void Register(GameObject obj, TrumpetStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static TrumpetStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsNotPlayed(GameObject obj) => stateTable[obj] == TrumpetStateEnum.NotPlayed;
        public static bool IsPlayed(GameObject obj) => stateTable[obj] == TrumpetStateEnum.Played;

        public static void SetNotPlayed(GameObject obj) => SetState(obj, TrumpetStateEnum.NotPlayed);
        public static void SetPlayed(GameObject obj) => SetState(obj, TrumpetStateEnum.Played);

        private static void SetState(GameObject obj, TrumpetStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

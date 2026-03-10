// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class RoundTableStateStorage
    {
        private static Dictionary<GameObject, RoundTableStateEnum> stateTable = new();

        public static event Action<GameObject, RoundTableStateEnum> OnStateChanged;

        public static void Register(GameObject obj, RoundTableStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static RoundTableStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsIdle(GameObject obj) => stateTable[obj] == RoundTableStateEnum.Idle;
        public static bool IsPlaying(GameObject obj) => stateTable[obj] == RoundTableStateEnum.Playing;

        public static void SetIdle(GameObject obj) => SetState(obj, RoundTableStateEnum.Idle);
        public static void SetPlaying(GameObject obj) => SetState(obj, RoundTableStateEnum.Playing);

        private static void SetState(GameObject obj, RoundTableStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class BalistaStateStorage
    {
        private static Dictionary<GameObject, BalistaStateEnum> stateTable = new();

        public static event Action<GameObject, BalistaStateEnum> OnStateChanged;

        public static void Register(GameObject obj, BalistaStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static BalistaStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsReady(GameObject obj) => stateTable[obj] == BalistaStateEnum.Ready;

        public static void SetReady(GameObject obj) => SetState(obj, BalistaStateEnum.Ready);

        private static void SetState(GameObject obj, BalistaStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

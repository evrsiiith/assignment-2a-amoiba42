// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_2
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

        public static bool IsIdle(GameObject obj) => stateTable[obj] == BalistaStateEnum.Idle;
        public static bool IsLoaded(GameObject obj) => stateTable[obj] == BalistaStateEnum.Loaded;
        public static bool IsFired(GameObject obj) => stateTable[obj] == BalistaStateEnum.Fired;

        public static void SetIdle(GameObject obj) => SetState(obj, BalistaStateEnum.Idle);
        public static void SetLoaded(GameObject obj) => SetState(obj, BalistaStateEnum.Loaded);
        public static void SetFired(GameObject obj) => SetState(obj, BalistaStateEnum.Fired);

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

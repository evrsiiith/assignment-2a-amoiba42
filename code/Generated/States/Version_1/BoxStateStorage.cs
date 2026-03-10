// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class BoxStateStorage
    {
        private static Dictionary<GameObject, BoxStateEnum> stateTable = new();

        public static event Action<GameObject, BoxStateEnum> OnStateChanged;

        public static void Register(GameObject obj, BoxStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static BoxStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsReady(GameObject obj) => stateTable[obj] == BoxStateEnum.Ready;

        public static void SetReady(GameObject obj) => SetState(obj, BoxStateEnum.Ready);

        private static void SetState(GameObject obj, BoxStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

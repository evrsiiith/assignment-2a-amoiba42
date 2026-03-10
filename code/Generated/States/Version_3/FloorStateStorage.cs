// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_3
{
    public static class FloorStateStorage
    {
        private static Dictionary<GameObject, FloorStateEnum> stateTable = new();

        public static event Action<GameObject, FloorStateEnum> OnStateChanged;

        public static void Register(GameObject obj, FloorStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static FloorStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsIdle(GameObject obj) => stateTable[obj] == FloorStateEnum.Idle;

        public static void SetIdle(GameObject obj) => SetState(obj, FloorStateEnum.Idle);

        private static void SetState(GameObject obj, FloorStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

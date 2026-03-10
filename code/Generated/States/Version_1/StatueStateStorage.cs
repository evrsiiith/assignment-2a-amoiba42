// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class StatueStateStorage
    {
        private static Dictionary<GameObject, StatueStateEnum> stateTable = new();

        public static event Action<GameObject, StatueStateEnum> OnStateChanged;

        public static void Register(GameObject obj, StatueStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static StatueStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsIdle(GameObject obj) => stateTable[obj] == StatueStateEnum.Idle;
        public static bool IsRotating(GameObject obj) => stateTable[obj] == StatueStateEnum.Rotating;

        public static void SetIdle(GameObject obj) => SetState(obj, StatueStateEnum.Idle);
        public static void SetRotating(GameObject obj) => SetState(obj, StatueStateEnum.Rotating);

        private static void SetState(GameObject obj, StatueStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

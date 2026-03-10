// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_3
{
    public static class BustStateStorage
    {
        private static Dictionary<GameObject, BustStateEnum> stateTable = new();

        public static event Action<GameObject, BustStateEnum> OnStateChanged;

        public static void Register(GameObject obj, BustStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static BustStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsIdle(GameObject obj) => stateTable[obj] == BustStateEnum.Idle;
        public static bool IsRotating(GameObject obj) => stateTable[obj] == BustStateEnum.Rotating;

        public static void SetIdle(GameObject obj) => SetState(obj, BustStateEnum.Idle);
        public static void SetRotating(GameObject obj) => SetState(obj, BustStateEnum.Rotating);

        private static void SetState(GameObject obj, BustStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

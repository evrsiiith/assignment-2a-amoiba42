// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class ChestStateStorage
    {
        private static Dictionary<GameObject, ChestStateEnum> stateTable = new();

        public static event Action<GameObject, ChestStateEnum> OnStateChanged;

        public static void Register(GameObject obj, ChestStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static ChestStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsLocked(GameObject obj) => stateTable[obj] == ChestStateEnum.Locked;
        public static bool IsOpen(GameObject obj) => stateTable[obj] == ChestStateEnum.Open;

        public static void SetLocked(GameObject obj) => SetState(obj, ChestStateEnum.Locked);
        public static void SetOpen(GameObject obj) => SetState(obj, ChestStateEnum.Open);

        private static void SetState(GameObject obj, ChestStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

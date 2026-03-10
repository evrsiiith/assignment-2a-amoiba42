// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_3
{
    public static class CupboardStateStorage
    {
        private static Dictionary<GameObject, CupboardStateEnum> stateTable = new();

        public static event Action<GameObject, CupboardStateEnum> OnStateChanged;

        public static void Register(GameObject obj, CupboardStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static CupboardStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsClosed(GameObject obj) => stateTable[obj] == CupboardStateEnum.Closed;
        public static bool IsOpen(GameObject obj) => stateTable[obj] == CupboardStateEnum.Open;

        public static void SetClosed(GameObject obj) => SetState(obj, CupboardStateEnum.Closed);
        public static void SetOpen(GameObject obj) => SetState(obj, CupboardStateEnum.Open);

        private static void SetState(GameObject obj, CupboardStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

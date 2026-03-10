// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_2
{
    public static class DrawerStateStorage
    {
        private static Dictionary<GameObject, DrawerStateEnum> stateTable = new();

        public static event Action<GameObject, DrawerStateEnum> OnStateChanged;

        public static void Register(GameObject obj, DrawerStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static DrawerStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsClosed(GameObject obj) => stateTable[obj] == DrawerStateEnum.Closed;
        public static bool IsOpen(GameObject obj) => stateTable[obj] == DrawerStateEnum.Open;

        public static void SetClosed(GameObject obj) => SetState(obj, DrawerStateEnum.Closed);
        public static void SetOpen(GameObject obj) => SetState(obj, DrawerStateEnum.Open);

        private static void SetState(GameObject obj, DrawerStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

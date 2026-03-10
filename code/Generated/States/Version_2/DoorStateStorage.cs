// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_2
{
    public static class DoorStateStorage
    {
        private static Dictionary<GameObject, DoorStateEnum> stateTable = new();

        public static event Action<GameObject, DoorStateEnum> OnStateChanged;

        public static void Register(GameObject obj, DoorStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static DoorStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsClosed(GameObject obj) => stateTable[obj] == DoorStateEnum.Closed;
        public static bool IsOpen(GameObject obj) => stateTable[obj] == DoorStateEnum.Open;

        public static void SetClosed(GameObject obj) => SetState(obj, DoorStateEnum.Closed);
        public static void SetOpen(GameObject obj) => SetState(obj, DoorStateEnum.Open);

        private static void SetState(GameObject obj, DoorStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

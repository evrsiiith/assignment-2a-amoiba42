// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class boStateStorage
    {
        private static Dictionary<GameObject, boStateEnum> stateTable = new();

        public static event Action<GameObject, boStateEnum> OnStateChanged;

        public static void Register(GameObject obj, boStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static boStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == boStateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == boStateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, boStateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, boStateEnum.Destroyed);

        private static void SetState(GameObject obj, boStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

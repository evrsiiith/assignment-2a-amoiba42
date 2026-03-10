// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class bStateStorage
    {
        private static Dictionary<GameObject, bStateEnum> stateTable = new();

        public static event Action<GameObject, bStateEnum> OnStateChanged;

        public static void Register(GameObject obj, bStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static bStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == bStateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == bStateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, bStateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, bStateEnum.Destroyed);

        private static void SetState(GameObject obj, bStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class booStateStorage
    {
        private static Dictionary<GameObject, booStateEnum> stateTable = new();

        public static event Action<GameObject, booStateEnum> OnStateChanged;

        public static void Register(GameObject obj, booStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static booStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == booStateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == booStateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, booStateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, booStateEnum.Destroyed);

        private static void SetState(GameObject obj, booStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

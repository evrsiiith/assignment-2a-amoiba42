// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_2
{
    public static class Rug2StateStorage
    {
        private static Dictionary<GameObject, Rug2StateEnum> stateTable = new();

        public static event Action<GameObject, Rug2StateEnum> OnStateChanged;

        public static void Register(GameObject obj, Rug2StateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static Rug2StateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsIdle(GameObject obj) => stateTable[obj] == Rug2StateEnum.Idle;

        public static void SetIdle(GameObject obj) => SetState(obj, Rug2StateEnum.Idle);

        private static void SetState(GameObject obj, Rug2StateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class Vase_1StateStorage
    {
        private static Dictionary<GameObject, Vase_1StateEnum> stateTable = new();

        public static event Action<GameObject, Vase_1StateEnum> OnStateChanged;

        public static void Register(GameObject obj, Vase_1StateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static Vase_1StateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsIdle(GameObject obj) => stateTable[obj] == Vase_1StateEnum.Idle;
        public static bool IsRotating(GameObject obj) => stateTable[obj] == Vase_1StateEnum.Rotating;

        public static void SetIdle(GameObject obj) => SetState(obj, Vase_1StateEnum.Idle);
        public static void SetRotating(GameObject obj) => SetState(obj, Vase_1StateEnum.Rotating);

        private static void SetState(GameObject obj, Vase_1StateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

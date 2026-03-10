// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_2
{
    public static class LionStatueStateStorage
    {
        private static Dictionary<GameObject, LionStatueStateEnum> stateTable = new();

        public static event Action<GameObject, LionStatueStateEnum> OnStateChanged;

        public static void Register(GameObject obj, LionStatueStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static LionStatueStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsIdle(GameObject obj) => stateTable[obj] == LionStatueStateEnum.Idle;
        public static bool IsRotating(GameObject obj) => stateTable[obj] == LionStatueStateEnum.Rotating;

        public static void SetIdle(GameObject obj) => SetState(obj, LionStatueStateEnum.Idle);
        public static void SetRotating(GameObject obj) => SetState(obj, LionStatueStateEnum.Rotating);

        private static void SetState(GameObject obj, LionStatueStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_3
{
    public static class PlayerStateStorage
    {
        private static Dictionary<GameObject, PlayerStateEnum> stateTable = new();

        public static event Action<GameObject, PlayerStateEnum> OnStateChanged;

        public static void Register(GameObject obj, PlayerStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static PlayerStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsActive(GameObject obj) => stateTable[obj] == PlayerStateEnum.Active;

        public static void SetActive(GameObject obj) => SetState(obj, PlayerStateEnum.Active);

        private static void SetState(GameObject obj, PlayerStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

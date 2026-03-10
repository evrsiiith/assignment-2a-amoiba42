// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class book_brownStateStorage
    {
        private static Dictionary<GameObject, book_brownStateEnum> stateTable = new();

        public static event Action<GameObject, book_brownStateEnum> OnStateChanged;

        public static void Register(GameObject obj, book_brownStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static book_brownStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == book_brownStateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == book_brownStateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, book_brownStateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, book_brownStateEnum.Destroyed);

        private static void SetState(GameObject obj, book_brownStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

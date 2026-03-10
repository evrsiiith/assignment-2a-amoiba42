// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class book_greenStateStorage
    {
        private static Dictionary<GameObject, book_greenStateEnum> stateTable = new();

        public static event Action<GameObject, book_greenStateEnum> OnStateChanged;

        public static void Register(GameObject obj, book_greenStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static book_greenStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == book_greenStateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == book_greenStateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, book_greenStateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, book_greenStateEnum.Destroyed);

        private static void SetState(GameObject obj, book_greenStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

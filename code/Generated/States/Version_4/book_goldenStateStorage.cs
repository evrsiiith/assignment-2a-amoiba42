// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class book_goldenStateStorage
    {
        private static Dictionary<GameObject, book_goldenStateEnum> stateTable = new();

        public static event Action<GameObject, book_goldenStateEnum> OnStateChanged;

        public static void Register(GameObject obj, book_goldenStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static book_goldenStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == book_goldenStateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == book_goldenStateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, book_goldenStateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, book_goldenStateEnum.Destroyed);

        private static void SetState(GameObject obj, book_goldenStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

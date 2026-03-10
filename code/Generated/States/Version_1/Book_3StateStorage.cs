// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class Book_3StateStorage
    {
        private static Dictionary<GameObject, Book_3StateEnum> stateTable = new();

        public static event Action<GameObject, Book_3StateEnum> OnStateChanged;

        public static void Register(GameObject obj, Book_3StateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static Book_3StateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == Book_3StateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == Book_3StateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, Book_3StateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, Book_3StateEnum.Destroyed);

        private static void SetState(GameObject obj, Book_3StateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

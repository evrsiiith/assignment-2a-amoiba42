// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class Book_1StateStorage
    {
        private static Dictionary<GameObject, Book_1StateEnum> stateTable = new();

        public static event Action<GameObject, Book_1StateEnum> OnStateChanged;

        public static void Register(GameObject obj, Book_1StateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static Book_1StateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == Book_1StateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == Book_1StateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, Book_1StateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, Book_1StateEnum.Destroyed);

        private static void SetState(GameObject obj, Book_1StateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

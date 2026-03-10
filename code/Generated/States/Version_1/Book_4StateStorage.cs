// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_1
{
    public static class Book_4StateStorage
    {
        private static Dictionary<GameObject, Book_4StateEnum> stateTable = new();

        public static event Action<GameObject, Book_4StateEnum> OnStateChanged;

        public static void Register(GameObject obj, Book_4StateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static Book_4StateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == Book_4StateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == Book_4StateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, Book_4StateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, Book_4StateEnum.Destroyed);

        private static void SetState(GameObject obj, Book_4StateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class book_redStateStorage
    {
        private static Dictionary<GameObject, book_redStateEnum> stateTable = new();

        public static event Action<GameObject, book_redStateEnum> OnStateChanged;

        public static void Register(GameObject obj, book_redStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static book_redStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsPresent(GameObject obj) => stateTable[obj] == book_redStateEnum.Present;
        public static bool IsDestroyed(GameObject obj) => stateTable[obj] == book_redStateEnum.Destroyed;

        public static void SetPresent(GameObject obj) => SetState(obj, book_redStateEnum.Present);
        public static void SetDestroyed(GameObject obj) => SetState(obj, book_redStateEnum.Destroyed);

        private static void SetState(GameObject obj, book_redStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}

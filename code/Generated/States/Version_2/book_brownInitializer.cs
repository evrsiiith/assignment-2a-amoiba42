// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class book_brownInitializer : MonoBehaviour
    {
        public book_brownStateEnum initialState = book_brownStateEnum.Present;

        void Awake()
        {
            book_brownStateStorage.Register(gameObject, initialState);
        }
    }
}

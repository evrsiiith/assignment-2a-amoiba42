// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class book_greenInitializer : MonoBehaviour
    {
        public book_greenStateEnum initialState = book_greenStateEnum.Present;

        void Awake()
        {
            book_greenStateStorage.Register(gameObject, initialState);
        }
    }
}

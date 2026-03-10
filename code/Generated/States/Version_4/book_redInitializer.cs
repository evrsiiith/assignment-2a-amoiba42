// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class book_redInitializer : MonoBehaviour
    {
        public book_redStateEnum initialState = book_redStateEnum.Present;

        void Awake()
        {
            book_redStateStorage.Register(gameObject, initialState);
        }
    }
}

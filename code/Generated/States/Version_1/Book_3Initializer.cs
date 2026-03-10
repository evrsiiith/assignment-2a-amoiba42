// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class Book_3Initializer : MonoBehaviour
    {
        public Book_3StateEnum initialState = Book_3StateEnum.Present;

        void Awake()
        {
            Book_3StateStorage.Register(gameObject, initialState);
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class Book_1Initializer : MonoBehaviour
    {
        public Book_1StateEnum initialState = Book_1StateEnum.Present;

        void Awake()
        {
            Book_1StateStorage.Register(gameObject, initialState);
        }
    }
}

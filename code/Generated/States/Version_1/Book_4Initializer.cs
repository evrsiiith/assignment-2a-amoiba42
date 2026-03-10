// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class Book_4Initializer : MonoBehaviour
    {
        public Book_4StateEnum initialState = Book_4StateEnum.Present;

        void Awake()
        {
            Book_4StateStorage.Register(gameObject, initialState);
        }
    }
}

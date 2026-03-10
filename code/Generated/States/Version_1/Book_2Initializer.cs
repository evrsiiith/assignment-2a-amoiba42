// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_1
{
    public class Book_2Initializer : MonoBehaviour
    {
        public Book_2StateEnum initialState = Book_2StateEnum.Present;

        void Awake()
        {
            Book_2StateStorage.Register(gameObject, initialState);
        }
    }
}

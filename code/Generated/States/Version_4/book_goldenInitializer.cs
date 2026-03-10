// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class book_goldenInitializer : MonoBehaviour
    {
        public book_goldenStateEnum initialState = book_goldenStateEnum.Present;

        void Awake()
        {
            book_goldenStateStorage.Register(gameObject, initialState);
        }
    }
}

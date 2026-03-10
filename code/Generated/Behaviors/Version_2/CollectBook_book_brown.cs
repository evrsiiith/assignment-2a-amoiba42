// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class CollectBook_book_brown : MonoBehaviour
    {
        void Update()
        {
            if ((book_brownStateStorage.Get(GameObject.Find("book_brown")) == book_brownStateEnum.Present && UserAlgorithms.IsBookGrabbed(GameObject.Find("book_brown"))))
            {
                UserAlgorithms.CollectBook(GameObject.Find("book_brown"));
            }
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class CollectGreenBook_book_green : MonoBehaviour
    {
        void Update()
        {
            if ((book_greenStateStorage.Get(GameObject.Find("book_green")) == book_greenStateEnum.Present && UserAlgorithms.IsBookGrabbed(GameObject.Find("book_green"))))
            {
                UserAlgorithms.CollectBook(GameObject.Find("book_green"));
            }
        }
    }
}

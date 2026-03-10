// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class CollectRedBook_book_red : MonoBehaviour
    {
        void Update()
        {
            if ((book_redStateStorage.Get(GameObject.Find("book_red")) == book_redStateEnum.Present && UserAlgorithms.IsBookGrabbed(GameObject.Find("book_red"))))
            {
                UserAlgorithms.CollectBook(GameObject.Find("book_red"));
            }
        }
    }
}

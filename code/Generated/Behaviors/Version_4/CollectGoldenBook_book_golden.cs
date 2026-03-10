// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class CollectGoldenBook_book_golden : MonoBehaviour
    {
        void Update()
        {
            if ((book_goldenStateStorage.Get(GameObject.Find("book_golden")) == book_goldenStateEnum.Present && UserAlgorithms.IsBookGrabbed(GameObject.Find("book_golden"))))
            {
                UserAlgorithms.CollectBook(GameObject.Find("book_golden"));
            }
        }
    }
}

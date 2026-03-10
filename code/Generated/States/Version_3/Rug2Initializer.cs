// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class Rug2Initializer : MonoBehaviour
    {
        public Rug2StateEnum initialState = Rug2StateEnum.Idle;

        void Awake()
        {
            Rug2StateStorage.Register(gameObject, initialState);
        }
    }
}

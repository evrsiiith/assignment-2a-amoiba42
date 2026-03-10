// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_3
{
    public class Vase_1Initializer : MonoBehaviour
    {
        public Vase_1StateEnum initialState = Vase_1StateEnum.Idle;

        void Awake()
        {
            Vase_1StateStorage.Register(gameObject, initialState);
        }
    }
}

// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class Vase_2Initializer : MonoBehaviour
    {
        public Vase_2StateEnum initialState = Vase_2StateEnum.Idle;

        void Awake()
        {
            Vase_2StateStorage.Register(gameObject, initialState);
        }
    }
}

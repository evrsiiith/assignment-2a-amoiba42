// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class TrumpetInitializer : MonoBehaviour
    {
        public TrumpetStateEnum initialState = TrumpetStateEnum.NotPlayed;

        void Awake()
        {
            TrumpetStateStorage.Register(gameObject, initialState);
        }
    }
}

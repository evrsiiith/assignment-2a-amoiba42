// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_2
{
    public class ToggleCupboard_Cupboard : MonoBehaviour
    {
        void Update()
        {
            if (UserAlgorithms.IsCupboardInteracted(GameObject.Find("Cupboard")))
            {
                UserAlgorithms.ToggleCupboard(GameObject.Find("Cupboard"));
            }
        }
    }
}

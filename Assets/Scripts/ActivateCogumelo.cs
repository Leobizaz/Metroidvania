using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityEngine.Experimental.Rendering.Universal
{
    public class ActivateCogumelo : MonoBehaviour
    {
        public TriggerCogumelo[] cogumelos;
        bool once;

        private void OnEnable()
        {
            if (!once)
            {
                once = true;
                for (int i = 0; i < cogumelos.Length; i++)
                {
                    cogumelos[i].Trigger();
                }
            }
        }

    }
}

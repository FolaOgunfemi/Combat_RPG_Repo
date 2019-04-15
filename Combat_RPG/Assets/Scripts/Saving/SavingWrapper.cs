using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    /// <summary>
    /// Class contains saving logic that uis specific to the current game. Shortcuts etc. Not the core logic of a saving system
    /// </summary>
    public class SavingWrapper : MonoBehaviour
    {

        const string m_DefaultSaveFile = "save";
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<SavingSystem>().Save(m_DefaultSaveFile);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SavingSystem>().Load(m_DefaultSaveFile);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class ActionScheduler : MonoBehaviour
    {
        private IAction m_CurrentAction;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Helps to cancel certain actions before starting another
        /// </summary>
        public void StartAction(IAction action)
        {
            //no need to cancel if this is first action
            if (m_CurrentAction == action)
            {
                return;
            }
            
                if (m_CurrentAction != null)
                {
                m_CurrentAction.Cancel();
                    Debug.Log("Cancelling " + m_CurrentAction);
                }
                m_CurrentAction = action;
       
            }

        
    }
}
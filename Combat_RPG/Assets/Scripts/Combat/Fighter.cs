using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat { 
public class Fighter : MonoBehaviour, IAction
{
        public static string SampleEnemyDialogue = "Think You Can Take Me?! Don't Forget Me!!";
        private Transform m_Target;
        [SerializeField] [Tooltip("The range at which we stop approaching and start attacking")]
        private float m_WeaponRange = 2f;
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
            if (m_Target != null)
            {
                bool isInRange = Vector3.Distance(transform.position, m_Target.position) < m_WeaponRange;

                Mover mover = GetComponent<Mover>();

                //move to target
                if ( !isInRange)
                {

                    mover.MoveTo(m_Target.position);
                }
                else
                {
                 //   mover.CancelMovement();
                }
            }
            
    }

        /// <summary>
        /// When we attack, we move within range of the target and then attack
        /// </summary>
        /// <param name="target"></param>
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            m_Target = combatTarget.transform;
            Debug.Log(SampleEnemyDialogue);
        }

        public void Cancel()
        {
            CancelAttack();
        }

        //Unsets the target so that we can move and not continue attacking
        private void CancelAttack()
        {
            m_Target = null;
        }
}
}
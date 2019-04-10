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
        private Animator m_Animator;
        [SerializeField]
        private float m_DelayBetweenAttacks = 1f;
        private float m_timeSinceLastAttack = 0f;
        [SerializeField]
       private float m_WeaponDamage = 5f;

        // Start is called before the first frame update
        void Start()
    {
            m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            //updat Time since last attack
            m_timeSinceLastAttack += Time.deltaTime;


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
                    mover.Cancel();
                    AttackBehavior();
                }
            }
            
    }


        private void AttackBehavior()
        {
            if (m_timeSinceLastAttack > m_DelayBetweenAttacks)
            {
                ///NOTE: DAMAGE SHOULD BE DEALT AT THE RIGHT POINT IN THE ANIMAITON. WE IMPLEMENT THIS BY APPLYING DAMAGE IN THE "HIT" METHOD, WHICH IS AN ANIMAITON EVENT
                m_Animator.SetTrigger("attack");
                m_timeSinceLastAttack = 0f;

            }
         
        }


        /// <summary>
        /// Called from Animator Event, not from any methods
        /// </summary>
        void Hit()
        {

            if (m_Target != null)
            {
                Health healthComponenet = m_Target.GetComponent<Health>();

                healthComponenet.TakeDamage(m_WeaponDamage);
            }
            Debug.Log("Hit Event Called");
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
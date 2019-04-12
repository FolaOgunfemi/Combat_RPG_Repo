using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat { 
public class Fighter : MonoBehaviour, IAction
{
        public static string SampleEnemyDialogue = "Think You Can Take Me?! Don't Forget Me!!";
        private Health m_Target;
        [SerializeField] [Tooltip("The range at which we stop approaching and start attacking")]
        private float m_WeaponRange = 2f;
        private Animator m_Animator;
        [SerializeField]
        private float m_DelayBetweenAttacks = 1f;
        
        private float m_timeSinceLastAttack = Mathf.Infinity;
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


            if (m_Target == null)
            {
                return;
            }

            if (m_Target.GetIsDead())
            {
                return;
            }
                bool isInRange = Vector3.Distance(transform.position, m_Target.transform.position) < m_WeaponRange;

                Mover mover = GetComponent<Mover>();

                //move to target
                if ( !isInRange)
                {

                    mover.MoveTo(m_Target.transform.position);
                }
                else
                {
                    mover.Cancel();
                    AttackBehavior();
                }
            
            
    }


        private void AttackBehavior()
        {
            //Look at enemy when attacking
            transform.LookAt(m_Target.transform);
            if (m_timeSinceLastAttack > m_DelayBetweenAttacks)
            {
                ///NOTE: DAMAGE SHOULD BE DEALT AT THE RIGHT POINT IN THE ANIMAITON. WE IMPLEMENT THIS BY APPLYING DAMAGE IN THE "HIT" METHOD, WHICH IS AN ANIMAITON EVENT
                TriggerAttack();
                m_timeSinceLastAttack = 0f;

            }

        }

        private void TriggerAttack()
        {
            m_Animator.ResetTrigger("attack");
            m_Animator.SetTrigger("attack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Health targetHealth = combatTarget.GetComponent<Health>();
            if(targetHealth != null && targetHealth.GetIsDead() == false)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Called from Animator Event, not from any methods
        /// </summary>
        void Hit()
        {

            if (m_Target == null)
            {
                return;
            }
              //  Health healthComponenet = m_Target.GetComponent<Health>();

                m_Target.TakeDamage(m_WeaponDamage);
            
            Debug.Log("Hit Event Called");
        }

        /// <summary>
        /// When we attack, we move within range of the target and then attack
        /// </summary>
        /// <param name="target"></param>
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            m_Target = combatTarget.transform.GetComponent<Health>();
            
            Debug.Log(SampleEnemyDialogue);
           
        }

        public void Cancel()
        {
            CancelAttack();
        }

        //Unsets the target so that we can move and not continue attacking
        private void CancelAttack()
        {
            StopAttack();
            m_Target = null;
        }

        private void StopAttack()
        {
            m_Animator.ResetTrigger("attack");
            m_Animator.SetTrigger("stopAttacking");
        }

    }
}
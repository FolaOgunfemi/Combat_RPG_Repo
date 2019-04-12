using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    public class Health : MonoBehaviour
    {
        [SerializeField] [Range (0f, 100f)]
        private float m_HealthPoints = 100f;
        //[SerializeField]
        private Animator m_Animator;
        private bool m_IsDead = false;

        public void TakeDamage(float damage)
        {
            //remaining health will equal whichever of these two parameters is larger
            m_HealthPoints = Mathf.Max(m_HealthPoints - damage, 0);
            Debug.Log("Health" + m_HealthPoints);

            //Kill the Character when health is 0
            if (m_HealthPoints <= 0 )
            {
                TriggerDeath();
                
            }
        }

        public bool GetIsDead()
        {
            return m_IsDead;
        }

        /// <summary>
        /// When Character health reaches 0, they should die
        /// </summary>
        private void TriggerDeath()
        {
            if ( m_IsDead ) {
                return;
                 }

                
                m_IsDead = true;
            m_Animator.SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_Animator = GetComponent<Animator>();
            Mathf.Clamp(m_HealthPoints, 0f, 100f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

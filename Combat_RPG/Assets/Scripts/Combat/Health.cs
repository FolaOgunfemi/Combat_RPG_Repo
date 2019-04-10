using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    public class Health : MonoBehaviour
    {
        [SerializeField] [Range (0f, 100f)]
        private float m_Health = 100f;

        public void TakeDamage(float damage)
        {
            //remaining health will equal whichever of these two parameters is larger
            m_Health = Mathf.Max(m_Health - damage, 0);
            Debug.Log("Health" + m_Health);
        }

        // Start is called before the first frame update
        void Start()
        {
            Mathf.Clamp(m_Health, 0f, 100f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

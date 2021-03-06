﻿using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Control { 
public class AIController : MonoBehaviour
{
        //
        [SerializeField]
        private float m_ChaseDistance = 5f;
      //  [SerializeField]
        private float m_DistanceFromPlayer = Mathf.Infinity;

        [Header("Basic Components")]
        private GameObject m_Player;
        private Fighter m_Fighter;
        [SerializeField]
        PatrolPath m_PatrolPath ;
        private Health m_Health;
        private Mover m_Mover;
        private ActionScheduler m_ActionScheduler;

        [Header("Guard's Memory")]
        Vector3 m_PositionToGuard;
        [Tooltip ("Used when we want the guard to be suspicous of the area after losing the player")]
        float m_TimeSinceLastSawPlayer = Mathf.Infinity;
        [SerializeField] [Tooltip("Note that Suspicion Time includes the time it takes the guard to walk to the players final sighted position, Which is why it needs to be longer that expected...See Q&A https://www.udemy.com/unityrpg/learn/lecture/14049407#questions/6666208")]
        private float m_SuspicionTime = 5f;
        [SerializeField]
        private float m_WaypointTolerance = 1f;
        [SerializeField]
        private int m_CurrentWaypointIndex = 0;
        private float m_TimeSinceArrivedAtLastWaypoint = Mathf.Infinity;
        [SerializeField]
        private float m_WaypointDwellTime = 3f;




        // [SerializeField]
        // private float m_AttackRange 
        // Start is called before the first frame update
        void Start()
    {
            m_PositionToGuard = transform.position;
            m_Player = GameObject.FindWithTag("Player");
           m_Fighter = GetComponent<Fighter>();
            m_Health = GetComponent<Health>();
            m_Mover = GetComponent<Mover>();
            m_ActionScheduler = GetComponent<ActionScheduler>();

            
        }

    // Update is called once per frame
    void Update()
        {
            if (m_Health.GetIsDead())
            {
                return;
            }
            #region ChaseAndAttack
            if (InAttackRangeOfPlayer(m_Player) && m_Fighter.CanAttack(m_Player))
            {

                Debug.Log("Player is within Chase Distance of " + gameObject.name);

                
                AttackBehavior();
            }
            #endregion
            #region SuspicionState
            else if (m_TimeSinceLastSawPlayer < m_SuspicionTime && m_Fighter.CanAttack(m_Player))
            {
                Debug.Log("SUSPICIOUN STATE");
                SuspiciounBehavior();
            }
            #endregion

            #region ReturnToPost
            //STOP ATTACKING WHEN WE ARE NOT IN RANGE, look around for awhile WITH SUSPICION, AND RETURN TO YOUR POST
            else
            {

                //Our Current logic makes StartMoveAciton automatically cancel the fight action //   m_Fighter.Cancel();
                PatrolBehaviour();
            }
            #endregion


            //increment counters 
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            m_TimeSinceLastSawPlayer += Time.deltaTime;
            m_TimeSinceArrivedAtLastWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {


            //initialize first destination
            Vector3 nextPosition = m_PositionToGuard;
                    if (m_PatrolPath != null)
                    { 
                    if (AtWayPoint())
                    {
                        

                    m_ActionScheduler.CancelCurrentAction();
                    m_TimeSinceArrivedAtLastWaypoint = 0f;
                    CycleWayPoint();

                    
                }
                    nextPosition = GetCurrentWaypoint();
                      }

            //Don't move until we have dwelled first
            if (m_TimeSinceArrivedAtLastWaypoint > m_WaypointDwellTime)
            {
                m_Mover.StartMoveAction(nextPosition);

            }
        }

     

        private void CycleWayPoint()
        {
            m_CurrentWaypointIndex = m_PatrolPath.GetNextIndex(m_CurrentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            
            return m_PatrolPath.GetWaypoint(m_CurrentWaypointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < m_WaypointTolerance;
        }

        private void SuspiciounBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            m_TimeSinceLastSawPlayer = 0;
            m_Fighter.Attack(m_Player);
        }

        private bool InAttackRangeOfPlayer(GameObject player )
        {
            
            
            m_DistanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            bool shouldChase = m_DistanceFromPlayer < m_ChaseDistance;
            return shouldChase;
        }

        /// <summary>
        /// Called by Unity Events, not from any methods
        /// </summary>
        private void OnDrawGizmos()
        {

        }


        /// <summary>
        /// Like OnDrawGizmos, but only when object is selected, Called by Unity Events, not from any methods
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, m_ChaseDistance);
        }


        //////////////namespace












    }

}

using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField]
        private Transform m_Target;

        private NavMeshAgent m_NavMeshAgent;
    //    private Fighter m_Fighter;
        Ray m_LastRay;
        // Start is called before the first frame update
        void Start()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
       //     m_Fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {

            UpdateAnimator();
        }



        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            //stop attacking if we are...
           // m_Fighter.Cancel();
            //move to destination
            MoveTo(destination);
           
        }

        public void MoveTo(Vector3 destination)
        {
          
            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.SetDestination(destination);
            //allow the navmesh to move again
            m_NavMeshAgent.isStopped = false;
        }

        /// <summary>
        /// The forwardSpeed animation using a BlendTree will switch from one animation to the next, based on velocity
        /// </summary>
        private void UpdateAnimator()
        {

            //Get the curent speed of the navmesh agent, 
            Vector3 velocity = m_NavMeshAgent.velocity;
            //inverse basically takes the global and converts it to local, that is meaniongful for the character animaiton
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            //The blend tree has thresholds that determine which animaiton to use, idle, wlak or run, and everything blended inbetween. When it's set, the animation will be applied
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        private void CancelMovement()
        {

                m_NavMeshAgent.isStopped = true;
                Debug.Log("Stopping navMeshAgent");
            
        }

        public void Cancel()
        {
            CancelMovement();
        }

    }

}
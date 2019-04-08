using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;


    Ray m_LastRay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        UpdateAnimator();
    }



    public void MoveTo(Vector3 destination)
    {
        NavMeshAgent nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(destination);
    }

    /// <summary>
    /// The forwardSpeed animation using a BlendTree will switch from one animation to the next, based on velocity
    /// </summary>
    private void UpdateAnimator()
    {

        //Get the curent speed of the navmesh agent, 
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        //inverse basically takes the global and converts it to local, that is meaniongful for the character animaiton
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float speed = localVelocity.z;
        //The blend tree has thresholds that determine which animaiton to use, idle, wlak or run, and everything blended inbetween. When it's set, the animation will be applied
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

}

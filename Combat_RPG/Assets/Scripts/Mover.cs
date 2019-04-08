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

        //when we left click, we will do the following
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

    }


    /// <summary>
    /// RayCast to a position and tell the NavMesh Agent to set that as the target
    /// </summary>
    private void MoveToCursor()
    {
        //cast a ray from camera to mouse possition
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        NavMeshAgent nav = GetComponent<NavMeshAgent>();

        //the out parameter is the raycasthit collision point that we will manipulate
        bool hasHit = Physics.Raycast(ray, out hit);

        //if we hit something, move to that point
        if (hasHit)
        {
            nav.SetDestination(hit.point);

        }
    }
}

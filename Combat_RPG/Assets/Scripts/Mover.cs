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

        NavMeshAgent nav = GetComponent<NavMeshAgent>();

        nav.SetDestination(m_Target.position);

        RayCastPointer();

    }

    private void RayCastPointer()
    {
        //when we left click, we will do the following
        if (Input.GetMouseButtonDown(0))
        {
            m_LastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        //starting point is origin... we multiply by 100 to make the ray go much further than the destination point, that way we can see it well
        Debug.DrawRay(m_LastRay.origin, m_LastRay.direction * 100);
    }
}

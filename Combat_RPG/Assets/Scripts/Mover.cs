using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        NavMeshAgent nav = GetComponent<NavMeshAgent>();

        nav.SetDestination(m_Target.position);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when we left click, we will do the following
           if (Input.GetMouseButton(0))
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


        //the out parameter is the raycasthit collision point that we will manipulate
        bool hasHit = Physics.Raycast(ray, out hit);

        //if we hit something, move to that point
        if (hasHit)
        {
            Mover mover = GetComponent<Mover>();
            mover.MoveTo(hit.point);

        }
    }

}

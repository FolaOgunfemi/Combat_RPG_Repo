using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Show Relationship between waypoints in editor
/// </summary>
public class PatrolPath : MonoBehaviour
{

    /// <summary>
    /// Draw lines between each waypoint to visualize a path
    /// </summary>
    private void OnDrawGizmos()
    {
        const float waypointGizmoRadius = 0.3f;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < transform.childCount; i++)
        {


            int j = GetNextIndex(i);
            Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);

            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));


        }
    }

    public int GetNextIndex(int i)
    {
        //corner case for when the last waypoint needs to lead the the first one, not just plus one
        if(i+1 == transform.childCount)
        {
       //     Debug.Log("NEXT INDEX " + 0);
            return 0;
            
        }
      //  Debug.Log("NEXT INDEX " + i + 1);
        return i + 1;
        
    }

    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

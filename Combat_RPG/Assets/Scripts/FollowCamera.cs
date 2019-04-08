using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Will be the parent of the main camera, responsible for following the player movement around, without letting the camera rotate when the player rotates(what would happen if we simply childed the maincamera to the player and called it a day)
/// </summary>
public class FollowCamera : MonoBehaviour
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
        //LOCK CAMERAS POSITION TO WHATECER THE TARGET IS
        transform.position = m_Target.position;
    }
}

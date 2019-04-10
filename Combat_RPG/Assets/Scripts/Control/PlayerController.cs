using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control { 


public class PlayerController : MonoBehaviour
{

      


        #region UnityEvents
        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
        {
            #region ACTION PRIORITY
            //if you clickj on something, first check if we can attack it, if not, move there
            if ( InteractWithCombat())
            {
                return;
            }
            if (InteractWithMovement())
            {
                return;
            }

                //if we cannot perform any of the previous actions, we will print this... like when we hover over off-world space
                Debug.Log("nothing to do");

            #endregion
        }

        /// <summary>
        /// Combat raycasting should supercede movement. If I clock the target, but a tree is obscuruing it, the enemy should take priority
        /// </summary>
        private bool InteractWithCombat()
        {
            //Returns all of the things that the ray hits, not just the first
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                //skip the rest of the body of this loop, and go to the next iteration...same as checking if its not null
                if (target == null)
                {
                    continue;
                }
                //atatcks are clicks, not holding down, so its mousedown ironically
                if (Input.GetMouseButtonDown(0)) { 
                    GetComponent<Fighter>().Attack(target);

                    
                        }
                //whether we are hovering over or clicking a combat target, we want to be in combat mode..that way we can display that to the user with a special cursor
                return true;
            }
            //no hits were with combat targets
            return false;
        }

        #endregion



        private bool InteractWithMovement()
        {

            Ray ray = GetMouseRay();
            RaycastHit hit;


            //the out parameter is the raycasthit collision point that we will manipulate
            bool hasHit = Physics.Raycast(ray, out hit);

            //if we hit something, move to that point
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    Mover mover = GetComponent<Mover>();
                    mover.StartMoveAction(hit.point);
                }
                return true;

            }

            return false;
         
        }

        private static Ray GetMouseRay()
        {
            //cast a ray from camera to mouse possition
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }


    
}

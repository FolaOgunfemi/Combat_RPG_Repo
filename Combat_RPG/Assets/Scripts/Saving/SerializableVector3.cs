using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    [System.Serializable]
    public class SerializableVector3 : MonoBehaviour
    {
        float x, y, z;

        //constructor
        public SerializableVector3(Vector3 vector)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
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
}
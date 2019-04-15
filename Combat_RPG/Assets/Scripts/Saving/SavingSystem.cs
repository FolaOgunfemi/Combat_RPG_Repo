using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string savePath = GetPathFromSaveFile(saveFile);


            Debug.Log("saving to " +savePath);

            ///In order to preventissues with forgetting to close a stream or failing due to an excepiton, we can use this syntax
            using (FileStream stream = File.Open(savePath, FileMode.Create))
            {
                byte[] testbytes = Encoding.UTF8.GetBytes("testing");

                //SAVE PLAYER POSITION
                Transform playerTransform = GetPlayerTransform();
            //    byte[] buffer = ManualLSerializeVector(playerTransform.position);

                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 position = new SerializableVector3(playerTransform.position);
                formatter.Serialize(stream, position);
                //NOTE: NO NEED TO USE WRITE WHEN USING BINARY FORMATTER, IT DOES THIS AUTO
               // stream.Write(buffer, 0, buffer.Length);

       
            }
        }

        private Transform GetPlayerTransform()
        {
           return GameObject.FindWithTag("Player").transform;
        }

        /// <summary>
        /// Use this to serialize and save things like player position
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private byte[] SerializeVector(Vector3 vector)
        {
                //each float should take 4 bytes
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);

            return vectorBytes;
        }

        private byte[] ManualSerializeVector(Vector3 vector)
        {
            //each float should take 4 bytes
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);

            return vectorBytes;
        }

        private Vector3 DeserializeVector(byte[] buffer)
        {
           Vector3 result;
            //single as opposed to a double
          float x =  BitConverter.ToSingle(buffer, 0);
         float y =   BitConverter.ToSingle(buffer, 4);
          float z =  BitConverter.ToSingle(buffer, 8);
            result = new Vector3(x, y, z);
            return result;
        }

        private Vector3 ManualDeserializeVector(byte[] buffer)
        {
            Vector3 result;
            //single as opposed to a double
            float x = BitConverter.ToSingle(buffer, 0);
            float y = BitConverter.ToSingle(buffer, 4);
            float z = BitConverter.ToSingle(buffer, 8);
            result = new Vector3(x, y, z);
            return result;
        }

        public void Load(string saveFile)
        {
            string savePath = GetPathFromSaveFile(saveFile);


            Debug.Log("loading from " + savePath);

            ///In order to preventissues with forgetting to close a stream or failing due to an excepiton, we can use this syntax
            using (FileStream stream = File.Open(savePath, FileMode.Open))
            {
                //safe way to read things bit by bit
               // byte[] byteBuffer = new byte[stream.Length];
                //Read from 0 to the last element
              //  stream.Read(byteBuffer, 0, byteBuffer.Length);

                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 playerPosition = new SerializableVector3(playerTransform.position);

                SerializableVector3 resultPosition = (SerializableVector3) formatter.Deserialize(stream);

                playerTransform.position = resultPosition.ToVector();
             //   playerTransform.position = ManualDeserializeVector(byteBuffer);


                //convert bytes to string
                //string result = Encoding.UTF8.GetString(byteBuffer);
                // Debug.Log("load result is :" + result);
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav") ;
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

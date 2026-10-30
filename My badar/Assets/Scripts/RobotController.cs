
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [System.Serializable]
    public class RobotJoints
    {
        public Transform trasnform;
        public float speed = 25f;
        public float maxAngle;
        public float minAngle;
        public float currentAngle;
    }

    public List<RobotJoints> allJoints = new List<RobotJoints>();
    public int selectedJointIndex = 0;

 
    // Update is called once per frame
   // void Update()
    //{
      //  bool nextJoint = Input.GetKeyDown(KeyCode.Space);

       // if(nextJoint)
        //{
          //  SelectNextJoint();
        //}
   // }

    public float CheckLimits(float angleDelta, float max, float min, int index)
    {
        float currentAngle = allJoints[index].currentAngle + angleDelta;

        if(currentAngle > max)
        {
            angleDelta = max - allJoints[index].currentAngle;
        }

        if(currentAngle < min)
        {
            angleDelta = min - allJoints[index].currentAngle;
        }

        return angleDelta;
    }

    public float ProccessInputtAction(float angleDelta, int index)
    {
        //Speed from class
        float speed = allJoints[index].speed;

        bool leftPressed = Input.GetKey(KeyCode.LeftArrow);

        if(leftPressed)
        {
            angleDelta = angleDelta + speed * Time.deltaTime;
        }

        bool rightPressed = Input.GetKey(KeyCode.RightArrow);

        if(rightPressed)
        {
            angleDelta = angleDelta - speed * Time.deltaTime;
        }

        

        return angleDelta;
    }

    public float GUIInputAction(float angleDelta, int index, bool robotAnglePlus, bool robotAngleMinus)
    {
        float speed = allJoints[index].speed;

        if(robotAnglePlus)
        {
            angleDelta = angleDelta + speed *Time.deltaTime;
        }

        if(robotAngleMinus)
        {
            angleDelta = angleDelta - speed * Time.deltaTime;
        }

        return angleDelta;
    }

    

    public void SelectNextJoint()
    {
        selectedJointIndex++;
        if(selectedJointIndex > allJoints.Count-1)
        {
            selectedJointIndex=0;
        }        

    }
}

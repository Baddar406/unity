using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkMoveController : MonoBehaviour
{
    
    public RobotController joint;

    public float currentAngle;

    public int index;

    public bool robotAnglePlus = false;
    public bool robotAngleMinus = false;

    public float maxAngle;
    public float minAngle;

    void Start()
    {
        for(int i=0; i < joint.allJoints.Count; i++)
        {
            if(joint.allJoints[i].trasnform.name == transform.name)
            {
                index = i;
            }
        }
        maxAngle= joint.allJoints[index].maxAngle;
        minAngle = joint.allJoints[index].minAngle;
    }

    // Update is called once per frame
    void Update()
    {

        float angleDelta = 0;


        if(joint.selectedJointIndex == index)
        {
            angleDelta = joint.ProccessInputtAction(angleDelta, index);
        }        
        angleDelta = joint.GUIInputAction(angleDelta, index, robotAnglePlus, robotAngleMinus);
        angleDelta = joint.CheckLimits(angleDelta, maxAngle, minAngle, index);
        var rotationDelta = new Vector3(x:0,y:0, z:angleDelta);
        transform.Rotate(rotationDelta);


        currentAngle += angleDelta;
        joint.allJoints[index].currentAngle = currentAngle;

    }


    // Help functions
     public void StartAnglePlus()
    {
       // if(joint.selectedJointIndex == index)
        {
            robotAnglePlus = true;
            robotAngleMinus = false;
        }
        
    }

    public void StartAngleMinus()
    {
       // if(joint.selectedJointIndex == index)
        {
            robotAnglePlus = false;
            robotAngleMinus = true;
        }
    }

    public void StartAngleStop()
    {
      //  if(joint.selectedJointIndex == index)
        {
            robotAnglePlus = false;
            robotAngleMinus = false;
        }
        
    }

    
}

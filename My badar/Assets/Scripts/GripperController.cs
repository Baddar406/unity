using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GripperController : MonoBehaviour
{   
    //Enum for grip state
    public enum GripState {Opened = 1, Closed = -1}
    
    public GameObject rightFingerObject;
    public GameObject leftFingerObject;

    public bool rightFingerCollision;
    public bool leftFingerCollision;

    public string collidedGameobject = null;
    public GameObject newChild;
    public Transform gripperParentObject;

    public Vector3 rightFingerOpendPosition;
    public Vector3 leftFingerOpendPosition;
    
    public Vector3 rightFingerCurrentPosition;
    public Vector3 leftFingerCurrentPosition;    
    
    public float closingSpeed = 0.3f;    
    public int closingState = 0;

    public GripState gripState=GripState.Opened;

    FingerCollisionChecker rightFinger;
    FingerCollisionChecker leftFinger;


    // Start is called before the first frame update
    void Start()
    {
        leftFingerOpendPosition = leftFingerObject.GetComponent<Transform>().localPosition;
        rightFingerOpendPosition = rightFingerObject.GetComponent<Transform>().localPosition;

        rightFinger = rightFingerObject.GetComponent<FingerCollisionChecker>();
        leftFinger = leftFingerObject.GetComponent<FingerCollisionChecker>();

        rightFingerCollision = rightFinger.isCollision;
        leftFingerCollision = leftFinger.isCollision;


    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        bool open = Input.GetKeyDown(KeyCode.O);
        bool close = Input.GetKeyDown(KeyCode.C);

        if(open){closingState=1;}
        if(close){closingState=-1;}
    }

    void FixedUpdate() {

        //Fingers current positions
        rightFingerCurrentPosition = rightFingerObject.GetComponent<Transform>().localPosition;
        leftFingerCurrentPosition = leftFingerObject.GetComponent<Transform>().localPosition;

        //Check if the gripper fingers are collided with other object
        rightFingerCollision = rightFinger.isCollision;
        leftFingerCollision = leftFinger.isCollision;

        // Closing gripper fingers
        if(closingState == -1)
        {

            rightFingerObject.transform.localPosition += new Vector3(closingState*closingSpeed*Time.deltaTime,0,0);
            leftFingerObject.transform.localPosition += new Vector3(-closingState*closingSpeed*Time.deltaTime,0,0);
            if(rightFinger.nameOfGameobject == leftFinger.nameOfGameobject)
            {
                collidedGameobject = rightFinger.nameOfGameobject;
                newChild = GameObject.Find(collidedGameobject);
                if(newChild != null)
                {
                    newChild.transform.SetParent(gripperParentObject);
                    newChild.GetComponent<Rigidbody>().isKinematic = true;
                    closingState = 0;
                    gripState = GripState.Closed;
                }
            }
        }
        

        //Open gripper fingers
        if(closingState == 1)
        {

            rightFingerObject.transform.localPosition += new Vector3(closingState*closingSpeed*Time.deltaTime,0,0);
            leftFingerObject.transform.localPosition += new Vector3(-closingState*closingSpeed*Time.deltaTime,0,0 );
            
            if(newChild != null)
            {

                //newChild.GetComponent<Rigidbody>().useGravity = true;
                newChild.GetComponent<Rigidbody>().isKinematic = false;
                newChild.transform.parent = null;
            }
            collidedGameobject = null;
            newChild = null;

            

        }


        // Stop gripper movement
        if(leftFingerOpendPosition.x > leftFingerCurrentPosition.x && rightFingerOpendPosition.x < rightFingerCurrentPosition.x)
        {

            closingState = 0;
            rightFingerObject.transform.localPosition = rightFingerOpendPosition;
            leftFingerObject.transform.localPosition = leftFingerOpendPosition;
            gripState = GripState.Opened;
        }

        if(rightFinger.nameOfGameobject == leftFingerObject.name && closingState != 1)
        {
            closingState = 0;
            gripState = GripState.Closed;
        }
        

        
    }

    public void GOpen()
    {
        closingState = 1;
    }

    public void GClose()
    {
        closingState = -1;
    }
}

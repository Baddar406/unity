using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class PlayProgramCode : MonoBehaviour
{
    public List<string> programCodeLines = new List<string>();

    public List<string> codeLine = new List<string>();

    public GripperController gripState;

    public RobotController rController;

    public List<LinkMoveController> lmController;


    public int index = 1;


    //public float currentAngle;

    
    public int reach = 0;

    public int startProgram = 0;
    // Start is called before the first frame update
    void Start()
    {
        //lmController = new List<LinkMoveController>(rController.allJoints.Count);
        string readFromFilePath = "Assets/Resources/"+"program.txt";

        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        programCodeLines = fileLines;

        for(int i = 0; i < rController.allJoints.Count; i++)
        {
            GameObject link = GameObject.Find(rController.allJoints[i].trasnform.name);
            lmController.Add(link.GetComponent<LinkMoveController>());
        }

        //codeLine = programCodeLines[1].Split(char.Parse(",")).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        string readFromFilePath = "Assets/Resources/"+"program.txt";

        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        programCodeLines = fileLines;
        
        bool playProgram = Input.GetKeyDown(KeyCode.P);
        if(playProgram){startProgram = 1;}
        //Debug.Log("Mis on");
        if(startProgram == 1)
        {
            //Debug.Log("Mis on2");
            codeLine = programCodeLines[index].Split(';').ToList();

Debug.Log("Reading line " + index + ": '" +
          programCodeLines[index] + "'");
            
            
            
            if(gripState.closingState == 0 && codeLine.Count > 1)
            {
                //Debug.Log("Line count more than 1");
                
                

                if(float.Parse(codeLine[rController.selectedJointIndex]) > rController.allJoints[rController.selectedJointIndex].currentAngle)
                {

                    lmController[rController.selectedJointIndex].StartAnglePlus();

                    //robotMove.CheckLimits(robotMove.angleDelta, float.Parse(codeLine[robotMove.selectedJointIndex]), robotMove.selectedJoint.minAngle);
                    lmController[rController.selectedJointIndex].maxAngle = float.Parse(codeLine[rController.selectedJointIndex]);
                }

                if(float.Parse(codeLine[rController.selectedJointIndex]) < rController.allJoints[rController.selectedJointIndex].currentAngle)
                {
                    lmController[rController.selectedJointIndex].StartAngleMinus();
                    //robotMove.CheckLimits(robotMove.angleDelta, robotMove.selectedJoint.maxAngle, float.Parse(codeLine[robotMove.selectedJointIndex]));
                    lmController[rController.selectedJointIndex].minAngle = float.Parse(codeLine[rController.selectedJointIndex]);
                }

                if(rController.allJoints[rController.selectedJointIndex].currentAngle == float.Parse(codeLine[rController.selectedJointIndex]) )
                {
                    reach ++;
                    lmController[rController.selectedJointIndex].StartAngleStop();
                    //Debug.Log("Reached "+ rController.allJoints[rController.selectedJointIndex].trasnform.name);
                    
                }
                else
                {
                    reach = 0;
                }
                
                
                rController.SelectNextJoint();

            }

            // Control gripper
            // Control gripper
if (codeLine.Count == 1 && gripState.closingState == 0)
{
    string gripperCommand = codeLine[0].Trim();

    if (int.TryParse(gripperCommand, out int gripperState))
    {
        Debug.Log("Gripper moving state: " + gripState.closingState);

        gripState.closingState = gripperState;

        Debug.Log("New gripper state: " + gripState.closingState);

        index++;
    }
    else
    {
        Debug.LogError(
            "Invalid gripper command at line " + index +
            ": '" + gripperCommand + "'"
        );

        startProgram = 0;
    }
}

            if(reach == rController.allJoints.Count && gripState.closingState==0)
            {
                rController.selectedJointIndex = 0;
                //Debug.Log("Index +1");
                index++;
            }

            if(index > programCodeLines.Count-1)
            {
                
                index =1;
                startProgram = 0;
                reach = 0;
                for(int i = 0; i < rController.allJoints.Count - 1; i++) {
                    lmController[i].maxAngle = rController.allJoints[i].maxAngle;
                    lmController[i].minAngle = rController.allJoints[i].minAngle;
                    lmController[i].StartAngleStop();
                }
                
            }



        }
    }

    public void PlayCode()
    {
        startProgram = 1;
    }
}

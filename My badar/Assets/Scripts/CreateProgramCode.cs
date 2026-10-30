using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class CreateProgramCode : MonoBehaviour
{
    public RobotController robot;

    public GripperController gripStateCheck;
    public GripperController.GripState currentGripStateCheck;

    public float[] linkAngles;

    string textDokumentNanme = "Assets/Resources/"+ "program.txt";
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(robot.allJoints[0].currentAngle);
        linkAngles = new float[robot.allJoints.Count];
        
        if(!Directory.Exists("Assets/Resources/"))
        {
            Directory.CreateDirectory("Assets/Resources/");
            Debug.Log("Folder is created");
        }
        CreateTextFile();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(robot.selectedJoint.currentAngle);
        bool check = Input.GetKeyDown(KeyCode.W);
        if(check)
        {
            AppendTextInFile();
        }
        
        
        
    }

    public void CreateTextFile()
    {
        

        if(!File.Exists(textDokumentNanme))
        {

            File.WriteAllText(textDokumentNanme, "Program start point! \n");
            //File.AppendAllText(textDokumentNanme, "\n");
        }


    }

    public void AppendTextInFile()
    {
        if(currentGripStateCheck != gripStateCheck.gripState)
        {
            Debug.Log("Write state of gripper");
            currentGripStateCheck = gripStateCheck.gripState;
            Debug.Log(((int)currentGripStateCheck).ToString());
            File.AppendAllText(textDokumentNanme, ((int)currentGripStateCheck).ToString()+"\n");
        }
        else
        {
            for(int i=0; i<robot.allJoints.Count; i++)
            {
                Debug.Log(robot.allJoints[i].currentAngle);
                linkAngles[i]=robot.allJoints[i].currentAngle;
                File.AppendAllText(textDokumentNanme, linkAngles[i].ToString()+";");
            }
            File.AppendAllText(textDokumentNanme,"\n");
        }
        
        
    }
}

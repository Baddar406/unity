using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerCollisionChecker : MonoBehaviour
{
    // Variables for checking collision and collided object name
    public bool isCollision = false;
    public string nameOfGameobject = null;
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Yes");
        isCollision = true;
        nameOfGameobject = other.gameObject.name;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("No");
        isCollision = false;
        nameOfGameobject = null;
    }
}

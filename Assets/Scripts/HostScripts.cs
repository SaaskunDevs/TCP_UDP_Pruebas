using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saaskun;

public class HostScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveFromClient(string message)
    {
        Debug.Log("Message received from client: " + message);
    }
}

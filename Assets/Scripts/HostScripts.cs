using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saaskun;

public class HostScripts : MonoBehaviour
{
    [SerializeField] UDP_Sender _sendScripts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            sendMessageToClient();
        }
    }

    void sendMessageToClient()
    {
        Debug.Log("Sending message to client");
        _sendScripts.SendData("Recibo", "Envio desde host", 8001);
    }

    public void ReceiveFromClient(string message)
    {
        Debug.Log("Message received from client: " + message);
    }
}

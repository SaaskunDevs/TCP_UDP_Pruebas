using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saaskun;
using System.Net.Sockets;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class ClientScripts : MonoBehaviour
{
    [SerializeField] UDP_Sender _send;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sendMessage();
        }
    }

    void sendMessage()
    {
        Debug.Log("Sending message");
        _send.SendData("Dinamita", "Envio desde cliente", 8080);
    }

    public void ReceiveFromHost(string message)
    {
        Debug.Log("Message received from host: " + message);
    }   

    public void RessScene(string message) {
        Debug.Log("Resetting ClientScene");
        SceneManager.LoadScene(0);
    }

}

using System.Collections;
using System.Collections.Generic;
using Saaskun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientScrptsTcp : MonoBehaviour
{
    [SerializeField] TCPSender _tcpSender;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SendToHost();
        }
    }
    public void SendToHost()
    {
        Debug.Log("Send to host");
        _tcpSender.SendMessageToServers("Dinamita" + "Envio desde cliente" + 8080);
    }

    public void ReciveFromHost(string message)
    {
        Debug.Log("Recive from host: " + message);
    }

    public void ResetClientScene()
    {
        Debug.Log("ResetClient");
        SceneManager.LoadScene(0);
    }
}

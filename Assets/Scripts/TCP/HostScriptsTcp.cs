using System.Collections;
using System.Collections.Generic;
using Saaskun;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostScriptsTcp : MonoBehaviour
{
    [SerializeField] TCPSender _tcpSender;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SendToClient();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ResetClientAndHost();
        }
    }

    void SendToClient()
    {
        Debug.Log("Send to client");
        _tcpSender.SendMessageToServers("Explosion|" + "Envio desde host|" + 8001);
    }

    public void ReciveFromClient(string message)
    {
        Debug.Log("Recive from client: " + message);
    }

    void ResetClientAndHost()
    {
        Debug.Log("ResetScene");
        _tcpSender.SendMessageToServers("ResetClient|" + "Envio desde host|" + 8001);

        Invoke("ResetHostScene", 3f);
    }

    void ResetHostScene()
    {
        Debug.Log("Reset Host");
        SceneManager.LoadScene(0);
    }
}

using UnityEngine;
using Saaskun;

public class Client2 : MonoBehaviour
{
    [SerializeField] TCPSender _tcpSender;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SendToHost();
        }
    }
    public void SendToHost()
    {
        Debug.Log("Send to host");
        _tcpSender.SendMessageToServers("Cliente2|" + "Envio desde cliente|" + 8080);
    }

    public void ReciveFromHost(string message)
    {
        Debug.Log("Recive from host: " + message);
    }

    
}

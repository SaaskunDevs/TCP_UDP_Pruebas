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

    [Header("Envio de paquetes")]
    [SerializeField] int minPackets = 1;
    [SerializeField] int maxPackets = 150;

    [SerializeField] float interval = 1f;
    [SerializeField] int currentID = 0;

    [SerializeField] bool sending = false;

    
    void Start()
    {
        StartCoroutine(SendPacketsByBroadCast());
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sending = !sending;
        }
    }

    void sendMessage()
    {
        Debug.Log("Sending message");
        _send.SendData("Dinamita", "Envio desde cliente", 8080);
    }

    // Coroutine para enviar paquetes cada cierto intervalo de tiempo
    IEnumerator SendPacketsByBroadCast()
    {
        while (sending)
        {
            yield return new WaitForSeconds(interval);

            // Generar una cantidad aleatoria de paquetes entre minPackets y maxPackets
            int numPacketsToSend = Random.Range(minPackets, maxPackets + 1);

            // Incrementar el ID del envío
            currentID++;

            Debug.Log($"Sending {numPacketsToSend} packets with ID {currentID}");

            for (int i = 0; i < numPacketsToSend; i++)
            {
                string message = currentID + "|" + i + "|" + "Paquete";
                
                // Envía el paquete con el contenido y puerto UDP
                _send.SendData("Dinamita", message, 8080);
            }

            // Enviar el número total de paquetes enviados
            _send.SendData("Dinamita", numPacketsToSend + "|" + currentID + "|" + "Finish", 8080);
        }
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

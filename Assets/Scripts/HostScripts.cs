using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saaskun;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class HostScripts : MonoBehaviour
{
    [SerializeField] UDP_Sender _sendScripts;
    
    //Data Pool
    [Header("Data Pool")]
    public List<PackagePool> packagePoolList = new List<PackagePool>();
    public List<PackageTotal> packageTotalList = new List<PackageTotal>();

    private int currentReceivingID = -1; // Para manejar el ID actual del paquete
    private int totalPacketsExpected = 0;
    private float startTime = 0f;

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
        if (Input.GetKeyDown(KeyCode.A))
        {
            SendRestart();
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
        ProcessReceivedMessage(message);
    }

    void ProcessReceivedMessage(string message)
    {
        string[] splitMessage = message.Split('|');
        int receivedID = int.Parse(splitMessage[0]);

        // Si es un paquete individual (ID|Número de paquete)
        if (splitMessage[2] == "Paquete")
        {
            int packageNumber = int.Parse(splitMessage[1]);

            // Iniciar una nueva recepción si cambia el ID
            if (receivedID != currentReceivingID)
            {
                currentReceivingID = receivedID;
                totalPacketsExpected = 0; // Resetear el total hasta que se sepa
                startTime = Time.time; // Registrar el inicio de la recepción
                packagePoolList.Clear(); // Limpiar la lista de paquetes recibidos
            }

            // Guardar la información del paquete en `packagePool`
            float currentPackageTime = Time.time - startTime;
            packagePoolList.Add(new PackagePool
            {
                id = receivedID,
                currentPackage = packageNumber,
                currentPackageTime = currentPackageTime,
                message = message
            });

            Debug.Log($"Received package {packageNumber} from ID {receivedID} after {currentPackageTime} seconds");
        }
        // Si es el mensaje final con el total de paquetes enviados (Total|ID)
        else if (splitMessage[2] == "Finish")
        {
            totalPacketsExpected = int.Parse(splitMessage[0]);

            // Registrar el tiempo total
            float totalTime = Time.time - startTime;

            // Guardar los datos en `packageTotal`
            packageTotalList.Add(new PackageTotal
            {
                id = receivedID,
                totalPackages = totalPacketsExpected,
                totalPakcagesTime = totalTime,
                message = $"All {totalPacketsExpected} packets received from ID {receivedID}"
            });

            Debug.Log($"Received all {totalPacketsExpected} packets from ID {receivedID} in {totalTime} seconds");
        }
    }

    void SendRestart() 
    {
        Debug.Log("Resetting Client");

        _sendScripts.SendData("Reset", "Reset", 8001);

        Invoke("Restart", 2f);
    }

    void Restart() {
        SceneManager.LoadScene(0);
    }
}

[System.Serializable]
public class PackagePool
{
    public int id;
    public string message;
    public int currentPackage;
    public float currentPackageTime;
}

[System.Serializable]
public class PackageTotal
{
    public int id;
    public string message;
    public int totalPackages;
    public float totalPakcagesTime;
}
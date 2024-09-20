using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class DBSend : MonoBehaviour
{
    string savePath;
    string fileName;
    HashSet<string> existingRecords = new HashSet<string>(); // Para evitar duplicados

    [Header("Host Data Reference")]
    public HostScripts hostScript;  // Referencia al script de host que contiene las listas

    void Start()
    {
        savePath = Application.persistentDataPath;
        fileName = "Broadcast_DB.csv";
        Debug.Log("Save Path: " + Application.persistentDataPath);
        CreateIfNew();
        LoadExistingRecords();  // Cargar registros existentes para evitar duplicados

        // Iniciar la coroutine para guardar cada hora
        StartCoroutine(SaveDataEveryHour());
    }

    // Crear el archivo si no existe
    void CreateIfNew()
    {
        if (!File.Exists(savePath + Path.DirectorySeparatorChar + fileName))
        {
            using (StreamWriter headerWriter = new StreamWriter(savePath + Path.DirectorySeparatorChar + fileName))
            {
                headerWriter.WriteLine("ID,Message,Packets,Time"); // Encabezado de columnas
            }
        }
    }

    // Cargar los registros existentes del archivo CSV para evitar duplicados
    void LoadExistingRecords()
    {
        if (File.Exists(savePath + Path.DirectorySeparatorChar + fileName))
        {
            using (StreamReader reader = new StreamReader(savePath + Path.DirectorySeparatorChar + fileName))
            {
                // Saltar la primera línea (encabezados)
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Añadir cada línea a la lista de registros existentes
                    existingRecords.Add(line);
                }
            }
        }
    }

    // Método para escribir en la "base de datos"
    public void WriteIntoDB(int id, string message, int packets, float time)
    {
        string newEntry = $"{id},{message},{packets},{time}";
        
        // Evitar guardar entradas duplicadas
        if (!existingRecords.Contains(newEntry))
        {
            using (StreamWriter writer = new StreamWriter(savePath + Path.DirectorySeparatorChar + fileName, true))
            {
                writer.WriteLine(newEntry);
                writer.Flush();
            }

            // Añadir el nuevo registro al hashset para futuras comparaciones
            existingRecords.Add(newEntry);

            Debug.Log("Guardado en BD: " + newEntry);
        }
        else
        {
            Debug.Log("El registro ya existe, no se guarda.");
        }
    }

    // Coroutine para guardar los datos cada hora
    IEnumerator SaveDataEveryHour()
    {
        while (true)
        {
            yield return new WaitForSeconds(3600); // Esperar 1 hora (3600 segundos)
            SaveHostDataToDB();
        }
    }

    // Guardar los datos de las listas del Host en el archivo
    public void SaveHostDataToDB()
    {
        Debug.Log("Guardando datos del Host...");

        // Guardar paquetes individuales de packagePoolList
        foreach (var package in hostScript.packagePoolList)
        {
            WriteIntoDB(package.id, package.message, package.currentPackage, package.currentPackageTime);
        }

        // Guardar paquetes totales de packageTotalList
        foreach (var total in hostScript.packageTotalList)
        {
            WriteIntoDB(total.id, total.message, total.totalPackages, total.totalPakcagesTime);
        }

        Debug.Log("Datos del Host guardados en el archivo CSV.");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTcp : MonoBehaviour
{
    [SerializeField] GameObject _hostGameObject;
    [SerializeField] GameObject _clientGameObject;

    public bool isHost = true;
    void Awake()
    {
        if (isHost)
            _hostGameObject.SetActive(true);
        else
            _clientGameObject.SetActive(true);
        // DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAll();
        }
    }

    public void ResetAll()
    {
        Debug.Log("Reset all");
        SceneManager.LoadScene(0);
    }
}

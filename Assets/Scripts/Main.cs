using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

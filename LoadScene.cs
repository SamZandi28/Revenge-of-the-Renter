using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string SceneNameSF;
    [SerializeField] GameObject Loading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            Loading.SetActive(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(SceneNameSF);
        }
    }

    public void LoadSceneFunction(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}

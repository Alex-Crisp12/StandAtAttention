using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string LevelName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Console.WriteLine("Checking");
        if (collision.gameObject.CompareTag("Player"))
        {
            Console.WriteLine(collision.gameObject.name);
            SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
        }
    }
}

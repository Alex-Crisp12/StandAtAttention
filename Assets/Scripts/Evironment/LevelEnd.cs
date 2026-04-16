using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private CircleCollider2D collidor;
    public string LevelName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collidor = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
        }
    }
}

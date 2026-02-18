using UnityEngine;

public abstract class ToggleableObject : MonoBehaviour
{
    public bool overlapping, enables = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (overlapping)
        {
            if (enables)
            {
                RemakeCollider2();
            }
            else
            {
                RemakeCollider();
            }
        }
    }

    public void Exit()
    {
        Update();
        overlapping = false;
    }

    protected abstract void RemakeCollider();
    protected abstract void RemakeCollider2();
}

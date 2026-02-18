using System.IO;
using UnityEditor.Search;
using UnityEngine;

public class ToggleableObject : MonoBehaviour
{
    LineRenderer shape;
    EdgeCollider2D collidor;
    BoxCollider2D trigger;
    System.Collections.Generic.List<Vector2> default_path;
    bool overlapping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shape = GetComponent<LineRenderer>();
        collidor = GetComponent<EdgeCollider2D>();
        trigger = GetComponent<BoxCollider2D>();

        default_path = new System.Collections.Generic.List<Vector2>();

        for (int index = 0; index != shape.positionCount; index ++)
        {
            default_path.Add(new Vector2(shape.GetPosition(index).x, shape.GetPosition(index).y));
        }
        default_path.Add(new Vector2(shape.GetPosition(0).x, shape.GetPosition(0).y));

        collidor.SetPoints(default_path);

        Vector2 highest = new Vector2(0, -1000000000);
        Vector2 lowest = new Vector2(0, 1000000000);
        Vector2 leftest = new Vector2(1000000000, 0);
        Vector2 rightest = new Vector2(-1000000000, 0);

        foreach (Vector2 point in default_path)
        {
            if (point.x < leftest.x)
            {
                leftest = point;
            }
            else if (point.x > rightest.x)
            {
                rightest = point;
            }
            if (point.y < lowest.y)
            {
                lowest = point;
            }
            else if (point.y > highest.y)
            {
                highest = point;
            }
        }
        trigger.size = new Vector2(rightest.x - leftest.x, highest.y - lowest.y);
        trigger.offset = new Vector2(rightest.x + leftest.x, highest.y + lowest.y) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (overlapping)
        {
            RemakeCollider();
        }
    }

    private void RemakeCollider()
    {
        for (int i = 0; i != default_path.Count - 1; i++)
        {
            if (FocusScript.isInside(default_path[i]))
            {
                System.Collections.Generic.List<Vector2> path = new System.Collections.Generic.List<Vector2>();
                if (i == 0)
                {
                    path.Add(FocusScript.Intersects(default_path[0], default_path[1])[0]);
                    for (int index = 1; index != default_path.Count - 1; index++)
                    {
                        path.Add(default_path[index]);
                    }
                    path.Add(FocusScript.Intersects(default_path[0], default_path[default_path.Count - 2])[0]);
                }
                else
                {
                    path.Add(FocusScript.Intersects(default_path[i + 1], default_path[i])[0]);
                    for (int index = i + 1; index != default_path.Count - 1; index++)
                    {
                        path.Add(default_path[index]);
                    }
                    for (int index = 0; index != i; index++)
                    {
                        path.Add(default_path[index]);
                    }
                    path.Add(FocusScript.Intersects(default_path[i - 1], default_path[i])[0]);
                }
                collidor.SetPoints(path);
                return;
            }
        }
        for (int i = 0; i != default_path.Count - 1; i++) {
            System.Collections.Generic.List<Vector2> results = FocusScript.Intersects(default_path[i], default_path[i + 1]);
            if (results.Count == 2)
            {
                Debug.Log("Successful Edge");
                System.Collections.Generic.List<Vector2> path = new System.Collections.Generic.List<Vector2>();
                path.Add(results[1]);
                for (int index = i + 1; index != default_path.Count - 1; index++)
                {
                    path.Add(default_path[index]);
                }
                for (int index = 0; index != i + 1; index++)
                {
                    path.Add(default_path[index]);
                }
                path.Add(results[0]);
                collidor.SetPoints(path);
                return;
            }
        }
        collidor.SetPoints(default_path);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Focus")
        {
            overlapping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Focus")
        {
            overlapping = false;
        }
    }
}

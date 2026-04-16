using System;
using UnityEngine;

public class CameraMovingScript : MonoBehaviour
{
    public bool FollowsHorizontal = false;
    public bool FollowsVertical  = false;
    public static Vector3 pos;
    public Vector2 CameraSpeed = new Vector2(1.75f, 1.75f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowsHorizontal)
            pos.x += (PlayerScript.pos.x - pos.x) * Math.Min(Time.deltaTime * CameraSpeed.x, 1.0f);
        if (FollowsVertical)
            pos.y += (PlayerScript.pos.y - pos.y) * Math.Min(Time.deltaTime * CameraSpeed.y, 1.0f);
        transform.position = pos;
    }
}

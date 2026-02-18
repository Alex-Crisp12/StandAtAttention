using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusScript : MonoBehaviour
{
    enum InputMode { 
        EyeTracking,
        Dualsense,
        Mouse
    };

    static InputMode inputMode = InputMode.Mouse;
    static SpriteRenderer sprite;
    public static CircleCollider2D collidor;
    static float radius = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        pointM = InputSystem.actions.FindAction("PointMouse");
        pointE = InputSystem.actions.FindAction("PointEyes");
        pointC = InputSystem.actions.FindAction("PointController");
        toggle = InputSystem.actions.FindAction("ToggleInputs");

        //if (betInputDevice)
        //{
        //    sprite.color = Color.lightPink;
        //    inputMode = InputMode.EyeTracking;
        //}
        //if ()
        //{
        //    sprite.color = Color.lightBlue;
        //    inputMode = InputMode.Dualsense;
        //}
        //else
        //{
        //    sprite.color = Color.lightYellow;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        toggleInput();

        Vector2 rawPosition = new Vector2();
        switch (inputMode)
        {
            case InputMode.EyeTracking:
                rawPosition = pointE.ReadValue<Vector2>();
                break;
            case InputMode.Dualsense:
                rawPosition = pointC.ReadValue<Vector2>();
                break;
            default:
                rawPosition = pointM.ReadValue<Vector2>();
                rawPosition /= Screen.height;
                rawPosition *= 18;
                rawPosition.x -= 9 * Screen.width / Screen.height;
                rawPosition.y -= 9;
                break;
        }
        setPosition(rawPosition);
    }

    void setPosition(Vector2 pos)
    {
        //Debug.Log("Moving To" + pos);
        this.gameObject.transform.position = pos;
    }

    InputAction pointM;
    InputAction pointE;
    InputAction pointC;
    InputAction toggle;
    bool isToggled = false;

    void toggleInput()
    {
        if (isToggled)
        {
            if (toggle.ReadValue<float>() == 0.0f)
            {
                isToggled = false;
            }
        }
        else if (toggle.ReadValue<float>() != 0.0f)
        {
            isToggled = true;
            inputMode = (InputMode)(((int)inputMode + 1) % 3);
        }
    }

    public static Vector2 getCenter()
    {
        return sprite.transform.position;
    }

    public static float getRadius()
    {
        return radius;
    }

    public static bool isInside(Vector2 point)
    {
        return (point - getCenter()).sqrMagnitude < radius * radius;
    }

    static float sum(Vector2 point)
    {
        return point.x + point.y;
    }

    public static System.Collections.Generic.List<Vector2> Intersects(Vector2 a, Vector2 b)
    {
        Vector2 d = b - a;
        float A = d.sqrMagnitude;
        float B = sum((a - getCenter()) * d) * 2;
        float C = (a - getCenter()).sqrMagnitude - radius * radius;

        float determinant = (float)Math.Sqrt(B * B - 4 * A * C);
        float t1 = (-B - determinant) / (2 * A);
        float t2 = (-B + determinant) / (2 * A);

        System.Collections.Generic.List<Vector2> results = new System.Collections.Generic.List<Vector2>();

        if (0 < t1 && t1 < 1)
        {
            results.Add(a + t1 * d);
        }
        if (0 < t2 && t2 < 1)
        {
            results.Add(a + t2 * d);
        }

        return results;
    }
}

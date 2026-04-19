using Eyeware.BeamEyeTracker.Unity;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusScript : BeamEyeTrackerMonoBehaviour
{
    enum InputMode { 
        EyeTracking,
        Dualsense,
        Mouse
    };

    static InputMode inputMode = InputMode.Mouse;
    static SpriteRenderer sprite;
    public static CircleCollider2D collidor;
    public static float radius = 1.0f;
    private Color[] colours = new Color[3];
    public Camera theCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        pointM = InputSystem.actions.FindAction("PointMouse");
        pointE = InputSystem.actions.FindAction("PointEyes");
        pointC = InputSystem.actions.FindAction("PointController");
        toggle = InputSystem.actions.FindAction("ToggleInputs");
        colours[(int)InputMode.EyeTracking] = Color.lightBlue;
        colours[(int)InputMode.Dualsense] = Color.lightSeaGreen;
        colours[(int)InputMode.Mouse] = Color.lightPink;
        for (int index = 0; index != 3; index++) { colours[index].a = 0.25f; }
        collidor.radius = radius;
        betControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        toggleInput();

        Vector2 rawPosition = new Vector2();
        switch (inputMode)
        {
            case InputMode.EyeTracking:
                rawPosition = betInputDevice.viewportGazePosition.ReadValue();
                //rawPosition *= new Vector2(32, 18);

                // Clamp gaze position to viewport bounds (0-1)
                rawPosition.x = Mathf.Clamp01(rawPosition.x);
                rawPosition.y = Mathf.Clamp01(rawPosition.y);

                // Convert screen (pixel) coordinates to world space
                Vector3 screenPosition = new Vector3(
                    rawPosition.x * Screen.width,
                    rawPosition.y * Screen.height,
                    theCamera.nearClipPlane
                );
                Vector3 worldPosition = theCamera.ScreenToWorldPoint(screenPosition);
                rawPosition.x = worldPosition.x;
                rawPosition.y = worldPosition.y;
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
        setPosition(rawPosition + new Vector2(CameraMovingScript.pos.x, CameraMovingScript.pos.y));
    }

    void setPosition(Vector2 pos)
    {
        //Debug.Log("Moving To" + pos);
        transform.position = pos;
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
            sprite.color = colours[(int)inputMode];
            if (inputMode == InputMode.EyeTracking)
            {
                betControls.Enable();
            }
            else
            {
                betControls.Disable();
            }
        }
    }

    public static Vector2 getCenter()
    {
        if (sprite == null)
        {
            return new Vector2(0, 0);
        }
        return sprite.gameObject.transform.position;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<ToggleableObject>() != null && collision == collision.GetComponentInParent<BoxCollider2D>())
        {
            collision.GetComponentInParent<ToggleableObject>().overlapping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<ToggleableObject>() != null && collision == collision.GetComponentInParent<BoxCollider2D>())
        {
            collision.GetComponentInParent<ToggleableObject>().Exit();
        }
    }
}

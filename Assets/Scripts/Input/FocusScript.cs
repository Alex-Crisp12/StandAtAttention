using UnityEngine;
using UnityEngine.InputSystem;

public class FocusScript : MonoBehaviour
{
    enum InputMode { 
        EyeTracking,
        Dualsense,
        Mouse
    };

    InputMode inputMode = InputMode.Mouse;
    SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();


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
        Debug.Log("Moving To" + pos);
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
            if (!toggle.ReadValue<bool>())
            {
                isToggled = false;
            }
        }
        else if (toggle.ReadValue<bool>())
        {
            isToggled = true;
            inputMode = (InputMode)(((int)inputMode + 1) % 3);
        }
    }
}

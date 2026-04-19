using UnityEngine;

public class MovingPlatform : ShyObject
{
    public float TimeInAnimation = 0.0f;
    public float AnimationDuration = 5.0f;
    public Vector3 StartPos;
    public Vector3 EndPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePosition();
        eyes.sprite = Resources.Load<Sprite>("EyesRight");
    }

    // Update is called once per frame
    void Update()
    {
        if (observed == activeWhenObserved)
        {
            TimeInAnimation += Time.deltaTime;
            UpdatePosition();
        }
        if (!observed)
        {
            animateEyes();
        }
    }

    void UpdatePosition()
    {
        if (TimeInAnimation > AnimationDuration)
            TimeInAnimation -= AnimationDuration;
        if (TimeInAnimation < AnimationDuration / 2.0f)
        {
            transform.position = StartPos + 2 * TimeInAnimation * (EndPos - StartPos) / AnimationDuration;
        }
        else
        {
            transform.position = EndPos + (StartPos - EndPos) * (TimeInAnimation * 2 / AnimationDuration - 1.0f);
        }
    }
}

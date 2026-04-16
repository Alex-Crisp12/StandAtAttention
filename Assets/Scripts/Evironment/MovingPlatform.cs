using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float TimeInAnimation = 0.0f;
    public float AnimationDuration = 5.0f;
    public Vector3 StartPos;
    public Vector3 EndPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeInAnimation += Time.deltaTime;
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

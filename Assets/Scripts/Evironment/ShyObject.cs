using UnityEngine;

public class ShyObject : MonoBehaviour
{
    protected bool observed;
    public bool activeWhenObserved = true;
    public SpriteRenderer eyes;
    protected float eye_cooldown = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eyes.sprite = Resources.Load<Sprite>("EyesRight");
    }

    // Update is called once per frame
    void Update()
    {
        if (!observed)
        {
            animateEyes();
        }
    }

    public void animateEyes()
    {
        float prev_cooldown = eye_cooldown;
        eye_cooldown += Time.deltaTime;
        if (eye_cooldown >= 1.0f)
        {
            eye_cooldown -= 1.0f;
            eyes.sprite = Resources.Load<Sprite>("EyesRight");
        }
        else if (eye_cooldown > 0.5f && prev_cooldown <= 0.5f)
        {
            eyes.sprite = Resources.Load<Sprite>("EyesLeft");
        }
    }

    public void SetObserved(bool isSeen)
    {
        if (observed != isSeen) {
            observed = isSeen;
            if (observed)
            {
                eyes.sprite = Resources.Load<Sprite>("EyesTense");
            }
        }
    }
}

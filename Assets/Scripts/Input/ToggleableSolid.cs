using UnityEngine;

public class ToggleableSolid : ToggleableObject
{
    SpriteRenderer sprite;
    CircleCollider2D circle;
    BoxCollider2D box;
    Rigidbody2D body;
    CompositeCollider2D collidor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        enables = sprite.maskInteraction == SpriteMaskInteraction.VisibleInsideMask;
        circle = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        foreach (BoxCollider2D box_ in GetComponents<BoxCollider2D>())
        {
            if (!box_.isTrigger)
            {
                box = box_;
                break;
            }
        }
        collidor = GetComponent<CompositeCollider2D>();
        if (enables)
        {
            circle.compositeOperation = Collider2D.CompositeOperation.Intersect;
            box.compositeOperation = Collider2D.CompositeOperation.Intersect;
        }

        RemakeCollider();
    }

    override protected void RemakeCollider()
    {
        circle.offset = FocusScript.getCenter() - (Vector2)transform.position;
        collidor.GenerateGeometry();
    }

    override protected void RemakeCollider2()
    {
        circle.offset = FocusScript.getCenter() - (Vector2)transform.position;
        collidor.GenerateGeometry();
    }
}

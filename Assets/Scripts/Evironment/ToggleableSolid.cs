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
        circle.radius = 1.5f;
        body = GetComponent<Rigidbody2D>();
        foreach (BoxCollider2D box_ in GetComponents<BoxCollider2D>())
        {
            if (!box_.isTrigger)
            {
                box = box_;
                break;
            }
        }
        //box.transform.localScale = sprite.size;
        //transform.localScale = new Vector3( 1.0f, 1, 1);
        collidor = GetComponent<CompositeCollider2D>();
        if (enables)
        {
            circle.compositeOperation = Collider2D.CompositeOperation.Intersect;
            box.compositeOperation = Collider2D.CompositeOperation.Intersect;
            if (CompareTag("Respawn"))
            {
                sprite.color = Color.red;
            }
            else
            {
                sprite.color = Color.purple;
            }
        }
        else
        {
            if (CompareTag("Respawn"))
            {
                sprite.color = Color.red;
            }
            else
            {
                sprite.color = Color.turquoise;
            }
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

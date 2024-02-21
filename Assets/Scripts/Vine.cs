using UnityEngine;

public interface IWandable
{
    public void Wand();
}

public class Vine : MonoBehaviour, IWandable
{

    public float growthSpeed;
    public float maxSize;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D vineCollider;
    private float vineHeight;

    private void Awake()
    {
        vineCollider = gameObject.AddComponent<BoxCollider2D>();
        vineCollider.isTrigger = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        vineHeight = spriteRenderer.size.y;
        vineCollider.size = new Vector2(0.8f, vineHeight);
        vineCollider.offset = new Vector2(0.5f, -(vineHeight / 2));
    }

    public void Wand()
    {
        Grow();
    }

    private void Grow()
    {
        if (vineHeight < maxSize)
        {
            vineHeight += growthSpeed;
            spriteRenderer.size = new Vector2(1, vineHeight);
            vineCollider.size = new Vector2(0.8f, vineHeight);
            vineCollider.offset = new Vector2(0.5f, -(vineHeight / 2));
        }
    }

}

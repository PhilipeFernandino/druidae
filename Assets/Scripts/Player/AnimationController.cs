using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Animator animator;
    private Movement mov;
    private SpriteRenderer sr;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mov = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(mov.Velocity.x));
        animator.SetFloat("vSpeed", mov.Velocity.y);
        if (sr.flipX && mov.Velocity.x > 0)
            sr.flipX = false;
        else if (!sr.flipX && mov.Velocity.x < 0)
            sr.flipX = true;
    }

}

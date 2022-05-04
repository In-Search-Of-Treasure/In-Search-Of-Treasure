using UnityEngine;

public class NPCMarcoController : MonoBehaviour
{

    private Rigidbody2D rigid;    
    private SpriteRenderer sprite;    
    private Animator anime;

    private bool indo;
    private float moveSpeed;

    [SerializeField]
    public float moveSpeedBase = 2;
    public bool isVertical;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
    }

    void Update()
    {

        if (isVertical)
        {
            if (indo)
            {
                moveSpeed = moveSpeedBase;
                anime.Play(AnimationConstants.MarcoNPC.GoingVertical);
            }
            else
            {
                moveSpeed = -moveSpeedBase;
                anime.Play(AnimationConstants.MarcoNPC.ComingVertical);
            }
            rigid.velocity = transform.TransformDirection(Vector2.up * moveSpeed);
        }
        else
        {
            if (indo)
            {
                moveSpeed = moveSpeedBase;
            }
            else
            {
                moveSpeed = -moveSpeedBase;
            }
            
            sprite.flipX = !indo;
            anime.Play(AnimationConstants.MarcoNPC.Horizontal);
            rigid.velocity = transform.TransformDirection(Vector2.right * moveSpeed);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagsConstants.MarcoNPC.Turn))
        {
            indo = !indo;
        }
    }
}

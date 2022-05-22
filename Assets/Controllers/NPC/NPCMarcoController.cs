using UnityEngine;

public class NPCMarcoController : MonoBehaviour
{

    private Rigidbody2D rigid;    
    private SpriteRenderer sprite;    
    private Animator anime;

    private bool indo;
    private float moveSpeed;

    [SerializeField]
    public float moveSpeedBase;
    public bool isVertical;

    public GameObject tilemapBg;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();

        // Avoid npc collision with camera follow area.
        Physics2D.IgnoreCollision(tilemapBg.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        ResetEnemySpeed();
    }

    void Update()
    {
        moveSpeedBase = GetEnemySpeed();

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.MarcoNPC.Turn))
        {
            indo = !indo;
        }
    }

    private float GetEnemySpeed()
    {
        var speed = PlayerPrefs.GetFloat(PlayerPrefConstants.SkillsRelated.EnemySpeed, PlayerPrefConstants.SkillsRelated.EnemyDefaultSpeedValue);
        return speed;
    }

    private void ResetEnemySpeed()
    {
        PlayerPrefs.SetFloat(PlayerPrefConstants.SkillsRelated.EnemySpeed, PlayerPrefConstants.SkillsRelated.EnemyDefaultSpeedValue);
    }
}

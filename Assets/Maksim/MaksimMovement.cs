using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaksimMovement : MonoBehaviour
{
    public float speed;

    private Vector2 direction;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        direction = Vector2.zero;
    }


    // Update is called once per frame
    void Update()
    {
        CharacterInput();
        //mover em velocidade constante
        transform.Translate(speed * Time.deltaTime * direction);
        if(direction.x != 0 || direction.y != 0)
        {
            AnimateCharacter(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0f);
        }

    }

    void CharacterInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }
    }

    void AnimateCharacter(Vector2 dir)
    {
        animator.SetLayerWeight(1, 1f);
        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.y);
    }

}
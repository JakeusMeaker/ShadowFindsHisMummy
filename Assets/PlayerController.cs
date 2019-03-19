using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private Sprite hidingSprite;
    [SerializeField]
    private Sprite unHidingSprite;
    [SerializeField]
    private Sprite normalSprite;
    [SerializeField]
    private bool isHide;
    [SerializeField]
    private float hideTime;
    
    private Transform catTransform;
    private float movementDir;
    private SpriteRenderer sprite;
    private Animator anim;

	// Use this for initialization
	void Start () {
        catTransform = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        movementDir = Input.GetAxisRaw("Horizontal");
        anim.SetBool("isWalking", false);
        if (!isHide)
        {


            if (movementDir == 1)
            {
                GoRight();
            }

            if (movementDir == -1)
            {
                GoLeft();
            }

            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(Hiding());
            }
        }
	}

    void GoRight()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        sprite.flipX = false;
        anim.SetBool("isWalking", true);
    }

    void GoLeft()
    {
        transform.position += Vector3.right * -speed * Time.deltaTime;
        sprite.flipX = true;
        anim.SetBool("isWalking", true);
    }

    IEnumerator Hiding()
    {
        isHide = true;
        this.GetComponent<SpriteRenderer>().sprite = hidingSprite;
        yield return new WaitForSeconds(hideTime);
        sprite.sprite = unHidingSprite;
        yield return new WaitForSeconds(0.5f);
        sprite.sprite = normalSprite;
        isHide = false;
        yield return null;
    }
}

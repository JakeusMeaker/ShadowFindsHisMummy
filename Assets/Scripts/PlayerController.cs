using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isHide;
    [SerializeField]
    private float hideTime;
    [SerializeField]
    private Text hideText;

    private Fear fear;
    private bool canHide;
    private float movementDir;
    private SpriteRenderer sprite;
    private Animator anim;

	// Use this for initialization
	void Start () {        
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        fear = GetComponent<Fear>();
        canHide = false;
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
                if (canHide)
                {
                    Hiding();
                }
                else
                    return;
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

    void Hiding()
    {
        isHide = true;
        canHide = false;
        fear.ChangeFillRate(80f);
        anim.SetTrigger("startedHiding");
        anim.SetBool("isHiding", true);
        Invoke("UnHidingAnim", hideTime);
        
    }

    void UnHidingAnim()
    {
        anim.SetBool("isHiding", false);
        isHide = false;
        fear.ChangeFillRate(120f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collided");
        if(other.tag == "Shadow")
        {
            Debug.Log("canHide");
            canHide = true;
            hideText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Shadow")
        {
            Debug.Log("Unhide");
            canHide = false;
            hideText.enabled = false;
        }
    }
}

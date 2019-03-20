using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float hideTime;
    [SerializeField]
    private Text hideText;
    [SerializeField]
    private WrapperScript gameManager;
    [SerializeField]
    private CinemachineVirtualCamera cM;

    private bool isHide;
    private Fear fear;
    private bool canHide;
    private float movementDir;
    private SpriteRenderer sprite;
    private Animator anim;
    private Collider2D collider;
    private bool canInput = true;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        fear = GetComponent<Fear>();
        collider = GetComponent<Collider2D>();
        canHide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            movementDir = Input.GetAxisRaw("Horizontal"); 
        }
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

        if (Input.GetKey(KeyCode.Escape))
        {
            gameManager.MainMenu();
        }
    }

    void GoRight()
    {
        Debug.Log("Going Right");
        transform.position += Vector3.right * speed * Time.deltaTime;
        sprite.flipX = false;
        anim.SetBool("isWalking", true);
    }

    void GoLeft()
    {
        Debug.Log("Going Left");
        transform.position += Vector3.right * -speed * Time.deltaTime;
        sprite.flipX = true;
        anim.SetBool("isWalking", true);
    }

    void Hiding()
    {
        isHide = true;
        canHide = false;
        collider.enabled = false;
        fear.ChangeFillRate(30f);
        anim.SetTrigger("startedHiding");
        anim.SetBool("isHiding", true);
        Invoke("UnHidingAnim", hideTime);

    }

    void UnHidingAnim()
    {
        anim.SetBool("isHiding", false);
        isHide = false;
        collider.enabled = true;
        fear.ChangeFillRate(120f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collided");
        if (other.tag == "Shadow")
        {
            Debug.Log("canHide");
            canHide = true;
            hideText.enabled = true;
        }

        if (other.tag == "Enemy")
        {
            canInput = false;
            anim.SetTrigger("Hit");
            cM.Follow = null;
            Invoke("GameOver", 3);
            StartCoroutine(Run());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Shadow")
        {
            Debug.Log("Unhide");
            canHide = false;
            hideText.enabled = false;
        }
    }

    void GameOver()
    {
        gameManager.GameOver();
    }

    IEnumerator Run()
    {
        float t = 0;
        speed = 5;

        while (t < 3)
        {
            Debug.Log(t);
            GoLeft();
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }

        yield return null;
    }
}

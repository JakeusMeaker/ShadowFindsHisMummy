using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbAI : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float waitTime;
    bool isMovingLeft = true;
    SpriteRenderer spRend;

    Vector3 startPos;
    Vector3 newPos;
    Collider2D col;
    private Vector2 pos1;
    private Vector2 pos2;
    private bool movingforward;
    private Vector2 nextPos;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider2D>();
        pos1 = transform.position;
        startPos = pos1;
        pos2 = (Vector2)transform.position + nextPos;
        spRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        StartCoroutine(MoveObject());
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos.x > transform.position.x)
        {
            if (isMovingLeft)
            {
                FlipSprite(false);
                isMovingLeft = false;
            }

        }
        else if (startPos.x < transform.position.x)
        {
            if (!isMovingLeft)
            {
                FlipSprite(true);
                isMovingLeft = true;
            }
        }
        startPos = transform.position;
    }

    void FlipSprite(bool flippp)
    {
        spRend.flipX = flippp;
        Vector2 newOff = col.offset;
        newOff.x = -newOff.x;
        col.offset = newOff;
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            
            pos1 = transform.position;

            for (int i = 0; i < wayPoints.Length; i++)
            {
                anim.SetTrigger("A");
                yield return new WaitForSeconds(waitTime);
                anim.SetTrigger("A");
                pos2 = wayPoints[i].position;
                float t = 0;
                while (t < 1)
                {
                    transform.position = Vector2.Lerp(pos1, pos2, t);
                    t += Time.deltaTime / moveSpeed;
                    yield return new WaitForEndOfFrame();
                }
                pos1 = pos2;
            }

            //movingforward = !movingforward;
        }
    }
}

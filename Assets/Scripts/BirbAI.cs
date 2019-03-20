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

    private Vector2 pos1;
    private Vector2 pos2;
    private bool movingforward;
    private Vector2 nextPos;


    // Use this for initialization
    void Start()
    {
        pos1 = transform.position;
        pos2 = (Vector2)transform.position + nextPos;

        StartCoroutine(MoveObject());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator MoveObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < wayPoints.Length; i++)
            {
                pos2 = wayPoints[i].position;
                
                float t = 0;
                while (t < 1)
                {
                    transform.position = Vector2.Lerp(movingforward ? pos1 : pos2, movingforward ? pos2 : pos1, t);
                    t += Time.deltaTime / moveSpeed;
                    yield return null;
                }

            }

            movingforward = !movingforward;
        }
    }
}

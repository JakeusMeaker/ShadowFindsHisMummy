using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fear : MonoBehaviour
{

    [SerializeField]
    private Image fearMeter;
    private float fillRate;
    private float fill;
    [SerializeField] float startFillTimeInSeconds;
    [SerializeField] WrapperScript sceneManager;


    // Use this for initialization
    void Start()
    {
        ChangeFillRate(startFillTimeInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        fill += fillRate;
        fearMeter.fillAmount = fill;
        Debug.Log(fill);
        if (fearMeter.fillAmount == 1)
        {
            Debug.Log("GameOver");
            sceneManager.GameOver();
        }
    }

    public void ChangeFillRate(float NewFillTime)
    {
        fillRate = Time.deltaTime / NewFillTime;
    }

    public float GetFillRate()
    {
        return fillRate;
    }

}

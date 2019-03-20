using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fear : MonoBehaviour {

    [SerializeField]
    private Image fearMeter;

    private float fill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        fearMeter.fillAmount = fill;
	}

   
  
}

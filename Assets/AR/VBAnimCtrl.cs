using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VBAnimCtrl : MonoBehaviour, IVirtualButtonEventHandler {
    
    public GameObject VBBtn;
    public Animator characterAnim;
	// Use this for initialization
	void Start () {
        VBBtn = GameObject.Find("Btn_Rotation");
        VBBtn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        characterAnim.GetComponent<Animator>();
	}
	

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        characterAnim.SetBool("isWalk", true);
        characterAnim.SetBool("isIdle", false);
        Debug.Log("Pressed");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        characterAnim.SetBool("isIdle", true);
        characterAnim.SetBool("isWalk", false);
        Debug.Log("Released");
    }
}

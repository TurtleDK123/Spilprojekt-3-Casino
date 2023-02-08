using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    Animator animator;

    private bool openclose = true;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetBool("OpenClose", openclose);
            openclose = !openclose;
        }
    }
}

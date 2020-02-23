using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    private int choice = 1;
    public Transform Op1;
    public Transform Op2;

    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.W))
        {
            choice = 1;
            transform.position = Op1.position;
        }
        else if (Input.GetKeyDown (KeyCode.S))
        {
            choice = 2;
            transform.position = Op2.position;
        }
        if (choice == 1 && Input.GetKeyDown (KeyCode.Space))
        {
            SceneManager.LoadScene (1);
        }

    }
}
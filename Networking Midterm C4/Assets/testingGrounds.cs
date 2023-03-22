using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingGrounds : MonoBehaviour
{
    RInput tester;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tester = new RInput();
        Debug.Log(tester.input);
    }
}

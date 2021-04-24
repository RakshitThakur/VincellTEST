using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveDoor : MonoBehaviour
{
    bool isOpening = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpening)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,  15f,transform.position.z), 1f * Time.deltaTime);
        }
    }
    public void MoveCave()
    {
        isOpening = true;
    }
}

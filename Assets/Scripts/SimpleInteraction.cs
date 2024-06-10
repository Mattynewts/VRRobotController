using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody Ball;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float triggerLeft = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        float triggerRight = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);

        //float triggerRight = OVRInput.Get(OVRInput.RawAxis1D.button);


        if (triggerRight > 0.9f)
        {
            Instantiate(Ball, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}

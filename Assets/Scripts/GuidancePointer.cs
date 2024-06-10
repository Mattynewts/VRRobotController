using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuidancePointer : MonoBehaviour
{

    public int rayLength = 10;
    public float delay = 0.1f;
    public double step = 0.0001d;
    Vector3 pointerPos = new Vector3(); //postion of pointer

    public GameObject follower;         //object which goes to the pointer
    public GameObject pointer;          //pointer is at location that follower goes to

    public Material tempMaterial;       //material used for the beams
    public GameObject player;           //player game object

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;


        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength * 10))
            {

                if (hit.collider.gameObject.tag == "Floor")
                {

                    //follower.transform.pos

                    GameObject myLine = new GameObject();
                    myLine.transform.position = transform.position;
                    myLine.AddComponent<LineRenderer>();

                    LineRenderer lr = myLine.GetComponent<LineRenderer>();
                    lr.material = tempMaterial;

                    lr.startWidth = 0.01f;
                    lr.endWidth = 0.01f;
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, hit.point);
                    GameObject.Destroy(myLine, delay);

                    pointerPos = hit.point;


                    pointer.transform.position = hit.point;


                }
                else
                {

                    GameObject myLine = new GameObject();
                    myLine.transform.position = transform.position;
                    myLine.AddComponent<LineRenderer>();

                    LineRenderer lr = myLine.GetComponent<LineRenderer>();

                    lr.startColor = new Color(1, 0, 0);
                    lr.endColor = new Color(1, 0, 0); //makes the end of beam red
                    lr.startWidth = 0.01f;
                    lr.endWidth = 0.01f;
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, hit.point);
                    GameObject.Destroy(myLine, delay);


                }
            }
        }


        //update the balls position in relation to the selected location
        // Might want to add a final check so when its close enough to the end it just "snaps" into place
        double followerMagnitude;
        Vector3 followerUnitVector = new Vector3();
        Vector3 displacement = new Vector3();
        displacement = pointer.transform.position - follower.transform.position;
        followerMagnitude = Math.Sqrt(displacement.x*displacement.x + displacement.y*displacement.y + displacement.z*displacement.z);
        followerUnitVector.x = (float)((displacement.x / (float)followerMagnitude) * 0.01);
        followerUnitVector.y = (float)((displacement.y / (float)followerMagnitude) * 0.01);
        followerUnitVector.z = (float)((displacement.z / (float)followerMagnitude) * 0.01);

        //followerUnitVector = pointer.transform.position;//

        //updatePosition.SetX(pointer.transform.position.x);

        //updatePosition = follower.transform.position + (pointer.transform.position * step);

        follower.transform.position += followerUnitVector;


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{

    public int rayLength = 10;
    public float delay = 0.1f;
    bool aboutToTeleport = false;
    Vector3 teleportPos = new Vector3();

    public Material tempMaterial;

    public GameObject pointer;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;


        if (OVRInput.Get(OVRInput.Button.Three))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength * 10))
            {

                if (hit.collider.gameObject.tag == "Floor")
                {
                    aboutToTeleport = true;
                    //teleportPos = hit.point;

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

                    teleportPos = hit.point;


                    pointer.transform.position = hit.point;


                }
                else
                {
                    aboutToTeleport = false;
                    Vector3 v1 = transform.position;
                    v1 = transform.TransformPoint(Vector3.forward * rayLength);

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

        if(OVRInput.GetUp(OVRInput.Button.Three) && aboutToTeleport == true)
        {
            aboutToTeleport = false;

            var playerV = GetComponentInChildren<OVRPlayerController>();
            teleportPos.y += 2;
            player.transform.position = teleportPos;
            playerV.Teleported = true;

        }
        
    }
}

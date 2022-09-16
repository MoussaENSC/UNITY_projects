using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    //values that will be set in the Inspector
    public Transform TargetLook = null;
    public Transform TargetMove = null;
    public Transform TargetLook2 = null;
    public Transform TargetMove2 = null;
    private float cameraSpeed = 15f;
    public bool follow = true;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    public Transform player;
    private Vector3 offset = new Vector3(0f, 0.5f, -11f);
    private int a = 0;
    private int b = 0;
    // Update is called once per frame
    void Update()
    {
        a = GameObject.Find("Player").GetComponent<PlayerMovement>().level;     
        if(follow == true)
        {
            transform.position = player.position + offset;
            
            
        }
        if (a == 1 && b == 0)
        {
            StartCoroutine(MoveDelay());
        }
        if (a == 2 && b == 1)
        {
            StartCoroutine(MoveDelay2());
        }
    }
    IEnumerator MoveDelay()
    {
        follow = false;
        //find the vector pointing from our position to the target
        _direction = (TargetLook.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * cameraSpeed);
        transform.position = Vector3.MoveTowards(transform.position, TargetMove.position, cameraSpeed * Time.deltaTime);
        yield return new WaitForSeconds(2f);
        offset = new Vector3(-10f, 0.5f, 0f);
        follow = true;
        b = 1;
        yield return null;
    }
    IEnumerator MoveDelay2()
    {
        follow = false;
        //find the vector pointing from our position to the target
        _direction = (TargetLook2.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * cameraSpeed);
        transform.position = Vector3.MoveTowards(transform.position, TargetMove2.position, cameraSpeed * Time.deltaTime);
        yield return new WaitForSeconds(2f);
        offset = new Vector3(0f, 10f, 0f);
        transform.rotation = Quaternion.Euler(87.932f, 89.899f, 0f);
        follow = true;
        b = 2;
        yield return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 10f;
    public int numJumps = 0;
    public int numJumpsUsed = 0;
    public int level = 0;
    //audio
    public AudioClip jumpClip;
    public AudioSource audioSource;
    public AudioClip deadClip;
    public AudioClip collectClip;
    public AudioClip musicClip1;
    public AudioClip musicClip2;
    public AudioClip musicClip3;
    public AudioClip winClip;
    //
    private string DEAD_TAG = "BadTile";
    private string COLLECT_TAG = "JumpCoin";
    private string LEVEL_TAG = "CheckPoint";
    private Vector3 startArea = new Vector3(0f, 0f, 0f);
    private Rigidbody rb;
    private Transform tr;
    private Transform trCamera;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        trCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
            Jump();
            Move();
            Again();
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && numJumps > numJumpsUsed)
        {
            numJumpsUsed += 1;
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            audioSource.PlayOneShot(jumpClip, 0.7f);
        }
        


    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (level == 0)
        {
            tr.position += new Vector3(h, 0f, 0f) * moveSpeed * Time.deltaTime;
        }
        if (level == 1)
        {
            if(v*(-1) == 1)
            {
                v = 0f;
            }
            tr.position += new Vector3(v, 0f, -h) * moveSpeed * Time.deltaTime;
        }
        if (level == 2)
        {
            tr.position += new Vector3(v, 0f, -h) * moveSpeed * Time.deltaTime;
        }
    }
    void Again()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            tr.position = startArea;
            tr.rotation = new Quaternion(0f, 0f, 0f, 0f);
            numJumpsUsed = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(DEAD_TAG)){
            numJumpsUsed = 0;
            tr.position = startArea;
            tr.rotation = new Quaternion(0f, 0f, 0f, 0f);
            audioSource.PlayOneShot(deadClip, 0.7f);
        }
        if (collision.gameObject.CompareTag(COLLECT_TAG))
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(collectClip, 0.7f);
            numJumps += 1;
        }
        if (collision.gameObject.CompareTag(LEVEL_TAG))
        {
            level += 1;
            Destroy(collision.gameObject);
                if (level == 1)
            {
                startArea = new Vector3(8f, -130.76f, 0.62f);
            }
                if (level == 2)
            {
                startArea = new Vector3(68f, -154.8096f, 0.62f);
            }
           
            audioSource.PlayOneShot(winClip, 0.7f);
            
            Debug.Log("yo");
        }
    }
}

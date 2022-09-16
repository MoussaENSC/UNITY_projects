using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class JumpText : MonoBehaviour
{
    public TMP_Text jumps;
    public TMP_Text win;
    private int numLevel = 0;
    private int a = 0;
    private int b = 0;
    private int c = 0;

    public void Update()
    {
        a = GameObject.Find("Player").GetComponent<PlayerMovement>().numJumps;
        b = GameObject.Find("Player").GetComponent<PlayerMovement>().numJumpsUsed;
        c = a - b;
        jumps.text = c.ToString();

        numLevel = GameObject.Find("Player").GetComponent<PlayerMovement>().level;
        if(numLevel == 3)
        {
            StartCoroutine(Win());
        }
    }
    IEnumerator Win()
    {
        win.text = "YOU WIN!!!";
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

}

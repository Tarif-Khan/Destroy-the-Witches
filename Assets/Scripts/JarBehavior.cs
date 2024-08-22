using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public static int jarCount = 0;
    public AudioClip pickupSFX;
    public int scoreValue = 1;
    void Start()
    {
        jarCount++;
        Debug.Log("Total jarCount :" + jarCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver)
        {
            jarCount = 0;
        }
    }

    private void OnDestroy()
    {
        if (!LevelManager.isGameOver)
        {
            jarCount--;
            Debug.Log("Pickups remaining " + jarCount);

            if (jarCount <= 0)
            {
                Debug.Log("You Win");
                FindObjectOfType<LevelManager>().LevelBeat();

            }
        }
    }
}
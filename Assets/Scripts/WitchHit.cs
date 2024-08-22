using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchHit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lootPrefab;
    public GameObject WitchExpelled;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (other.CompareTag("Projectile"))
        {
            if (levelManager != null)
            {
                levelManager.AddWitchScore(1);
            }
            DestroyWitch();
        }
    }

    void DestroyWitch()
    {
        Instantiate(WitchExpelled, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Instantiate(lootPrefab, transform.position, transform.rotation);
        Destroy(gameObject, .5f);
    }
}
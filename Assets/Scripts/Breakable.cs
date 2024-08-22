using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject bucketPieces;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BucketProjectile"))
        {
            Transform currentCrate = gameObject.transform;

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.AddScore(1);
            }
            GameObject pieces = Instantiate(bucketPieces, currentCrate.position, currentCrate.rotation);
            Rigidbody[] rbPieces = pieces.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbPieces)
            {
                rb.AddExplosionForce(explosionForce, currentCrate.position, explosionRadius);
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace S3 {
    public class Bullet : MonoBehaviour {
        // bullet collacton, see if the bulet hits a object
        void OnCollisionEnter(Collision collision) {
            GameObject hit = collision.gameObject;
            Health health = hit.GetComponent<Health>();
            if(health != null) {
                //Take damage to health
                health.TakeDamage(10);
            }
            //destroy the object
            Destroy(gameObject);
        }

    }
}


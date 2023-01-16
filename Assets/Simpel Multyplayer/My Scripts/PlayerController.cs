using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace S3
{
    public class PlayerController : NetworkBehaviour {
   
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Update()
    {   
        // does it so only the local player can control locla
        if(!isLocalPlayer) {
            return;
        }
        // make him go on x and y 
        float x = Input.GetAxis("Horizontal")* Time.deltaTime * 150.0f;
        float z = Input.GetAxis("Vertical")* Time.deltaTime * 3.0f;
        transform.Rotate(0,x,0);
        transform.Translate(0,0,z);

        // if key leftClick is press use Fire() function
        if(Input.GetKeyDown(KeyCode.Space)) {
            CmdFire();
        }

        [Command]
        void CmdFire() {
            //Create the bullet form prefrap
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            // add velocity  to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 6.0f;

            //Spawn the bullet on the clients
            NetworkServer.Spawn(bullet);

            //destroy bullet after 2s 
            Destroy(bullet, 2);
        }


        // if key press is r 
        // if(Input.GetKeyDown(KeyCode.R))
        //     {
        //         // random color on body
        //         GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                
        //     }
    }
    public override void OnStartLocalPlayer() {
            GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}  
}

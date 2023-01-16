using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace S3 {
    public class Health : NetworkBehaviour {
        //max health
        public const int maxHealth = 100;
        [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
        public RectTransform healthbar;
        public bool DestroyOnDeath;
        private NetworkStartPosition[] spawnPoints;

        void start() {
            if(isLocalPlayer) {
                spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            }
        }

        // TakeDamage 
        public void TakeDamage(int amount) {
            //if its not sevrer 
            if(!isServer) {
                return;
            }
                // currentHealth - amount of damage
                currentHealth -= amount;
                // if its les ore = to 0 
                if(currentHealth <= 0) {

                    if(DestroyOnDeath) {
                        Destroy(gameObject);
                    } else {
                        //respawn when thay die/0 health
                        currentHealth = maxHealth;
                        RpcRespawn();
                    }
                }
        }
        // function to chance helth
        void OnChangeHealth(int oldValue, int newValue) {
            float healthPercent = (float)newValue / maxHealth;
            healthbar.sizeDelta = new Vector2(healthPercent * healthbar.parent.GetComponent<RectTransform>().rect.width, healthbar.sizeDelta.y);
        }
        [ClientRpc]
        //function on for respawn to work
        void RpcRespawn() {
            if(isLocalPlayer){
                Vector3 spawnPoint = Vector3.zero;
                if(spawnPoints !=  null && spawnPoints.Length > 0) {
                    spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                }
                transform.position = spawnPoint;
            }
        }
    }    
}

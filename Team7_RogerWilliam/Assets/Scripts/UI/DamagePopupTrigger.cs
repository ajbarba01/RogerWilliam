    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DamagePopupTrigger : MonoBehaviour
    {
        [SerializeField] private bool player;
        [SerializeField] private float cooldown = 0.25f;

        private Health health;
        private float sinceLastHit, sinceLastCreate;
        private float runningDamage = 0f;
        
        private void Start() {
            if (player) {
                health = GameHandler.playerHealth;
            }

            else {
                health = GetComponent<Health>();
            }

            health.tookDamage.AddListener(TookDamage);
        }

        public void TookDamage(float amount) {
            sinceLastHit = Time.time;

            runningDamage += amount;

            if (runningDamage >= 1 && Time.time > sinceLastCreate + cooldown) {
                DamagePopup.Create(transform.position, (int)runningDamage, DamagePopup.DEFAULT);
                sinceLastCreate = Time.time;
                runningDamage = 0f;
            }

            // if (Time.time > sinceLastCreate + cooldown) {
            //     DamagePopup.Create(transform.position, (int)runningDamage, DamagePopup.DEFAULT);
            //     sinceLastCreate = Time.time;
            //     runningDamage = 0f;
            // }
        }
    }

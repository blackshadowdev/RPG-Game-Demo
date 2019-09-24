using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        [SerializeField] Text healthValue;

        private void Awake() {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthValue = GetComponent<Text>();
        }

        private void Update() {
            healthValue.text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}
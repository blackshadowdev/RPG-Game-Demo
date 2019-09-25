using System;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        [SerializeField] Text healthValue;

        private void Awake() {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthValue = GetComponent<Text>();
        }

        private void Update() {
            if(fighter.GetTarget() == null){
                healthValue.text = "N/A";
                return;
            }
            
            healthValue.text = String.Format("{0:0}%", fighter.GetTarget().GetPercentage());
        }
    }
}
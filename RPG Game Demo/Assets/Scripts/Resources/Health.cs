using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using RPG.Combat;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead;

        private void Start() {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public bool IsDead(){
            return isDead;
        }


        public void TakeDamage(float damage){
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if(healthPoints <= 0){
                Die();
            }
        }

        public float GetPercentage(){
            return healthPoints / GetComponent<BaseStats>().GetHealth() * 100;
        }

        private void Die()
        {    
            if(isDead) return;
                    
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        
        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }    
}
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField]
        float weaponRange = 2f;
        [SerializeField] 
        Transform target = null;
        Mover mover;

        private void Awake() {
            mover = FindObjectOfType<Mover>();
        }

        private void Update()
        {
            if (target == null) return;
            
            if (!IsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget){
            target = combatTarget.transform;
        }

        public void Cancel(){
            target = null;
        }
    }
}
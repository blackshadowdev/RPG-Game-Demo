using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
       
       
        Health target;
        Mover mover;
        Animator animator;
        Weapon currentWeapon = null;

        float timeSinceLastAttack = Mathf.Infinity;


        private void Awake() {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Start() {
            EquipWeapon(defaultWeapon);
        }

        public void EquipWeapon(Weapon weaponType)
        {
             currentWeapon = weaponType;
             weaponType.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (target.IsDead()) return;
            
            if (!IsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks){
                animator.ResetTrigger("stopAttack");
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        //Animation event
        void Hit(){
            if(target == null) return;
            if(!currentWeapon.HasProjectile()) return;

            currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
        }

        //Animation event
        void Shoot(){
            Hit();
        }

        public bool CanAttack(GameObject combatTarget){
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        public void Attack(GameObject combatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel(){
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
            target = null;
            mover.Cancel();
        }

    }
}
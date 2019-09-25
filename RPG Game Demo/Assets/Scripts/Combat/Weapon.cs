using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{   
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] Projectile projectile = null;
        [SerializeField] float weaponDamage = 5f; 
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if (equippedPrefab != null){
                Transform hand = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(equippedPrefab, hand);
                weapon.name = weaponName;
            }
            
            //Cache the "Character" animation controller as anim. override controller 
            //in case we need to override back to it.
            var defaultAnimatorController = animator.runtimeAnimatorController as AnimatorOverrideController;

            //Override the animation controllers
            if (animatorOverride != null){
                animator.runtimeAnimatorController = animatorOverride;
            }
            //In case one of the anim. override controllers is not set.
            else if(defaultAnimatorController != null){ 
                //Override it by the default animation controller
                animator.runtimeAnimatorController = defaultAnimatorController.runtimeAnimatorController;
        
            }else{
                Debug.LogWarning("Animator override controller of " + this.name + " scriptable object is not set correctly");
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform previousWeapon = rightHand.Find(weaponName);
            
            if(previousWeapon == null) { 
                previousWeapon = leftHand.Find(weaponName); 
            }

            if(previousWeapon == null) return;

            previousWeapon.name = "DESTROYING";

            Destroy(previousWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        public bool HasProjectile(){
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator){
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, weaponDamage);
        }

        public float GetDamage(){
            return weaponDamage;
        }

        public float GetRange(){
            return weaponRange;
        }
    }
}

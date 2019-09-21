using RPG.Core;
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

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab == null) return;
            if (animatorOverride == null) return;
            Transform hand = GetTransform(rightHand, leftHand);
            Instantiate(equippedPrefab, hand);
            animator.runtimeAnimatorController = animatorOverride;
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        public bool HasProjectile(){
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target){
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        public float GetRange(){
            return weaponRange;
        }
    }
}

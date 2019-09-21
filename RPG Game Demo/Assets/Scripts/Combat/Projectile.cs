using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        float damage = 0;
        Health target = null;
        
        private void Update() {
            if(target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null) return target.transform.position;
            
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        public void SetTarget(Health target, float damage){
            this.target = target;
            this.damage = damage;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.GetComponent<Health>() != target) return;
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

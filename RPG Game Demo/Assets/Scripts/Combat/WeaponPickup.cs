using System;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;

        BoxCollider m_collider;
        Transform weaponModel;

        private void Start() {
            m_collider = GetComponent<BoxCollider>();
            weaponModel = this.gameObject.transform.GetChild(0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
              
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        IEnumerator HideForSeconds(float seconds){
            HidePickup();
            yield return new WaitForSeconds(seconds);
            ShowPickup();
        }

        private void ShowPickup()
        {
            m_collider.enabled = true;
            weaponModel.gameObject.SetActive(true);
        }

        private void HidePickup()
        {
            m_collider.enabled = false;
            weaponModel.gameObject.SetActive(false);
        }
    }

}
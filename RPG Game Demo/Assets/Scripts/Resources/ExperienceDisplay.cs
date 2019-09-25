using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        [SerializeField] Text experienceValue;

        private void Awake() {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            experienceValue = GetComponent<Text>();
        }

        private void Update() {
            experienceValue.text = String.Format("{0:0}%", experience.GetPoints());
        }
    }
}
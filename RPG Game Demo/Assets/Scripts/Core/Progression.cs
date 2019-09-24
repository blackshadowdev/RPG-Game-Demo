using UnityEngine;
using RPG.Stats;

namespace RPG.Core
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        
        [System.Serializable]
        class ProgressionCharacterClass{
            [SerializeField] CharacterClass characterClass;
            [SerializeField] float[] health;
        }
    }    
}


using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isAlreadyTrigged;
        PlayableDirector director;

        private void Awake() {
            director = GetComponent<PlayableDirector>(); 
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player" && !isAlreadyTrigged)
            {
                isAlreadyTrigged = true;
                director.Play();
         
            }
        }
    }
}

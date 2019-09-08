using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isAlreadyTrigged;
        PlayableDirector director;
        PlayerController playerController;
        [SerializeField] ActionScheduler actionScheduler;

        private void Awake() {
            director = GetComponent<PlayableDirector>();  
            playerController = FindObjectOfType<PlayerController>();
            actionScheduler = playerController.gameObject.GetComponent<ActionScheduler>();
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player" && !isAlreadyTrigged)
            {
                isAlreadyTrigged = true;
                director.Play();
                CheckCinematicStateAndRespond();
            }
        }

        void OnTriggerStay(Collider other)
        {
            CheckCinematicStateAndRespond();
        }

        private void CheckCinematicStateAndRespond()
        {
            if(director.state == PlayState.Playing){
                actionScheduler.CancelCurrentAction();
            }
        }
    }
}

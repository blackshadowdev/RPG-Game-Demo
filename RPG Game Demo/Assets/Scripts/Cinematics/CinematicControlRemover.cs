using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;
using RPG.Core;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayerController playerController;

        void Awake()
        {
            playerController = FindObjectOfType<PlayerController>();    
        }
        
        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void DisableControl(PlayableDirector director){
            playerController.enabled = false;
            playerController.gameObject.GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        
        void EnableControl(PlayableDirector director){
            playerController.enabled = true;
       }

        void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }
    }
}
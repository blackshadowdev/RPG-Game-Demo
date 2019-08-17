using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;

        void Start()
        {
            mover = FindObjectOfType<Mover>();
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        void MoveToCursor()
        {
            //Create raycast to locate hit point of the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                //Move the player to the destination
                mover.MoveTo(hit.point);
            }
        }
    }
}

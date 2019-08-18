using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;
        RaycastHit[] m_Results = new RaycastHit[1];

        void Start()
        {
            mover = FindObjectOfType<Mover>();
            fighter = FindObjectOfType<Fighter>();
        }

        void Update()
        {
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        bool InteractWithCombat()
        {
            if(Physics.RaycastNonAlloc(GetMouseRay(), m_Results) > 0){
                for (int i = 0; i < m_Results.Length; i++)
                {
                    CombatTarget target = m_Results[i].transform.gameObject.GetComponent<CombatTarget>();
                    if(target == null) continue;

                    if(Input.GetMouseButtonDown(0)){
                        fighter.Attack(target);
                    } 
                    return true;
                }
            } 
            return false;
        }

        bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0)){
                    mover.MoveTo(hit.point);
                    fighter.Cancel();
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            //Create raycast to locate hit point of the mouse
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

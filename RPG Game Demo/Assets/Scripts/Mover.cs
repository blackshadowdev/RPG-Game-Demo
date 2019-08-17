using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            MoveToCursor();
        }
   }

    void MoveToCursor(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit){
            agent.destination = hit.point; 
        }
    }
}

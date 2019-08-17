using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Mover mover;
    RaycastHit hit;
        
    void Start()
    {
        mover = FindObjectOfType<Mover>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0)){
            CreateRaycast();
            mover.MoveTo(hit.point);
        }
    }

    void CreateRaycast(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit){
            hit.point = hit.point;
        }
    }
}

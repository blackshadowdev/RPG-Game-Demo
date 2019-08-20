using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float waypointTolerance = 1;
        [SerializeField] PatrolPath patrolPath;

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;

        Vector3 guardPosition; 
        int currentWaypointIndex = 0;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Awake() {
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;
        }

        private void Update()
        {
            if(health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();

            }
            else if(timeSinceLastSawPlayer < suspicionTime )
            {
                SuspiciousBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            
            if(patrolPath != null){
                if(AtWaypoint()){
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetCurrentWaypoint()
        {
             return patrolPath.GetWaypoint(currentWaypointIndex);   
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());    
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspiciousBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }
        
        //Called by Unity
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
   }

}
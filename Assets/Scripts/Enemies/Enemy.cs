using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        public event Action OnSpawn = delegate { };
        public event Action OnDeath = delegate { };
        BuildingManager buildingManager;
        Building townCenter;
        Vector3 destination;
        int maxHP = 100;
        HealthPoints healthPoints = new HealthPoints(100);



        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();
    
        private void FetchComponents()
        {
            agent ??= GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            healthPoints.SetMaxHP(maxHP);
            healthPoints.AddOnDeath(Die);
            buildingManager = ServiceLocator.Instance.GetService<BuildingManager>();
            townCenter = buildingManager.GiveBuilding();
            if (townCenter == null)
            {
                Debug.LogError($"{name}: Found no {nameof(townCenter)}!! :(");
                return;
            }

            destination = townCenter._transform.position;
            destination.y = transform.position.y;
            agent.SetDestination(destination);
            StartCoroutine(AlertSpawn());
        }

        private IEnumerator AlertSpawn()
        {
            //Waiting one frame because event subscribers could run their onEnable after us.
            yield return null;
            OnSpawn();
        }

        private void Update()
        {
            if (!townCenter._isAlive)
            {
                townCenter = buildingManager.GiveBuilding();
                destination = townCenter._transform.position;
                destination.y = transform.position.y;
                agent.SetDestination(destination);
            }
            if (agent.hasPath
                && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                Debug.Log($"{name}: I'll die for my people!");
                healthPoints.Kill();
            }
        }

        private void Die()
        {
            healthPoints.RemoveOnDeath(Die);
            if(townCenter.HealthPoints != null) 
            { 
                townCenter.HealthPoints.TakeDamage(20);
            }
            OnDeath();
            Destroy(gameObject);
        }
    

    
    }
}

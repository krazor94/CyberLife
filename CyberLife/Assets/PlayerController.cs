using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    public int wp;

    public int
        hygene,
        hunger,
        bladder,
        energy;

    public float
       hygeneTimer = 2.23f, hygeneTimerReset,
       hungerTimer = 3.65f, hungerTimerReset,
       bladderTimer = 6.45f, bladderTimerReset,
       energyTimer = 4.85f, energyTimerReset;

    public enum State
    {
        idle,
        cooking,
        eating,
        watchTv,
        dancing,
        sleeping,
        shower,
        toilet,
        working
    }

    public State state;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[wp].position;

        hygene = Random.Range(10, 100);
        hunger = Random.Range(10, 100);
        bladder = Random.Range(10, 100);
        energy = Random.Range(10, 100);

        hygeneTimerReset = hygeneTimer;
        hungerTimerReset = hungerTimer;
        bladderTimerReset = bladderTimer;
        energyTimerReset = energyTimer;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[wp].position) < GetComponent<NavMeshAgent>().stoppingDistance)
        {
            //wp++;
            //if (wp >= waypoints.Length)
            //wp = 0;
            //print(wp);
            //agent.destination = waypoints[wp].position;
        }

        ReduceStats();
        CheckState();
        States();
    }

    private void States()
    {
        if (state == State.cooking)
        {
            agent.SetDestination(waypoints[0].position);

            if (Vector3.Distance(transform.position, waypoints[0].position) < 0.25f)
            {
                //goto next waypoint
            }

            //set destination to fridge, then to oven, then to table to eat

            //set destination to fridge, then to oven, then to table to eatagent.SetDestination(waypoints[].position);
        }
    }

    private void CheckState()
    {
        if (hunger < 25)
        {
            state = State.cooking;
            print("go eat");
        }
        else if (bladder < 15)
        {
            state = State.toilet;
            print("go toilet");
        }
        else if (energy < 30)
        {
            state = State.sleeping;
            print("go to bed");
        }
        else if (hygene < 35)
        {
            state = State.shower;
            print("go shower");
        }
        else
        {
            state = State.watchTv;
            print("Entertainment");
        }
    }

    private void ReduceStats()
    {
        if (Time.time > hygeneTimer)
        {
            hygeneTimer += Time.deltaTime + hygeneTimerReset;
            if (hygene > 0)
                hygene--;
        }
        if (Time.time > hungerTimer)
        {
            hungerTimer += Time.deltaTime + hungerTimerReset;
            if (hunger > 0)
                hunger--;
        }
        if (Time.time > bladderTimer)
        {
            bladderTimer += Time.deltaTime + bladderTimerReset;
            if (bladder > 0)
                bladder--;
        }
        if (Time.time > energyTimer)
        {
            energyTimer += Time.deltaTime + energyTimerReset;
            if (energy > 0)
                energy--;
        }
    }
}
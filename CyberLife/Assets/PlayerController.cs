using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    public int wp;
    public float dist = 0.25f;

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
        hunger,
        entertainment,
        sleeping,
        shower,
        toilet,
        working
    }

    public State state;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = waypoints[wp].position;
        StartCoroutine(WaitForSeconds(5f, 0));
        agent.stoppingDistance = dist;

        hygene = Random.Range(10, 100);
        hunger = Random.Range(10, 100);
        bladder = Random.Range(10, 100);
        energy = Random.Range(10, 100);

        hygeneTimerReset = hygeneTimer;
        hungerTimerReset = hungerTimer;
        bladderTimerReset = bladderTimer;
        energyTimerReset = energyTimer;
    }

    public float timer, nextTime;

    private void Update()
    {
        timer -= 1 * Time.deltaTime;
        if (timer < 0)
            timer = 0;

        ReduceStats();
        States();
    }

    private void States()
    {
        #region hunger

        if (state == State.hunger)
        {
            //cycle through the waypoints to go to eating
            if (Vector3.Distance(transform.position, waypoints[0].position) < dist)
            {
                print("Walk to Fridge");
                StartCoroutine(WaitForSeconds(5f, 1));
            }
            else if (Vector3.Distance(transform.position, waypoints[1].position) < dist)
            {
                print("Walk to Oven");
                StartCoroutine(WaitForSeconds(5f, 2));
            }
            else if (Vector3.Distance(transform.position, waypoints[2].position) < dist)
            {
                print("Walk to Table");
                if (hunger < 70)
                {
                    // hunger += (int)Time.deltaTime * 10;
                    hunger = Random.Range(70, 100);
                }
                //StartCoroutine(WaitForSeconds(5f, 3));
            }
            //else if (Vector3.Distance(transform.position, waypoints[3].position) < dist)
            //{
            //    print("now what do we do ?");
            //    CheckState();
            //}
            else
                CheckState();
            agent.SetDestination(waypoints[wp].position);
        }

        #endregion hunger

        #region entertainment

        else if (state == State.entertainment)
        {
            if (Vector3.Distance(transform.position, waypoints[3].position) < dist)
            {
                CheckState();
            }

            agent.SetDestination(waypoints[wp].position);
        }

        #endregion entertainment

        #region Sleeping

        else if (state == State.sleeping)
        {
            if (Vector3.Distance(transform.position, waypoints[5].position) < dist)
            {
                if (energy < 70)
                {
                    energy = Random.Range(70, 100);
                }
                CheckState();
            }

            agent.SetDestination(waypoints[wp].position);
        }

        #endregion Sleeping

        #region Shower

        else if (state == State.shower)
        {
            if (Vector3.Distance(transform.position, waypoints[4].position) < dist)
            {
                if (hygene < 70)
                {
                    hygene = Random.Range(70, 100);
                }
                CheckState();
            }

            agent.SetDestination(waypoints[wp].position);
        }

        #endregion Shower

        #region Toilet

        else if (state == State.toilet)
        {
            if (Vector3.Distance(transform.position, waypoints[4].position) < dist)
            {
                if (bladder < 70)
                {
                    bladder = Random.Range(70, 100);
                }
                CheckState();
            }

            agent.SetDestination(waypoints[wp].position);
        }

        #endregion Toilet

        #region Working

        else if (state == State.toilet)
        {
            if (Vector3.Distance(transform.position, waypoints[6].position) < dist)
            {
                CheckState();
            }

            agent.SetDestination(waypoints[wp].position);
        }
    }

    #endregion Working

    private void CheckState()
    {
        if (hunger < 25)
        {
            print("Hunger");
            state = State.hunger;
            //wp = 0;
            StartCoroutine(WaitForSeconds(5, 0));
        }
        else if (bladder < 30)
        {
            print("Bladder");
            state = State.toilet;
            //wp = 4;
            StartCoroutine(WaitForSeconds(5, 4));
        }
        else if (energy < 30)
        {
            print("Energy");
            state = State.sleeping;
            //wp = 5;
            StartCoroutine(WaitForSeconds(30, 5));
        }
        else if (hygene < 35)
        {
            print("Hygene");
            state = State.shower;
            //wp = 4;
            StartCoroutine(WaitForSeconds(8, 4));
        }
        else
        {
            print("entertainment");
            state = State.entertainment;
            //wp = 3;
            StartCoroutine(WaitForSeconds(5, 3));
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

    private IEnumerator WaitForSeconds(float time, int waypoint)
    {
        print(Time.deltaTime);
        yield return new WaitForSeconds(time);
        print(Time.deltaTime);
        wp = waypoint;
    }
}
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject dayNight, gm;
    private NavMeshAgent agent;
    public Transform[] waypoints;
    public int wp;
    public float dist = 0.25f;
    public bool atWork;
    public bool sit, idle;

    public int
        hygiene,
        hunger,
        bladder,
        energy;

    public float
       hygieneTimer = 2.23f, hygieneTimerReset,
       hungerTimer = 3.65f, hungerTimerReset,
       bladderTimer = 6.45f, bladderTimerReset,
       energyTimer = 4.85f, energyTimerReset;

    public float timer;
    public bool countDown;
    public State state;

    public bool stateFinished;

    #region State Machines

    public enum State
    {
        hunger,
        entertainment,
        sleeping,
        shower,
        toilet,
        working
    }

    #endregion State Machines

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = waypoints[wp].position;
        //StartCoroutine(WaitForSeconds(5f, 0));

        agent.stoppingDistance = dist;

        hygiene = Random.Range(10, 100);
        hunger = Random.Range(10, 100);
        bladder = Random.Range(10, 100);
        energy = Random.Range(10, 100);

        hygieneTimerReset = hygieneTimer;
        hungerTimerReset = hungerTimer;
        bladderTimerReset = bladderTimer;
        energyTimerReset = energyTimer;

        CheckState();
    }

    private void Update()
    {
        if (DialogSystem.pause)
            return;

        if (countDown)
        {
            timer -= 1 * Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                countDown = false;
                stateFinished = true;
                print("Task Complete: " + stateFinished);
            }
        }

        ReduceStats();
        States();

        agent.SetDestination(waypoints[wp].position);
        if (GetComponent<Animator>().GetBool("Sit") == true && agent.velocity.magnitude > 0.4f)
        {
            GetComponent<Animator>().SetBool("Sit", false);
        }
        GetComponent<Animator>().SetFloat("Momentum", agent.velocity.magnitude);

        if (state != State.hunger)
            if (GetComponent<Animator>().GetInteger("Hunger") != 0)
                GetComponent<Animator>().SetInteger("Hunger", 0);
    }

    private void States()
    {
        #region Working

        if (dayNight.GetComponent<DayNightCycle>().hr > 8 && dayNight.GetComponent<DayNightCycle>().hr < 17)
        {
            if (!atWork)
            {
                atWork = true;
                timer = 0;
                state = State.working;
            }
            if (wp != 6)
            {
                SetTask(2, 6);
            }
            //StartCoroutine(WaitForSeconds(10f, 6));
        }
        else {
            if (atWork)
            {
                atWork = false;
                DialogSystem.pause = true;
                gm.GetComponent<DialogSystem>().runOnce = true;
                CheckState();
                agent.Stop();
                return;
            }

            #endregion Working

            #region hunger

            if (state == State.hunger)
            {
                //cycle through the waypoints to go to eating
                if (Vector3.Distance(transform.position, waypoints[0].position) < dist)
                {
                    print("Walk to Fridge");
                    countDown = true;
                    transform.rotation = waypoints[0].rotation;
                    if (GetComponent<Animator>().GetInteger("Hunger") == 0)
                        GetComponent<Animator>().SetInteger("Hunger", 1);
                    // StartCoroutine(WaitForSeconds(5f, 1));
                    SetTask(12, 1);
                }
                else if (Vector3.Distance(transform.position, waypoints[1].position) < dist)
                {
                    print("Walk to Oven");
                    transform.rotation = waypoints[1].rotation;
                    if (GetComponent<Animator>().GetInteger("Hunger") == 1)
                        GetComponent<Animator>().SetInteger("Hunger", 2);
                    // StartCoroutine(WaitForSeconds(5f, 2));
                    countDown = true;
                    SetTask(15, 2);
                }
                else if (Vector3.Distance(transform.position, waypoints[2].position) < dist)
                {
                    print("Walk to Table");
                    if (hunger < 70)
                    {
                        if (GetComponent<Animator>().GetBool("Sit") == false)
                        {
                            GetComponent<Animator>().SetBool("Sit", true);
                            transform.rotation = waypoints[2].rotation;
                        }

                        if (GetComponent<Animator>().GetInteger("Hunger") == 2 && GetComponent<Animator>().GetBool("Sit") == true)
                            GetComponent<Animator>().SetInteger("Hunger", 1);
                        // hunger += (int)Time.deltaTime * 10;
                        hunger = Random.Range(70, 100);
                    }

                    CheckState();
                    //StartCoroutine(WaitForSeconds(5f, 3));
                }
            }

            #endregion hunger

            #region entertainment

            else if (state == State.entertainment)
            {
                if (Vector3.Distance(transform.position, waypoints[3].position) < dist)
                {
                    countDown = true;
                    transform.rotation = waypoints[3].rotation;
                    CheckState();
                    if (GetComponent<Animator>().GetBool("Sit") == false)
                        GetComponent<Animator>().SetBool("Sit", true);
                }
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
                    countDown = true;
                    CheckState();
                }
            }

            #endregion Sleeping

            #region Shower

            else if (state == State.shower)
            {
                if (Vector3.Distance(transform.position, waypoints[4].position) < dist)
                {
                    if (hygiene < 70)
                    {
                        hygiene = Random.Range(70, 100);
                    }
                    countDown = true;
                    CheckState();
                }
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
                    countDown = true;
                    CheckState();
                }
            }

            #endregion Toilet
        }

        #region Working

        if (state == State.working)
        {
            if (Vector3.Distance(transform.position, waypoints[6].position) < dist)
            {
                countDown = true;
                CheckState();
            }
            print("Working!");
        }
    }

    #endregion Working

    private void CheckState()
    {
        if (hunger < 25 && stateFinished)
        {
            print("Hunger");
            state = State.hunger;
            //wp = 0;
            if (!atWork)
                SetTask(7.5f, 0);
            //StartCoroutine(WaitForSeconds(5, 0));
        }
        else if (bladder < 30 && stateFinished)
        {
            print("Bladder");
            state = State.toilet;
            //wp = 4;
            if (!atWork)
                SetTask(8, 4);
            //StartCoroutine(WaitForSeconds(5, 4));
        }
        else if (energy < 30 && stateFinished)
        {
            print("Energy");
            state = State.sleeping;
            //wp = 5;
            if (!atWork && stateFinished)
                SetTask(20, 5);
            // StartCoroutine(WaitForSeconds(30, 5));
        }
        else if (hygiene < 35 && stateFinished)
        {
            print("Hygene");
            state = State.shower;
            //wp = 4;
            if (!atWork)
                SetTask(13, 4);
            //StartCoroutine(WaitForSeconds(8, 4));
        }
        else
        {
            if (!stateFinished)
                return;
            print("entertainment");
            state = State.entertainment;
            //wp = 3;
            if (!atWork)
                SetTask(15, 3);
            //StartCoroutine(WaitForSeconds(5, 3));
        }
    }

    private void ReduceStats()
    {
        if (Time.time > hygieneTimer)
        {
            hygieneTimer += Time.deltaTime + hygieneTimerReset;
            if (hygiene > 0)
                hygiene--;
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

    private void SetTask(float time, int waypoint)
    {
        // if we have initiated a state and have not done it yet return
        if (!stateFinished)
            return;

        timer = time;
        wp = waypoint;
        print("Setting Task: " + wp);
        stateFinished = false;
    }

    private IEnumerator WaitForSeconds(float time, int waypoint)
    {
        yield return new WaitForSeconds(time);
        wp = waypoint;
    }
}
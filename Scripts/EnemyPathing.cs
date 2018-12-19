using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    int waypointIndex = 0;

    private List<Transform> waypoints;
    public WaveConfig waveConfig;
	

    // Use this for initialization
	void Start () {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        waypointIndex++;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        //Debug.Log(waypointIndex);
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {

        if (waypointIndex <= waypoints.Count - 1)
        {
            //Debug.Log(transform.position);
            var target = waypoints[waypointIndex].transform.position;
            //Debug.Log(target);
            var movementThisFrame = waveConfig.GetmoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target,
                                                        movementThisFrame);
            if (transform.position == target)
            {
                //Debug.Log("visits2");
                waypointIndex++;
            }
        }
        else
        {
            //Debug.Log("destroyed");
            Destroy(gameObject);
        }
    }
}

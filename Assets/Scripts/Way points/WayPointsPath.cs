using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class WayPointsPath : MonoBehaviour
{
    [SerializeField]
    List<WayPoint> wayPoints;
    [SerializeField]
    GameObject wayPointsAgent;
    [SerializeField]
    int agentSpeed;
    [SerializeField]
    GameEvent arrivedEvent;
    [SerializeField]
    GameEvent winEvent;
    [SerializeField]
    GameEvent moveToNextPointEvent;
    bool canAgentMove;
    [SerializeField] BoolValue canPlay;
    [SerializeField] IntVariable sustainedValue;
    [SerializeField] StringVariable typeOfAttention;
    [SerializeField] IntVariable index;
    private void Awake()
    {
        index.Value = 0;
    }
    void Start()
    {
        wayPoints = this.GetComponentsInChildren<WayPoint>().ToList();
        DetermineStopPoints();
        //wayPoints.RemoveAt(0);
        canAgentMove= true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&canPlay.Value)
            MoveAgentToNextPoint();
    }
    //when pressing
    public void MoveAgentToNextPoint()
    {
        if (index.Value < wayPoints.Count)
        {
            moveToNextPointEvent.Raise();
            canAgentMove = true;
            // Debug.Log("move to next point");
        }
        else if (index.Value == wayPoints.Count)
        {
            canAgentMove = false;
            moveToNextPointEvent.Raise();
            // Debug.Log("move to next point");
        }
    }
    public void AgentArrivedAtNoneStopPoint()
    {
        if (index.Value < wayPoints.Count - 1) index.Value++;
        else canAgentMove = false;
    }
    public void AgentArrivedAtStopPoint()
    {
        canAgentMove = false;
        if (index.Value < wayPoints.Count ) { index.Value++; }
        else
        {
            // winEvent.Raise();
        }
        arrivedEvent.Raise();
        Debug.Log("agent arrived");
    }

    private void FixedUpdate()
    {
        if (canAgentMove&& canPlay.Value)
        {
            wayPointsAgent.transform.position += GetAgentDirection() * agentSpeed * Time.fixedDeltaTime;
            //wayPointsAgent.transform.LookAt(GetAgentDirection());
            wayPointsAgent.transform.rotation = Quaternion.Slerp(wayPointsAgent.transform.rotation, Quaternion.LookRotation(GetAgentDirection()), 0.55f);
        }
    }
    private Vector3 GetAgentDirection()
    {
        // Debug.Log("get agent Direction");
        Vector3 dir;
        Vector3 dirNormalized;
        dir = wayPoints[index.Value].myPosition - wayPointsAgent.transform.position;
        dirNormalized = dir.normalized;
        return dirNormalized;
    }
    #region Determine Stop Points
    void DetermineStopPoints()
    {
        if (typeOfAttention.Value == "sustained")
        {
            if (sustainedValue.Value == 20) MakeStopPoints(5);
            else if (sustainedValue.Value == 40) MakeStopPoints(10);
            else if (sustainedValue.Value == 60) MakeStopPoints(15);
        }
        else MakeStopPoints(10);
    }
    void MakeStopPoints(int No)
    {
        if (No == 5) for (int i = 0; i < 15; i++)
            {
                if (i % 3 == 0) wayPoints[i].isStopPoint = true;
                else wayPoints[i].isStopPoint = false;
            }
        else if (No == 10) for (int i = 0; i < 15; i++)
            {
                if (i % 3 == 2) wayPoints[i].isStopPoint = false;
                else wayPoints[i].isStopPoint = true;
            }
        else for (int i = 0; i < 15; i++)
            {
                wayPoints[i].isStopPoint = true;
            }
    }
    #endregion
}
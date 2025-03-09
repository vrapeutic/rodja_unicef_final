using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestractorWayPoint : MonoBehaviour
{
    [SerializeField]
    List<MeshRenderer> wayPoints;
    [SerializeField]
    GameObject level2Destractor;
    [SerializeField]
    int destractorSpeed;
    int NextIndex;
    int currentIndex;
    bool CanAgentMove;
    float distance;
    bool canStartCheckDistance = true;

    void Start()
    {
        wayPoints = this.GetComponentsInChildren<MeshRenderer>().ToList();
        wayPoints.RemoveAt(0);
        CanAgentMove = false;
        MoveAgentToNextPoint();
    }


    private void MoveAgentToNextPoint()
    {
        CanAgentMove = true;
        // Debug.Log("move to next point");
    }

    private void GetNextPointIndex()
    {
        int nextIndex = Random.Range(0, wayPoints.Count);
        if (nextIndex == currentIndex) GetNextPointIndex();
        currentIndex = nextIndex;
    }


    IEnumerator DestractorArrivedAtPoint()
    {
        GetNextPointIndex();
        //  Debug.Log("destractor arrived");
        CanAgentMove = false;
        yield return new WaitForSeconds(2f);
        MoveAgentToNextPoint();
        yield return new WaitForSeconds(3f);
        canStartCheckDistance = true;
    }


    private void Update()
    {
        Vector3 wayPointPosition = new Vector3(wayPoints[currentIndex].transform.position.x, 2, wayPoints[currentIndex].transform.position.z);
        distance = Vector3.Distance(level2Destractor.transform.position, wayPointPosition);
        if (distance < 0.1) StartCoroutine(DestractorArrivedAtPoint());

    }

    private void FixedUpdate()
    {
        if (CanAgentMove)
        {
            level2Destractor.transform.position += GetAgentDirection() * destractorSpeed * Time.fixedDeltaTime;
            //level2Destractor.transform.LookAt(GetAgentDirection());
            level2Destractor.transform.rotation = Quaternion.Slerp(level2Destractor.transform.rotation, Quaternion.LookRotation(GetAgentDirection()), 0.55f);

        }
    }

    private Vector3 GetAgentDirection()
    {
        // Debug.Log("get agent Direction");
        Vector3 dir;
        Vector3 dirNormalized;
        Vector3 wayPointNewPosition;
        wayPointNewPosition = new Vector3(wayPoints[currentIndex].transform.position.x, 2, wayPoints[currentIndex].transform.position.z);
        dir = wayPointNewPosition - level2Destractor.transform.position;
        dirNormalized = dir.normalized;
        return dirNormalized;
    }

}


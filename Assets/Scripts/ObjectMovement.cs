using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Vector3 myPosition;
    private float xPosition;
    private float yPosition;
    private float zPosition;
    private Vector3 startPosition;
    private Vector3 newPosition;
    private Vector3 direction;
    private Vector3 directionNormalized;

    float distance;
    public float movementSpeed = 0.25f;
    bool canMove = true;
    private void Start()
    {
        myPosition = this.transform.position;

        if (FindObjectOfType<MenuManger>().menu.level != 3)
            this.enabled = false;
        startPosition = this.gameObject.transform.position;

        GetRandomPosition();
        //StartCoroutine(GetNewPoint());
    }

    private void Update()
    {
        distance = Vector3.Distance(this.gameObject.transform.position, newPosition);
        if (distance <= 0.1f)
        {
            GetRandomPosition();
        }

    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            this.gameObject.transform.position += directionNormalized * movementSpeed * Time.fixedDeltaTime;
        }
    }
    public void GetRandomPosition()
    {
        xPosition = Random.Range(-1.5f, 1.5f);
        yPosition = Random.Range(0.5f, 2f);
        zPosition = Random.Range(-1.5f, 1.5f);
        newPosition.Set(startPosition.x + xPosition, startPosition.y + yPosition, startPosition.z + zPosition);

        direction = newPosition - this.gameObject.transform.position;
        directionNormalized = direction.normalized;
    }

    public void Stop()
    {

        canMove = false;
        this.transform.position = myPosition;
    }
    //IEnumerator GetNewPoint()
    //{
    //    if (canMove)
    //    {
    //        Debug.Log(" start move");
    //        this.gameObject.transform.position += GetRandomPosition() * movementSpeed;
    //        canMove = false;
    //    }
    //    yield return new WaitForSeconds(2);
    //    canMove = true;
    //    GetRandomPosition();
    //    Debug.Log("wait");

    //}

}

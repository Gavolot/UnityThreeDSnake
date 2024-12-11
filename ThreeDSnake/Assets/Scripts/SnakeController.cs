using UnityEngine;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 90f;
    public GameObject bodySegmentPrefab;

    public float segmentDistance = 0.5f;
    private List<Transform> bodySegments = new List<Transform>();


    private GroundController groundController;

    void Start()
    {
        bodySegments.Add(transform);

        groundController = FindAnyObjectByType<GroundController>();
    }

    void Update()
    {
        MoveSnake();
    }

    void MoveSnake()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);


        UpdateBodySegments();
    }

    void UpdateBodySegments()
    {
        for (int i = 1; i < bodySegments.Count; i++)
        {
            Transform currentSegment = bodySegments[i];
            Transform previousSegment = bodySegments[i - 1];
            Vector3 direction = previousSegment.position - currentSegment.position;
            float distance = direction.magnitude;

            if (distance > segmentDistance)
            {
                currentSegment.position = Vector3.MoveTowards(
                    currentSegment.position,
                    previousSegment.position - direction.normalized * segmentDistance,
                    speed * Time.deltaTime
                );

                currentSegment.rotation = Quaternion.Slerp(
                    currentSegment.rotation,
                    previousSegment.rotation,
                    Time.deltaTime * 10f
                );
            }
        }
    }

    public void AddSegment()
    {
        GameObject newSegment = Instantiate(bodySegmentPrefab);
        Transform lastSegment = bodySegments[bodySegments.Count - 1];

        newSegment.transform.position = lastSegment.position - lastSegment.forward * segmentDistance;

        bodySegments.Add(newSegment.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            AddSegment();
            speed += 0.1f;
            if (groundController != null)
            {
                groundController.MoveFoodToNewPosition(); // Перемещаем еду
            }
        }
    }
}



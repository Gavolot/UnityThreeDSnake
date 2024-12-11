using UnityEngine;

public class GroundController : MonoBehaviour
{
    public GameObject food;
    public GameObject snakeHead;
    public Vector2 groundSize = new Vector2(10, 10);

    void Update()
    {
        CheckSnakeOnGround();
    }

    void CheckSnakeOnGround()
    {
        if (snakeHead != null)
        {
            Vector3 headPosition = snakeHead.transform.position;
            Vector3 groundPosition = transform.position;
            
            float halfWidth = groundSize.x / 2;
            float halfHeight = groundSize.y / 2;

            float minX = groundPosition.x - halfWidth;
            float maxX = groundPosition.x + halfWidth;
            float minZ = groundPosition.z - halfHeight;
            float maxZ = groundPosition.z + halfHeight;

            if (headPosition.x < minX || headPosition.x > maxX ||
                headPosition.z < minZ || headPosition.z > maxZ)
            {
                Debug.LogWarning("Голова змеи вышла за пределы игрового поля!");
            }
        }
    }


    public void MoveFoodToNewPosition()
    {
        float halfWidth = groundSize.x / 2;
        float halfHeight = groundSize.y / 2;

        float newX = Random.Range(-halfWidth, halfWidth) + transform.position.x;
        float newZ = Random.Range(-halfHeight, halfHeight) + transform.position.z;

        newX = Mathf.Clamp(newX, transform.position.x - halfWidth + 0.5f, transform.position.x + halfWidth - 0.5f);
        newZ = Mathf.Clamp(newZ, transform.position.z - halfHeight + 0.5f, transform.position.z + halfHeight - 0.5f);

        food.transform.position = new Vector3(newX, food.transform.position.y, newZ);
    }


}

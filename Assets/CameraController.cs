using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset = 0.8f;
    public float offsetSmooth = 0.1f;
    private Vector3 playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        if (player.transform.localScale.x > 0)
        {
            playerPos = new Vector3(playerPos.x + offset, playerPos.y, transform.position.z);
        }
        else
        {
            playerPos = new Vector3(playerPos.x - offset, playerPos.y, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPos, offsetSmooth * Time.deltaTime);
    }
    
}

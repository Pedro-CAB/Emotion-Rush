using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;

    private Vector3 posOffset; // Offset to keep the camera at a distance

    void Start(){
        posOffset = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + posOffset;
    }
}

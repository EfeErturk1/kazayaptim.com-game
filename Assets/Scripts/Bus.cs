using UnityEngine;

public class Bus : MonoBehaviour
{
    public int speed = 10;
    public bool active = true;
    
    public void Init(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(true);
    }
    
    void Update()
    {
        if (transform.position.y < -10)
        {
            gameObject.SetActive(false);
            active = false;
        }
        else
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        }
    }
}

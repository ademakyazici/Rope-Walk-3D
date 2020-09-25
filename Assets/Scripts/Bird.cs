
using UnityEngine;

public class Bird : MonoBehaviour
{
    private float speed;
   
    void Update()
    {
        speed = GameManager.Instance.worldSpeed;
        transform.Translate(-transform.forward*speed*Time.deltaTime);

        if (transform.localPosition.z < -0.2f)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {        
        transform.localPosition = new Vector3(Random.Range(-0.9f,0.9f), Random.Range(-0.7f,0.8f), 5);
        while(Mathf.Abs(transform.localPosition.x) < 0.01f)
        {
            transform.localPosition = new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(-0.5f, 0.8f), 5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Stick"))
        {
            if(other.transform.position.x-transform.position.x>0)
            {
                other.GetComponentInParent<PlayerController>().Fall(true);
            } else
            {
                other.GetComponentInParent<PlayerController>().Fall(false);
            }
        }
    }

}

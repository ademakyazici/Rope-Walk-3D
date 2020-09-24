using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform stick;
    [SerializeField] private Transform arms;
    
    private Vector3 playerWorldPos;

    private Vector3 prevPos=Vector3.zero;
    [SerializeField] private float stickSpeed;
    [SerializeField] private bool inverseStick = false;

    private Rigidbody physics;
    private bool haveFallen = false;

    

    [SerializeField]private BalanceBar balanceBar;

    private void Awake()
    {
        playerWorldPos = Camera.main.WorldToScreenPoint(transform.position);
        physics = GetComponent<Rigidbody>();
    }
      
    void Update()
    {
        GetComponent<Animator>().speed = GameManager.Instance.worldSpeed/4;
        PlayerInput();
        Unbalance();
        CheckBalance();
    }

    void PlayerInput()
    {
        
        if (Input.GetMouseButton(0))
        {
            float rotSpeed;
            
            if (inverseStick)
                rotSpeed = -stickSpeed * Time.deltaTime;
            else
                rotSpeed = stickSpeed * Time.deltaTime;
                

            if (Input.mousePosition.x>playerWorldPos.x)
            {
                stick.transform.Rotate(new Vector3(0, 0,rotSpeed));
                arms.transform.Rotate(new Vector3(0, 0, rotSpeed));
                /*
                Vector3 current = Input.mousePosition;
                if (current.y > prevPos.y)
                {
                    stick.transform.Rotate(new Vector3(0,0,rotSpeed));
                    arms.transform.Rotate(new Vector3(0, 0, rotSpeed));
                }
                else if(current.y < prevPos.y)
                {
                    stick.transform.Rotate(new Vector3(0, 0, -rotSpeed));
                    arms.transform.Rotate(new Vector3(0, 0, -rotSpeed));
                }
                prevPos = current;
                */
            } else
            {
                stick.transform.Rotate(new Vector3(0, 0, -rotSpeed));
                arms.transform.Rotate(new Vector3(0, 0, -rotSpeed));
                /*
                Vector3 current = Input.mousePosition;
                if (current.y > prevPos.y)
                {
                    stick.transform.Rotate(new Vector3(0, 0, -rotSpeed));
                    arms.transform.Rotate(new Vector3(0, 0, -rotSpeed));
                }
                else if (current.y < prevPos.y)
                {
                    stick.transform.Rotate(new Vector3(0, 0, rotSpeed));
                    arms.transform.Rotate(new Vector3(0, 0, rotSpeed));
                }
                prevPos = current;
                */

            }
        }

    }

    void Unbalance()
    {
        float stickRotation = EulerToRotation(stick.transform.localRotation.eulerAngles.z);
        float unbalanceSpeed = (stickRotation / 100)*stickSpeed;

        stick.transform.Rotate(new Vector3(0, 0, unbalanceSpeed * Time.deltaTime));
    }


    void CheckBalance()
    {
        if(!haveFallen)
        {
            float stickRotation = EulerToRotation(stick.transform.localRotation.eulerAngles.z);
            float charRotation = stickRotation / 2;
            transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, charRotation);

            if (charRotation > 20)
            {
                Fall(true);
            }
            else if(charRotation < -20 )
            {
                Fall(false);
            }
          
        }
              
        /*
        if(stickRotation >15 || stickRotation < -15)
        {
            balanceBar.gameObject.SetActive(true);
            balanceBar.CalculateDecreaseSpeed(Mathf.Abs(stickRotation));
            
        }
        else
        {
            balanceBar.gameObject.SetActive(false);
        }
        */
    }

    private float EulerToRotation(float value)
    {
        if (value > 180)
        {
            return value - 360f;
        }
        else if (value < -180)
        {
            return value + 360f;
        }
        else
        {
            return value;
        }
    }

    public void Fall(bool toRight)
    {
        if(!haveFallen)
        {
            if (toRight)
            {
                physics.useGravity = true;               
                transform.DORotateQuaternion(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, EulerToRotation(45)), 0.4f);
                haveFallen = true;
            }
            else
            {
                physics.useGravity = true;
                transform.DORotateQuaternion(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, EulerToRotation((-45))), 0.4f);
                haveFallen = true;
            }
        }
       
    }


}

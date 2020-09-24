using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBar : MonoBehaviour
{
    private Image image;   

    private float totalTime = 60;
    private float decreaseSpeed = 1;
    [SerializeField]private float minSpeed = 5;
    [SerializeField]private float maxSpeed = 45;

    
    void Start()
    {
        image = GetComponent<Image>();        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime = totalTime - Time.deltaTime;
        image.fillAmount -= (1 / totalTime) * Time.deltaTime*decreaseSpeed;
    }

    private void OnDisable()
    {
        image.fillAmount = 1;
    }

    public void CalculateDecreaseSpeed(float deltaAngle)
    {
        float ratio = deltaAngle / 90;
        decreaseSpeed = ratio * maxSpeed;
        decreaseSpeed = Mathf.Clamp(decreaseSpeed, minSpeed, maxSpeed);
    }
}

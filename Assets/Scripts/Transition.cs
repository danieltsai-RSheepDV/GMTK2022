using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] GameObject die;
    [SerializeField] GameObject mainCamera;
    [SerializeField] private Vector3 offset;
    [SerializeField] Animator title;
    [SerializeField] Animator button;
    bool inTransition = false;

    Vector3 step;  // to follow a non-direct path from camera to die

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inTransition)
        {
            title.Play("Fade Out");
            button.Play("Fade Out");
            Vector3 lookVector = die.transform.position - mainCamera.transform.position;
            mainCamera.transform.forward = Vector3.Lerp(mainCamera.transform.forward, lookVector, 0.8f * Time.deltaTime);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, die.transform.position + offset, 0.8f * Time.deltaTime);
            if (die.transform.position.y < 20)
            {
                inTransition = false;
                mainCamera.GetComponent<CameraFollow>().enabled = true;
                button.gameObject.SetActive(false);
            }
        }
    }

    public void StartTransition()
    {
        inTransition = true;
    }
}

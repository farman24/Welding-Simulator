using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class slugs : MonoBehaviour
{
    public XRGrabInteractable interactable;
    public Transform gunPoint;
    public GameObject[] Slugs;
    bool enabled = true;
    bool inColliderTrigger = false;
    public ParticleSystem spark;
    public ParticleSystem blueFlame;
    public GameObject blueFlare;
    public InputActionProperty pinchAction;
    float triggerValue = 0;
    bool o;
    void Start()
    {
        var interactable = gameObject.GetComponent<XRGrabInteractable>();
        spark.Stop();
        blueFlame.Stop();
        blueFlare.SetActive(false);
    }

    void Update()
    {
        triggerValue =pinchAction.action.ReadValue<float>();
        if (triggerValue > 0 && inColliderTrigger)
        {
            StartCoroutine(ShootSlugPerSecond());
        }
        if(triggerValue > 0 && inColliderTrigger &&o)
        {
           o = false;
            spark.Play();
            blueFlame.Play();
            blueFlare.SetActive(true);
        }
        else if(triggerValue == 0 && !o&&!inColliderTrigger)
        {
            o=true;
            spark.Stop();
            blueFlame.Stop();
            blueFlare.SetActive(false);
        }        
    }

    IEnumerator ShootSlugPerSecond()
    {
        while (triggerValue > 0 && enabled &&inColliderTrigger)
        {
            
            enabled = false;
            int index = Random.Range(0, Slugs.Length);
            Instantiate(Slugs[index], transform.position,transform.rotation);
            yield return new WaitForSeconds(0.2f);
            enabled = true;
        }     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Metal"))
        {
            inColliderTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Metal"))
        {
            inColliderTrigger =false;

        }
    }
   

}

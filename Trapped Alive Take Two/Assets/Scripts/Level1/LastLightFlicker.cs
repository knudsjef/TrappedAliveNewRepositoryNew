using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLightFlicker : MonoBehaviour {

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject silhouette;

    Light MyLight;
    float TimeAlive;
    bool visible;
    float visibleTime = 0;
    float onTime;
    float offTime;
    Vector3 viewPos;

    // Use this for initialization
    void Start () {
        MyLight = this.transform.GetChild(0).GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        
        TimeAlive += Random.Range(0, 10);
        if (visibleTime < 2)
        {
            if (Mathf.RoundToInt(TimeAlive) % 5 == 0)
            {
                MyLight.enabled = !MyLight.enabled;

            }
        }
        else
        {
            MyLight.enabled = true;
            onTime += Time.deltaTime;
            if(onTime > 1)
            {
                MyLight.enabled = false;
                silhouette.SetActive(false);
                foreach (GameObject light in player.GetComponent<LightFlicks>().Lights)
                {
                    light.transform.GetChild(3).transform.GetChild(0).GetComponent<Light>().enabled = false;
                }

                offTime += Time.deltaTime;
                if(offTime > 1.5)
                {
                    MyLight.enabled = true;
                }
            }
        }

        if (visible)
        {
            visibleTime += Time.deltaTime;
        }
        
	}

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }

}

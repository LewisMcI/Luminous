using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnVisibleSingular : MonoBehaviour
{
    [SerializeField] Material Mat;

    float distance;
    float range;
    float strength;


    public Light spotLight1;
    public Light spotLight2;
    public GameObject[] playersArray;
    private void Awake()
    {
        StartCoroutine(FindThePlayers(playersArray));
    }

    private void Update()
    {
        if (spotLight1 != null && spotLight2 != null)
        {
            SendToShader(spotLight1);
        }
    }

    private void SendToShader(Light SpotLight)
    {
        distance = Vector3.Distance(SpotLight.transform.position, transform.position);
        strength = Mathf.Clamp(range / distance, 0.0f, 1.0f);

        range = SpotLight.range;
        Mat.SetFloat("_StrengthDistance", strength);
        Mat.SetVector("_LightPosition", SpotLight.transform.position);
        Mat.SetVector("_LightDirection", -SpotLight.transform.forward);
        Mat.SetFloat("_LightAngle", SpotLight.spotAngle);
    }

    private void AssignSpotlights(GameObject[] players)
    {
        playersArray = players;

        spotLight1 = playersArray[0].GetComponentInChildren<Light>();
        spotLight2 = playersArray[1].GetComponentInChildren<Light>();
        Debug.Log("Assigned Spotlights");
    }

    IEnumerator FindThePlayers(GameObject[] players)
    {
        int length = 1;
        for (int i = 0; i < length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            players = GameController.Instance.GetPlayersArray();
            if (players.Length != 2)
            {
                length++;
                Debug.Log("Finding Players");
            }
        }
        Debug.Log("Found Players");
        AssignSpotlights(players);
        yield break;
    }
}
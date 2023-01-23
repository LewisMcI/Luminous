using System.Collections;
using UnityEngine;


public class TurnVisible : MonoBehaviour
{
    [SerializeField] Material Mat;
    public Light spotLight1;
    public Light spotLight2;
    public GameObject[] playersArray;

    float distance;
    float distance2;
    float range;
    float range2;
    float strength;
    float strength2;
    private void Awake()
    {
        StartCoroutine(FindThePlayers(playersArray));
    }

    private void Update()
    {
        if (spotLight1 != null && spotLight2 != null)
        {
            SendToShader(spotLight1, spotLight2);
        }
    }

    private void SendToShader(Light SpotLight, Light SpotLight2)
    {
        range = SpotLight.range;
        distance = Vector3.Distance(SpotLight.transform.position, transform.position);
        strength = Mathf.Clamp(range / distance, 0.0f, 1.0f);
        Mat.SetFloat("_StrengthDistance", strength);
        Mat.SetVector("_LightPosition", SpotLight.transform.position);
        Mat.SetVector("_LightDirection", -SpotLight.transform.forward);
        Mat.SetFloat("_LightAngle", SpotLight.spotAngle);

        range2 = SpotLight2.range;
        distance2 = Vector3.Distance(SpotLight2.transform.position, transform.position);
        strength2 = Mathf.Clamp(range2 / distance2, 0.0f, 1.0f);
        Mat.SetFloat("_StrengthDistance2", strength2);
        Mat.SetVector("_LightPosition2", SpotLight2.transform.position);
        Mat.SetVector("_LightDirection2", -SpotLight2.transform.forward);
        Mat.SetFloat("_LightAngle2", SpotLight2.spotAngle);
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
using System;
using System.Collections;
using UnityEngine;
using static Colour;

public class TurnVisibleMLMObj : MonoBehaviour
{
    [SerializeField] Material Mat;
    public Light spotLight1;
    public Light spotLight2;
    public GameObject[] playersArray;
    public Color miscColour;
    public Light miscLight;

    public ColourState colourStateRequired;
    public Color colourRequired;
    public Colour playerOneCScript;
    public Colour playerTwoCScript;

    float range;
    private void Awake()
    {
        if (miscLight != null)
        {
            SetMiscLight(miscLight);
        }
        StartCoroutine(FindThePlayers(playersArray));
    }

    private void Update()
    {
        try
        {
            if (spotLight1 != null || spotLight2 != null)
            {
                SendToShader(spotLight1, spotLight2);
            }
        }
        catch (Exception e)
        {
            spotLight1 = null;
            spotLight2 = null;
            StartCoroutine(FindThePlayers(playersArray));
        }
    }

    private void SetMiscLight(Light miscLight)
    {
        Debug.Log("Sending MiscLight to Shader");
        range = miscLight.range;
        Mat.SetVector("_LightPosition3", miscLight.transform.position);
        Mat.SetVector("_LightDirection3", -miscLight.transform.forward);
        Mat.SetFloat("_LightAngle3", miscLight.spotAngle);
        Mat.SetFloat("_LightRange3", range);
        Mat.SetFloat("_StrenghScalar3", 10);
        Mat.SetColor("_EmissionColor3", colourRequired);
    }

    private void SendToShader(Light SpotLight, Light SpotLight2)
    {
        range = SpotLight.range;
        if (playerOneCScript.cState != colourStateRequired)
        {
            Mat.SetFloat("_LightAngle", 0.001f);
        }
        else
        {
            Mat.SetVector("_LightPosition", SpotLight.transform.position);
            Mat.SetVector("_LightDirection", -SpotLight.transform.forward);
            Mat.SetFloat("_LightAngle", SpotLight.spotAngle);
            Mat.SetFloat("_LightRange", range);
            Mat.SetFloat("_StrenghScalar", 5);
            Mat.SetColor("_EmissionColor", spotLight1.color);
        }

        if (spotLight2 == null)
        {
            Debug.Log("Cannot Find Other Players Spotlight");
            Mat.SetFloat("_LightAngle2", 0.001f);
        }
        else
        {
            if (playerTwoCScript.cState != colourStateRequired)
            {
                Mat.SetFloat("_LightAngle2", 0.001f);
            }
            else
            {
                Mat.SetVector("_LightPosition2", SpotLight2.transform.position);
                Mat.SetVector("_LightDirection2", -SpotLight2.transform.forward);
                Mat.SetFloat("_LightAngle2", SpotLight2.spotAngle);
                Mat.SetFloat("_LightRange2", range);
                Mat.SetFloat("_StrenghScalar2", 5);
                Mat.SetColor("_EmissionColor2", spotLight2.color);
            }

        }
    }

    private void AssignSpotlights(GameObject[] players)
    {
        playersArray = players;

        spotLight1 = playersArray[0].GetComponentInChildren<Light>();
        playerOneCScript = playersArray[0].GetComponent<Colour>();
        spotLight2 = playersArray[1].GetComponentInChildren<Light>();
        playerTwoCScript = playersArray[1].GetComponent<Colour>();
        Debug.Log("Assigned Both Spotlights");
    }
    private void AssignFirstSpotlight(GameObject player)
    {
        spotLight1 = player.GetComponentInChildren<Light>();
        playerOneCScript = player.GetComponent<Colour>();
        Debug.Log("Assigned One Spotlight");
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
                Debug.Log("Searching for Players");
            }
            if (players.Length == 1)
            {
                AssignFirstSpotlight(players[0]);
            }
        }
        Debug.Log("Found Players");
        AssignSpotlights(players);
        yield break;
    }

}
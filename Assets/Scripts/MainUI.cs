using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    // Start is called before the first frame update

    //Prefabs
    [SerializeField] private GameObject infopanelprefab;
    [SerializeField] private GameObject commandpanelprefab;
    [SerializeField] private GameObject roundnumprefab;
    [SerializeField] private GameObject processinfoprefab;

    private GameObject infopanel;
    private GameObject commandpanel;
    private GameObject roundnum;
    private GameObject processinfo;

    void Start()
    {
        infopanel = Instantiate(infopanelprefab, transform);
        infopanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        infopanel.GetComponent<RectTransform>().offsetMax = new Vector2(-1156.44f ,- 528.62f);
        infopanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { Command(); });

        commandpanel = Instantiate(commandpanelprefab, transform);
        commandpanel.GetComponent<RectTransform>().offsetMin = new Vector2(312.56f, 0);
        commandpanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, -656.16f);
        commandpanel.SetActive(false);

        roundnum = Instantiate(roundnumprefab, transform);
        roundnum.GetComponent<RectTransform>().offsetMin = new Vector2(0, 657.84f);
        roundnum.GetComponent<RectTransform>().offsetMax = new Vector2(-1335.25f, 0);

        processinfo = Instantiate(processinfoprefab, transform);
        processinfo.GetComponent<RectTransform>().offsetMin = new Vector2(298.91f, 657.845f);
        processinfo.GetComponent<RectTransform>().offsetMax = new Vector2(-258.43f, 0);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Command()
    {
        commandpanel.SetActive(!commandpanel.activeSelf);
    }
}

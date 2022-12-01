using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    // Start is called before the first frame update

    //Prefabs
    //[SerializeField] private GameObject infopanelprefab;
    //[SerializeField] private GameObject commandpanelprefab;
    //[SerializeField] private GameObject roundnumprefab;
    //[SerializeField] private GameObject processinfoprefab;

    private GameObject infopanel;
    private GameObject commandpanel;
    private GameObject roundnum;
    private GameObject processinfo;

    void Start()
    {
        //infopanel = Instantiate(infopanelprefab, transform);
        //infopanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        //infopanel.GetComponent<RectTransform>().offsetMax = new Vector2(-1156.44f ,- 528.62f);
        //infopanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { Command(); });

        //commandpanel = Instantiate(commandpanelprefab, transform);
        //commandpanel.GetComponent<RectTransform>().offsetMin = new Vector2(312.56f, 0);
        //commandpanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, -656.16f);
        //commandpanel.SetActive(false);

        //roundnum = Instantiate(roundnumprefab, transform);
        //roundnum.GetComponent<RectTransform>().offsetMin = new Vector2(0, 657.84f);
        //roundnum.GetComponent<RectTransform>().offsetMax = new Vector2(-1335.25f, 0);

        //processinfo = Instantiate(processinfoprefab, transform);
        //processinfo.GetComponent<RectTransform>().offsetMin = new Vector2(298.91f, 657.845f);
        //processinfo.GetComponent<RectTransform>().offsetMax = new Vector2(-258.43f, 0);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Command()
    {
        commandpanel.SetActive(!commandpanel.activeSelf);
    }

    public void setinfo(int hp, int ms, int ar, Sprite img)
    {
        Transform[] myTransforms = infopanel.GetComponentsInChildren<Transform>();
        foreach (var child in myTransforms)
        {
            if (child.name == "HP")
            {
                child.GetComponent<TextMeshProUGUI>().text = hp.ToString();
            }
            else if (child.name == "MS")
            {
                child.GetComponent<TextMeshProUGUI>().text = ms.ToString();
            }
            else if (child.name == "AR")
            {
                child.GetComponent<TextMeshProUGUI>().text = ar.ToString();
            }
            if (child.name == "Image")
            {
                child.GetComponent<Image>().sprite = img;
            }
        }
    }

    public void updateroundnum(int num)
    {
        TextMeshProUGUI text = roundnum.transform.Find("roundnum/Roundnum").GetComponent<TextMeshProUGUI>();
        text.text = "Round " + num.ToString();
    }

    public void updatestage(int num)
    {
        TextMeshProUGUI text = roundnum.transform.Find("process info/Process").GetComponent<TextMeshProUGUI>();
        
        switch (num)
        {
            case 1:
                text.text = "Team red planning phase";
                break;
            case 2:
                text.text = "Team blue planning phase";
                break;
            case 3:
                text.text = "Team red executing phase";
                break;
            case 4:
                text.text = "Team blue executing phase";
                break;
            default:
                text.text = "waitting";
                break;
        }
    }

}

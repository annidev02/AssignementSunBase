using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManagerTO : Singelton<UIManagerTO>
{
    [SerializeField] private Transform scrollPanelParent;
    [SerializeField] private Transform scrollPanel;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject filterLabel;
    [SerializeField] private TMP_Dropdown dropDown;

    public enum Options
    {
        AllClients = 0,
        Managers = 1,
        NonManagers = 2
    }

    public List<GameObject> managers = new List<GameObject>();
    public List<GameObject> nonManagers = new List<GameObject>();

    [Header("POP UP WINDOW")]
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private Button closeButton;

    private GameObject[] mainPanel => new GameObject[] { scrollPanel.gameObject, dropDown.gameObject, filterLabel};

    private void Start()
    {
        dropDown.onValueChanged.AddListener(OnDropDownValueChange);
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }
    public void PopulateTheScrollbar(JsonData data)
    {
        for (int i = 0; i < data.clients.Count; i++)
        {
            GameObject client = Instantiate(buttonPrefab, scrollPanelParent);

            // Adding the clientData and populating it with the relevant data based on my understanding of json 
            ClientData clientData = client.AddComponent<ClientData>();
            clientData.name = data.data.ContainsKey((i + 1).ToString()) ? data.data[(i + 1).ToString()].name : "NULL";
            clientData.label = data.clients[i].label != null ? data.clients[i].label : "NULL";
            clientData.address = data.data.ContainsKey((i + 1).ToString()) ? data.data[(i + 1).ToString()].address : "NULL";
            clientData.isManager = data.clients[i].isManager;
            clientData.points = data.data.ContainsKey((i + 1).ToString()) ? data.data[(i + 1).ToString()].points : 0;
            clientData.id = data.clients[i].id;

            // Assigning the Label and the Points 
            client.transform.GetChild(0).GetComponent<TMP_Text>().text = clientData.label;
            client.transform.GetChild(1).GetComponent<TMP_Text>().text = clientData.points.ToString();

            if (data.clients[i].isManager) managers.Add(client);
            else nonManagers.Add(client);

            // Hoking the event dynamically 
            client.GetComponent<Button>().onClick.AddListener(OnListButtonClick);
        }
    }

    // Filteration 
    public void OnDropDownValueChange(int value)
    {
        if (value == (int) Options.AllClients)
        {
            foreach (GameObject client in managers) client.SetActive(true);
            foreach (GameObject client in nonManagers) client.SetActive(true);
        }
        else if (value == (int) Options.Managers)
        {
            foreach (GameObject client in managers) client.SetActive(true);
            foreach (GameObject client in nonManagers) client.SetActive(false);
        }
        else if (value == (int) Options.NonManagers)
        {
            foreach (GameObject client in managers) client.SetActive(false);
            foreach (GameObject client in nonManagers) client.SetActive(true);
        }
    }

    public void OnListButtonClick()
    {
        // Hierarchy of pop up Name -> Points -> Address

        GameObject currentButton = EventSystem.current.currentSelectedGameObject;
        ClientData clientData = currentButton.GetComponent<ClientData>();

        popUpPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Name : " + clientData.name;
        popUpPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = "Points : " + clientData.points;
        popUpPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Address : " + clientData.address;

        popUpPanel.SetActive(true);
        MainPanelState(false);
    }

    private void MainPanelState(bool state)
    {
        foreach(GameObject element in mainPanel) element.SetActive(state);
    }

    public void OnCloseButtonClick()
    {
        popUpPanel.SetActive(false);
        MainPanelState(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject currentPlayer;
    public GameObject attackArrow;
    private GameObject camera;
    public GameObject[] MenuPanels = new GameObject[8];
    private int defaultPanelID = -1, panelID = -1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MenuPanels.Length; i++)
        {
            MenuPanels[i] = gameObject.transform.GetChild(i).gameObject;
        }
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer != null)
        {
            MenuSelect();
            SelectionMade();
        }

        if (defaultPanelID != panelID)
        {
            defaultPanelID = panelID;
        }
        
       
    }

    public void GetPlayer(GameObject player)
    {
        PanelsReset();
        currentPlayer = player;
    }

    private void MenuSelect()
    {

        //Attack
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PanelsReset();
            panelID = 1;
            PanelSelect();
        }
        //Skills
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PanelsReset();
            panelID = 2;
            PanelSelect();
        }
        //Item
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PanelsReset();
            panelID = 3;
            PanelSelect();
        }
        //Defend
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PanelsReset();
            panelID = 4;
            PanelSelect();
        }
    }

    // 1-4- 1 = Attack, 2 = Skills, 3 = Items, 4 = Defend
    private void PanelSelect()
    {
        panelID--;

        //Change Menu UI Image
        MenuPanels[panelID].SetActive(false);
        MenuPanels[panelID + 4].SetActive(true);

    }

    private void PanelsReset()
    {


        for (int i = 0; i < MenuPanels.Length / 2; i++)
        {
            MenuPanels[i].SetActive(true);
            MenuPanels[i + 4].SetActive(false);
        }
    }

    private void SelectionMade()
    {

        if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 0)
            {

                Debug.Log("Attack");

                //End Turn
                foreach (GameObject panel in MenuPanels)
                {
                    panel.SetActive(false);
                }

                defaultPanelID = -1;
                panelID = -1;

                attackArrow.SetActive(true);
                attackArrow.GetComponent<Attack>().player = currentPlayer;
                gameObject.SetActive(false);

            }

        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 1)
            {

                Debug.Log("Skills");



            }
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 2)
            {

                Debug.Log("Items");


            }
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 3)
            {
                Debug.Log("Defend");

                //Increase defense for 1 turn
                currentPlayer.GetComponent<PlayerBase>().defenseBuff = true;

                //End Turn
                foreach (GameObject panel in MenuPanels)
                {
                    panel.SetActive(false);
                }

                defaultPanelID = -1;
                panelID = -1;

                currentPlayer.GetComponent<PlayerBase>().TurnEnd();

            }
        }

    
    }

}
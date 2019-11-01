using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuControl : MonoBehaviour
{
    public GameObject currentPlayer;
    public GameObject[] MenuPanels = new GameObject[8];
    private GameObject battleMenu;
    public Text[] labelCollection1 = new Text[3], labelCollection2 = new Text[3], labelCollection3 = new Text[3];
    private int defaultPanelID = -1, panelID = -1;

    private bool startup = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MenuPanels.Length; i++)
        {
            MenuPanels[i] = gameObject.transform.GetChild(i).gameObject;
        }

        battleMenu = GameObject.FindGameObjectWithTag("BattleMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer != null)
        {
            MenuSelect();
            SelectionMade();

            //Get the items for the current character
            for (int i = 0; i < currentPlayer.GetComponent<PlayerBase>().Items.Length; i++)
            {
                Text[] labCo = null;
                switch (i)
                {
                    case 0:
                        labCo = labelCollection1;
                        break;

                    case 1:
                        labCo = labelCollection2;
                        break;

                    case 2:
                        labCo = labelCollection3;
                        break;
                }

                foreach (Text txt in labCo)
                {
                    if (currentPlayer.GetComponent<PlayerBase>().Items[i] != "")
                    {
                        txt.text = currentPlayer.GetComponent<PlayerBase>().Items[i];
                    }
                    else
                    {
                        txt.text = "None";
                    }

                }
            }
        }

        if (defaultPanelID != panelID)
        {
            defaultPanelID = panelID;
        }


        if ((startup) && (MenuPanels != null))
        {
            startup = false;
            gameObject.SetActive(false);

            for (int i = 4; i < MenuPanels.Length; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

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

               
            }

        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 1)
            {

                Debug.Log("Item2");

                defaultPanelID = -1;
                panelID = -1;

            }
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 2)
            {

                Debug.Log("Item3");

                defaultPanelID = -1;
                panelID = -1;

            }
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 3)
            {
                Debug.Log("Back");



                defaultPanelID = -1;
                panelID = -1;

                battleMenu.SetActive(true);
                gameObject.SetActive(false);
            }
        }


    }

}
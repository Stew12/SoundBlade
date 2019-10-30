using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuControl : MonoBehaviour
{
    public GameObject currentPlayer;
    public GameObject[] MenuPanels = new GameObject[8];
    public GameObject[] SkillObjects = new GameObject[8];
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

            //Get the skills for the current character
            for (int i = 0; i < currentPlayer.GetComponent<PlayerBase>().Skills.Length; i++)
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
                    if (currentPlayer.GetComponent<PlayerBase>().Skills[i] != "")
                    {
                        txt.text = currentPlayer.GetComponent<PlayerBase>().Skills[i];
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

                Debug.Log("Skill1");

                //End Turn
                foreach (GameObject panel in MenuPanels)
                {
                    panel.SetActive(false);
                }

                defaultPanelID = -1;
                panelID = -1;


                //End turn when skill aniamtion finishes
                // currentPlayer.GetComponent<PlayerBase>().TurnEnd();
                gameObject.SetActive(false);

            }

        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 1)
            {

                Debug.Log("Skill2");

                defaultPanelID = -1;
                panelID = -1;

            }
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.Z))))
        {
            if (defaultPanelID == 2)
            {

                Debug.Log("Skill3");

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

    public void SkillActivate(string skillname)
    {
        GameObject animObj;

        switch (skillname)
        {
            //Thunder Buff: electric attack, raises attack
            case "Thunder Buff":
                animObj = SkillObjects[0];
        break;
            //Flame Chord: fire damaging attack, keyboard
            case "Flame Chord":
                animObj = SkillObjects[1];
        break;
            //Thunder Debuff: ice attack, lowers enemy attack
            case "Ice Debuff":
                animObj = SkillObjects[2];
        break;
            //Healing Wind: heals whole party, flute
            case "Healing Wind":
                animObj = SkillObjects[3];
                Instantiate(animObj, currentPlayer.transform.position, Quaternion.identity);
                
                //Heal self
                currentPlayer.GetComponent<PlayerBase>().currentHP += currentPlayer.GetComponent<PlayerBase>().maxHP *= 0.5f;
                if (currentPlayer.GetComponent<PlayerBase>().currentHP > currentPlayer.GetComponent<PlayerBase>().maxHP)
                {
                    currentPlayer.GetComponent<PlayerBase>().currentHP = currentPlayer.GetComponent<PlayerBase>().maxHP;
                }
                break;

        }

        
    }

public string SkillDescription(string skillname)
{
    string desc = " ";
    switch (skillname)
    {
        //Thunder Buff: electric attack, raises attack
        case "Thunder Buff":
            desc = "";
            break;
        //Flame Chord: fire damaging attack, keyboard
        case "Flame Chord":
            desc = "";
            break;
        //Thunder Debuff: ice attack, lowers enemy attack
        case "Ice Debuff":
            desc = "";
            break;
        //Healing Wind: heals whole party, flute
        case "Healing Wind":
            desc = "";
            break;
    }
    return desc;
}
    

}
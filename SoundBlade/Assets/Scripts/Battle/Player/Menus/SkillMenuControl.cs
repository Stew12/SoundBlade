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
    private GameObject enemy;
    private GameObject turnManager;
    private bool timer;
    private float animTime = 5f;

    private bool startup = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MenuPanels.Length; i++)
        {
            MenuPanels[i] = gameObject.transform.GetChild(i).gameObject;
        }

        battleMenu = GameObject.FindGameObjectWithTag("BattleMenu");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        turnManager = GameObject.FindGameObjectWithTag("TurnManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        animTime -= Time.time;
        if (animTime <= 0)
        {

        }

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

    public void PanelsReset()
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

                SkillActivate(currentPlayer.GetComponent<PlayerBase>().Skills[0]);

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
        GameObject animObj, spawnedObj;
        float yOffset = 5f;
        float zOffset = 2f;

        switch (skillname)
        {
            //Thunder Buff: electric attack, raises attack
            case "Elec Tome":
                animObj = SkillObjects[0];
                spawnedObj = Instantiate(animObj, new Vector3(enemy.transform.position.x, enemy.transform.position.y - yOffset, enemy.transform.position.z - zOffset), Quaternion.identity);

                // Deal large amount of damage, no other effects
                currentPlayer.GetComponent<PlayerBase>().DealSpellDamage(2.5f, 15, false, false);
                break;

            //Flame Chord: fire damaging attack, keyboard
            case "Fiery Voice":
                animObj = SkillObjects[1];
                spawnedObj = Instantiate(animObj, new Vector3(enemy.transform.position.x, enemy.transform.position.y - yOffset, enemy.transform.position.z - zOffset), Quaternion.identity);

                //Deal large amount of damage, no other effects
                currentPlayer.GetComponent<PlayerBase>().DealSpellDamage(5, 25, false, false);
                break;

            //Ice Debuff: ice attack, lowers enemy attack
            case "Ice Clang":
                animObj = SkillObjects[2];
                spawnedObj = Instantiate(animObj, new Vector3(enemy.transform.position.x, enemy.transform.position.y - yOffset, enemy.transform.position.z - zOffset), Quaternion.identity);

                // Deal large amount of damage, no other effects
                currentPlayer.GetComponent<PlayerBase>().DealSpellDamage(2, 7, false, false);
                break;

            //Healing Wind: heals whole party, flute
            case "Healing Wind":
                animObj = SkillObjects[3];
                Instantiate(animObj, new Vector3(currentPlayer.transform.position.x, currentPlayer.transform.position.y, currentPlayer.transform.position.z), Quaternion.identity);

                //Heal all party members
                foreach (GameObject battler in turnManager.GetComponent<TurnManager>().battlers)
                {
                    if (battler.tag == "Player")
                    {
                        spawnedObj = Instantiate(animObj, new Vector3(battler.transform.position.x, battler.transform.position.y, battler.transform.position.z), Quaternion.identity);
                        spawnedObj.GetComponent<AnimationObject>().changeTurn = false;

                        battler.GetComponent<PlayerBase>().currentHP /= 0.5f;
                        if (battler.GetComponent<PlayerBase>().currentHP > battler.GetComponent<PlayerBase>().maxHP)
                        {
                            battler.GetComponent<PlayerBase>().currentHP = battler.GetComponent<PlayerBase>().maxHP;
                        }

                       battler.GetComponent<PlayerBase>().SetHPLabel();

                        //Just Reduce MP- non-damaging
                        currentPlayer.GetComponent<PlayerBase>().DealSpellDamage(0, 10, false, false);
                    }
                }
                
                break;
        }

        currentPlayer.GetComponent<PlayerBase>().turn = false;
        currentPlayer.GetComponent<PlayerBase>().turnSent = false;
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
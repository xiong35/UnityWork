using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeated;

    public GameObject born;
    public Text playerScoreText;
    public Text playerLifeValueText;
    public GameObject gameOver;

    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private void Awake ()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        if (isDefeated)
        {
            gameOver.SetActive (true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        if (isDead)
        {
            Recover ();
        }
        playerScoreText.text = playerScore.ToString ();
        playerLifeValueText.text = lifeValue.ToString ();
    }
    private void Recover ()
    {
        if (lifeValue <= 0)
        {
            isDefeated = true;
        }
        else
        {
            lifeValue -= 1;
            GameObject go = Instantiate (born, new Vector3 (-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born> ().createPlayer = true;
            isDead = false;
        }
    }

    private void ReturnToTheMainMenu ()
    {
        SceneManager.LoadScene (0);
    }

}
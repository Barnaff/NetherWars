using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour
{
	private NW_GameManager m_gameManager;
	public PlayerController mr_PlayerController;

	// Use this for initialization
	void Start ()
	{
		this.m_gameManager = new NW_GameManager ();
		string[] deck = new string[20] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1"};
		this.m_gameManager.StartGame("player 1 (player)",deck , "player 2 (opponent)", deck);

		this.mr_PlayerController.Init(this.m_gameManager.Player);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}

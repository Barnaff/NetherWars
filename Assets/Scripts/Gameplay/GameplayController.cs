using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour
{
	private NW_GameManager m_gameManager;

    private static GameplayController s_sharedManager;

	public PlayerController mr_PlayerController;

    public static GameplayController Instance
    {
        get
        {
            return s_sharedManager;
        }
    }

    public IPlayer CurrentPlayerTurn
    {
        get
        {
            return this.m_gameManager.CurrentPLayerTurn;
        }
    }

    public void Awake()
    {
        if (s_sharedManager == null)
        {
            s_sharedManager = this;
        }
    }

	// Use this for initialization
	public void Start ()
	{
		this.m_gameManager = new NW_GameManager();
		string[] deck = new string[20] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1"};
		this.m_gameManager.InitGame("player 1 (player)",deck , "player 2 (opponent)", deck);

        this.mr_PlayerController.Init(this.m_gameManager.Player);

        this.m_gameManager.StartGame();
	}

    public bool CanPlayerPlayCard(IPlayer i_Player, NW_Card i_Card)
    {
        return this.m_gameManager.CanPlayCard(i_Player, i_Card);
    }

    public bool CanPlayerPlayCardAsResource(IPlayer i_Player, NW_Card i_Card)
    {
        return this.m_gameManager.CanPlayResourceThisTurn(i_Player);
    }

    public void TryPlayCardAsResourceForPlayer(IPlayer i_Player, NW_Card i_Card)
    {
        if (this.m_gameManager.CanPlayResourceThisTurn(i_Player))
        {
        }
    }

    public void TryPlayCardForPlayer(IPlayer i_Player, NW_Card i_Card)
    {

    }
}

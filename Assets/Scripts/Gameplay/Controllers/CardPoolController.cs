using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardPoolController : MonoBehaviour
{
    public static CardPoolController Instance
    {
        get
        {
            return s_instance;
        }
    }


    private static CardPoolController s_instance;

    private CardFactoryController m_cardFactoryController;
    private Dictionary<NW_Card, CardController> m_cardPool;

    public void Awake()
    {
        if (Instance == null)
        {
            s_instance = this;
        }
        this.m_cardPool = new Dictionary<NW_Card, CardController>();
        this.m_cardFactoryController = this.gameObject.GetComponent<CardFactoryController>();
    }

    public CardController CardControllerFromCard(NW_Card i_Card)
    {
        if (this.m_cardPool.ContainsKey(i_Card))
        {
            return this.m_cardPool[i_Card];
        }

        CardController newCard = this.m_cardFactoryController.CreateCardController(i_Card);
        this.m_cardPool.Add(i_Card, newCard);
        return newCard;
    }
}

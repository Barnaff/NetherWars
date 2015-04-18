using UnityEngine;
using System.Collections;

public class CardFactoryController : MonoBehaviour
{
    [SerializeField]
    private CardController m_cardControllerToInstantiate;

    public CardController CreateCardController(NW_Card i_Card)
    {
        CardController newCard = Instantiate(this.m_cardControllerToInstantiate);
        newCard.Init(i_Card);
        return newCard;
    }
}

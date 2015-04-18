using UnityEngine;
using System.Collections;

public interface IResourcePool  {

	void ResetPool();

	bool CanPayForCard(NW_Card card);

	void PayForCard(NW_Card card);
		
	int ThrasholdForColor(NW_Color color);

	int CurrentMana{ get; }

}

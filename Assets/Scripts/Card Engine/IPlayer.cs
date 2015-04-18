using UnityEngine;
using System.Collections;

public interface IPlayer   {

	string PlayerName { get; }

	int PlayerId { get; }

	void StartTurn();

	void Draw();

	void Draw(int numberOfCards);

	void ShuffleLibrary();

	IResourcePool ResourcePool { get; }

	NW_Zone Hand { get; }

	NW_Zone Battlefield { get; }

	void PutCardInResource(NW_Card card);


}

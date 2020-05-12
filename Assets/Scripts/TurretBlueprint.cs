using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
	public GameObject turretPrefab;
	public GameObject buildEffectPrefab;
	public GameObject sellEffectPrefab;
	public int cost;

	public GameObject upgradedTurretPrefab;
	public int upgradeCost;

	// TODO: If Turret is upgraded already, return half of the upgrade cost as well.
	public int GetSellAmount()
	{
		return cost / 2;
	}

	// TODO: Branching upgrade tree per turret and upgrade tree tracking
}

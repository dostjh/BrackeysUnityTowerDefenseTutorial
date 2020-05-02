using UnityEngine;

public class Shop : MonoBehaviour
{
	public TurretBlueprint turretStandard;
	public TurretBlueprint turretMissile;

	BuildManager buildManager;

	void Start()
	{
		buildManager = BuildManager.instance;
	}

	public void SelectTurretStandard()
	{
		Debug.Log("Standard Turret Selected");
		buildManager.SelectTurretToBuild(turretStandard);
	}

	public void SelectTurretMissile()
	{
		Debug.Log("Missile Launcher Selected");
		buildManager.SelectTurretToBuild(turretMissile);
	}
}

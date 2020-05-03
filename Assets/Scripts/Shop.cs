using UnityEngine;

public class Shop : MonoBehaviour
{
	public TurretBlueprint turretStandard;
	public TurretBlueprint turretMissile;
	public TurretBlueprint turretLaser;

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

	public void SelectTurretLaser()
	{
		Debug.Log("Laser Turret Selected");
		buildManager.SelectTurretToBuild(turretLaser);
	}
}

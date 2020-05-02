using UnityEngine;

public class BuildManager : MonoBehaviour
{
	#region Singleton implementation
	// NOTE: Singleton pattern - Only one instance of BuildManager in the scene 
	// that is easily accessible by everything in the scene.
	public static BuildManager instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}
	#endregion

	public GameObject turretStandard;
	public GameObject turretMissile;

	public GameObject buildEffectPrefab;

	TurretBlueprint turretToBuild;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool CanAfford { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public void BuildTurretOn(Node node)
	{
		if (PlayerStats.Money < turretToBuild.cost)
		{
			// TODO: Display status to user.
			Debug.Log($"Not enough money to build that -- Current: {PlayerStats.Money}; Cost: {turretToBuild.cost}");
			return;
		}

		PlayerStats.Money -= turretToBuild.cost;
		
		// NOTE: Quaternion.identity means we don't rotate
		var turret = Instantiate(turretToBuild.turretPrefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		// TODO: Make this part of the turret prefab instead of having a single build effect for all turrets so that we can call the appropriate effect for the turret.
		var buildEffect = Instantiate(turretToBuild.buildEffectPrefab, node.GetBuildPosition(), Quaternion.identity);
		Destroy(buildEffect, 5.0f);

		Debug.Log($"Turret built for {turretToBuild.cost}. Money remaining: {PlayerStats.Money}");
	}
	
	public void SelectTurretToBuild(TurretBlueprint turret)
	{
		turretToBuild = turret;
	}
}

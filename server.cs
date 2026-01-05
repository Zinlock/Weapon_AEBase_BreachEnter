function gameConnection::DropInventory(%client)
{
	if(isObject(%client.player))
	{
		for(%i=0;%i<%client.player.getDatablock().maxTools;%i++)
		{	
			%item = %client.player.tool[%i];
			if(isObject(%item))
			{
				%pos = %client.player.getPosition();
				%rand = getRandom() * 3.14159 * 20;
				%x = mSin(%rand);
				%y = mCos(%rand);
				%offset = vectorNormalize(%x SPC %y);
				%vec = %client.player.getVelocity();
				%item = new Item()
				{
					dataBlock = %item;
					position = vectorAdd(%pos, %offset);
				};
				%itemVec = %vec;
				%itemVec = vectorAdd(%itemVec,"0 0 5");
				%item.BL_ID = %client.BL_ID;
				%item.minigame = %client.minigame;
				%item.spawnBrick = -1;
				%item.setVelocity(%itemVec);
				%item.schedulePop();
			}
			%client.player.tool[%i] = "";
		}
	}
	Parent::DropInventory(%client);
}

//we need the base add-on for this, so force it to load
$error = ForceRequiredAddOn("Weapon_AEBase");

if($error == $Error::AddOn_NotFound)
{
	//we don't have the base, so we're screwed =(
	error("ERROR: AEBase_BreachEnter - required add-on Weapon_AEBase not found");
}
else
{
	exec("./SmokesFlashes.cs");
	exec("./Sounds/Sounds.cs");
	exec("./Weapon_AA12.cs");
	exec("./Weapon_AK47.cs");
	exec("./Weapon_AR15.cs");
	exec("./Weapon_Bizon.cs");
	exec("./Weapon_Contender.cs");
	exec("./Weapon_Deagle.cs");
	exec("./Weapon_Falcon.cs");
	exec("./Weapon_Famas.cs");
	exec("./Weapon_FNFAL.cs");
	exec("./Weapon_Glock18.cs");
	exec("./Weapon_Ithaca37.cs");
	exec("./Weapon_L96.cs");
	exec("./Weapon_M4A1.cs");
	exec("./Weapon_M9.cs");
	exec("./Weapon_M60.cs");
	exec("./Weapon_M82A1.cs");
	exec("./Weapon_MG42.cs");
	exec("./Weapon_MK14.cs");
	exec("./Weapon_MP5K.cs");
	exec("./Weapon_RPG.cs");
	exec("./Weapon_RPK.cs");
	exec("./Weapon_Spas12.cs");
	exec("./Weapon_SVD.cs");
	exec("./Weapon_UMP45.cs");

}
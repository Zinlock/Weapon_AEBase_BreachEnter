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
%error = ForceRequiredAddOn("Weapon_AEBase");

if(%error == $Error::AddOn_NotFound)
{
	//we don't have the base, so we're screwed =(
	error("ERROR: AEBase_BreachEnter - required add-on Weapon_AEBase not found");
}
else
{
	exec("./SmokesFlashes.cs");
	exec("./Item_Ammo.cs");
	exec("./Sounds/Sounds.cs");
//	exec("./Weapon_AR15S.cs"); // polish humor     :PP
	exec("./Weapon_AK47.cs");
	exec("./Weapon_KBP9A91.cs");
	exec("./Weapon_M9.cs");
	exec("./Weapon_AUG.cs");
	exec("./Weapon_OLINCAWS.cs");
	exec("./Weapon_Ithaca37.cs");
	exec("./Weapon_Falcon.cs");
	exec("./Weapon_FAMAS.cs");
	exec("./Weapon_MK14.cs");
	exec("./Weapon_M16A1.cs");
	exec("./Weapon_SPAS12.cs");
	exec("./Weapon_M9S.cs");
	exec("./Weapon_Ranger.cs");
	exec("./Weapon_FMG.cs");
	exec("./Weapon_USP45.cs");
	exec("./Weapon_MP7.cs");
	exec("./Weapon_TMP.cs");
	exec("./Weapon_TMPS.cs");
	exec("./Weapon_Taser.cs");
	exec("./Weapon_L96.cs");
	exec("./Weapon_SKSM.cs");
	exec("./Weapon_M60.cs");
	exec("./Weapon_Intervention.cs");
	exec("./Weapon_AA12.cs");
	exec("./Weapon_PDR.cs");
	exec("./Weapon_Striker.cs");
	exec("./Weapon_P90.cs");
	exec("./Weapon_Mac10.cs");
	exec("./Weapon_Glock18.cs");
	exec("./Weapon_Glock17.cs");
	exec("./Weapon_M93R.cs");
	exec("./Weapon_UMP45.cs");
	exec("./Weapon_Patriot.cs");
	exec("./Weapon_NCRRifle.cs");
	exec("./Weapon_M16A3.cs");
	exec("./Weapon_M4A1.cs");
	exec("./Weapon_M16Com.cs");
	exec("./Weapon_AK103.cs");
	exec("./Weapon_AKS47U.cs");
	exec("./Weapon_AR15.cs");
	exec("./Weapon_LR300.cs");
	exec("./Weapon_RPK.cs");
	exec("./Weapon_Thompson.cs");
	exec("./Weapon_TommyGun.cs");
	exec("./Weapon_American180.cs");
	exec("./Weapon_FAD.cs");
	exec("./Weapon_G3.cs");
	exec("./Weapon_RPG.cs");
	exec("./Weapon_KS23.cs");
	exec("./Weapon_1911.cs");
	exec("./Weapon_AR1550.cs");
	exec("./Weapon_SVD.cs");
	exec("./Weapon_MP5K.cs");
	exec("./Weapon_Saiga.cs");
	exec("./Weapon_Python.cs");
	// exec("./Weapon_1911S.cs"); // todo (mesh, anims)
	// exec("./Weapon_KH2002.cs"); // todo (mesh, anims, code)
	// exec("./Weapon_ScarH.cs"); // todo (mesh, anims, code)
}
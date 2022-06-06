datablock AudioProfile(BNE_FNFALFireLoopSound)
{
   filename    = "./Sounds/Fire/FNFAL/FNFAL_FireLoop.wav";
   description = BAADFireMediumLoop3D;
   preload = true;
};

datablock AudioProfile(BNE_FNFALFireLoopEndSound)
{
   filename    = "./Sounds/Fire/FNFAL/FNFAL_FireLoopEnd.wav";
   description = MediumClose3D;
   preload = true;
};

// FNFAL
datablock DebrisData(BNE_FNFALMagDebris)
{
	shapeFile = "./FNFAL/FNFALMag.dts";
	lifetime = 2.0;
	minSpinSpeed = -700.0;
	maxSpinSpeed = -600.0;
	elasticity = 0.5;
	friction = 0.1;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};

//////////
// item //
//////////
datablock ItemData(BNE_FNFALItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./FNFAL/FNFAL.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: FN FAL";
	iconName = "./Icons/6";
	doColorShift = true;
	colorShiftColor = "0.42 0.41 0.4 1";

	 // Dynamic properties defined by the scripts
	image = BNE_FNFALImage;
	canDrop = true;

	AEAmmo = 20;
	AEType = AE_HeavyRAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 700;
	recoil = "Heavy";
	uiColor = "1 1 1";
	description = "The FN FAL is a battle rifle developed in Belgium in 1953, famously nicknamed 'Right Arm of the Free World.'" NL "The weapon is chambered in 7.62x51mm NATO cartridges and packs quite a hefty punch, while having a lot of recoil." NL "'Our journey to victory has begun! Death to the MPLA!!' - Jonas Savimbi";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_FNFALImage)
{
   // Basic Item properties
   shapeFile = "./FNFAL/FNFAL.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.025 0 -0.04";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = BNE_FNFALItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BERifleShellDebris;
   shellExitDir        = "1 0 0.75";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_FNFALSafetyImage;
    scopingImage = BNE_FNFALIronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_FNFALItem.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellRifle;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

    loopingEndSound = BNE_FNFALFireLoopEndSound;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 35;
	projectileCount = 1;
	projectileHeadshotMult = 1.85;
	projectileVelocity = 400;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.8;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 150; // m
	spreadBase = 25;
	spreadMin = 150;
	spreadMax = 1000;

	screenshakeMin = "0.125 0.125 0.125";
	screenshakeMax = "0.175 0.175 0.175";

	farShotSound = RifleGDistantSound;
	farShotDistance = 40;

	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	staticHitscan = true;
	staticEffectiveRange = 110;
	staticTotalRange = 2000;
	staticGravityScale = 1.5;
	staticSwayMod = 2;
	staticEffectiveSpeedBonus = 0;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = ""; // defaults to AEBulletStaticShape
	staticScaleCalibre = 0.25;
	staticScaleLength = 0.25;
	staticUnitsPerSecond = $ae_RifleUPS;

	projectileFalloffStart = $ae_falloffRifleStart;
	projectileFalloffEnd = $ae_falloffRifleEnd;
	projectileFalloffDamage = $ae_falloffRifle;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.
   // Initial start up state
	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.01;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;

	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";

	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload2";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.25;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";

	stateName[8]				= "ReloadMagOut";
	stateTimeoutValue[8]			= 0.6;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "MagOut";
	stateSound[8]				= BNE_FNFALMagOutSound;

	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.4;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= BNE_FNFALMagInSound;

	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.25;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";

	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.09;
	stateTransitionOnTimeout[11]		= "FireLoadCheckB";

	stateName[12]				= "FireLoadCheckB";
	stateTransitionOnAmmo[12]		= "TrigCheck";
	stateTransitionOnNoAmmo[12]		= "EndLoopEmpty";
	stateTransitionOnNotLoaded[12]  = "EndLoop";

	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.2;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";

// EMPTY RELOAD STATE

	stateName[15]				= "Reload2";
	stateTimeoutValue[15]			= 0.25;
	stateScript[15]				= "onReloadStart";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "ReloadStart";

	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 0.6;
	stateScript[16]				= "onReload2MagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "MagOut";
	stateSound[16]				= BNE_FNFALMagOutSound;

	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.3;
	stateScript[17]				= "onReload2MagIn";
	stateTransitionOnTimeout[17]		= "Reload2Bolt";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagIn";
	stateSound[17]				= BNE_FNFALMagInSound;

	stateName[18]				= "Reload2Bolt";
	stateTimeoutValue[18]			= 0.45;
	stateScript[18]				= "onReload2Bolt";
	stateTransitionOnTimeout[18]		= "Reload2End";
	stateWaitForTimeout[18]			= true;
	stateSequence[18]			= "Bolt";
	stateSound[18]				= BNE_AKBoltPullSound;

	stateName[19]				= "Reload2End";
	stateTimeoutValue[19]			= 0.25;
	stateScript[19]				= "onReload2End";
	stateTransitionOnTimeout[19]		= "Ready";
	stateWaitForTimeout[19]			= true;
	stateSequence[19]			= "ReloadEnd";

	stateName[20]				= "ReadyLoop";
	stateTransitionOnTimeout[20]		= "Ready";

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "Reload2";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";
	
	stateName[23]          = "TrigCheck";
	stateTransitionOnTriggerDown[23]  = "preFire";
	stateTransitionOnTimeout[23]		= "EndLoop";
	
	stateName[24]          = "EndLoop";
	stateScript[24]				= "onEndLoop";
	stateTransitionOnTimeout[24]		= "Ready";
	
	stateName[25]          = "EndLoopEmpty";
	stateScript[25]				= "onEndLoop";
	stateTransitionOnTimeout[25]		= "Reload2";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_FNFALImage::AEOnFire(%this,%obj,%slot)
{
	%obj.playAudio(0, BNE_FNFALFireLoopSound);
    %obj.FireLoop = true;
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);
	
	Parent::AEOnFire(%this, %obj, %slot); 	
}

function BNE_FNFALImage::onEndLoop(%this, %obj, %slot)
{
    %obj.playAudio(0, %this.loopingEndSound);
    %obj.FireLoop = false;
}

function BNE_FNFALImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_FNFALImage::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
}

function BNE_FNFALImage::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
}

function BNE_FNFALImage::onReloadMagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(0, shiftright);
}

function BNE_FNFALImage::onReload2MagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
}

function BNE_FNFALImage::onReload2Bolt(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_FNFALImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_FNFALImage::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
   %obj.reload3Schedule = %this.schedule(225,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(400,500),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_FNFALImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_FNFALImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FNFALImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_FNFALImage::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_FNFALMagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_FNFALMagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.1 0.75 0.05";
   rotation = eulerToMatrix( "0 20 0" );

	casing = BNE_FNFALMagDebris;
	shellExitDir        = "0 1 -0.25";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;
	shellVelocity       = 2.0;

	stateName[0]					= "Ready";
	stateTimeoutValue[0]			= 0.01;
	stateTransitionOnTimeout[0] 	= "EjectA";
	
	stateName[1]					= "EjectA";
	stateTimeoutValue[1]			= 1;
	stateEjectShell[1]				= true;
	stateTransitionOnTimeout[1] 	= "Done";
	
	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};

function BNE_FNFALMagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_FNFALSafetyImage)
{
   shapeFile = "./FNFAL/FNFAL.dts";
   emap = true;
   mountPoint = 0;
   offset = "-0.025 0 -0.04";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_FNFALItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_FNFALIronsightImage;
   safetyImage = BNE_FNFALImage;
   doColorShift = true;
   colorShiftColor = BNE_FNFALItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_FNFALSafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_FNFALSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_FNFALSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);
	parent::onUnMount(%this,%obj,%slot);
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_FNFALIronsightImage : BNE_FNFALImage)
{
	recoilHeight = 0.2;

	scopingImage = BNE_FNFALImage;
	sourceImage = BNE_FNFALImage;

   offset = "-0.025 0 -0.04";
	eyeOffset = "-0.0005 1.0 -1.0535";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;

	stateName[15]				= "Reload2";
	stateScript[15]				= "onDone";
	stateTimeoutValue[15]			= 1;
	stateTransitionOnTimeout[15]		= "";
	stateSound[15]				= "";
	
	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function BNE_FNFALIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_FNFALIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_FNFALIronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.playAudio(0, BNE_FNFALFireLoopSound);
    %obj.FireLoop = true;
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);
	
	Parent::AEOnFire(%this, %obj, %slot); 	
}

function BNE_FNFALIronsightImage::onEndLoop(%this, %obj, %slot)
{
    %obj.playAudio(0, %this.loopingEndSound);
    %obj.FireLoop = false;
}

function BNE_FNFALIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_FNFALIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FNFALIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}

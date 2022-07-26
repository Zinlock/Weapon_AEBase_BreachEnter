datablock AudioProfile(BNE_FalconFire1Sound)
{
   filename    = "./Sounds/Fire/Falcon/Falcon_FIRE_1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_FalconFire2Sound)
{
   filename    = "./Sounds/Fire/Falcon/Falcon_FIRE_2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_FalconFire3Sound)
{
   filename    = "./Sounds/Fire/Falcon/Falcon_FIRE_3.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_FalconFire4Sound)
{
   filename    = "./Sounds/Fire/Falcon/Falcon_FIRE_4.wav";
   description = HeavyClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(FalconItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Falcon/Falcon.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Falcon";
	iconName = "./Icons/13";
	doColorShift = true;
	colorShiftColor = "0.55 0.5 0.5 1";

	 // Dynamic properties defined by the scripts
	image = BNE_FalconImage;
	canDrop = true;

	AEAmmo = 1;
	AEType = AE_HeavierSRAmmoItem.getID();
	AEBase = 1;

	RPM = 600;
	recoil = "Heavy";
	uiColor = "1 1 1";
	description = "The Falcon is a short but heavy and powerful sniper rifle similar to the Serbu BFG-50.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_FalconImage)
{
   // Basic Item properties
   shapeFile = "./Falcon/Falcon.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
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
   item = FalconItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BEFiftyShellDebris;
   shellExitDir        = "-1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_FalconBipodImage;
    scopingImage = BNE_FalconIronsightImage;
	doColorShift = true;
	colorShiftColor = FalconItem.colorShiftColor;//"0.400 0.196 0 1.000";
	R_MovePenalty = 0.75;

	muzzleFlashScale = "2 2 2";
	bulletScale = "1 1 1";

	gunType = "Sniper";

	projectileDamage = 100;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 1000;
	projectileTagStrength = 0.78;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate
	projectileRGibExplosionBody = 1;
	projectileRGibExplosionHead = 1;
	projectileVehicleDamageMult = 1;

	recoilHeight = 2;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 150; // m
	spreadBase = 750;
	spreadMin = 750;
	spreadMax = 750;

	screenshakeMin = "0.5 0.5 0.5";
	screenshakeMax = "9999 9999 9999"; //trollface

	farShotSound = SniperBDistantSound;
	farShotDistance = 100;

	sonicWhizz = true;
	whizzSupersonic = 2; // 2 is heavier
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	projectileFalloffStart = $ae_falloffSniperStart;
	projectileFalloffEnd = $ae_falloffSniperEnd;
	projectileFalloffDamage = $ae_falloffSniper;

	staticHitscan = true;
	staticEffectiveRange = 260;
	staticTotalRange = 2000;
	staticGravityScale = 1;
	staticSwayMod = 0;
	staticEffectiveSpeedBonus = 0;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = ""; // defaults to AEBulletStaticShape
	staticScaleCalibre = 0.6;
	staticScaleLength = 0.6;
	staticUnitsPerSecond = $ae_AntiMatUPS;

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
	stateEmitter[2]					= AEBaseShotgunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
	stateEmitterTime[3]				= 0.05;
	stateTimeoutValue[3]             	= 0.65;
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
	stateTransitionOnNoAmmo[6]		= "Reload";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.45;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadWait";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";
	stateSound[7]				= BNE_FalconBoltOpenSound;

	stateName[8]				= "ReloadWait";
	stateScript[8]				= "onReloadWait";
	stateTimeoutValue[8]			= 0.25;
	stateTransitionOnTimeout[8]		= "ReloadInsert";

	stateName[9]				= "ReloadInsert";
	stateTimeoutValue[9]			= 0.35;
	stateScript[9]				= "onReloadInsert";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "Reload";
	stateSound[9]				= BNE_FalconInsertSound;

	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.45;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";
	stateSound[10]				= BNE_FalconBoltCloseSound;

	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.1;
	stateTransitionOnTimeout[11]		= "FireLoadCheckB";

	stateName[12]				= "FireLoadCheckB";
	stateTransitionOnNoAmmo[12]		= "Reload";
	stateTransitionOnAmmo[12]  = "Ready";
	stateTransitionOnNotLoaded[12]  = "Ready";

	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";

	stateName[20]				= "ReadyLoop";
	stateTransitionOnTimeout[20]		= "Ready";

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "Reload";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";
	
	stateName[23]           = "SemiAutoCheck"; //heeeeeeeeeeeeey
	stateTransitionOnTriggerUp[23]	  	= "FireLoadCheckA";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_FalconImage::AEOnFire(%this,%obj,%slot)
{
	%obj.stopAudio(0);
  %obj.playAudio(0, BNE_FalconFire @ getRandom(1, 4) @ Sound);

	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_FalconImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_FalconImage::AEOnLowClimb(%this, %obj, %slot)
{
   %obj.aeplayThread(2, jump);
}

function BNE_FalconImage::onReloadInsert(%this,%obj,%slot)
{
  %obj.schedule(50, "aeplayThread", "2", "plant");
  %obj.schedule(350, "aeplayThread", "3", "shiftright");
}

function BNE_FalconImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_FalconImage::onReload2End(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_FalconImage::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
   %obj.reload3Schedule = %this.schedule(250,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(500,600),0,serverPlay3D,AEShellSniper @ getRandom(1,2) @ Sound,%obj.getPosition());
}

function BNE_FalconImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_FalconImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FalconImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_FalconImage::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_FalconCasingImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_FalconCasingImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "0 0.2	 0.35";
   rotation = eulerToMatrix( "0 0 0" );

	casing = AE_BEFiftyShellDebris;
	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;
	shellVelocity       = 4.0;

	stateName[0]					= "Ready";
	stateTimeoutValue[0]			= 0.01;
	stateTransitionOnTimeout[0] 	= "EjectA";

	stateName[1]					= "EjectA";
	stateEjectShell[1]				= true;
	stateTimeoutValue[1]			= 1;
	stateTransitionOnTimeout[1] 	= "Done";

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};

function BNE_FalconCasingImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_FalconIronsightImage : BNE_FalconImage)
{
	recoilHeight = 1;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;

	scopingImage = BNE_FalconImage;
	sourceImage = BNE_FalconImage;

	isScopedImage = true;

	offset = "0 0 0";
	eyeOffset = "-0.0008 1.45 -1.2632";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_HighScopeFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.25;

	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function BNE_FalconIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_FalconIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_FalconIronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	%obj.stopAudio(0);
  %obj.playAudio(0, BNE_FalconFire @ getRandom(1, 4) @ Sound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_FalconIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_FalconIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn6Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FalconIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}

//ALT BIPOD STATES

datablock ShapeBaseImageData(BNE_FalconBipodImage : BNE_FalconImage)
{
    shapeFile = "./Falcon/FalconBipod.dts";
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;
	safetyImage = BNE_FalconImage;
    scopingImage = BNE_FalconIronsightBipodImage;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_FalconBipodImage::AEOnFire(%this,%obj,%slot)
{
	BNE_FalconImage::AEOnFire(%this, %obj, %slot);
}

function BNE_FalconBipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_FalconImage::onDryFire(%this, %obj, %slot);
}

function BNE_FalconBipodImage::AEOnLowClimb(%this, %obj, %slot)
{
	BNE_FalconImage::AEOnLowClimb(%this, %obj, %slot);
}

function BNE_FalconBipodImage::onReloadInsert(%this,%obj,%slot)
{
	BNE_FalconImage::onReloadInsert(%this, %obj, %slot);
}

function BNE_FalconBipodImage::onReloadEnd(%this,%obj,%slot)
{
	BNE_FalconImage::onReloadEnd(%this, %obj, %slot);
}

function BNE_FalconBipodImage::onReload2End(%this,%obj,%slot)
{
	BNE_FalconImage::onReload2End(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_FalconBipodImage::onReloadStart(%this,%obj,%slot)
{
	BNE_FalconImage::onReloadStart(%this, %obj, %slot);
}

function BNE_FalconBipodImage::onReady(%this,%obj,%slot)
{
	BNE_FalconImage::onReady(%this, %obj, %slot);
}

// HIDES ALL HAND NODES

function BNE_FalconBipodImage::onMount(%this,%obj,%slot)
{
	BNE_FalconImage::onMount(%this, %obj, %slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FalconBipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_FalconImage::onUnMount(%this, %obj, %slot);
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_FalconBipodImage::onMagDrop(%this,%obj,%slot)
{
	BNE_FalconImage::onMagDrop(%this, %obj, %slot);
}

datablock ShapeBaseImageData(BNE_FalconIronsightBipodImage : BNE_FalconIronsightImage)
{
    shapeFile = "./Falcon/FalconBipod.dts";
	sourceImage = BNE_FalconBipodImage;
	scopingImage = BNE_FalconBipodImage;
	safetyImage = BNE_FalconImage;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_FalconIronsightBipodImage::onDone(%this,%obj,%slot)
{
	BNE_FalconIronsightImage::onDone(%this, %obj, %slot);
}

function BNE_FalconIronsightBipodImage::onReady(%this,%obj,%slot)
{
	BNE_FalconIronsightImage::onReady(%this, %obj, %slot);
}

function BNE_FalconIronsightBipodImage::AEOnFire(%this,%obj,%slot)
{
	BNE_FalconIronsightImage::AEOnFire(%this, %obj, %slot);
}

function BNE_FalconIronsightBipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_FalconIronsightImage::onDryFire(%this, %obj, %slot);
}

// HIDES ALL HAND NODES

function BNE_FalconIronsightBipodImage::onMount(%this,%obj,%slot)
{
	BNE_FalconIronsightImage::onMount(%this, %obj, %slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FalconIronsightBipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_FalconIronsightImage::onUnMount(%this, %obj, %slot);
}
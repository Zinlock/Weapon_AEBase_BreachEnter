datablock AudioProfile(BNE_ContenderFire1Sound)
{
   filename    = "./Sounds/Fire/Contender/Contender_FIRE1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_ContenderFire2Sound)
{
   filename    = "./Sounds/Fire/Contender/Contender_FIRE2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_ContenderFire3Sound)
{
   filename    = "./Sounds/Fire/Contender/Contender_FIRE3.wav";
   description = MediumClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(BNE_ContenderItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Contender/Contender.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: G2 Contender";
	iconName = "./Icons/Icon_Contender";
	doColorShift = true;
	colorShiftColor = "0.55 0.5 0.5 1";

	 // Dynamic properties defined by the scripts
	image = BNE_ContenderImage;
	canDrop = true;

	AEAmmo = 1;
	AEType = AE_HeavyPAmmoItem.getID();
	AEBase = 1;

	RPM = 600;
	recoil = "Heavy";
	uiColor = "1 1 1";
	description = "The Contender is a short but heavy and powerful sniper rifle similar to the Serbu BFG-50.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_ContenderImage)
{
   // Basic Item properties
   shapeFile = "./Contender/Contender.dts";
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
   item = BNE_ContenderItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BERifleShellDebris;
   shellExitDir        = "-1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_ContenderSafetyImage;
  scopingImage = BNE_ContenderIronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_ContenderItem.colorShiftColor;//"0.400 0.196 0 1.000";

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	gunType = "Sniper";

	projectileDamage = 75;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 1000;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 2;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 150; // m
	spreadBase = 300;
	spreadMin = 300;
	spreadMax = 300;

	screenshakeMin = "0.25 0.25 0.25";
	screenshakeMax = "9999 9999 9999"; //trollface

	farShotSound = RevolverDistantSound;
	farShotDistance = 40;

	sonicWhizz = true;
	whizzSupersonic = 2; // 2 is heavier
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	projectileFalloffStart = $ae_falloffRifleStart;
	projectileFalloffEnd = $ae_falloffRifleEnd;
	projectileFalloffDamage = $ae_falloffRifle;

	staticHitscan = true;
	staticEffectiveRange = 260;
	staticTotalRange = 2000;
	staticGravityScale = 1;
	staticSwayMod = 0;
	staticEffectiveSpeedBonus = 0;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = ""; // defaults to AEBulletStaticShape
	staticScaleCalibre = 0.25;
	staticScaleLength = 0.25;
	staticUnitsPerSecond = $ae_RifleUPS;

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

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateTimeoutValue[3]             	= 0.5;
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
	stateSound[7]				= ""; //BNE_ContenderOpenSound;

	stateName[8]				= "ReloadWait";
	stateScript[8]				= "onReloadWait";
	stateTimeoutValue[8]			= 0.15;
	stateTransitionOnTimeout[8]		= "ReloadInsert";

	stateName[9]				= "ReloadInsert";
	stateTimeoutValue[9]			= 0.35;
	stateScript[9]				= "onReloadInsert";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "ReloadIn";
	stateSound[9]				= BNE_M1873Insert3Sound; //BNE_ContenderInsertSound;

	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.55;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";
	stateSound[10]				= ""; //BNE_ContenderCloseSound;

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

function BNE_ContenderImage::AEOnFire(%this,%obj,%slot)
{
	%obj.stopAudio(0);
  %obj.playAudio(0, BNE_ContenderFire @ getRandom(1, 3) @ Sound);

	%obj.blockImageDismount = true;
	%obj.schedule(250, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_ContenderImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_ContenderImage::AEOnLowClimb(%this, %obj, %slot)
{
   %obj.aeplayThread(2, plant);
}

function BNE_ContenderImage::onReloadInsert(%this,%obj,%slot)
{
  %obj.schedule(50, "aeplayThread", "2", "plant");
  %obj.schedule(450, "aeplayThread", "3", "shiftright");
	%obj.schedule(450, playAudio, 2, BNE_ContenderCloseSound);
}

function BNE_ContenderImage::onReloadEnd(%this,%obj,%slot)
{
	%obj.schedule(250, playAudio, 1, BNE_RPG7CockSound);//BNE_ContenderCockSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}


// MAGAZINE DROPPING

function BNE_ContenderImage::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
	 %obj.schedule(100, playAudio, 1, BNE_ContenderOpenSound);
   %obj.reload3Schedule = %this.schedule(150,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(400,500),0,serverPlay3D,AEShellRifle @ getRandom(1,2) @ Sound,%obj.getPosition());
}

function BNE_ContenderImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_ContenderImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_ContenderImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_ContenderImage::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_ContenderCasingImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_ContenderCasingImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.1 0.6	0.4";
   rotation = eulerToMatrix( "0 0 0" );

	casing = AE_BERifleShellDebris;
	shellExitDir        = "0 -0.5 0.5";
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

function BNE_ContenderCasingImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_ContenderIronsightImage : BNE_ContenderImage)
{
	recoilHeight = 1;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;

	scopingImage = BNE_ContenderImage;
	sourceImage = BNE_ContenderImage;

	isScopedImage = true;

	offset = "0 0 0";
	eyeOffset = "-0.01015 1.15 -0.80925";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_HighScopeFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;

	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function BNE_ContenderIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_ContenderIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_ContenderIronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(250, unBlockImageDismount);

	%obj.stopAudio(0);
  %obj.playAudio(0, BNE_ContenderFire @ getRandom(1, 3) @ Sound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_ContenderIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_ContenderIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn6Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_ContenderIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}

datablock ShapeBaseImageData(BNE_ContenderSafetyImage)
{
	shapeFile = "./Contender/Contender.dts";
	emap = true;
	mountPoint = 0;
	offset = "0 0 -0.065";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = BNE_ContenderItem;
	ammo = " ";
	melee = false;
	armReady = false;
	hideHands = false;
	scopingImage = BNE_ContenderIronsightImage;
	safetyImage = BNE_ContenderImage;
	doColorShift = true;
	colorShiftColor = BNE_ContenderItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_ContenderSafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_ContenderSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_ContenderSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);
	parent::onUnMount(%this,%obj,%slot);
}
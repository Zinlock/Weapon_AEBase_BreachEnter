datablock AudioProfile(BNE_FAMASFire1Sound)
{
   filename    = "./Sounds/Fire/FAMAS/FAMAS_fire1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_FAMASFire2Sound)
{
   filename    = "./Sounds/Fire/FAMAS/FAMAS_fire2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_FAMASFire3Sound)
{
   filename    = "./Sounds/Fire/FAMAS/FAMAS_fire3.wav";
   description = MediumClose3D;
   preload = true;
};

// FAMAS
datablock DebrisData(BNE_FAMASMagDebris)
{
	shapeFile = "./FAMAS/famasmag.dts";
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
datablock ItemData(BNE_FAMASItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./FAMAS/famas.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: FAMAS";
	iconName = "./Icons/14";
	doColorShift = true;
	colorShiftColor = "0.35 0.35 0.35 1.000";

	 // Dynamic properties defined by the scripts
	image = BNE_FAMASImage;
	canDrop = true;

	AEAmmo = 24;
	AEType = AE_LightRAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 900;
	recoil = "Medium"; 
	uiColor = "1 1 1";
	description = "The FAMAS is a fast-firing French bullpup assault rifle, mostly replaced in service by the HK-416.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_FAMASImage)
{
   // Basic Item properties
   shapeFile = "./FAMAS/famas.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 -0.1 0";
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
   item = BNE_FAMASItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BERifleShellDebris;
   shellExitDir        = "1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_FAMASBipodImage;
    scopingImage = BNE_FAMASIronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_FAMASItem.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellRifle;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 30;
	projectileCount = 1;
	projectileHeadshotMult = 1.9;
	projectileVelocity = 400;
	projectileTagStrength = 0.35;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.38;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 18;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 350; // m
	spreadBase = 25;
	spreadMin = 100;
	spreadMax = 1000;

	screenshakeMin = "0.015 0.015 0.015"; 
	screenshakeMax = "0.25 0.25 0.25";

	farShotSound = RifleBDistantSound;
	farShotDistance = 40;
	
	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	projectileFalloffStart = $ae_falloffRifleStart;
	projectileFalloffEnd = $ae_falloffRifleEnd;
	projectileFalloffDamage = $ae_falloffRifle;

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
// please pay no attention to the state spam =3
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
	
	stateName[4]				= "FireLoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.05;
	stateTransitionOnTimeout[4]		= "FireLoadCheckB";
	
	stateName[5]				= "FireLoadCheckB";
	stateTransitionOnAmmo[5]		= "preFire2";
	stateTransitionOnNoAmmo[5]		= "Reload2";
	stateTransitionOnNotLoaded[5]  = "Ready";
// fire 2
	stateName[6]                       = "preFire2";
	stateTransitionOnTimeout[6]        = "Fire2";
	stateScript[6]                     = "AEOnFire";
	stateEmitter[6]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[6]				= 0.05;
	stateEmitterNode[6]				= "muzzlePoint";
	stateFire[6]                       = true;
	stateEjectShell[6]                       = true;

	stateName[7]                    = "Fire2";
	stateTransitionOnTimeout[7]     = "FireLoadCheckA2";
	stateEmitter[7]					= AEBaseSmokeEmitter;
	stateEmitterTime[7]				= 0.05;
	stateEmitterNode[7]				= "muzzlePoint";
	stateAllowImageChange[7]        = false;
	stateSequence[7]                = "Fire";
	stateWaitForTimeout[7]			= true;
	
	stateName[8]				= "FireLoadCheckA2";
	stateScript[8]				= "AEMagLoadCheck";
	stateTimeoutValue[8]			= 0.05;
	stateTransitionOnTimeout[8]		= "FireLoadCheckB2";
	
	stateName[9]				= "FireLoadCheckB2";
	stateTransitionOnAmmo[9]		= "preFire3";
	stateTransitionOnNoAmmo[9]		= "Reload2";
	stateTransitionOnNotLoaded[9]  = "Ready";
// fire 3
	stateName[10]                       = "preFire3";
	stateTransitionOnTimeout[10]        = "Fire3";
	stateScript[10]                     = "AEOnFire";
	stateEmitter[10]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[10]				= 0.05;
	stateEmitterNode[10]				= "muzzlePoint";
	stateFire[10]                       = true;
	stateEjectShell[10]                       = true;

	stateName[11]                    = "Fire3";
	stateTransitionOnTimeout[11]     = "FireLoadCheckA3";
	stateEmitter[11]					= AEBaseSmokeEmitter;
	stateEmitterTime[11]				= 0.05;
	stateEmitterNode[11]				= "muzzlePoint";
	stateAllowImageChange[11]        = false;
	stateSequence[11]                = "Fire";
	stateWaitForTimeout[11]			= true;
	
	stateName[12]				= "FireLoadCheckA3";
	stateScript[12]				= "AEMagLoadCheck";
	stateTimeoutValue[12]			= 0.3;
	stateTransitionOnTimeout[12]		= "FireLoadCheckB3";
	
	stateName[13]				= "FireLoadCheckB3";
	stateTransitionOnAmmo[13]		= "Ready";
	stateTransitionOnNoAmmo[13]		= "Reload2";	
	stateTransitionOnNotLoaded[13]  = "Ready";

	stateName[14]				= "LoadCheckA";
	stateScript[14]				= "AEMagLoadCheck";
	stateTimeoutValue[14]			= 0.1;
	stateTransitionOnTimeout[14]		= "LoadCheckB";
	
	stateName[15]				= "LoadCheckB";
	stateTransitionOnAmmo[15]		= "Ready";
	stateTransitionOnNotLoaded[15] = "Empty";
	stateTransitionOnNoAmmo[15]		= "Reload2";

	stateName[16]				= "Reload";
	stateTimeoutValue[16]			= 0.25;
	stateScript[16]				= "onReloadStart";
	stateTransitionOnTimeout[16]		= "ReloadMagOut";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "ReloadStart";
	
	stateName[17]				= "ReloadMagOut";
	stateTimeoutValue[17]			= 0.5;
	stateScript[17]				= "onReloadMagOut";
	stateTransitionOnTimeout[17]		= "ReloadMagIn";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagOut";
	stateSound[17]				= BNE_FAMASMagOutSound;
	
	stateName[18]				= "ReloadMagIn";
	stateTimeoutValue[18]			= 0.35;
	stateScript[18]				= "onReloadMagIn";
	stateTransitionOnTimeout[18]		= "ReloadEnd";
	stateWaitForTimeout[18]			= true;
	stateSequence[18]			= "MagIn";
	stateSound[18]				= BNE_FAMASMagInSound;
	
	stateName[19]				= "ReloadEnd";
	stateTimeoutValue[19]			= 0.25;
	stateScript[19]				= "onReloadEnd";
	stateTransitionOnTimeout[19]		= "Ready";
	stateWaitForTimeout[19]			= true;
	stateSequence[19]			= "ReloadEnd";
		
	stateName[20]				= "Reloaded";
	stateTimeoutValue[20]			= 0.1;
	stateScript[20]				= "AEMagReloadAll";
	stateTransitionOnTimeout[20]		= "Ready";

// EMPTY RELOAD STATE

	stateName[21]				= "Reload2";
	stateTimeoutValue[21]			= 0.25;
	stateScript[21]				= "onReload2Start";
	stateTransitionOnTimeout[21]		= "Reload2MagOut";
	stateWaitForTimeout[21]			= true;
	stateSequence[21]			= "ReloadStart";
	
	stateName[22]				= "Reload2MagOut";
	stateTimeoutValue[22]			= 0.5;
	stateScript[22]				= "onReload2MagOut";
	stateTransitionOnTimeout[22]		= "Reload2MagIn";
	stateWaitForTimeout[22]			= true;
	stateSequence[22]			= "MagOut";
	stateSound[22]				= BNE_FAMASMagOutSound;
	
	stateName[28]				= "Reload2MagIn";
	stateTimeoutValue[28]			= 0.35;
	stateScript[28]				= "onReload2MagIn";
	stateTransitionOnTimeout[28]		= "Reload2Bolt";
	stateWaitForTimeout[28]			= true;
	stateSequence[28]			= "MagIn";
	stateSound[28]				= BNE_FAMASMagInSound;

	stateName[23]				= "Reload2Bolt";
	stateTimeoutValue[23]			= 0.35;
	stateScript[23]				= "onReload2Bolt";
	stateTransitionOnTimeout[23]		= "Reload2End";
	stateWaitForTimeout[23]			= true;
	stateSequence[23]			= "Bolt";
	stateSound[23]				= BNE_FAMASBoltSound;
	
	stateName[24]				= "Reload2End";
	stateTimeoutValue[24]			= 0.25;
	stateScript[24]				= "onReload2End";
	stateTransitionOnTimeout[24]		= "Ready";
	stateWaitForTimeout[24]			= true;
	stateSequence[24]			= "ReloadEnd";
	
	stateName[25]				= "ReadyLoop";
	stateTransitionOnTimeout[25]		= "Ready";

	stateName[26]          = "Empty";
	stateTransitionOnTriggerDown[26]  = "Dryfire";
	stateTransitionOnLoaded[26] = "Reload2";
	stateScript[26]        = "AEOnEmpty";

	stateName[27]           = "Dryfire";
	stateTransitionOnTriggerUp[27] = "Empty";
	stateWaitForTimeout[27]    = false;
	stateScript[27]      = "onDryFire";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_FAMASImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_FAMASFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_FAMASImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// MAGAZINE DROPPING

function BNE_FAMASImage::onReload2MagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(3, shiftright);
   %obj.reload3Schedule = %this.schedule(0,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(200,300),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_FAMASImage::onReloadMagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(3, shiftright);
   %obj.reload3Schedule = %this.schedule(0,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(200,300),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_FAMASImage::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "shiftleft");
   %obj.schedule(50, "aeplayThread", "3", "plant");
}

function BNE_FAMASImage::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "shiftleft");
   %obj.schedule(50, "aeplayThread", "3", "plant");
}

function BNE_FAMASImage::onReloadStart(%this,%obj,%slot)
{
  %obj.aeplayThread(2, plant);
}

function BNE_FAMASImage::onReload2Start(%this,%obj,%slot)
{
  %obj.aeplayThread(2, plant);
}

function BNE_FAMASImage::onReload2Bolt(%this,%obj,%slot)
{
	%obj.aeplayThread("3", "shiftleft");
	%obj.schedule(60, "aeplayThread", "2", "plant");
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_FAMASImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_FAMASImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_FAMASImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FAMASImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_FAMASImage::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_FAMASMagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_FAMASMagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "0.05 -0.25 -0.1";
   rotation = eulerToMatrix( "40 25 0" );	
	
	casing = BNE_FAMASMagDebris;
	shellExitDir        = "-0.05 0 -0.25";
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

function BNE_FAMASMagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_FAMASIronsightImage : BNE_FAMASImage)
{
	recoilHeight = 0.1;

	scopingImage = BNE_FAMASImage;
	sourceImage = BNE_FAMASImage;
	
	offset = "0 0 0";
	eyeOffset = "0.004 1.0 -1.215";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;
   
	stateName[21]				= "Reload2";
	stateScript[21]				= "onDone";
	stateTimeoutValue[21]			= 1;
	stateTransitionOnTimeout[21]		= "";
	stateSound[21]				= "";
	
	stateName[16]				= "Reload";
	stateScript[16]				= "onDone";
	stateTimeoutValue[16]			= 1;
	stateTransitionOnTimeout[16]		= "";
	stateSound[16]				= "";
};

function BNE_FAMASIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_FAMASIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_FAMASIronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_FAMASFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_FAMASIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_FAMASIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FAMASIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}

//ALT BIPOD STATES

datablock ShapeBaseImageData(BNE_FAMASBipodImage : BNE_FAMASImage)
{
    shapeFile = "./FAMAS/FAMASBipod.dts";
	safetyImage = BNE_FAMASImage;
    scopingImage = BNE_FAMASIronsightBipodImage;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_FAMASBipodImage::AEOnFire(%this,%obj,%slot)
{	
	BNE_FAMASImage::AEOnFire(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_FAMASImage::onDryFire(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_FAMASBipodImage::onReload2MagOut(%this,%obj,%slot)
{
	BNE_FAMASImage::onReload2MagOut(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onReloadMagOut(%this,%obj,%slot)
{
	BNE_FAMASImage::onReloadMagOut(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onReloadMagIn(%this,%obj,%slot)
{
	BNE_FAMASImage::onReloadMagIn(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onReload2MagIn(%this,%obj,%slot)
{
	BNE_FAMASImage::onReload2MagIn(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onReloadStart(%this,%obj,%slot)
{
	BNE_FAMASImage::onReloadStart(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onReload2Start(%this,%obj,%slot)
{
	BNE_FAMASImage::onReload2Start(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onReload2Bolt(%this,%obj,%slot)
{
	BNE_FAMASImage::onReload2Bolt(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onReloadEnd(%this,%obj,%slot)
{
	BNE_FAMASImage::onReloadEnd(%this, %obj, %slot);
}

function BNE_FAMASBipodImage::onReady(%this,%obj,%slot)
{
	BNE_FAMASImage::onReady(%this, %obj, %slot);
}

// HIDES ALL HAND NODES

function BNE_FAMASBipodImage::onMount(%this,%obj,%slot)
{
	BNE_FAMASImage::onMount(%this, %obj, %slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FAMASBipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_FAMASImage::onUnMount(%this, %obj, %slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_FAMASBipodImage::onMagDrop(%this,%obj,%slot)
{
	BNE_FAMASImage::onMagDrop(%this, %obj, %slot);
}

datablock ShapeBaseImageData(BNE_FAMASIronsightBipodImage : BNE_FAMASIronsightImage)
{
    shapeFile = "./FAMAS/FAMASBipod.dts";
	sourceImage = BNE_FAMASBipodImage;
	scopingImage = BNE_FAMASBipodImage;
	safetyImage = BNE_FAMASImage;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_FAMASIronsightBipodImage::onDone(%this,%obj,%slot)
{
	BNE_FAMASIronsightImage::onDone(%this, %obj, %slot);
}

function BNE_FAMASIronsightBipodImage::onReady(%this,%obj,%slot)
{
	BNE_FAMASIronsightImage::onReady(%this, %obj, %slot);
}

function BNE_FAMASIronsightBipodImage::AEOnFire(%this,%obj,%slot)
{	
	BNE_FAMASIronsightImage::AEOnFire(%this, %obj, %slot);
}

function BNE_FAMASIronsightBipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_FAMASIronsightImage::onDryFire(%this, %obj, %slot);
}

// HIDES ALL HAND NODES

function BNE_FAMASIronsightBipodImage::onMount(%this,%obj,%slot)
{
	BNE_FAMASIronsightImage::onMount(%this, %obj, %slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_FAMASIronsightBipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_FAMASIronsightImage::onUnMount(%this, %obj, %slot);
}

datablock AudioProfile(AUGFire1Sound)
{
   filename    = "./Sounds/Fire/AUG/AUG_fire1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(AUGFire2Sound)
{
   filename    = "./Sounds/Fire/AUG/AUG_fire2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(AUGFire3Sound)
{
   filename    = "./Sounds/Fire/AUG/AUG_fire3.wav";
   description = MediumClose3D;
   preload = true;
};

// AUG
datablock DebrisData(AEAUGMagDebris)
{
	shapeFile = "./AUG/AUGMag.dts";
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
datablock ItemData(AUGItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./AUG/AUG.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: AUG A2";
	iconName = "./Icons/11";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = AUGImage;
	canDrop = true;

	AEAmmo = 30;
	AEType = AE_LightRAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 680;
	recoil = "Low"; 
	uiColor = "1 1 1";
	description = "The AUG is a powerful and accurate Austrian bullpup assault rifle chambered in 5.56mm NATO.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(AUGImage)
{
   // Basic Item properties
   shapeFile = "./AUG/AUG.dts";
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
   item = AUGItem;
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
	safetyImage = AUGSafetyImage;
    scopingImage = AUGIronsightImage;
	doColorShift = true;
	colorShiftColor = AUGItem.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellRifle;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 27;
	projectileCount = 1;
	projectileHeadshotMult = 1.6;
	projectileVelocity = 400;
	projectileTagStrength = 0.35;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.28;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 250; // m
	spreadBase = 20;
	spreadMin = 30;
	spreadMax = 300;

	screenshakeMin = "0.015 0.015 0.015"; 
	screenshakeMax = "0.1 0.1 0.1"; 

	farShotSound = RifleCDistantSound;
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
	
	projectileFalloffStart = 64;
	projectileFalloffEnd = 192;
	projectileFalloffDamage = 0.45;
	
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
	
	stateName[4]				= "FireLoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.095;
	stateTransitionOnTimeout[4]		= "FireLoadCheckB";
	
	stateName[5]				= "FireLoadCheckB";
	stateTransitionOnAmmo[5]		= "Ready";
	stateTransitionOnNoAmmo[5]		= "Reload2";
	stateTransitionOnNotLoaded[5]  = "Ready";

	stateName[14]				= "LoadCheckA";
	stateScript[14]				= "AEMagLoadCheck";
	stateTimeoutValue[14]			= 0.1;
	stateTransitionOnTimeout[14]		= "LoadCheckB";
	
	stateName[15]				= "LoadCheckB";
	stateTransitionOnAmmo[15]		= "Ready";
	stateTransitionOnNotLoaded[15] = "Empty";
	stateTransitionOnNoAmmo[15]		= "Reload2";

	stateName[16]				= "Reload";
	stateTimeoutValue[16]			= 0.3;
	stateScript[16]				= "onReloadStart";
	stateTransitionOnTimeout[16]		= "ReloadMagOut";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "ReloadStart";
	
	stateName[17]				= "ReloadMagOut";
	stateTimeoutValue[17]			= 0.65;
	stateScript[17]				= "onReloadMagOut";
	stateTransitionOnTimeout[17]		= "ReloadMagIn";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagOut";
	stateSound[17]				= AUGMagOutSound;
	
	stateName[18]				= "ReloadMagIn";
	stateTimeoutValue[18]			= 0.3;
	stateScript[18]				= "onReloadMagIn";
	stateTransitionOnTimeout[18]		= "ReloadEnd";
	stateWaitForTimeout[18]			= true;
	stateSequence[18]			= "MagIn";
	stateSound[18]				= AUGMagInSound;
	
	stateName[19]				= "ReloadEnd";
	stateTimeoutValue[19]			= 0.35;
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
	stateTimeoutValue[21]			= 0.5;
	stateScript[21]				= "onReload2Start";
	stateTransitionOnTimeout[21]		= "Reload2MagOut";
	stateWaitForTimeout[21]			= true;
	stateSequence[21]			= "ReloadStartEmpty";
	stateSound[21]				= AUGBoltLockSound;
	
	stateName[22]				= "Reload2MagOut";
	stateTimeoutValue[22]			= 0.65;
	stateScript[22]				= "onReload2MagOut";
	stateTransitionOnTimeout[22]		= "Reload2MagIn";
	stateWaitForTimeout[22]			= true;
	stateSequence[22]			= "MagOutEmpty";
	stateSound[22]				= AUGMagOutSound;
	
	stateName[23]				= "Reload2MagIn";
	stateTimeoutValue[23]			= 0.3;
	stateScript[23]				= "onReload2MagIn";
	stateTransitionOnTimeout[23]		= "Reload2End";
	stateWaitForTimeout[23]			= true;
	stateSequence[23]			= "MagInEmpty";
	stateSound[23]				= AUGMagInSound;
	
	stateName[24]				= "Reload2End";
	stateTimeoutValue[24]			= 0.5;
	stateScript[24]				= "onReload2End";
	stateTransitionOnTimeout[24]		= "Ready";
	stateWaitForTimeout[24]			= true;
	stateSequence[24]			= "ReloadEndEmpty";
	
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

function AUGImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, AUGFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function AUGImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function AUGImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function AUGImage::onReload2End(%this,%obj,%slot)
{
    %obj.schedule(300, playAudio, 1, AUGBoltSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function AUGImage::onReload2MagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(3, shiftright);
   %obj.reload3Schedule = %this.schedule(0,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(200,300),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function AUGImage::onReloadMagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(3, shiftright);
   %obj.reload3Schedule = %this.schedule(0,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(200,300),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function AUGImage::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
}

function AUGImage::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
   %obj.schedule(500, "aeplayThread", "2", "shiftleft");
   %obj.schedule(600, "aeplayThread", "3", "plant");
}

function AUGImage::onReloadStart(%this,%obj,%slot)
{
  %obj.aeplayThread(2, plant);
}

function AUGImage::onReload2Start(%this,%obj,%slot)
{
  %obj.aeplayThread(2, plant);
  %obj.aeplayThread(3, shiftright);
}

function AUGImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function AUGImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function AUGImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function AUGImage::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(AUGMagImage,0);
	%a.schedule(1000,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(AUGMagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "0.05 -0.1 -0.1";
   rotation = eulerToMatrix( "40 25 0" );	
	
	casing = AEAUGMagDebris;
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

function AUGMagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(AUGSafetyImage)
{
   shapeFile = "./AUG/AUG.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = AUGItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = AUGImage;
   doColorShift = true;
   colorShiftColor = AUGItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function AUGSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function AUGSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(AUGIronsightImage : AUGImage)
{
	recoilHeight = 0.07;

	scopingImage = AUGImage;
	sourceImage = AUGImage;
	
	offset = "0 0 0";
	eyeOffset = "-0.011 1.0 -1.275";
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

function AUGIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function AUGIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function AUGIronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, AUGFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function AUGIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function AUGIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function AUGIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
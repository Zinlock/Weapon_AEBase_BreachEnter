datablock AudioProfile(L96Fire1Sound)
{
   filename    = "./Sounds/Fire/L96/L96_FIRE_1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(L96Fire2Sound)
{
   filename    = "./Sounds/Fire/L96/L96_FIRE_2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(L96Fire3Sound)
{
   filename    = "./Sounds/Fire/L96/L96_FIRE_3.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(L96Fire4Sound)
{
   filename    = "./Sounds/Fire/L96/L96_FIRE_4.wav";
   description = MediumClose3D;
   preload = true;
};

// L96
datablock DebrisData(AEL96MagDebris)
{
	shapeFile = "./L96/L96Mag.dts";
	lifetime = 2.0;
	minSpinSpeed = -200.0;
	maxSpinSpeed = -100.0;
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
datablock ItemData(L96Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./L96/L96.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: L96";
	iconName = "./Icons/22";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = L96Image;
	canDrop = true;

	AEAmmo = 5;
	AEType = AE_MediumSRAmmoItem.getID();
	AEBase = 1;

    RPM = 60;
    Recoil = "Medium";
	uiColor = "1 1 1";
    description = "The L96 is a British bolt-action arctic warfare sniper rifle.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(L96Image)
{
   // Basic Item properties
   shapeFile = "./L96/L96.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
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
   item = L96Item;
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
   safetyImage = L96BipodImage;
   scopingImage = L96IronsightImage;
   doColorShift = true;
   colorShiftColor = L96Item.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 45;
	projectileCount = 1;
	projectileHeadshotMult = 3.1;
	projectileVelocity = 750;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.8;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 150; // m
	spreadBase = 750;
	spreadMin = 750;
	spreadMax = 750;

	gunType = "Sniper";

	screenshakeMin = "0.15 0.15 0.15";
	screenshakeMax = "9999 9999 9999"; //trollface

	farShotSound = SniperCDistantSound;
	farShotDistance = 40;

	sonicWhizz = true;
	whizzSupersonic = 2;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	staticHitscan = true;
	staticEffectiveRange = 260;
	staticTotalRange = 2000;
	staticGravityScale = 1;
	staticSwayMod = 0;
	staticEffectiveSpeedBonus = 0;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = ""; // defaults to AEBulletStaticShape
	staticScaleCalibre = 0.35;
	staticScaleLength = 0.35;
	staticUnitsPerSecond = $ae_SniperUPS;


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
	stateTimeoutValue[3]            	= 0.4;
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;

	stateName[4]                    	= "Bolt";
	stateTimeoutValue[4]            	= 0.6;
	stateScript[4]                  	= "onBolt";
	stateTransitionOnTimeout[4]     	= "FireLoadCheckA";
	stateAllowImageChange[4]        	= false;
	stateSequence[4]			= "Bolt";
	stateWaitForTimeout[4]		  	= true;
	stateEjectShell[4]                = true;

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
	stateTimeoutValue[8]			= 0.65;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "MagOut";
	stateSound[8]				= L96MagOutSound;

	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.5;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= L96MagInSound;

	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.25;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";

	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.1;
	stateTransitionOnTriggerUp[11]		= "FireLoadCheckB";

	stateName[12]				= "FireLoadCheckB";
	stateTransitionOnAmmo[12]		= "Ready";
	stateTransitionOnNoAmmo[12]		= "Reload2";
	stateTransitionOnNotLoaded[12]  = "Ready";

	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";

// EMPTY RELOAD STATE

	stateName[15]				= "Reload2";
	stateTimeoutValue[15]			= 0.4;
	stateScript[15]				= "onReload2Start";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "ReloadStartEmpty";
	stateEjectShell[15]                = true;
	stateSound[15]				= L96BoltOpenSound;

	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 0.65;
	stateScript[16]				= "onReload2MagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "MagOutEmpty";
	stateSound[16]				= L96MagOutSound;

	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.5;
	stateScript[17]				= "onReload2MagIn";
	stateTransitionOnTimeout[17]		= "Reload2End";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagInEmpty";
	stateSound[17]				= L96MagInSound;

	stateName[19]				= "Reload2End";
	stateTimeoutValue[19]			= 0.5;
	stateScript[19]				= "onReload2End";
	stateTransitionOnTimeout[19]		= "Ready";
	stateWaitForTimeout[19]			= true;
	stateSequence[19]			= "ReloadEndEmpty";
	stateSound[19]				= L96BoltCloseSound;

	stateName[20]				= "BoltCheck";
	stateTransitionOnNoAmmo[20]		= "EmptyCheckA";
	stateTransitionOnTimeout[20]		= "Bolt";

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "Reload2";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";

	stateName[23]				= "EmptyCheckA";
	stateTimeoutValue[23]			= 0.03;
	stateScript[23]				= "AEMagLoadCheck";
	stateTransitionOnTimeout[23]		= "EmptyCheckB";

	stateName[24]				= "EmptyCheckB";
	stateTransitionOnNotLoaded[24]  = "Empty";
	stateTransitionOnTimeout[24]		= "FireLoadCheckA";
	
	stateName[25]           = "SemiAutoCheck"; //heeeeeeeeeeeeey
	stateTransitionOnTriggerUp[25]	  	= "BoltCheck";
};

function L96Image::AEOnFire(%this,%obj,%slot)
{
	%obj.stopAudio(0);
  %obj.playAudio(0, L96Fire @ getRandom(1, 4) @ Sound);

	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function L96Image::AEOnLowClimb(%this, %obj, %slot)
{
   %obj.aeplayThread(2, plant);
}


function L96Image::onBolt(%this,%obj,%slot)
{
    %obj.schedule(0, playAudio, 1, L96BoltOpenSound);
    %obj.schedule(300, playAudio, 1, L96BoltCloseSound);
	%obj.aeplayThread(2, shiftleft);
	schedule(500, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
}
function L96Image::onReload2MagOut(%this,%obj,%slot)
{
  %obj.aeplayThread(2, shiftright);
   %obj.aeplayThread(3, plant);
}

function L96Image::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread(2, shiftright);
   %obj.aeplayThread(3, plant);
}

function L96Image::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(75, "aeplayThread", "2", "shiftright");
   %obj.schedule(75, "aeplayThread", "3", "plant");
}

function L96Image::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(75, "aeplayThread", "2", "shiftright");
   %obj.schedule(75, "aeplayThread", "3", "plant");
}

function L96Image::onReloadStart(%this,%obj,%slot)
{
  %obj.reload3Schedule = %this.schedule(215,onMagDrop,%obj,%slot);
  %obj.reload4Schedule = schedule(getRandom(500,600),0,serverPlay3D,AEMagMetalAr @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function L96Image::onReload2Start(%this,%obj,%slot)
{
  %obj.reload3Schedule = %this.schedule(400,onMagDrop,%obj,%slot);
  %obj.reload4Schedule = schedule(getRandom(700,800),0,serverPlay3D,AEMagMetalAr @ getRandom(1,3) @ Sound,%obj.getPosition());
	schedule(500, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
  %obj.aeplayThread(2, shiftleft);
}

function L96Image::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function L96Image::onReload2End(%this,%obj,%slot)
{
    %obj.aeplayThread(2, shiftright);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function L96Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function L96Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 200)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function L96Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function L96Image::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function L96Image::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(L96MagImage,0);
	%a.schedule(1000,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(L96MagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "0.05 0.65 0.15";
   rotation = eulerToMatrix( "0 15 0" );

	casing = AEL96MagDebris;
	shellExitDir        = "-0.25 0.25 -0.75";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;
	shellVelocity       = 3.0;

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

function L96MagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

datablock ShapeBaseImageData(L96IronsightImage : L96Image)
{
	recoilHeight = 1;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;


	scopingImage = L96Image;
	sourceImage = L96Image;

	offset = "0 0 0";
	eyeOffset = "0.0039 1.54 -0.9291";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_HighScopeFOV;
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

function L96IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function L96IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function L96IronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0);
	%obj.playAudio(0, L96Fire @ getRandom(1, 4) @ Sound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function L96IronsightImage::onBolt(%this,%obj,%slot)
{
    %obj.schedule(0, playAudio, 1, L96BoltOpenSound);
    %obj.schedule(300, playAudio, 1, L96BoltCloseSound);
	%obj.aeplayThread(2, plant);
	schedule(500, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
}

function L96IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function L96IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);

	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn6Sound);

	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function L96IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);

	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}

//ALT BIPOD STATES

datablock ShapeBaseImageData(L96BipodImage : L96Image)
{
    shapeFile = "./L96/L96Bipod.dts";
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;
	safetyImage = L96Image;
    scopingImage = L96IronsightBipodImage;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function L96BipodImage::AEOnFire(%this,%obj,%slot)
{
	L96Image::AEOnFire(%this, %obj, %slot);
}

function L96BipodImage::AEOnLowClimb(%this, %obj, %slot)
{
	L96Image::AEOnLowClimb(%this, %obj, %slot);
}

function L96BipodImage::onBolt(%this,%obj,%slot)
{
	L96Image::onBolt(%this, %obj, %slot);
}
function L96BipodImage::onReload2MagOut(%this,%obj,%slot)
{
	L96Image::onReload2MagOut(%this, %obj, %slot);
}

function L96BipodImage::onReloadMagOut(%this,%obj,%slot)
{
	L96Image::onReloadMagOut(%this, %obj, %slot);
}

function L96BipodImage::onReloadMagIn(%this,%obj,%slot)
{
	L96Image::onReloadMagIn(%this, %obj, %slot);
}

function L96BipodImage::onReload2MagIn(%this,%obj,%slot)
{
	L96Image::onReload2MagIn(%this, %obj, %slot);
}

function L96BipodImage::onReloadStart(%this,%obj,%slot)
{
	L96Image::onReloadStart(%this, %obj, %slot);
}

function L96BipodImage::onReload2Start(%this,%obj,%slot)
{
	L96Image::onReload2Start(%this, %obj, %slot);
}

function L96BipodImage::onReloadEnd(%this,%obj,%slot)
{
	L96Image::onReloadEnd(%this, %obj, %slot);
}

function L96BipodImage::onReload2End(%this,%obj,%slot)
{
	L96Image::onReload2End(%this, %obj, %slot);
}

function L96BipodImage::onDryFire(%this, %obj, %slot)
{
	L96Image::onDryFire(%this, %obj, %slot);
}

function L96BipodImage::onReady(%this,%obj,%slot)
{
	L96Image::onReady(%this, %obj, %slot);
}

function L96BipodImage::onMount(%this,%obj,%slot)
{
	L96Image::onMount(%this, %obj, %slot);
}

function L96BipodImage::onUnMount(%this, %obj, %slot)
{
	L96Image::onUnMount(%this, %obj, %slot);
}

function L96BipodImage::onMagDrop(%this,%obj,%slot)
{
	L96Image::onMagDrop(%this, %obj, %slot);
}

datablock ShapeBaseImageData(L96IronsightBipodImage : L96IronsightImage)
{
    shapeFile = "./L96/L96Bipod.dts";
	sourceImage = L96BipodImage;
	scopingImage = L96BipodImage;
	safetyImage = L96Image;
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

function L96IronsightBipodImage::onDone(%this,%obj,%slot)
{
	L96IronsightImage::onDone(%this, %obj, %slot);
}

function L96IronsightBipodImage::onReady(%this,%obj,%slot)
{
	L96IronsightImage::onReady(%this, %obj, %slot);
}

function L96IronsightBipodImage::AEOnFire(%this,%obj,%slot)
{
	L96IronsightImage::AEOnFire(%this, %obj, %slot);
}

function L96IronsightBipodImage::onBolt(%this,%obj,%slot)
{
	L96IronsightImage::onBolt(%this, %obj, %slot);
}

function L96IronsightBipodImage::onDryFire(%this, %obj, %slot)
{
	L96IronsightImage::onDryFire(%this, %obj, %slot);
}

// HIDES ALL HAND NODES

function L96IronsightBipodImage::onMount(%this,%obj,%slot)
{
	L96IronsightImage::onMount(%this, %obj, %slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function L96IronsightBipodImage::onUnMount(%this,%obj,%slot)
{
	L96IronsightImage::onUnMount(%this, %obj, %slot);
}
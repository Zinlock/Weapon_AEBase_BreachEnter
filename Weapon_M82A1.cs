datablock AudioProfile(BNE_M82A1Fire1Sound)
{
   filename    = "./Sounds/Fire/M82A1/M82A1_FIRE1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_M82A1Fire2Sound)
{
   filename    = "./Sounds/Fire/M82A1/M82A1_FIRE2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_M82A1Fire3Sound)
{
   filename    = "./Sounds/Fire/M82A1/M82A1_FIRE3.wav";
   description = HeavyClose3D;
   preload = true;
};

// M82A1
datablock DebrisData(BNE_M82A1MagDebris)
{
	shapeFile = "./M82A1/M82A1Mag.dts";
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
datablock ItemData(BNE_M82A1Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./M82A1/M82A1.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Barrett M82";
	iconName = "./Icons/Icon_M82A1";
	doColorShift = true;
	colorShiftColor = "0.82 0.8 0.8 1";

	 // Dynamic properties defined by the scripts
	image = BNE_M82A1Image;
	canDrop = true;

	AEAmmo = 5;
	AEType = AE_HeavySRAmmoItem.getID();
	AEBase = 1;

    RPM = 60;
    Recoil = "Medium";
	uiColor = "1 1 1";
    description = "MOM GET THE CAMERA WOOOOOOO 360 Noscope muddafucka!!1";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_M82A1Image)
{
   // Basic Item properties
   shapeFile = "./M82A1/M82A1.dts";
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
   item = BNE_M82A1Item;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BEFiftyShellDebris;
   shellExitDir        = "1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   safetyImage = BNE_M82A1BipodImage;
   scopingImage = BNE_M82A1IronsightImage;
   doColorShift = true;
   colorShiftColor = BNE_M82A1Item.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	gunType = "Sniper";

	shellSound = AEShellSniper;
	shellSoundMin = 450;
	shellSoundMax = 550;

	projectileDamage = 83;
	projectileCount = 1;
	projectileHeadshotMult = 2.25;
	projectileVelocity = 750;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate
	projectileVehicleDamageMult = 1;

	recoilHeight = 1.5;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 850; // m
	spreadBase = 600;
	spreadMin = 750;
	spreadMax = 900;

	screenshakeMin = "0.25 0.25 0.25";
	screenshakeMax = "0.65 0.65 0.65";

	farShotSound = SniperBDistantSound;
	farShotDistance = 40;

	sonicWhizz = true;
	whizzSupersonic = 2;
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
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	stateEjectShell[3]                = true;

	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";

	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload2";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.4;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";

	stateName[8]				= "ReloadMagOut";
	stateTimeoutValue[8]			= 1.0;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "MagOut";
	stateSound[8]				= BNE_M200MagOutSound; //BNE_M82A1MagOutSound;

	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.65;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= BNE_M200MagInSound; //BNE_M82A1MagInSound;

	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.5;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";

	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
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
	stateTimeoutValue[15]			= 0.5;
	stateScript[15]				= "onReload2Start";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "ReloadStart";

	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 1.0;
	stateScript[16]				= "onReload2MagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "MagOut";
	stateSound[16]				= BNE_M200MagOutSound; //BNE_M82A1MagOutSound;

	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.65;
	stateScript[17]				= "onReload2MagIn";
	stateTransitionOnTimeout[17]		= "Reload2End";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagIn";
	stateSound[17]				= BNE_M200MagInSound; //BNE_M82A1MagInSound;

	stateName[19]				= "Reload2End";
	stateTimeoutValue[19]			= 0.8;
	stateScript[19]				= "onReload2End";
	stateTransitionOnTimeout[19]		= "Ready";
	stateWaitForTimeout[19]			= true;
	stateSequence[19]			= "ReloadEndEmpty";

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
	stateTransitionOnTimeout[25]	  	= "FireLoadCheckA";
	stateTimeoutValue[11]			= 0.55;
};

function BNE_M82A1Image::AEOnFire(%this,%obj,%slot)
{
	%obj.stopAudio(0);
  %obj.playAudio(0, BNE_M82A1Fire @ getRandom(1, 3) @ Sound);

	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_M82A1Image::AEOnLowClimb(%this, %obj, %slot)
{
   %obj.aeplayThread(2, plant);
}

function BNE_M82A1Image::onReload2MagOut(%this,%obj,%slot)
{
	%this.onMagDrop(%obj,%slot);
	schedule(getRandom(250,350),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
  %obj.aeplayThread(2, shiftright);
	%obj.aeplayThread(3, plant);
}

function BNE_M82A1Image::onReloadMagOut(%this,%obj,%slot)
{
	%this.onMagDrop(%obj,%slot);
	schedule(getRandom(250,350),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
  %obj.aeplayThread(2, shiftright);
	%obj.aeplayThread(3, plant);
}

function BNE_M82A1Image::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(100, "aeplayThread", "2", "shiftright");
   %obj.schedule(100, "aeplayThread", "3", "plant");
}

function BNE_M82A1Image::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(100, "aeplayThread", "2", "shiftright");
   %obj.schedule(100, "aeplayThread", "3", "plant");
}

function BNE_M82A1Image::onReloadStart(%this,%obj,%slot)
{
  %obj.aeplayThread(2, shiftleft);
}

function BNE_M82A1Image::onReload2Start(%this,%obj,%slot)
{
  %obj.aeplayThread(2, shiftleft);
}

function BNE_M82A1Image::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_M82A1Image::onReload2End(%this,%obj,%slot)
{
  %obj.schedule(250, playAudio, 1, BNE_M82A1BoltSound);
  %obj.schedule(250, aeplayThread, 2, shiftleft);
	%obj.aeplayThread(2, shiftright);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_M82A1Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_M82A1Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 200)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function BNE_M82A1Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function BNE_M82A1Image::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_M82A1Image::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_M82A1MagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M82A1MagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.05 0.8 0.2";
   rotation = eulerToMatrix( "0 15 0" );

	casing = BNE_M82A1MagDebris;
	shellExitDir        = "-0.15 0.45 -0.95";
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

function BNE_M82A1MagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M82A1IronsightImage : BNE_M82A1Image)
{
	recoilHeight = 1.0;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;

	isScopedImage = true;

	scopingImage = BNE_M82A1Image;
	sourceImage = BNE_M82A1Image;

	offset = "0 0 0";
	eyeOffset = "-0.0007 0.87 -0.63385";
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

function BNE_M82A1IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_M82A1IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_M82A1IronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0);
	%obj.playAudio(0, BNE_M82A1Fire @ getRandom(1, 3) @ Sound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_M82A1IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_M82A1IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);

	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn6Sound); // %obj.getHackPosition());

	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_M82A1IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());

	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}

//ALT BIPOD STATES

datablock ShapeBaseImageData(BNE_M82A1BipodImage : BNE_M82A1Image)
{
    shapeFile = "./M82A1/M82A1Bipod.dts";
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;
	safetyImage = BNE_M82A1Image;
    scopingImage = BNE_M82A1IronsightBipodImage;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_M82A1BipodImage::AEOnFire(%this,%obj,%slot)
{
	BNE_M82A1Image::AEOnFire(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::AEOnLowClimb(%this, %obj, %slot)
{
	BNE_M82A1Image::AEOnLowClimb(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReload2MagOut(%this,%obj,%slot)
{
	BNE_M82A1Image::onReload2MagOut(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReloadMagOut(%this,%obj,%slot)
{
	BNE_M82A1Image::onReloadMagOut(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReloadMagIn(%this,%obj,%slot)
{
	BNE_M82A1Image::onReloadMagIn(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReload2MagIn(%this,%obj,%slot)
{
	BNE_M82A1Image::onReload2MagIn(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReloadStart(%this,%obj,%slot)
{
	BNE_M82A1Image::onReloadStart(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReload2Start(%this,%obj,%slot)
{
	BNE_M82A1Image::onReload2Start(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReloadEnd(%this,%obj,%slot)
{
	BNE_M82A1Image::onReloadEnd(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReload2End(%this,%obj,%slot)
{
	BNE_M82A1Image::onReload2End(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_M82A1Image::onDryFire(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onReady(%this,%obj,%slot)
{
	BNE_M82A1Image::onReady(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onMount(%this,%obj,%slot)
{
	BNE_M82A1Image::onMount(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onUnMount(%this, %obj, %slot)
{
	BNE_M82A1Image::onUnMount(%this, %obj, %slot);
}

function BNE_M82A1BipodImage::onMagDrop(%this,%obj,%slot)
{
	BNE_M82A1Image::onMagDrop(%this, %obj, %slot);
}

datablock ShapeBaseImageData(BNE_M82A1IronsightBipodImage : BNE_M82A1IronsightImage)
{
    shapeFile = "./M82A1/M82A1Bipod.dts";
	sourceImage = BNE_M82A1BipodImage;
	scopingImage = BNE_M82A1BipodImage;
	safetyImage = BNE_M82A1Image;
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

function BNE_M82A1IronsightBipodImage::onDone(%this,%obj,%slot)
{
	BNE_M82A1IronsightImage::onDone(%this, %obj, %slot);
}

function BNE_M82A1IronsightBipodImage::onReady(%this,%obj,%slot)
{
	BNE_M82A1IronsightImage::onReady(%this, %obj, %slot);
}

function BNE_M82A1IronsightBipodImage::AEOnFire(%this,%obj,%slot)
{
	BNE_M82A1IronsightImage::AEOnFire(%this, %obj, %slot);
}

function BNE_M82A1IronsightBipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_M82A1IronsightImage::onDryFire(%this, %obj, %slot);
}

function BNE_M82A1IronsightBipodImage::onMount(%this,%obj,%slot)
{
	BNE_M82A1IronsightImage::onMount(%this, %obj, %slot);
}

function BNE_M82A1IronsightBipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_M82A1IronsightImage::onUnMount(%this, %obj, %slot);
}
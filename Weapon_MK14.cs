datablock AudioProfile(BNE_MK14Fire1Sound)
{
   filename    = "./Sounds/Fire/MK14/MK14_FIRE_1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_MK14Fire2Sound)
{
   filename    = "./Sounds/Fire/MK14/MK14_FIRE_2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_MK14Fire3Sound)
{
   filename    = "./Sounds/Fire/MK14/MK14_FIRE_3.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_MK14Fire4Sound)
{
   filename    = "./Sounds/Fire/MK14/MK14_FIRE_4.wav";
   description = MediumClose3D;
   preload = true;
};

// MK14
datablock DebrisData(BNE_MK14MagDebris)
{
	shapeFile = "./MK14/MK14Mag.dts";
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
datablock ItemData(BNE_MK14Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./MK14/MK14.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: MK14 EBR";
	iconName = "./Icons/35";
	doColorShift = true;
	colorShiftColor = "0.3 0.3 0.3 1";

	 // Dynamic properties defined by the scripts
	image = BNE_MK14Image;
	canDrop = true;

	AEAmmo = 20;
	AEType = AE_HeavyRAmmoItem.getID();
	AEBase = 1;

	RPM = 700;
	recoil = "Heavy"; 
	uiColor = "1 1 1";
	description = "The Mk14 EBR is an American military battle rifle with high recoil and high damage." NL "7.62 Millimeter...Full, Metal, Jacket.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_MK14Image)
{
   // Basic Item properties
   shapeFile = "./MK14/MK14.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 -0.02";
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
   item = BNE_MK14Item;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BERifleShellDebris;
   shellExitDir        = "0.5 0 1";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_MK14SafetyImage;
    scopingImage = BNE_MK14IronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_MK14Item.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellRifle;
	shellSoundMin = 700; //min delay for when the shell sound plays
	shellSoundMax = 800; //max delay for when the shell sound plays

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 45;
	projectileCount = 1;
	projectileHeadshotMult = 1.76;
	projectileVelocity = 400;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.8;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 2; // how much shots it takes to trigger spread i think
	spreadReset = 250; // m
	spreadBase = 20;
	spreadMin = 60;
	spreadMax = 1000;

	screenshakeMin = "0.025 0.025 0.025"; 
	screenshakeMax = "0.075 0.075 0.075"; 

	farShotSound = RifleDDistantSound;
	farShotDistance = 40;
	
	projectileFalloffStart = $ae_falloffDMRStart;
	projectileFalloffEnd = $ae_falloffDMREnd;
	projectileFalloffDamage = $ae_falloffDMR;
	
	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	staticHitscan = true;
	staticEffectiveRange = 160;
	staticTotalRange = 2000;
	staticGravityScale = 1.5;
	staticSwayMod = 0;
	staticEffectiveSpeedBonus = 0;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = ""; // defaults to AEBulletStaticShape
	staticScaleCalibre = 0.25;
	staticScaleLength = 0.25;
	staticUnitsPerSecond = $ae_SniperUPS;

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
	stateTransitionOnNoAmmo[2]       	= "NoAmmoFlashFix";
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
	
	stateName[23]                    = "FireEmpty";
	stateTransitionOnTimeout[23]     = "FireLoadCheckA";
	stateEmitter[23]					= AEBaseSmokeEmitter;
	stateEmitterTime[23]				= 0.05;
	stateEmitterNode[23]				= "muzzlePoint";
	stateAllowImageChange[23]        = false;
	stateSequence[23]                = "FireEmpty";
	stateWaitForTimeout[23]			= true;
	
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
	stateSound[8]				= BNE_MK14MagOutSound;
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.35;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= BNE_MK14MagInSound;
	
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
	stateTimeoutValue[15]			= 0.25;
	stateScript[15]				= "onReloadStart";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "ReloadStartEmpty";
	
	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 0.65;
	stateScript[16]				= "onReload2MagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "MagOutEmpty";
	stateSound[16]				= BNE_MK14MagOutSound;
	
	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.35;
	stateScript[17]				= "onReload2MagIn";
	stateTransitionOnTimeout[17]		= "Reload2End";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagInEmpty";
	stateSound[17]				= BNE_MK14MagInSound;
	
	stateName[19]				= "Reload2End";
	stateTimeoutValue[19]			= 0.45;
	stateScript[19]				= "onReload2End";
	stateTransitionOnTimeout[19]		= "Ready";
	stateWaitForTimeout[19]			= true;
	stateSequence[19]			= "ReloadEndEmpty";
	//stateSound[19]				= AECock10Sound;
	
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
	
	stateName[30]           = "NoAmmoFlashFix";
	stateTransitionOnTimeout[30] = "FireEmpty";
	stateEmitter[30]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[30]				= 0.05;
	stateEmitterNode[30]				= "muzzlePoint";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_MK14Image::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_MK14Fire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_MK14Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}


function BNE_MK14Image::onReload2MagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(3, shiftright);
}

function BNE_MK14Image::onReloadMagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(3, shiftright);
}

function BNE_MK14Image::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
}

function BNE_MK14Image::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
   %obj.schedule(600, "aeplayThread", "3", "plant");
}

function BNE_MK14Image::onReloadStart(%this,%obj,%slot)
{
  %obj.aeplayThread(2, shiftleft);
  %obj.reload3Schedule = %this.schedule(215,onMagDrop,%obj,%slot);
  %obj.reload4Schedule = schedule(getRandom(500,600),0,serverPlay3D,AEMagMetalAr @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_MK14Image::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_MK14Image::onReload2End(%this,%obj,%slot)
{
    %obj.schedule(300, playAudio, 1, BNE_MK14BoltSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_MK14Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_MK14Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_MK14Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_MK14Image::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_MK14MagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_MK14MagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "0.07 0.75 0.25";
   rotation = eulerToMatrix( "40 25 0" );	
	
	casing = BNE_MK14MagDebris;
	shellExitDir        = "-0.25 0.25 -0.25";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;	
	shellVelocity       = 2.0;
	
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

function BNE_MK14MagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_MK14SafetyImage)
{
   shapeFile = "./MK14/MK14.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.02";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_MK14Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_MK14IronsightImage;
   safetyImage = BNE_MK14Image;
   doColorShift = true;
   colorShiftColor = BNE_MK14Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_MK14SafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_MK14SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_MK14SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_MK14IronsightImage : BNE_MK14Image)
{
	recoilHeight = 0.2;

	scopingImage = BNE_MK14Image;
	sourceImage = BNE_MK14Image;
	
   offset = "0 0 -0.02";
	eyeOffset = "0.005 1.25 -1.06";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_HighIronsFOV;
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

function BNE_MK14IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_MK14IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_MK14IronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_MK14Fire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_MK14IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_MK14IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_MK14IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
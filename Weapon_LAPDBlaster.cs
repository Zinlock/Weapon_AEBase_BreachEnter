datablock AudioProfile(BNE_LAPDBlasterFire1Sound)
{
   filename    = "./Sounds/Fire/LAPDBlaster/LAPDBlaster_fire1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_LAPDBlasterFire2Sound)
{
   filename    = "./Sounds/Fire/LAPDBlaster/LAPDBlaster_fire2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_LAPDBlasterFire3Sound)
{
   filename    = "./Sounds/Fire/LAPDBlaster/LAPDBlaster_fire3.wav";
   description = MediumClose3D;
   preload = true;
};

// LAPDBlaster
datablock DebrisData(BNE_LAPDBlasterLoaderDebris)
{
	shapeFile = "./LAPDBlaster/LAPDBlasterLoader.dts";
	lifetime = 2.0;
	minSpinSpeed = -200.0;
	maxSpinSpeed = -100.0;
	elasticity = 0.5;
	friction = 0.1;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 3;
};

//////////
// item //
//////////
datablock ItemData(BNE_LAPDBlasterItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./LAPDBlaster/LAPDBlaster.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: That Gun";
	iconName = "./Icons/54";
	doColorShift = true;
	colorShiftColor = "0.75 0.75 0.75 1";

	 // Dynamic properties defined by the scripts
	image = BNE_LAPDBlasterImage;
	canDrop = true;

	AEAmmo = 5;
	AEType = AE_LightRAmmoItem.getID();
	AEBase = 1;

	RPM = 600;
	recoil = "Medium"; 
	uiColor = "1 1 1";
	description = "The LAPDBlaster is a .45 ACP semi-auto, magazine-fed pistol made by Colt in the year 1911.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_LAPDBlasterImage)
{
   // Basic Item properties
   shapeFile = "./LAPDBlaster/LAPDBlaster.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.01 -0.02 0";
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
   item = BNE_LAPDBlasterItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BEPistolShellDebris;
   shellExitDir        = "0.5 0 1";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_LAPDBlasterSafetyImage;
    scopingImage = BNE_LAPDBlasterIronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_LAPDBlasterItem.colorShiftColor;//"0.400 0.196 0 1.000";

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 50;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 400;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 2;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 400; // m
	spreadBase = 250;
	spreadMin = 250;
	spreadMax = 1000;

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "0.6 0.6 0.6"; 

	farShotSound = RifleADistantSound;
	farShotDistance = 40;
	
	projectileFalloffStart = 16;
	projectileFalloffEnd = 64;
	projectileFalloffDamage = 0.73;
	
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

	laserSize = "1.25 1.25 1.25";
	laserColor = "0.1 1.0 0.1 1";
	laserDistance = 50;
	laserOffStates = "Reload ReloadExtract ReloadIn ReloadWaitA ReloadWaitB";
  laserFade = 16;

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
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[4]				= "LoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.1;
	stateTransitionOnTimeout[4]		= "LoadCheckB";
	
	stateName[5]				= "LoadCheckB";
	stateTransitionOnAmmo[5]		= "Ready";
	stateTransitionOnNotLoaded[5] = "Empty";
	stateTransitionOnNoAmmo[5]		= "Reload";

	stateName[6]				= "Reload";
	stateTimeoutValue[6]			= 0.5;
	stateScript[6]				= "onReloadStart";
	stateTransitionOnTimeout[6]		= "ReloadExtract";
	stateWaitForTimeout[6]			= true;
	stateSequence[6]			= "ReloadStart";
	stateSound[6]				= BNE_LAPDBlasterOpenSound;
	
	stateName[7]			  	= "ReloadExtract";
	stateScript[7]				= "onReloadExtract";
	stateTransitionOnTimeout[7]	  	= "ReloadWaitA";
	stateTimeoutValue[7]		  	= 0.15;
	stateWaitForTimeout[7]		  	= false;
	stateSequence[7]			= "ReloadOut";
	stateSound[7]				= BNE_LAPDBlasterOutSound;
	
	stateName[8]                     	= "ReloadWaitA";
	stateTimeoutValue[8]             	= 0.25;
	stateTransitionOnTimeout[8]       	= "ReloadIn";
	
	stateName[9]				= "ReloadIn";
	stateTimeoutValue[9]			= 0.25;
	stateScript[9]				= "onReloadIn";
	stateTransitionOnTimeout[9]		= "ReloadWaitB";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "ReloadIn";
	stateSound[9]				= BNE_LAPDBlasterInSound;
	
	stateName[10]                     	= "ReloadWaitB";
	stateTimeoutValue[10]             	= 0.1;
	stateTransitionOnTimeout[10]       	= "ReloadEnd";
	
	stateName[11]				= "ReloadEnd";
	stateTimeoutValue[11]			= 0.5;
	stateScript[11]				= "onReloadEnd";
	stateTransitionOnTimeout[11]		= "Reloaded";
	stateWaitForTimeout[11]			= true;
	stateSequence[11]			= "ReloadEnd";
	stateSound[11]				= BNE_LAPDBlasterCloseSound;
	
	stateName[12]				= "FireLoadCheckA";
	stateScript[12]				= "AEMagLoadCheck";
	stateTimeoutValue[12]			= 0.35;
	stateTransitionOnTriggerUp[12]		= "FireLoadCheckB";
	
	stateName[13]				= "FireLoadCheckB";
	stateTransitionOnAmmo[13]		= "Ready";
	stateTransitionOnNoAmmo[13]		= "Reload";	
	stateTransitionOnNotLoaded[13]  = "Ready";
		
	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";
	
	stateName[15]				= "ReadyLoop";
	stateTransitionOnTimeout[15]		= "Ready";

	stateName[16]          = "Empty";
	stateTransitionOnTriggerDown[16]  = "Dryfire";
	stateTransitionOnLoaded[16] = "Reload";
	stateScript[16]        = "AEOnEmpty";

	stateName[17]           = "Dryfire";
	stateTransitionOnTriggerUp[17] = "Empty";
	stateWaitForTimeout[17]    = false;
	stateScript[17]      = "onDryFire";
	
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_LAPDBlasterImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_LAPDBlasterFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_LAPDBlasterImage::AEOnLowClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(0, jump);
}

function BNE_LAPDBlasterImage::AEOnMedClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(3, jump);
   %obj.aeplayThread(0, plant);
}

function BNE_LAPDBlasterImage::AEOnHighClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(0, jump);
}

function BNE_LAPDBlasterImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_LAPDBlasterImage::onReloadIn(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
}

function BNE_LAPDBlasterImage::onReloadEnd(%this,%obj,%slot)
{
	%obj.aeplayThread(2, shiftright); 
	
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_LAPDBlasterLoaderImage,0);
	%a.schedule(2500,delete);
	
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_LAPDBlasterImage::onReloadStart(%this,%obj,%slot)
{
	%obj.aeplayThread(2, shiftleft); 
}

function BNE_LAPDBlasterImage::onReloadExtract(%this, %obj, %slot)
{
//	%obj.playAudio(0, BNE_PythonOutSound);
    %obj.aeplayThread(0, shiftright);
	%obj.aeplayThread(2, shiftleft); 
	%obj.AEUnloadMag();
	%obj.baadDisplayAmmo(%this);
}

function BNE_LAPDBlasterImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_LAPDBlasterImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_LAPDBlasterImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_LAPDBlasterLoaderImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.3 0.1 0.25";
   rotation = eulerToMatrix( "0 0 0" );	
	
	casing = BNE_LAPDBlasterLoaderDebris;
	shellExitDir        = "0 -0.05 -0.25";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;	
	shellVelocity       = 0.0;
	
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

function BNE_LAPDBlasterLoaderImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_LAPDBlasterSafetyImage)
{
   shapeFile = "./LAPDBlaster/LAPDBlaster.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_LAPDBlasterItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_LAPDBlasterIronsightImage;
   safetyImage = BNE_LAPDBlasterImage;
   doColorShift = true;
   colorShiftColor = BNE_LAPDBlasterItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_LAPDBlasterSafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_LAPDBlasterSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_LAPDBlasterSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_LAPDBlasterIronsightImage : BNE_LAPDBlasterImage)
{
	recoilHeight = 0.1625;

	scopingImage = BNE_LAPDBlasterImage;
	sourceImage = BNE_LAPDBlasterImage;
	
	offset = "0 0 0";
	eyeOffset = "0.0005 0.75 -0.584";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;
	
	stateName[6]				= "Reload";
	stateScript[6]				= "onDone";
	stateTimeoutValue[6]			= 1;
	stateTransitionOnTimeout[6]		= "";
	stateSound[6]				= "";
};

function BNE_LAPDBlasterIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_LAPDBlasterIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_LAPDBlasterIronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_LAPDBlasterFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_LAPDBlasterIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_LAPDBlasterIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_LAPDBlasterIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
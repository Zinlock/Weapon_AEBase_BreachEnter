datablock AudioProfile(ThompsonFire1Sound)
{
   filename    = "./Sounds/Fire/Thompson/Thompson_fire1.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(ThompsonFire2Sound)
{
   filename    = "./Sounds/Fire/Thompson/Thompson_fire2.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(ThompsonFire3Sound)
{
   filename    = "./Sounds/Fire/Thompson/Thompson_fire3.wav";
   description = LightClose3D;
   preload = true;
};

// Thompson
datablock DebrisData(AEThompsonMagDebris)
{
	shapeFile = "./Thompson/ThompsonMag.dts";
	lifetime = 2.0;
	minSpinSpeed = -150.0;
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
datablock ItemData(ThompsonItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Thompson/Thompson.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: M1A1 Thompson";
	iconName = "./Icons/28";
	doColorShift = true;
	colorShiftColor = "0.4 0.4 0.4 1";

	 // Dynamic properties defined by the scripts
	image = ThompsonImage;
	canDrop = true;

	AEAmmo = 20;
	AEType = AE_MediumPAmmoItem.getID();
	AEBase = 1;

	Auto = true;  
	RPM = 700;
	recoil = "Medium"; 
	uiColor = "1 1 1";
	description = "The M1A1 Thompson is a cheaper variant of the M1928A1 used by America during World War 2." NL "It comes with a 20 round magazine of .45 ACP rounds.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(ThompsonImage)
{
   // Basic Item properties
   shapeFile = "./Thompson/Thompson.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0.026 -0.025";
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
   item = ThompsonItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BEPistolShellDebris;
   shellExitDir        = "0.5 0 0.6";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = ThompsonSafetyImage;
    scopingImage = ThompsonIronsightImage;
	doColorShift = true;
	colorShiftColor = ThompsonItem.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellSMG;
	shellSoundMin = 550; //min delay for when the shell sound plays
	shellSoundMax = 650; //max delay for when the shell sound plays

	muzzleFlashScale = "0.5 0.5 0.5";
	bulletScale = "1 1 1";

	projectileDamage = 24;
	projectileCount = 1;
	projectileHeadshotMult = 1.78;
	projectileVelocity = 200;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.25;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 250; // m
	spreadBase = 200;
	spreadMin = 250;
	spreadMax = 1000;

	screenshakeMin = "0.075 0.075 0.075"; 
	screenshakeMax = "0.1 0.1 0.1"; 

	farShotSound = SMGDDistantSound;
	farShotDistance = 40;
	staticTotalRange = 100;	
	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	projectileFalloffStart = 16;
	projectileFalloffEnd = 64;
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
	stateTransitionOnNoAmmo[2]       	= "NoAmmoFlashFix";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseGenericFlashEmitter;
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
	
	stateName[29]                    = "FireEmpty";
	stateTransitionOnTimeout[29]     = "FireLoadCheckA";
	stateEmitter[29]					= AEBaseSmokeEmitter;
	stateEmitterTime[29]				= 0.05;
	stateEmitterNode[29]				= "muzzlePoint";
	stateAllowImageChange[29]        = false;
	stateSequence[29]                = "FireEmpty";
	stateWaitForTimeout[29]			= true;
	
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
	stateSound[8]				= ThompsonMagOutSound;
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.25;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= ThompsonMagInSound;
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.25;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";
	
	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.095;
	stateTransitionOnTimeout[11]		= "FireLoadCheckB";
	
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
	stateTimeoutValue[15]			= 0.85;
	stateScript[15]				= "onReloadStartEmpty";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "ReloadStartEmpty";
	
	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 0.6;
	stateScript[16]				= "onReloadMagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "MagOut";
	stateSound[16]				= ThompsonMagOutSound;
	
	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.25;
	stateScript[17]				= "onReloadMagIn";
	stateTransitionOnTimeout[17]		= "Reload2End";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagIn";
	stateSound[17]				= ThompsonMagInSound;
	
	stateName[19]				= "Reload2End";
	stateTimeoutValue[19]			= 0.25;
	stateScript[19]				= "onReloadEnd";
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
	
	stateName[28]           = "NoAmmoFlashFix";
	stateTransitionOnTimeout[28] = "FireEmpty";
	stateEmitter[28]					= AEBaseGenericFlashEmitter;
	stateEmitterTime[28]				= 0.05;
	stateEmitterNode[28]				= "muzzlePoint";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function ThompsonImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, ThompsonFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function ThompsonImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function ThompsonImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function ThompsonImage::onReload2End(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function ThompsonImage::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(10, "aeplayThread", "2", "plant");
}

function ThompsonImage::onReloadMagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, wrench);
   %obj.aeplayThread(3, plant);
}

// MAGAZINE DROPPING

function ThompsonImage::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftLeft);
   %obj.reload3Schedule = %this.schedule(225,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(450,550),0,serverPlay3D,AEMagMetalAr @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function ThompsonImage::onReloadStartEmpty(%this,%obj,%slot)
{
   %obj.schedule(350, "aeplayThread", "2", "shiftLeft");
   %obj.schedule(250, playAudio, 1, ThompsonBoltSound);
   %obj.schedule(200, "aeplayThread", "3", "plant");
   %obj.reload3Schedule = %this.schedule(825,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(850,950),0,serverPlay3D,AEMagMetalAr @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function ThompsonImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function ThompsonImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function ThompsonImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function ThompsonImage::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(ThompsonMagImage,0);
	%a.schedule(1000,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(ThompsonMagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "0.1 0.675 0.15";
   rotation = eulerToMatrix( "15 15 0" );	
	
	casing = AEThompsonMagDebris;
	shellExitDir        = "-0.1 0 -0.25";
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

function ThompsonMagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(ThompsonSafetyImage)
{
   shapeFile = "./Thompson/Thompson.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0.026 -0.025";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = ThompsonItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = ThompsonImage;
   doColorShift = true;
   colorShiftColor = ThompsonItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function ThompsonSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function ThompsonSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(ThompsonIronsightImage : ThompsonImage)
{
	recoilHeight = 0.0625;

	scopingImage = ThompsonImage;
	sourceImage = ThompsonImage;
	
   offset = "0 0.026 -0.025";
	eyeOffset = "0.00225 1.0 -0.388";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 2;
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

function ThompsonIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function ThompsonIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function ThompsonIronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, ThompsonFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function ThompsonIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function ThompsonIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound, %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function ThompsonIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound, %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
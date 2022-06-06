datablock AudioProfile(BNE_USP45Fire1Sound)
{
   filename    = "./Sounds/Fire/USP45/USP_FIRE_1.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(BNE_USP45Fire2Sound)
{
   filename    = "./Sounds/Fire/USP45/USP_FIRE_2.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(BNE_USP45Fire3Sound)
{
   filename    = "./Sounds/Fire/USP45/USP_FIRE_3.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(BNE_USP45Fire4Sound)
{
   filename    = "./Sounds/Fire/USP45/USP_FIRE_4.wav";
   description = LightClose3D;
   preload = true;
};

// USP45
datablock DebrisData(BNE_USP45MagDebris)
{
	shapeFile = "./USP45/USP45Mag.dts";
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
datablock ItemData(BNE_USP45Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./USP45/USP45.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: USP .45";
	iconName = "./Icons/50";
	doColorShift = true;
	colorShiftColor = "0.3 0.3 0.3 1";

	 // Dynamic properties defined by the scripts
	image = BNE_USP45Image;
	canDrop = true;

	AEAmmo = 12;
	AEType = AE_MediumPAmmoItem.getID();
	AEBase = 1;

	RPM = 625;
	recoil = "Medium"; 
	uiColor = "1 1 1";
	description = "The USP is a semi-automatic pistol developed in Germany by Heckler & Koch GmbH of Oberndorf am Neckar as a replacement for the P7 series of handguns.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_USP45Image)
{
   // Basic Item properties
   shapeFile = "./USP45/USP45.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 -0.05";
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
   item = BNE_USP45Item;
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
	safetyImage = BNE_USP45SafetyImage;
    scopingImage = BNE_USP45IronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_USP45Item.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellSMG;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "0.5 0.5 0.5";
	bulletScale = "1 1 1";

	projectileDamage = 25;
	projectileCount = 1;
	projectileHeadshotMult = 1.76;
	projectileVelocity = 200;
	projectileTagStrength = 0.15;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate
	staticTotalRange = 100;	
	recoilHeight = 1;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 350; // m
	spreadBase = 100;
	spreadMin = 100;
	spreadMax = 1500;

	screenshakeMin = "0.075 0.075 0.075"; 
	screenshakeMax = "0.1 0.1 0.1"; 

	farShotSound = PistolADistantSound;
	farShotDistance = 40;
	
	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;
	
	laserSize = "1.25 1.25 1.25";
	laserColor = "1.0 0.0 0.0 1";
	laserDistance = 50;
	laserOffStates = "Reload Reload2 ReloadMagOut ReloadMagIn ReloadEnd Reload2MagIn Reload2MagOut Reload2End";
  laserFade = 16;

	projectileFalloffStart = $ae_falloffPistolStart;
	projectileFalloffEnd = $ae_falloffPistolEnd;
	projectileFalloffDamage = $ae_falloffPistol;

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
	stateTimeoutValue[7]			= 0.15;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "MagOut";
	stateSound[7]				= BNE_USP45MagOutSound;
	
	stateName[8]				= "ReloadMagOut";
	stateTimeoutValue[8]			= 0.5;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "ReloadStart";
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.35;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= BNE_USP45MagInSound;
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.35;
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
	stateTimeoutValue[14]			= 0.2;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";

// EMPTY RELOAD STATE

	stateName[15]				= "Reload2";
	stateTimeoutValue[15]			= 0.15;
	stateScript[15]				= "onReloadStart";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "MagOutEmpty";
	stateSound[15]				= BNE_USP45MagOutSound;
	
	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 0.5;
	stateScript[16]				= "onReload2MagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "ReloadStartEmpty";
	
	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.35;
	stateScript[17]				= "onReload2MagIn";
	stateTransitionOnTimeout[17]		= "Reload2End";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagInEmpty";
	stateSound[17]				= BNE_USP45MagInSound;
	
	stateName[19]				= "Reload2End";
	stateTimeoutValue[19]			= 0.35;
	stateScript[19]				= "onReload2End";
	stateTransitionOnTimeout[19]		= "Ready";
	stateWaitForTimeout[19]			= true;
	stateSequence[19]			= "ReloadEndEmpty";
	
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
	stateEmitter[30]					= AEBaseGenericFlashEmitter;
	stateEmitterTime[30]				= 0.05;
	stateEmitterNode[30]				= "muzzlePoint";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_USP45Image::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_USP45Fire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(75, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_USP45Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_USP45Image::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "shiftright");
   %obj.schedule(50, "aeplayThread", "3", "plant");
   %obj.schedule(300, "aeplayThread", "2", "shiftleft");
}

function BNE_USP45Image::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "shiftright");
   %obj.schedule(50, "aeplayThread", "3", "plant");
   %obj.schedule(300, "aeplayThread", "2", "shiftleft");
   %obj.schedule(400, "aeplayThread", "3", "plant");
}

function BNE_USP45Image::onReload2End(%this,%obj,%slot)
{
    %obj.schedule(50, playAudio, 1, BNE_USP45SlideReleaseSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_USP45Image::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_USP45Image::onReloadStart(%this,%obj,%slot)
{
   %obj.schedule(300, "aeplayThread", "3", "shiftleft");
   %obj.aeplayThread(2, wrench);
   %obj.aeplayThread(3, plant);
   %obj.reload3Schedule = %this.schedule(0,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(150,250),0,serverPlay3D,AEMagPistol @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_USP45Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_USP45Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_USP45Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_USP45Image::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_USP45MagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_USP45MagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "0 0.075 -0.1";
   rotation = eulerToMatrix( "0 0 0" );	
	
	casing = BNE_USP45MagDebris;
	shellExitDir        = "0 -0.05 -0.25";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;	
	shellVelocity       = 10.0;
	
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

function BNE_USP45MagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_USP45SafetyImage)
{
   shapeFile = "./USP45/USP45.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.05";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_USP45Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_USP45IronsightImage;
   safetyImage = BNE_USP45Image;
   doColorShift = true;
   colorShiftColor = BNE_USP45Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_USP45SafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}	

function BNE_USP45SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_USP45SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_USP45IronsightImage : BNE_USP45Image)
{
	recoilHeight = 0.25;

	scopingImage = BNE_USP45Image;
	sourceImage = BNE_USP45Image;
	
   offset = "0 0 -0.05";
	eyeOffset = "-0.0025 1.25 -0.71";
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

function BNE_USP45IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_USP45IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_USP45IronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_USP45Fire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(75, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_USP45IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_USP45IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_USP45IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
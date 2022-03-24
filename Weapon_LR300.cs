//////////
// item //
//////////
datablock ItemData(BNE_LR300Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./LR300/LR300.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: LR-300";
	iconName = "./Icons/23";
	doColorShift = true;
	colorShiftColor = "0.75 0.75 0.75 1";

	 // Dynamic properties defined by the scripts
	image = BNE_LR300Image;
	canDrop = true;

	AEAmmo = 30;
	AEType = AE_LightRAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 800;
	recoil = "Medium"; 
	uiColor = "1 1 1";
	description = "The LR-300 is a folding stock version of the common AR-15 rifle. This results in a more compact weapon system.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_LR300Image)
{
   // Basic Item properties
   shapeFile = "./LR300/LR300.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 -0.075";
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
   item = BNE_LR300Item;
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
	safetyImage = BNE_LR300SafetyImage;
    scopingImage = BNE_LR300IronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_LR300Item.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellRifle;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 29;
	projectileCount = 1;
	projectileHeadshotMult = 1.32;
	projectileVelocity = 400;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.25;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 150; // m
	spreadBase = 25;
	spreadMin = 100;
	spreadMax = 1000;

	screenshakeMin = "0.1 0.1 0.1"; 
	screenshakeMax = "0.15 0.15 0.15"; 

	farShotSound = RifleADistantSound;
	farShotDistance = 40;
	
	projectileFalloffStart = 75;
	projectileFalloffEnd = 200;
	projectileFalloffDamage = 0.5;
	
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
	stateSequence[7]			= "ReloadStart";
	
	stateName[8]				= "ReloadMagOut";
	stateTimeoutValue[8]			= 0.65;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "MagOut";
	stateSound[8]				= BNE_AR15MagOutSound;
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.45;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= BNE_AR15MagInSound;
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.25;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Reloaded";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";
	
	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.09;
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
	stateTimeoutValue[15]			= 0.15;
	stateScript[15]				= "onReloadStart";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "ReloadStart";
	
	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 0.65;
	stateScript[16]				= "onReload2MagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "MagOut";
	stateSound[16]				= BNE_AR15MagOutSound;
	
	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.45;
	stateScript[17]				= "onReload2MagIn";
	stateTransitionOnTimeout[17]		= "Reload2End";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagIn";
	stateSound[17]				= BNE_AR15MagInSound;
	
	stateName[19]				= "Reload2End";
	stateTimeoutValue[19]			= 0.5;
	stateScript[19]				= "onReload2End";     
	stateTransitionOnTimeout[19]		= "Reloaded";
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
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_LR300Image::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_AR15Fire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_LR300Image::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_LR300Image::onReload2End(%this,%obj,%slot)
{
    %obj.schedule(150, playAudio, 1, BNE_AR15BoltRackSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}


function BNE_LR300Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_LR300Image::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
}

function BNE_LR300Image::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
   %obj.schedule(400, "aeplayThread", "3", "shiftleft");
}

function BNE_LR300Image::onReload2Bolt(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
   %obj.schedule(200, "aeplayThread", "3", "shiftleft");
}

function BNE_LR300Image::onReloadMagOut(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
}

function BNE_LR300Image::onReload2MagOut(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
}

// MAGAZINE DROPPING

function BNE_LR300Image::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
   %obj.reload3Schedule = %this.schedule(125,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(400,500),0,serverPlay3D,AEMagMetalAr @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_LR300Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_LR300Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_LR300Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_LR300Image::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_M4A1MagImage,0);
	%a.schedule(2500,delete);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_LR300SafetyImage)
{
   shapeFile = "./LR300/LR300.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.075";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_LR300Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = BNE_LR300Image;
   doColorShift = true;
   colorShiftColor = BNE_LR300Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function BNE_LR300SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_LR300SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_LR300IronsightImage : BNE_LR300Image)
{
	recoilHeight = 0.0625;

	scopingImage = BNE_LR300Image;
	sourceImage = BNE_LR300Image;
	
   offset = "0 0 -0.075";
	eyeOffset = "0 1.0 -1.191";
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

function BNE_LR300IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_LR300IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_LR300IronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_AR15Fire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_LR300IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_LR300IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_LR300IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
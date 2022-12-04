datablock AudioProfile(BNE_MP5SDFire1Sound)
{
   filename    = "./Sounds/Fire/MP5/MP5SD_fire1.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(BNE_MP5SDFire2Sound)
{
   filename    = "./Sounds/Fire/MP5/MP5SD_fire2.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(BNE_MP5SDFire3Sound)
{
   filename    = "./Sounds/Fire/MP5/MP5SD_fire3.wav";
   description = LightClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(BNE_MP5SDItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./MP5/MP5SD.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: MP5SD";
	iconName = "./Icons/Icon_MP5SD";
	doColorShift = true;
	colorShiftColor = "0.28 0.3 0.33 1";

	 // Dynamic properties defined by the scripts
	image = BNE_MP5SDImage;
	canDrop = true;

	AEAmmo = 30;
	AEType = AE_LightPAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 680;
	recoil = "Medium";
	uiColor = "1 1 1";
	description = "The MP5SD is a more compact variant of the MP5 submachine gun.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_MP5SDImage)
{
   // Basic Item properties
   shapeFile = "./MP5/MP5SD.dts";
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
   item = BNE_MP5SDItem;
   ammo = " ";
   projectile = AEProjectile;
   projectileType = Projectile;

   casing = AE_BEPistolShellDebris;
   shellExitDir        = "1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_MP5SDSafetyImage;
    scopingImage = BNE_MP5SDIronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_MP5SDItem.colorShiftColor;//"0.400 0.196 0 1.000";

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 22;
	projectileCount = 1;
	projectileHeadshotMult = 1.65;
	projectileVelocity = 200;
	projectileTagStrength = 0.35;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.4;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 250; // m
	spreadBase = 40;
	spreadMin = 60;
	spreadMax = 1000;

	screenshakeMin = "0.015 0.015 0.015"; 
	screenshakeMax = "0.1 0.1 0.1"; 

	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	staticHitscan = false;
	
	projectileFalloffStart = $ae_falloffSMGStart;
	projectileFalloffEnd = $ae_falloffSMGEnd;
	projectileFalloffDamage = $ae_falloffSMG;
	
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
	stateTimeoutValue[4]			= 0.075;
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
	stateSound[17]				= BNE_HKMagOutSound;
	
	stateName[18]				= "ReloadMagIn";
	stateTimeoutValue[18]			= 0.45;
	stateScript[18]				= "onReloadMagIn";
	stateTransitionOnTimeout[18]		= "ReloadEnd";
	stateWaitForTimeout[18]			= true;
	stateSequence[18]			= "MagIn";
	stateSound[18]				= BNE_HKMagInSound;
	
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
	stateTimeoutValue[21]			= 0.4;
	stateScript[21]				= "onReload2Start";
	stateTransitionOnTimeout[21]		= "Reload2MagOut";
	stateWaitForTimeout[21]			= true;
	stateSequence[21]			= "ReloadStartEmpty";
	stateSound[21]				= BNE_HKBoltLockSound;
	
	stateName[22]				= "Reload2MagOut";
	stateTimeoutValue[22]			= 0.5;
	stateScript[22]				= "onReload2MagOut";
	stateTransitionOnTimeout[22]		= "Reload2MagIn";
	stateWaitForTimeout[22]			= true;
	stateSequence[22]			= "MagOutEmpty";
	stateSound[22]				= BNE_HKMagOutSound;
	
	stateName[23]				= "Reload2MagIn";
	stateTimeoutValue[23]			= 0.4;
	stateScript[23]				= "onReload2MagIn";
	stateTransitionOnTimeout[23]		= "Reload2End";
	stateWaitForTimeout[23]			= true;
	stateSequence[23]			= "MagInEmpty";
	stateSound[23]				= BNE_HKMagInSound;
	
	stateName[24]				= "Reload2End";
	stateTimeoutValue[24]			= 0.4;
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

function BNE_MP5SDImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_MP5SDFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_MP5SDImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_MP5SDImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_MP5SDImage::onReload2End(%this,%obj,%slot)
{
    %obj.schedule(50, playAudio, 1, BNE_HKBoltCloseSound);
    Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function BNE_MP5SDImage::onReload2MagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(3, shiftright);
}

function BNE_MP5SDImage::onReloadMagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.aeplayThread(3, shiftright);
}

function BNE_MP5SDImage::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
}

function BNE_MP5SDImage::onReload2MagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "plant");
   %obj.schedule(300, "aeplayThread", "2", "shiftleft");
   %obj.schedule(450, "aeplayThread", "3", "plant");
}

function BNE_MP5SDImage::onReloadStart(%this,%obj,%slot)
{
  %obj.aeplayThread(2, plant);
   %obj.reload3Schedule = %this.schedule(150,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(200,300),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_MP5SDImage::onReload2Start(%this,%obj,%slot)
{
  %obj.aeplayThread(2, plant);
  %obj.aeplayThread(3, shiftright);
   %obj.reload3Schedule = %this.schedule(400,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(200,300),0,serverPlay3D,AEMagMetalAR @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_MP5SDImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_MP5SDImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_MP5SDImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_MP5SDImage::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_MP5KMagImage,0);
	%a.schedule(2500,delete);
}

datablock ShapeBaseImageData(BNE_MP5SDSafetyImage)
{
   shapeFile = "./MP5/MP5SD.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_MP5SDItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_MP5SDIronsightImage;
   safetyImage = BNE_MP5SDImage;
   doColorShift = true;
   colorShiftColor = BNE_MP5SDItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_MP5SDSafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_MP5SDSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_MP5SDSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_MP5SDIronsightImage : BNE_MP5SDImage)
{
	recoilHeight = 0.3;

	scopingImage = BNE_MP5SDImage;
	sourceImage = BNE_MP5SDImage;
	
	offset = "0 0 0";
	eyeOffset = "-0.0035 1.0 -1.16";
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

function BNE_MP5SDIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_MP5SDIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_MP5SDIronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_MP5SDFire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_MP5SDIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_MP5SDIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_MP5SDIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
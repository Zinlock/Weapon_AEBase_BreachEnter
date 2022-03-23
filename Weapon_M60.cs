datablock AudioProfile(M60Fire1Sound)
{
   filename    = "./Sounds/Fire/M60/M60_fire1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(M60Fire2Sound)
{
   filename    = "./Sounds/Fire/M60/M60_fire2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(M60Fire3Sound)
{
   filename    = "./Sounds/Fire/M60/M60_fire3.wav";
   description = MediumClose3D;
   preload = true;
};

// M60
datablock DebrisData(AEM60MagDebris)
{
	shapeFile = "./M60/M60Mag.dts";
	lifetime = 2.0;
	minSpinSpeed = -150.0;
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
datablock ItemData(M60Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./M60/M60.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: M60";
	iconName = "./Icons/30";
	doColorShift = true;
	colorShiftColor = "0.75 0.75 0.75 1";

	 // Dynamic properties defined by the scripts
	image = M60Image;
	canDrop = true;

	AEAmmo = 100;
	AEType = AE_HeavyRAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 600;
	recoil = "Heavy"; 
	uiColor = "1 1 1";
	description = "Powerful and reliable, the AK-47 is one of the most popular assault rifles in the world.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(M60Image)
{
   // Basic Item properties
   shapeFile = "./M60/M60.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.05 0.05 0.02";
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
   item = M60Item;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BELMGShellDebris;
   shellExitDir        = "1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = M60BipodImage;
    scopingImage = M60IronsightImage;
	doColorShift = true;
	colorShiftColor = M60Item.colorShiftColor;//"0.400 0.196 0 1.000";
	R_MovePenalty = 0.9;

	shellSound = AEShellRifle;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 34;
	projectileCount = 1;
	projectileHeadshotMult = 1.27;
	projectileVelocity = 400;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.4;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 250; // m
	spreadBase = 35;
	spreadMin = 110;
	spreadMax = 510;

	screenshakeMin = "0.1 0.1 0.1"; 
	screenshakeMax = "0.15 0.15 0.15"; 

	farShotSound = RifleFDistantSound;
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
	staticUnitsPerSecond = 400;

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
	stateTimeoutValue[7]			= 0.35;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadHideBelt";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";
	
	stateName[8]				= "ReloadHideBelt";
	stateTimeoutValue[8]			= 0.5;
	stateScript[8]				= "onReloadHideBelt";
	stateTransitionOnTimeout[8]		= "ReloadMagOut";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "HideBelt";
	stateSound[8]				= M60RemoveSound;
	
	stateName[10]				= "ReloadMagOut";
	stateTimeoutValue[10]			= 0.75;
	stateScript[10]				= "onReloadMagOut";
	stateTransitionOnTimeout[10]		= "ReloadMagIn";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "MagOut";
	stateSound[10]				= M60MagOutSound;
	
	stateName[12]				= "ReloadMagIn";
	stateTimeoutValue[12]			= 0.35;
	stateScript[12]				= "onReloadMagIn";
	stateTransitionOnTimeout[12]		= "ReloadPutBelt";
	stateWaitForTimeout[12]			= true;
	stateSequence[12]			= "MagIn";
	stateSound[12]				= M60MagInSound;
	
	stateName[13]				= "ReloadPutBelt";
	stateTimeoutValue[13]			= 0.5;
	stateScript[13]				= "onReloadPutBelt";
	stateTransitionOnTimeout[13]		= "Reload2End";
	stateWaitForTimeout[13]			= true;
	stateSequence[13]			= "PutBelt";
	stateSound[13]				= M60AttachSound;
	
	stateName[15]				= "FireLoadCheckA";
	stateScript[15]				= "AEMagLoadCheck";
	stateTimeoutValue[15]			= 0.1;
	stateTransitionOnTimeout[15]		= "FireLoadCheckB";
	
	stateName[16]				= "FireLoadCheckB";
	stateTransitionOnAmmo[16]		= "Ready";
	stateTransitionOnNoAmmo[16]		= "Reload2";	
	stateTransitionOnNotLoaded[16]  = "Ready";
		
	stateName[17]				= "Reloaded";
	stateTimeoutValue[17]			= 0.1;
	stateScript[17]				= "AEMagReloadAll";
	stateTransitionOnTimeout[17]		= "Ready";

// EMPTY RELOAD STATE

	stateName[18]				= "Reload2";
	stateTimeoutValue[18]			= 0.35;
	stateScript[18]				= "onReload2Start";
	stateTransitionOnTimeout[18]		= "Reload2MagOut";
	stateWaitForTimeout[18]			= true;
	stateSequence[18]			= "ReloadStartEmpty";
	
	stateName[20]				= "Reload2MagOut";
	stateTimeoutValue[20]			= 0.75;
	stateScript[20]				= "onReloadMagOut";
	stateTransitionOnTimeout[20]		= "Reload2MagIn";
	stateWaitForTimeout[20]			= true;
	stateSequence[20]			= "MagOut";
	stateSound[20]				= M60MagOutSound;
	
	stateName[22]				= "Reload2MagIn";
	stateTimeoutValue[22]			= 0.35;
	stateScript[22]				= "onReloadMagIn";
	stateTransitionOnTimeout[22]		= "Reload2PutBelt";
	stateWaitForTimeout[22]			= true;
	stateSequence[22]			= "MagIn";
	stateSound[22]				= M60MagInSound;
	
	stateName[23]				= "Reload2PutBelt";
	stateTimeoutValue[23]			= 0.5;
	stateScript[23]				= "onReloadPutBelt";
	stateTransitionOnTimeout[23]		= "Reload2End";
	stateWaitForTimeout[23]			= true;
	stateSequence[23]			= "PutBelt";
	stateSound[23]				= M60AttachSound;
	
	stateName[24]				= "Reload2End";
	stateTimeoutValue[24]			= 0.75;
	stateScript[24]				= "onReload2End";     
	stateTransitionOnTimeout[24]		= "Reloaded";
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
	
	stateName[28]           = "NoAmmoFlashFix";
	stateTransitionOnTimeout[28] = "FireEmpty";
	stateEmitter[28]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[28]				= 0.05;
	stateEmitterNode[28]				= "muzzlePoint";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function M60Image::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, M60Fire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function M60Image::onReloadEnd(%this,%obj,%slot)
{
	%obj.aeplayThread(2, shiftright);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function M60Image::onReload2End(%this,%obj,%slot)
{
	%obj.schedule(250, "aeplayThread", "2", "plant");
    %obj.schedule(350, playAudio, 1, M60BoltSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function M60Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function M60Image::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "shiftright");
}

function M60Image::onReloadHideBelt(%this,%obj,%slot)
{
   %obj.schedule(0, "aeplayThread", "3", "shiftleft");
   %obj.schedule(200, "aeplayThread", "2", "plant");
}

function M60Image::onReloadPutBelt(%this,%obj,%slot)
{
   %obj.schedule(100, "aeplayThread", "3", "shiftleft");
   %obj.schedule(300, "aeplayThread", "2", "plant");
}

function M60Image::onReloadMagOut(%this,%obj,%slot)
{
	%obj.aeplayThread(3, shiftleft);
}

// MAGAZINE DROPPING

function M60Image::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.reload3Schedule = %this.schedule(825,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(1200,1300),0,serverPlay3D,AEMagHeavyBounce @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function M60Image::onReload2Start(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.reload3Schedule = %this.schedule(350,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(400,500),0,serverPlay3D,AEMagDrum @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function M60Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function M60Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function M60Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function M60Image::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(M60MagImage,0);
	%a.schedule(1000,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(M60MagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.2 0.3 0.35";
   rotation = eulerToMatrix( "0 25 0" );	
	
	casing = AEM60MagDebris;
	shellExitDir        = "-1 0 -0.25";
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

function M60MagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

///////// IRONSIGHTS?

datablock ShapeBaseImageData(M60IronsightImage : M60Image)
{
	recoilHeight = 0.125;

	scopingImage = M60Image;
	sourceImage = M60Image;
	
   offset = "-0.05 0.05 0.02";
	eyeOffset = "-0.0235 1.1 -0.965";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.25;
   
	stateName[18]				= "Reload2";
	stateScript[18]				= "onDone";
	stateTimeoutValue[18]			= 1;
	stateTransitionOnTimeout[18]		= "";
	stateSound[18]				= "";
	
	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function M60IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function M60IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function M60IronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, M60Fire @ getRandom(1, 3) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function M60IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function M60IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function M60IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}

//ALT BIPOD STATES

datablock ShapeBaseImageData(M60BipodImage : M60Image)
{
	spreadBurst = 2; // how much shots it takes to trigger spread i think
	spreadReset = 250; // m
	spreadBase = 15;
	spreadMin = 80;
	spreadMax = 380;

	shapeFile = "./M60/M60Bipod.dts";
	safetyImage = M60Image;
	scopingImage = M60IronsightBipodImage;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function M60BipodImage::AEOnFire(%this,%obj,%slot)
{	
	M60Image::AEOnFire(%this, %obj, %slot);
}

function M60BipodImage::onReloadEnd(%this,%obj,%slot)
{
	M60Image::onReloadEnd(%this, %obj, %slot);
}

function M60BipodImage::onReload2End(%this,%obj,%slot)
{
	M60Image::onReload2End(%this, %obj, %slot);
}

function M60BipodImage::onDryFire(%this, %obj, %slot)
{
	M60Image::onDryFire(%this, %obj, %slot);
}

function M60BipodImage::onReloadMagIn(%this,%obj,%slot)
{
	M60Image::onReloadMagIn(%this, %obj, %slot);
}

function M60BipodImage::onReloadHideBelt(%this,%obj,%slot)
{
	M60Image::onReloadHideBelt(%this, %obj, %slot);
}

function M60BipodImage::onReloadPutBelt(%this,%obj,%slot)
{
	M60Image::onReloadPutBelt(%this, %obj, %slot);
}

function M60BipodImage::onReloadMagOut(%this,%obj,%slot)
{
	M60Image::onReloadMagOut(%this, %obj, %slot);
}

function M60BipodImage::onReloadStart(%this,%obj,%slot)
{
	M60Image::onReloadStart(%this, %obj, %slot);
}

function M60BipodImage::onReload2Start(%this,%obj,%slot)
{
	M60Image::onReload2Start(%this, %obj, %slot);
}

function M60BipodImage::onReady(%this,%obj,%slot)
{
	M60Image::onReady(%this, %obj, %slot);
}

function M60BipodImage::onMount(%this,%obj,%slot)
{
	M60Image::onMount(%this, %obj, %slot);
}

function M60BipodImage::onUnMount(%this,%obj,%slot)
{
	M60Image::onUnMount(%this, %obj, %slot);
}

function M60BipodImage::onMagDrop(%this,%obj,%slot)
{
	M60Image::onMagDrop(%this, %obj, %slot);	
}

datablock ShapeBaseImageData(M60IronsightBipodImage : M60IronsightImage)
{
  shapeFile = "./M60/M60Bipod.dts";
	sourceImage = M60BipodImage;
	scopingImage = M60BipodImage;
	safetyImage = M60Image;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function M60IronsightBipodImage::onDone(%this,%obj,%slot)
{
	M60IronsightImage::onDone(%this, %obj, %slot);	
}

function M60IronsightBipodImage::onReady(%this,%obj,%slot)
{
	M60IronsightImage::onReady(%this, %obj, %slot);	
}

function M60IronsightBipodImage::AEOnFire(%this,%obj,%slot)
{	
	M60IronsightImage::AEOnFire(%this, %obj, %slot);	
}

function M60IronsightBipodImage::onDryFire(%this, %obj, %slot)
{
	M60IronsightImage::onDryFire(%this, %obj, %slot);	
}

function M60IronsightBipodImage::onMount(%this,%obj,%slot)
{
	M60IronsightImage::onMount(%this, %obj, %slot);	
}

function M60IronsightBipodImage::onUnMount(%this,%obj,%slot)
{
	M60IronsightImage::onUnMount(%this, %obj, %slot);	
}
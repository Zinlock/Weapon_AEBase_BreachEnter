datablock AudioProfile(BNE_M60FireLoopSound)
{
   filename    = "./Sounds/Fire/M60/M60_LP.wav";
   description = BAADFireMediumLoop3D;
   preload = true;
};

datablock AudioProfile(BNE_M60FireLoopEndSound)
{
   filename    = "./Sounds/Fire/M60/M60_LP_END.wav";
   description = MediumClose3D;
   preload = true;
};

// M60
datablock DebrisData(BNE_M60MagDebris)
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
datablock ItemData(BNE_M60Item)
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
	image = BNE_M60Image;
	canDrop = true;

	AEAmmo = 100;
	AEType = AE_HeavyRAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 600;
	recoil = "Heavy"; 
	uiColor = "1 1 1";
	description = "The M60 is a 7.62x51mm NATO belt-fed machine gun used by the United States military.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_M60Image)
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
   item = BNE_M60Item;
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
	safetyImage = BNE_M60BipodImage;
    scopingImage = BNE_M60IronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_M60Item.colorShiftColor;//"0.400 0.196 0 1.000";
	R_MovePenalty = 0.9;

	shellSound = AEShellRifle;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

    loopingEndSound = BNE_M60FireLoopEndSound;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 32;
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
	spreadBase = 100;
	spreadMin = 100;
	spreadMax = 1000;

	screenshakeMin = "0.1 0.1 0.1"; 
	screenshakeMax = "0.15 0.15 0.15"; 

	farShotSound = RifleFDistantSound;
	farShotDistance = 40;
	
	projectileFalloffStart = $ae_falloffRifleStart;
	projectileFalloffEnd = $ae_falloffRifleEnd;
	projectileFalloffDamage = $ae_falloffRifle;
	
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
	
	stateName[4]                    = "FireEmpty";
	stateTransitionOnTimeout[4]     = "FireLoadCheckA";
	stateEmitter[4]					= AEBaseSmokeEmitter;
	stateEmitterTime[4]				= 0.05;
	stateEmitterNode[4]				= "muzzlePoint";
	stateAllowImageChange[4]        = false;
	stateSequence[4]                = "FireEmpty";
	stateWaitForTimeout[4]			= true;
	
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
	stateSound[8]				= BNE_M60RemoveSound;
	
	stateName[9]				= "ReloadMagOut";
	stateTimeoutValue[9]			= 1.25;
	stateScript[9]				= "onReloadMagOut";
	stateTransitionOnTimeout[9]		= "ReloadMagIn";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagOut";
	stateSound[9]				= BNE_M60MagOutSound;
	
	stateName[10]				= "ReloadMagIn";
	stateTimeoutValue[10]			= 0.75;
	stateScript[10]				= "onReloadMagIn";
	stateTransitionOnTimeout[10]		= "ReloadPutBelt";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "MagIn";
	stateSound[10]				= BNE_M60MagInSound;
	
	stateName[11]				= "ReloadPutBelt";
	stateTimeoutValue[11]			= 0.5;
	stateScript[11]				= "onReloadPutBelt";
	stateTransitionOnTimeout[11]		= "Reload2End";
	stateWaitForTimeout[11]			= true;
	stateSequence[11]			= "PutBelt";
	stateSound[11]				= BNE_M60AttachSound;
	
	stateName[12]				= "FireLoadCheckA";
	stateScript[12]				= "AEMagLoadCheck";
	stateTimeoutValue[12]			= 0.1;
	stateTransitionOnTimeout[12]		= "FireLoadCheckB";
	
	stateName[13]				= "FireLoadCheckB";
	stateTransitionOnAmmo[13]		= "TrigCheck";
	stateTransitionOnNoAmmo[13]		= "EndLoopEmpty";
	stateTransitionOnNotLoaded[13]  = "EndLoop";
		
	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";

// EMPTY RELOAD STATE

	stateName[15]				= "Reload2";
	stateTimeoutValue[15]			= 0.35;
	stateScript[15]				= "onReload2Start";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "ReloadStartEmpty";
	
	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 1.25;
	stateScript[16]				= "onReloadMagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "MagOut";
	stateSound[16]				= BNE_M60MagOutSound;
	
	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.75;
	stateScript[17]				= "onReloadMagIn";
	stateTransitionOnTimeout[17]		= "Reload2PutBelt";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagIn";
	stateSound[17]				= BNE_M60MagInSound;
	
	stateName[18]				= "Reload2PutBelt";
	stateTimeoutValue[18]			= 0.5;
	stateScript[18]				= "onReloadPutBelt";
	stateTransitionOnTimeout[18]		= "Reload2End";
	stateWaitForTimeout[18]			= true;
	stateSequence[18]			= "PutBelt";
	stateSound[18]				= BNE_M60AttachSound;
	
	stateName[19]				= "Reload2End";
	stateTimeoutValue[19]			= 0.75;
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
	
	stateName[23]           = "NoAmmoFlashFix";
	stateTransitionOnTimeout[23] = "FireEmpty";
	stateEmitter[23]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[23]				= 0.05;
	stateEmitterNode[23]				= "muzzlePoint";
	
	stateName[24]          = "TrigCheck";
	stateTransitionOnTriggerDown[24]  = "preFire";
	stateTransitionOnTimeout[24]		= "EndLoop";
	
	stateName[25]          = "EndLoop";
	stateScript[25]				= "onEndLoop";
	stateTransitionOnTimeout[25]		= "Ready";
	
	stateName[26]          = "EndLoopEmpty";
	stateScript[26]				= "onEndLoop";
	stateTransitionOnTimeout[26]		= "Reload2";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_M60Image::AEOnFire(%this,%obj,%slot)
{
	%obj.playAudio(0, BNE_M60FireLoopSound);
    %obj.FireLoop = true;
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);
	
	Parent::AEOnFire(%this, %obj, %slot); 	
}

function BNE_M60Image::onEndLoop(%this, %obj, %slot)
{
    %obj.playAudio(0, %this.loopingEndSound);
    %obj.FireLoop = false;
}

function BNE_M60Image::onReloadEnd(%this,%obj,%slot)
{
	%obj.aeplayThread(2, shiftright);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_M60Image::onReload2End(%this,%obj,%slot)
{
	%obj.schedule(250, "aeplayThread", "2", "plant");
    %obj.schedule(350, playAudio, 1, BNE_M60BoltSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_M60Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_M60Image::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "shiftright");
}

function BNE_M60Image::onReloadHideBelt(%this,%obj,%slot)
{
   %obj.schedule(0, "aeplayThread", "3", "shiftleft");
   %obj.schedule(200, "aeplayThread", "2", "plant");
}

function BNE_M60Image::onReloadPutBelt(%this,%obj,%slot)
{
   %obj.schedule(100, "aeplayThread", "3", "shiftleft");
   %obj.schedule(300, "aeplayThread", "2", "plant");
}

function BNE_M60Image::onReloadMagOut(%this,%obj,%slot)
{
	%obj.aeplayThread(3, shiftleft);
}

// MAGAZINE DROPPING

function BNE_M60Image::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.reload3Schedule = %this.schedule(825,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(1200,1300),0,serverPlay3D,AEMagHeavyBounce @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_M60Image::onReload2Start(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.reload3Schedule = %this.schedule(350,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(400,500),0,serverPlay3D,AEMagDrum @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_M60Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_M60Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_M60Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_M60Image::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_M60MagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M60MagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.2 0.3 0.35";
   rotation = eulerToMatrix( "0 25 0" );	
	
	casing = BNE_M60MagDebris;
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

function BNE_M60MagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_M60IronsightImage : BNE_M60Image)
{
	recoilHeight = 0.125;

	scopingImage = BNE_M60Image;
	sourceImage = BNE_M60Image;
	
   offset = "-0.05 0.05 0.02";
	eyeOffset = "-0.0235 1.1 -0.965";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.25;
   
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

function BNE_M60IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_M60IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_M60IronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.playAudio(0, BNE_M60FireLoopSound);
    %obj.FireLoop = true;
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);
	
	Parent::AEOnFire(%this, %obj, %slot); 	
}

function BNE_M60IronsightImage::onEndLoop(%this, %obj, %slot)
{
    %obj.playAudio(0, %this.loopingEndSound);
    %obj.FireLoop = false;
}

function BNE_M60IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_M60IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_M60IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}

//ALT BIPOD STATES

datablock ShapeBaseImageData(BNE_M60BipodImage : BNE_M60Image)
{
	shapeFile = "./M60/M60Bipod.dts";
	safetyImage = BNE_M60Image;
	scopingImage = BNE_M60IronsightBipodImage;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_M60BipodImage::AEOnFire(%this,%obj,%slot)
{	
	BNE_M60Image::AEOnFire(%this, %obj, %slot);
}

function BNE_M60BipodImage::onEndLoop(%this, %obj, %slot)
{
	BNE_M60Image::onEndLoop(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReloadEnd(%this,%obj,%slot)
{
	BNE_M60Image::onReloadEnd(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReload2End(%this,%obj,%slot)
{
	BNE_M60Image::onReload2End(%this, %obj, %slot);
}

function BNE_M60BipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_M60Image::onDryFire(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReloadMagIn(%this,%obj,%slot)
{
	BNE_M60Image::onReloadMagIn(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReloadHideBelt(%this,%obj,%slot)
{
	BNE_M60Image::onReloadHideBelt(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReloadPutBelt(%this,%obj,%slot)
{
	BNE_M60Image::onReloadPutBelt(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReloadMagOut(%this,%obj,%slot)
{
	BNE_M60Image::onReloadMagOut(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReloadStart(%this,%obj,%slot)
{
	BNE_M60Image::onReloadStart(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReload2Start(%this,%obj,%slot)
{
	BNE_M60Image::onReload2Start(%this, %obj, %slot);
}

function BNE_M60BipodImage::onReady(%this,%obj,%slot)
{
	BNE_M60Image::onReady(%this, %obj, %slot);
}

function BNE_M60BipodImage::onMount(%this,%obj,%slot)
{
	BNE_M60Image::onMount(%this, %obj, %slot);
}

function BNE_M60BipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_M60Image::onUnMount(%this, %obj, %slot);
}

function BNE_M60BipodImage::onMagDrop(%this,%obj,%slot)
{
	BNE_M60Image::onMagDrop(%this, %obj, %slot);	
}

datablock ShapeBaseImageData(BNE_M60IronsightBipodImage : BNE_M60IronsightImage)
{
  shapeFile = "./M60/M60Bipod.dts";
	sourceImage = BNE_M60BipodImage;
	scopingImage = BNE_M60BipodImage;
	safetyImage = BNE_M60Image;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_M60IronsightBipodImage::onDone(%this,%obj,%slot)
{
	BNE_M60IronsightImage::onDone(%this, %obj, %slot);	
}

function BNE_M60IronsightBipodImage::onReady(%this,%obj,%slot)
{
	BNE_M60IronsightImage::onReady(%this, %obj, %slot);	
}

function BNE_M60IronsightBipodImage::AEOnFire(%this,%obj,%slot)
{	
	BNE_M60IronsightImage::AEOnFire(%this, %obj, %slot);	
}

function BNE_M60IronsightBipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_M60IronsightImage::onDryFire(%this, %obj, %slot);	
}

function BNE_M60IronsightBipodImage::onMount(%this,%obj,%slot)
{
	BNE_M60IronsightImage::onMount(%this, %obj, %slot);	
}

function BNE_M60IronsightBipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_M60IronsightImage::onUnMount(%this, %obj, %slot);	
}
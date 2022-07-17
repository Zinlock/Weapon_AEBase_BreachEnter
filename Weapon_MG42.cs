datablock AudioProfile(BNE_MG42FireLoopSound)
{
   filename    = "./Sounds/Fire/MG42/MG42_LP.wav";
   description = BAADFireMediumLoop3D;
   preload = true;
};

datablock AudioProfile(BNE_MG42FireLoopEndSound)
{
   filename    = "./Sounds/Fire/MG42/MG42_LP_END.wav";
   description = MediumClose3D;
   preload = true;
};

// MG42
datablock DebrisData(BNE_MG42MagDebris)
{
	shapeFile = "./MG42/MG42Mag.dts";
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
datablock ItemData(BNE_MG42Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./MG42/MG42.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: MG 42";
	iconName = "./Icons/30";
	doColorShift = true;
	colorShiftColor = "0.65 0.65 0.65 1";

	 // Dynamic properties defined by the scripts
	image = BNE_MG42Image;
	canDrop = true;

	AEAmmo = 50;
	AEType = AE_LMGAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 600;
	recoil = "Heavy"; 
	uiColor = "1 1 1";
	description = "The MG42 is a 7.62x51mm NATO belt-fed machine gun used by the United States military.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_MG42Image)
{
   // Basic Item properties
   shapeFile = "./MG42/MG42.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0.05 0.075";
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
   item = BNE_MG42Item;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BELMGShellDebris;
   shellExitDir        = "0 0 -1";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_MG42BipodImage;
    scopingImage = BNE_MG42IronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_MG42Item.colorShiftColor;//"0.400 0.196 0 1.000";
	R_MovePenalty = 0.9;

	shellSound = AEShellRifle;
	shellSoundMin = 250; //min delay for when the shell sound plays
	shellSoundMax = 350; //max delay for when the shell sound plays

    loopingEndSound = BNE_MG42FireLoopEndSound;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 32;
	projectileCount = 1;
	projectileHeadshotMult = 1.27;
	projectileVelocity = 400;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.5;
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
	stateTransitionOnTimeout[7]		= "OpenTop";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";
	
	stateName[8]				= "OpenTop";
	stateTimeoutValue[8]			= 0.35;
	stateScript[8]				= "onOpenTop";
	stateTransitionOnTimeout[8]		= "ReloadHideBelt";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "OpenTop";
	
	stateName[9]				= "ReloadHideBelt";
	stateTimeoutValue[9]			= 0.5;
	stateScript[9]				= "onReloadHideBelt";
	stateTransitionOnTimeout[9]		= "ReloadMagOut";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "HideBelt";
	stateSound[9]				= BNE_MG42RemoveSound;
	
	stateName[10]				= "ReloadMagOut";
	stateTimeoutValue[10]			= 1.25;
	stateScript[10]				= "onReloadMagOut";
	stateTransitionOnTimeout[10]		= "ReloadMagIn";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "MagOut";
	stateSound[10]				= BNE_MG42MagOutSound;
	
	stateName[11]				= "ReloadMagIn";
	stateTimeoutValue[11]			= 0.75;
	stateScript[11]				= "onReloadMagIn";
	stateTransitionOnTimeout[11]		= "ReloadPutBelt";
	stateWaitForTimeout[11]			= true;
	stateSequence[11]			= "MagIn";
	stateSound[11]				= BNE_MG42MagInSound;
	
	stateName[12]				= "ReloadPutBelt";
	stateTimeoutValue[12]			= 0.5;
	stateScript[12]				= "onReloadPutBelt";
	stateTransitionOnTimeout[12]		= "CloseTop";
	stateWaitForTimeout[12]			= true;
	stateSequence[12]			= "PutBelt";
	stateSound[12]				= BNE_MG42AttachSound;
	
	stateName[13]				= "CloseTop";
	stateTimeoutValue[13]			= 0.35;
	stateScript[13]				= "onCloseTop";
	stateTransitionOnTimeout[13]		= "Reload2End";
	stateWaitForTimeout[13]			= true;
	stateSequence[13]			= "CloseTop";
	
	stateName[14]				= "FireLoadCheckA";
	stateScript[14]				= "AEMagLoadCheck";
	stateTimeoutValue[14]			= 0.0325;
	stateTransitionOnTimeout[14]		= "FireLoadCheckB";
	
	stateName[15]				= "FireLoadCheckB";
	stateTransitionOnAmmo[15]		= "TrigCheck";
	stateTransitionOnNoAmmo[15]		= "EndLoopEmpty";
	stateTransitionOnNotLoaded[15]  = "EndLoop";
		
	stateName[16]				= "Reloaded";
	stateTimeoutValue[16]			= 0.1;
	stateScript[16]				= "AEMagReloadAll";
	stateTransitionOnTimeout[16]		= "Ready";

// EMPTY RELOAD STATE

	stateName[17]				= "Reload2";
	stateTimeoutValue[17]			= 0.35;
	stateScript[17]				= "onReload2Start";
	stateTransitionOnTimeout[17]		= "OpenTopEmpty";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "ReloadStartEmpty";
	
	stateName[18]				= "OpenTopEmpty";
	stateTimeoutValue[18]			= 0.35;
	stateScript[18]				= "onOpenTop";
	stateTransitionOnTimeout[18]		= "Reload2MagOut";
	stateWaitForTimeout[18]			= true;
	stateSequence[18]			= "OpenTopEmpty";
	
	stateName[19]				= "Reload2MagOut";
	stateTimeoutValue[19]			= 1.25;
	stateScript[19]				= "onReloadMagOut";
	stateTransitionOnTimeout[19]		= "Reload2MagIn";
	stateWaitForTimeout[19]			= true;
	stateSequence[19]			= "MagOut";
	stateSound[19]				= BNE_MG42MagOutSound;
	
	stateName[20]				= "Reload2MagIn";
	stateTimeoutValue[20]			= 0.75;
	stateScript[20]				= "onReloadMagIn";
	stateTransitionOnTimeout[20]		= "Reload2PutBelt";
	stateWaitForTimeout[20]			= true;
	stateSequence[20]			= "MagIn";
	stateSound[20]				= BNE_MG42MagInSound;
	
	stateName[21]				= "Reload2PutBelt";
	stateTimeoutValue[21]			= 0.5;
	stateScript[21]				= "onReloadPutBelt";
	stateTransitionOnTimeout[21]		= "CloseTop";
	stateWaitForTimeout[21]			= true;
	stateSequence[21]			= "PutBelt";
	stateSound[21]				= BNE_MG42AttachSound;
	
	stateName[22]				= "Reload2End";
	stateTimeoutValue[22]			= 1;
	stateScript[22]				= "onReload2End";     
	stateTransitionOnTimeout[22]		= "Reloaded";
	stateWaitForTimeout[22]			= true;
	stateSequence[22]			= "ReloadEnd";
	
	stateName[23]				= "ReadyLoop";
	stateTransitionOnTimeout[23]		= "Ready";

	stateName[24]          = "Empty";
	stateTransitionOnTriggerDown[24]  = "Dryfire";
	stateTransitionOnLoaded[24] = "Reload2";
	stateScript[24]        = "AEOnEmpty";

	stateName[25]           = "Dryfire";
	stateTransitionOnTriggerUp[25] = "Empty";
	stateWaitForTimeout[25]    = false;
	stateScript[25]      = "onDryFire";
	
	stateName[26]           = "NoAmmoFlashFix";
	stateTransitionOnTimeout[26] = "FireEmpty";
	stateEmitter[26]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[26]				= 0.05;
	stateEmitterNode[26]				= "muzzlePoint";
	
	stateName[27]          = "TrigCheck";
	stateTransitionOnTriggerDown[27]  = "preFire";
	stateTransitionOnTimeout[27]		= "EndLoop";
	
	stateName[28]          = "EndLoop";
	stateScript[28]				= "onEndLoop";
	stateTransitionOnTimeout[28]		= "Ready";
	
	stateName[29]          = "EndLoopEmpty";
	stateScript[29]				= "onEndLoop";
	stateTransitionOnTimeout[29]		= "Reload2";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_MG42Image::AEOnFire(%this,%obj,%slot)
{
	%obj.playAudio(0, BNE_MG42FireLoopSound);
    %obj.FireLoop = true;
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);
	
	Parent::AEOnFire(%this, %obj, %slot); 	
}

function BNE_MG42Image::onEndLoop(%this, %obj, %slot)
{
    %obj.playAudio(0, %this.loopingEndSound);
    %obj.FireLoop = false;
}

function BNE_MG42Image::onReloadEnd(%this,%obj,%slot)
{
	%obj.aeplayThread(2, shiftright);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_MG42Image::onReload2End(%this,%obj,%slot)
{
	%obj.schedule(250, "aeplayThread", "2", "plant");
    %obj.schedule(350, playAudio, 1, BNE_MG42BoltSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_MG42Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_MG42Image::onReloadMagIn(%this,%obj,%slot)
{
   %obj.schedule(50, "aeplayThread", "2", "shiftright");
}

function BNE_MG42Image::onOpenTop(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
}

function BNE_MG42Image::onCloseTop(%this,%obj,%slot)
{
   %obj.schedule(150, "aeplayThread", "2", "plant");
}

function BNE_MG42Image::onReloadHideBelt(%this,%obj,%slot)
{
   %obj.schedule(0, "aeplayThread", "3", "shiftleft");
   %obj.schedule(200, "aeplayThread", "2", "plant");
}

function BNE_MG42Image::onReloadPutBelt(%this,%obj,%slot)
{
   %obj.schedule(100, "aeplayThread", "3", "shiftleft");
   %obj.schedule(300, "aeplayThread", "2", "plant");
}

function BNE_MG42Image::onReloadMagOut(%this,%obj,%slot)
{
	%obj.aeplayThread(3, shiftleft);
}

// MAGAZINE DROPPING

function BNE_MG42Image::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.reload3Schedule = %this.schedule(1175,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(1500,1600),0,serverPlay3D,AEMagDrum @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_MG42Image::onReload2Start(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftleft);
   %obj.reload3Schedule = %this.schedule(675,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(1000,1100),0,serverPlay3D,AEMagDrum @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function BNE_MG42Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_MG42Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_MG42Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_MG42Image::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_MG42MagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_MG42MagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.2 0.575 0.45";
   rotation = eulerToMatrix( "0 25 0" );	
	
	casing = BNE_MG42MagDebris;
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

function BNE_MG42MagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_MG42IronsightImage : BNE_MG42Image)
{
	recoilHeight = 0.125;

	scopingImage = BNE_MG42Image;
	sourceImage = BNE_MG42Image;
	
   offset = "-0.05 0.05 0.02";
	eyeOffset = "0 0.8 -0.3255";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.25;
   
	stateName[17]				= "Reload2";
	stateScript[17]				= "onDone";
	stateTimeoutValue[17]			= 1;
	stateTransitionOnTimeout[17]		= "";
	stateSound[17]				= "";
	
	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function BNE_MG42IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_MG42IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_MG42IronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.playAudio(0, BNE_MG42FireLoopSound);
    %obj.FireLoop = true;
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);
	
	Parent::AEOnFire(%this, %obj, %slot); 	
}

function BNE_MG42IronsightImage::onEndLoop(%this, %obj, %slot)
{
    %obj.playAudio(0, %this.loopingEndSound);
    %obj.FireLoop = false;
}

function BNE_MG42IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_MG42IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_MG42IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}

//ALT BIPOD STATES

datablock ShapeBaseImageData(BNE_MG42BipodImage : BNE_MG42Image)
{
	shapeFile = "./MG42/MG42Bipod.dts";
	safetyImage = BNE_MG42Image;
	scopingImage = BNE_MG42IronsightBipodImage;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_MG42BipodImage::AEOnFire(%this,%obj,%slot)
{	
	BNE_MG42Image::AEOnFire(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onEndLoop(%this, %obj, %slot)
{
	BNE_MG42Image::onEndLoop(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReloadEnd(%this,%obj,%slot)
{
	BNE_MG42Image::onReloadEnd(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReload2End(%this,%obj,%slot)
{
	BNE_MG42Image::onReload2End(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_MG42Image::onDryFire(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReloadMagIn(%this,%obj,%slot)
{
	BNE_MG42Image::onReloadMagIn(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReloadHideBelt(%this,%obj,%slot)
{
	BNE_MG42Image::onReloadHideBelt(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReloadPutBelt(%this,%obj,%slot)
{
	BNE_MG42Image::onReloadPutBelt(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReloadMagOut(%this,%obj,%slot)
{
	BNE_MG42Image::onReloadMagOut(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReloadStart(%this,%obj,%slot)
{
	BNE_MG42Image::onReloadStart(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReload2Start(%this,%obj,%slot)
{
	BNE_MG42Image::onReload2Start(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onReady(%this,%obj,%slot)
{
	BNE_MG42Image::onReady(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onMount(%this,%obj,%slot)
{
	BNE_MG42Image::onMount(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_MG42Image::onUnMount(%this, %obj, %slot);
}

function BNE_MG42BipodImage::onMagDrop(%this,%obj,%slot)
{
	BNE_MG42Image::onMagDrop(%this, %obj, %slot);	
}

datablock ShapeBaseImageData(BNE_MG42IronsightBipodImage : BNE_MG42IronsightImage)
{
  shapeFile = "./MG42/MG42Bipod.dts";
	sourceImage = BNE_MG42BipodImage;
	scopingImage = BNE_MG42BipodImage;
	safetyImage = BNE_MG42Image;
	recoilHeight = 0;
	R_MovePenalty = 0.15;
	screenshakeMin = "0 0 0"; 
	screenshakeMax = "0 0 0"; 
	isSafetyImage = true;
	isBipod = true;
};

function BNE_MG42IronsightBipodImage::onDone(%this,%obj,%slot)
{
	BNE_MG42IronsightImage::onDone(%this, %obj, %slot);	
}

function BNE_MG42IronsightBipodImage::onReady(%this,%obj,%slot)
{
	BNE_MG42IronsightImage::onReady(%this, %obj, %slot);	
}

function BNE_MG42IronsightBipodImage::AEOnFire(%this,%obj,%slot)
{	
	BNE_MG42IronsightImage::AEOnFire(%this, %obj, %slot);	
}

function BNE_MG42IronsightBipodImage::onEndLoop(%this,%obj,%slot)
{	
	BNE_MG42IronsightImage::onEndLoop(%this, %obj, %slot);	
}

function BNE_MG42IronsightBipodImage::onDryFire(%this, %obj, %slot)
{
	BNE_MG42IronsightImage::onDryFire(%this, %obj, %slot);	
}

function BNE_MG42IronsightBipodImage::onMount(%this,%obj,%slot)
{
	BNE_MG42IronsightImage::onMount(%this, %obj, %slot);	
}

function BNE_MG42IronsightBipodImage::onUnMount(%this,%obj,%slot)
{
	BNE_MG42IronsightImage::onUnMount(%this, %obj, %slot);	
}
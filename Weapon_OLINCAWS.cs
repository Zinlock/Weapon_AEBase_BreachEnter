datablock AudioProfile(BNE_OLINCAWSFire1Sound)
{
   filename    = "./Sounds/Fire/OLINCAWS/CAWS_FIRE_1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_OLINCAWSFire2Sound)
{
   filename    = "./Sounds/Fire/OLINCAWS/CAWS_FIRE_2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_OLINCAWSFire3Sound)
{
   filename    = "./Sounds/Fire/OLINCAWS/CAWS_FIRE_3.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_OLINCAWSFire4Sound)
{
   filename    = "./Sounds/Fire/OLINCAWS/CAWS_FIRE_4.wav";
   description = HeavyClose3D;
   preload = true;
};

// OLINCAWS
datablock DebrisData(BNE_OLINCAWSMagDebris)
{
	shapeFile = "./OLINCAWS/OLINCAWSMag.dts";
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
datablock ItemData(BNE_OLINCAWSItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./OLINCAWS/OLINCAWS.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Olin CAWS";
	iconName = "./Icons/37";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = BNE_OLINCAWSImage;
	canDrop = true;

	AEAmmo = 8;
	AEType = AE_LightSAmmoItem.getID();
	AEBase = 1;

	RPM = 600;
	recoil = "Heavy"; 
	uiColor = "1 1 1";
	description = "This fictional depiction of the HK/Olin CAWS is near-useless at longer ranges but packs quite a punch close-up.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_OLINCAWSImage)
{
   // Basic Item properties
   shapeFile = "./OLINCAWS/OLINCAWS.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 -0.015";
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
   item = BNE_OLINCAWSItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BEShotgunShellDebris;
   shellExitDir        = "1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = BNE_OLINCAWSSafetyImage;
    scopingImage = BNE_OLINCAWSIronsightImage;
	doColorShift = true;
	colorShiftColor = BNE_OLINCAWSItem.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellShotgun;
	shellSoundMin = 500; //min delay for when the shell sound plays
	shellSoundMax = 600; //max delay for when the shell sound plays

	muzzleFlashScale = "1.5 1.5 1.5";
	bulletScale = "1 1 1";

	projectileDamage = 13;
	projectileCount = 8;
	projectileHeadshotMult = 1.5;
	projectileVelocity = 200;
	projectileTagStrength = 0.35;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate
	staticTotalRange = 100;	
	recoilHeight = 3;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 25;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 350; // m
	spreadBase = 1000;
	spreadMin = 1100;
	spreadMax = 1100;

	screenshakeMin = "0.15 0.15 0.15"; 
	screenshakeMax = "0.35 0.35 0.35"; 

	farShotSound = ShottyCDistantSound;
	farShotDistance = 40;
	
	sonicWhizz = true;
	whizzSupersonic = false;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	projectileFalloffStart = 18;
	projectileFalloffEnd = 48;
	projectileFalloffDamage = 0.12;
	
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
	stateEmitter[2]					= AEBaseShotgunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
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
	stateSound[8]				= BNE_CAWSMagOutSound;
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.3;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";
	stateSound[9]				= BNE_CAWSMagInSound;
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.25;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";
	
	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.15;
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
	stateTimeoutValue[15]			= 0.45;
	stateScript[15]				= "onReload2Start";
	stateTransitionOnTimeout[15]		= "Reload2MagOut";
	stateWaitForTimeout[15]			= true;
	stateSequence[15]			= "ReloadStartEmpty";
	stateSound[15]				= BNE_CAWSBoltLockSound;
	
	stateName[16]				= "Reload2MagOut";
	stateTimeoutValue[16]			= 0.65;
	stateScript[16]				= "onReload2MagOut";
	stateTransitionOnTimeout[16]		= "Reload2MagIn";
	stateWaitForTimeout[16]			= true;
	stateSequence[16]			= "MagOutEmpty";
	stateSound[16]				= BNE_CAWSMagOutSound;
	
	stateName[17]				= "Reload2MagIn";
	stateTimeoutValue[17]			= 0.3;
	stateScript[17]				= "onReload2MagIn";
	stateTransitionOnTimeout[17]		= "Reload2End";
	stateWaitForTimeout[17]			= true;
	stateSequence[17]			= "MagInEmpty";
	stateSound[17]				= BNE_CAWSMagInSound;
	
	stateName[18]				= "Reload2End";
	stateTimeoutValue[18]			= 0.45;
	stateScript[18]				= "onReload2End";
	stateTransitionOnTimeout[18]		= "Ready";
	stateWaitForTimeout[18]			= true;
	stateSequence[18]			= "ReloadEndEmpty";
	
	stateName[19]				= "ReadyLoop";
	stateTransitionOnTimeout[19]		= "Ready";

	stateName[20]          = "Empty";
	stateTransitionOnTriggerDown[20]  = "Dryfire";
	stateTransitionOnLoaded[20] = "Reload2";
	stateScript[20]        = "AEOnEmpty";

	stateName[21]           = "Dryfire";
	stateTransitionOnTriggerUp[21] = "Empty";
	stateWaitForTimeout[21]    = false;
	stateScript[21]      = "onDryFire";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BNE_OLINCAWSImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_OLINCAWSFire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_OLINCAWSImage::AEOnLowClimb(%this, %obj, %slot)
{
   %obj.aeplayThread(2, plant);
}

function BNE_OLINCAWSImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_OLINCAWSImage::onReload2MagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
}

function BNE_OLINCAWSImage::onReloadMagOut(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
}

function BNE_OLINCAWSImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.schedule(50, "aeplayThread", "3", "plant");
}

function BNE_OLINCAWSImage::onReload2MagIn(%this,%obj,%slot)
{
  %obj.schedule(50, "aeplayThread", "3", "plant");
  %obj.schedule(500, "aeplayThread", "2", "shiftleft");
  %obj.schedule(600, "aeplayThread", "3", "plant");
}

function BNE_OLINCAWSImage::onReloadStart(%this,%obj,%slot)
{
  %obj.reload3Schedule = %this.schedule(200,onMagDrop,%obj,%slot);
  %obj.reload4Schedule = schedule(getRandom(400,500),0,serverPlay3D,AEMagPlasticAR @ getRandom(1,3) @ Sound,%obj.getPosition());
  %obj.aeplayThread(2, shiftleft);
}

function BNE_OLINCAWSImage::onReload2Start(%this,%obj,%slot)
{
  %obj.reload3Schedule = %this.schedule(450,onMagDrop,%obj,%slot);
  %obj.reload4Schedule = schedule(getRandom(400,500),0,serverPlay3D,AEMagPlasticAR @ getRandom(1,3) @ Sound,%obj.getPosition());
  %obj.aeplayThread(2, plant);
  %obj.aeplayThread(3, shiftright);
}

function BNE_OLINCAWSImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_OLINCAWSImage::onReload2End(%this,%obj,%slot)
{
    %obj.schedule(300, playAudio, 1, BNE_CAWSBoltSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_OLINCAWSImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BNE_OLINCAWSImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_OLINCAWSImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_OLINCAWSImage::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_OLINCAWSMagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_OLINCAWSMagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.1 0.65 0.075";
   rotation = eulerToMatrix( "-25 25 0" );	
	
	casing = BNE_OLINCAWSMagDebris;
	shellExitDir        = "0 1 -0.25";
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

function BNE_OLINCAWSMagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_OLINCAWSSafetyImage)
{
   shapeFile = "./OLINCAWS/OLINCAWS.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_OLINCAWSItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = BNE_OLINCAWSImage;
   doColorShift = true;
   colorShiftColor = BNE_OLINCAWSItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function BNE_OLINCAWSSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_OLINCAWSSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(BNE_OLINCAWSIronsightImage : BNE_OLINCAWSImage)
{
	recoilHeight = 0.75;

	scopingImage = BNE_OLINCAWSImage;
	sourceImage = BNE_OLINCAWSImage;
	
   offset = "0 0 -0.015";
	eyeOffset = "-0.0125 1.0 -0.98";
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

function BNE_OLINCAWSIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_OLINCAWSIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_OLINCAWSIronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BNE_OLINCAWSFire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_OLINCAWSIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_OLINCAWSIronsightImage::onMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_OLINCAWSIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
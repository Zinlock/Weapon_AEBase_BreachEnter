datablock AudioProfile(StrikerFire1Sound)
{
   filename    = "./Sounds/Fire/Striker/Striker_fire1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(StrikerFire2Sound)
{
   filename    = "./Sounds/Fire/Striker/Striker_fire2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(StrikerFire3Sound)
{
   filename    = "./Sounds/Fire/Striker/Striker_fire3.wav";
   description = HeavyClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(StrikerItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Striker/Striker12.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Armsel Striker";
	iconName = "./Icons/10";
	doColorShift = true;
	colorShiftColor = "0.4 0.4 0.4 1";

	 // Dynamic properties defined by the scripts
	image = StrikerImage;
	canDrop = true;
	
	AEAmmo = 12;
	AEType = AE_LightSAmmoItem.getID(); 
	AEBase = 1;

   RPM = 352;
   Recoil = "Heavy";
   uiColor = "1 1 1";
   description = "The Armsel Striker is a 12-gauge shotgun with a revolving 12 round cylinder that was designed for riot control and combat.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(StrikerImage)
{
   // Basic Item properties
   shapeFile = "./Striker/Striker12.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = 0; //"0.7 1.2 -0.5";
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
   item = StrikerItem;
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
   safetyImage = StrikerSafetyImage;
   scopingImage = StrikerIronsightImage;
   doColorShift = true;
   colorShiftColor = StrikerItem.colorShiftColor;
	shellSound = AEShellShotgun;
	shellSoundMin = 600; //min delay for when the shell sound plays
	shellSoundMax = 700; //max delay for when the shell sound plays
   muzzleFlashScale = "1.5 1.5 1.5";
   bulletScale = "1 1 1";

   projectileDamage = 12;
   projectileCount = 8;
   projectileHeadshotMult = 1.65;
   projectileVelocity = 200;
   projectileTagStrength = 0.35;  // tagging strength
   projectileTagRecovery = 0.03; // tagging decay rate

   recoilHeight = 3.5;
   recoilWidth = 0;
   recoilWidthMax = 0;
   recoilHeightMax = 20;
   spreadBurst = 1; // how much shots it takes to trigger spread i think
   spreadBase = 1200;
   spreadReset = 450; // m
   spreadMin = 1200;
   spreadMax = 1200;
   screenshakeMin = "0.2 0.2 0.2"; 
   screenshakeMax = "0.4 0.4 0.4"; 
   farShotSound = ShottyCDistantSound;
   farShotDistance = 40;

		sonicWhizz = true;
		whizzSupersonic = false;
		whizzThrough = false;
		whizzDistance = 12;
		whizzChance = 100;
		whizzAngle = 80;

	projectileFalloffStart = 16;
	projectileFalloffEnd = 32;
	projectileFalloffDamage = 0.11;

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

	stateName[1]                    	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnTriggerDown[1] 	= "preFire";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]		= "ReloadStart";
	stateAllowImageChange[1]		= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseShotgunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[5]				= "FireLoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.2;
	stateTransitionOnTimeout[5]     = "FireLoadCheckB";
	
	stateName[6]				= "FireLoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNoAmmo[6]		= "ReloadStart2";
	stateTransitionOnNotLoaded[6]  = "Ready";	
	
	stateName[7]				= "LoadCheckA";
	stateScript[7]				= "AELoadCheck";
	stateTimeoutValue[7]			= 0.1;
	stateTransitionOnTimeout[7]		= "LoadCheckB";
						
	stateName[8]				= "LoadCheckB";
	stateTransitionOnAmmo[8]		= "Ready";
	stateTransitionOnNotLoaded[8] = "Empty";
	stateTransitionOnNoAmmo[8]		= "ReloadStart2";	

	stateName[9]			  	= "ReloadStart";
	stateScript[9]				= "onReloadStart";
	stateTransitionOnTimeout[9]	  	= "Reload";
	stateTimeoutValue[9]		  	= 0.25;
	stateWaitForTimeout[9]		  	= false;
	stateTransitionOnTriggerDown[9]	= "AnotherAmmoCheck";
	stateSequence[9]			= "ReloadStart";
	
	stateName[10]			  	= "ReloadStart2";
	stateScript[10]				= "onReloadStart";
	stateTransitionOnTimeout[10]	  	= "ReloadStart2A";
	stateTimeoutValue[10]			= 0.25;
	stateSequence[10]			= "ReloadStart";
	
	stateName[11]			  	= "ReloadStart2A";
	stateScript[11]				= "AEShotgunLoadOne";
	stateTransitionOnTimeout[11]	  	= "ReloadA";
	stateTimeoutValue[11]		  	= 0.3;
	stateSequence[11]			= "Insert";
	
	stateName[12]			  	= "ReloadStart2B";
	stateScript[12]				= "PartCycle";
	stateTransitionOnTimeout[12]	  	= "Reloaded";
	stateTimeoutValue[12]		  	= 0.45;
	stateSequence[12]			= "PartCycle";
	
	stateName[13]				= "Reload";
	stateTransitionOnTimeout[13]     	= "Reloaded";
	stateTransitionOnTriggerDown[13]	= "AnotherAmmoCheck";
	stateWaitForTimeout[13]			= false;
	stateTimeoutValue[13]			= 0.3;
	stateSequence[13]			= "Insert";
	stateScript[13]				= "AEShotgunLoadOne";

	stateName[15]				= "Reloaded";
	stateTransitionOnTimeout[15]     	= "CheckAmmoA";
	stateTransitionOnTriggerDown[15]	= "AnotherAmmoCheck";
	stateWaitForTimeout[15]			= false;
	//stateTimeoutValue[15]			= 0.2;
	
	stateName[16]				= "CheckAmmoA";
	stateScript[16]				= "AEShotgunLoadCheck";
	stateTransitionOnTriggerDown[16]	= "AnotherAmmoCheck";
	stateTransitionOnTimeout[16]		= "CheckAmmoB";	
	
	stateName[17]				= "CheckAmmoB";
	stateTransitionOnTriggerDown[17]	= "AnotherAmmoCheck";
	stateTransitionOnNotLoaded[17]  = "EndReload";
	stateTransitionOnAmmo[17]		= "EndReload";
	stateTransitionOnNoAmmo[17]		= "ReloadA";
	
	stateName[14]				= "ReloadA";
	stateTransitionOnTimeout[14]     	= "Reload";
	stateTransitionOnTriggerDown[14]	= "AnotherAmmoCheck";
	stateWaitForTimeout[14]			= false;
	stateTimeoutValue[14]			= 0.45;
	stateSequence[14]			= "PartCycle";
	stateScript[14]				= "PartCycle";	
	
	stateName[18]			  	= "EndReload";
	stateTransitionOnTriggerDown[18]	= "AnotherAmmoCheck";
	stateScript[18]				= "onEndReload";
	stateTimeoutValue[18]		  	= 0.25;
	stateSequence[18]			= "ReloadEnd";
	stateTransitionOnTimeout[18]	  	= "Ready";
	stateWaitForTimeout[18]		  	= false;

	stateName[19]          = "Empty";
	stateTransitionOnTriggerDown[19]  = "Dryfire";
	stateTransitionOnLoaded[19] = "ReloadStart2";
	stateScript[19]        = "AEOnEmpty";

	stateName[20]           = "Dryfire";
	stateTransitionOnTriggerUp[20] = "Empty";
	stateWaitForTimeout[20]    = false;
	stateScript[20]      = "onDryFire";
	
	stateName[21]           = "AnotherAmmoCheck";
	stateTransitionOnTimeout[21]	  	= "preFire";
	stateScript[21]				= "AELoadCheck";
	
	stateName[22]           = "SemiAutoCheck"; //heeeeeeeeeeeeey
	stateTransitionOnTriggerUp[22]	  	= "FireLoadCheckA";
	
	stateName[23]           = "Insert";
	stateTimeoutValue[23]			= 0.05;
	stateTransitionOnTimeout[23]	  	= "ReloadStart2B";
	stateScript[23]				= "AEShotgunLoadOneEffectless";
};

function StrikerImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.reloadSound1Schedule);
	cancel(%obj.reloadSound2Schedule);
	cancel(%obj.reloadSound3Schedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, StrikerFire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function StrikerImage::AEOnLowClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(2, plant);
}

function StrikerImage::onReloadStart(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftleft); 
}

function StrikerImage::onEndReload(%this, %obj, %slot)
{
//	%obj.aeplayThread(2, shiftleft); 
}

function StrikerImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function StrikerImage::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function StrikerImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function StrikerImage::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.reloadSound1Schedule);
	cancel(%obj.reloadSound2Schedule);
	cancel(%obj.reloadSound3Schedule);
	cancel(%obj.pumpSoundSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function StrikerImage::AEShotgunLoadOne(%this,%obj,%slot)
{
	%obj.reloadSoundSchedule = schedule(150, 0, serverPlay3D, "StrikerInsert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
   %obj.schedule(50, "aeplayThread", "2", "shiftright");
   %obj.schedule(50, "aeplayThread", "3", "shiftleft");
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
}

function StrikerImage::PartCycle(%this,%obj,%slot)
{
	serverPlay3D(StrikerCloseSound,%obj.getPosition());	
	%obj.aeplayThread(2, shiftleft);
	%obj.aeplayThread(3, plant); 
}

function StrikerImage::AEShotgunLoadOneEffectless(%this,%obj,%slot)
{
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(StrikerSafetyImage)
{
   shapeFile = "./Striker/Striker12.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = StrikerItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = StrikerImage;
   doColorShift = true;
   colorShiftColor = StrikerItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function StrikerSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function StrikerSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(StrikerIronsightImage : StrikerImage)
{
	recoilHeight = 0.875;

	scopingImage = StrikerImage;
	sourceImage = StrikerImage;
	
   offset = "0 0 -0.015";
	eyeOffset = "-0.001 1.25 -1.088";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 2;
	R_MovePenalty = 0.5;
	
	stateName[10]				= "ReloadStart2";
	stateScript[10]				= "onDone";
	stateTimeoutValue[10]			= 1;
	stateTransitionOnTimeout[10]		= "";
	stateSound[10]				= "";
	
	stateName[9]				= "ReloadStart";
	stateScript[9]				= "onDone";
	stateTimeoutValue[9]			= 1;
	stateTransitionOnTimeout[9]		= "";
	stateSound[9]				= "";
};

function StrikerIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function StrikerIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function StrikerIronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, StrikerFire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function StrikerIronsightImage::onPump(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(500, 0, serverPlay3D, AEShellShotgun @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(StrikerPumpSound,%obj.getPosition());	
}

function StrikerIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function StrikerIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound, %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function StrikerIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound, %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
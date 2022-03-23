datablock AudioProfile(BNE_PythonFire1Sound)
{
   filename    = "./Sounds/Fire/Python/Python_fire1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_PythonFire2Sound)
{
   filename    = "./Sounds/Fire/Python/Python_fire2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_PythonFire3Sound)
{
   filename    = "./Sounds/Fire/Python/Python_fire3.wav";
   description = HeavyClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(BNE_PythonItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Python/Python.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Colt Python";
	iconName = "./Python/icon_Python";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = BNE_PythonImage;
	canDrop = true;
	
	AEAmmo = 6;
	AEType = AE_HeavyPAmmoItem.getID(); 
	AEBase = 1;

  RPM = 60;
  Recoil = "Medium";
	uiColor = "1 1 1";
  description = "The Ithaca Model 37 is an accurate 12-gauge shotgun made for both civilian and military use.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_PythonImage)
{
   // Basic Item properties
   shapeFile = "./Python/Python.dts";
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
   item = BNE_PythonItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BEShotgunShellDebris;
   shellExitDir        = "0 0 -1";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   safetyImage = BNE_PythonSafetyImage;
   scopingImage = BNE_PythonIronsightImage;
   doColorShift = true;
   colorShiftColor = BNE_PythonItem.colorShiftColor;
//   shellSound = AEShellRifle;
//   shellSoundMin = 450; //min delay for when the shell sound plays
//   shellSoundMax = 550; //max delay for when the shell sound plays
   muzzleFlashScale = "1.5 1.5 1.5";
   bulletScale = "1 1 1";

   projectileDamage = 27;
   projectileCount = 1;
   projectileHeadshotMult = 1.9;
   projectileVelocity = 200;
   projectileTagStrength = 0.35;  // tagging strength
   projectileTagRecovery = 0.03; // tagging decay rate

   recoilHeight = 1;
   recoilWidth = 0;
   recoilWidthMax = 0;
   recoilHeightMax = 20;

   spreadBurst = 1; // how much shots it takes to trigger spread i think
   spreadBase = 50;
   spreadReset = 400; // m
   spreadMin = 90;
   spreadMax = 250;

   screenshakeMin = "0.25 0.25 0.25"; 
   screenshakeMax = "0.5 0.5 0.5"; 
   farShotSound = RevolverDistantSound;
   farShotDistance = 40;

		sonicWhizz = true;
		whizzSupersonic = true;
		whizzThrough = false;
		whizzDistance = 12;
		whizzChance = 100;
		whizzAngle = 80;
	staticTotalRange = 100;			

	projectileFalloffStart = 16;
	projectileFalloffEnd = 64;
	projectileFalloffDamage = 0.73;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
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

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateTimeoutValue[3]            	= 0.35;
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[5]				= "FireLoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.05;
	stateTransitionOnTimeout[5]		= "FireLoadCheckB";
	
	stateName[6]				= "FireLoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6]  = "Ready";
	stateTransitionOnNoAmmo[6]		= "ReloadStart2";
	
	stateName[7]				= "LoadCheckA";
	stateScript[7]				= "AELoadCheck";
	stateTimeoutValue[7]			= 0.1;
	stateTransitionOnTimeout[7]		= "LoadCheckB";
						
	stateName[8]				= "LoadCheckB";
	stateTransitionOnAmmo[8]		= "Ready";
	stateTransitionOnNotLoaded[8] = "Empty";
	stateTransitionOnNoAmmo[8]		= "ReloadStart2";

	stateName[22]			  	= "ReloadStart2";
	stateTransitionOnTimeout[22]	  	= "ReloadStart2A";
	stateTimeoutValue[22]			= 0.3;
	stateSequence[22]			= "ReloadStart";
	stateScript[22]				= "onReloadStart";
	
	stateName[11]			  	= "ReloadStart2A";
	stateScript[11]				= "onReloadExtract";
	stateTransitionOnTimeout[11]	  	= "ReloadStart2B";
	stateTimeoutValue[11]		  	= 0.25;
	stateSequence[11]			= "ReloadOut";
	stateWaitForTimeout[11]		  	= true;

	stateName[4]			  	= "ReloadStart2B";
	stateScript[4]				= "LoadEffect";
	stateTransitionOnTimeout[4]	  	= "ReloadStart2C";
	stateTimeoutValue[4]		  	= 0.3;
	stateSequence[4]			= "ReloadIn";
	stateWaitForTimeout[4]		  	= true;
	
	stateName[12]				= "ReloadStart2C";
	stateScript[12]				= "AEShotgunAltLoadCheck";
	stateTimeoutValue[12]			= 0.1;
	stateWaitForTimeout[12]		  	= false;
	stateTransitionOnTriggerDown[12]	= "AnotherAmmoCheck";
	stateTransitionOnTimeout[12]	  	= "Reload";
	stateTransitionOnNotLoaded[12] = "LoadCheckA";

	stateName[9]			  	= "ReloadStart";
	stateScript[9]				= "onReloadStart";
	stateTransitionOnTimeout[9]	  	= "ReloadExtract";
	stateTimeoutValue[9]		  	= 0.3;
	stateWaitForTimeout[9]		  	= false;
	stateTransitionOnTriggerDown[9]	= "AnotherAmmoCheck";
	stateSequence[9]			= "ReloadStart";

	stateName[10]			  	= "ReloadExtract";
	stateScript[10]				= "onReloadExtract";
	stateTransitionOnTimeout[10]	  	= "Reload";
	stateTimeoutValue[10]		  	= 0.25;
	stateWaitForTimeout[10]		  	= false;
	stateTransitionOnTriggerDown[10]	= "AnotherAmmoCheck";
	stateSequence[10]			= "ReloadOut";
	
	stateName[13]				= "Reload";
	stateTransitionOnTimeout[13]     	= "Reloaded";
	stateTransitionOnTriggerDown[13]	= "AnotherAmmoCheck";
	stateWaitForTimeout[13]			= false;
	stateTimeoutValue[13]			= 0.4;
	stateSequence[13]			= "ReloadIn";
	stateScript[13]				= "LoadEffect";
	
	stateName[14]				= "Reloaded";
	stateTransitionOnTimeout[14]     	= "CheckAmmoA";
	stateWaitForTimeout[14]			= false;
	//stateTimeoutValue[14]			= 0.2;
	
	stateName[15]				= "CheckAmmoA";
	stateTransitionOnTriggerDown[15]	= "AnotherAmmoCheck";
	stateScript[15]				= "AEShotgunLoadCheck";
	stateTransitionOnTimeout[15]		= "CheckAmmoB";	
	
	stateName[16]				= "CheckAmmoB";
	stateTransitionOnTriggerDown[16]	= "AnotherAmmoCheck";
	stateTransitionOnNotLoaded[16]  = "EndReload";
	stateTransitionOnAmmo[16]		= "EndReload";
	stateTransitionOnNoAmmo[16]		= "Reload";
	
	stateName[17]			  	= "EndReload";
	stateScript[17]				= "onReloadEnd";
	stateTimeoutValue[17]		  	= 0.4;
	stateSequence[17]			= "ReloadEnd";
	stateTransitionOnTimeout[17]	  	= "Ready";
	//stateWaitForTimeout[17]		  	= false;

	stateName[18]          = "Empty";
	stateTransitionOnTriggerDown[18]  = "Dryfire";
	stateTransitionOnLoaded[18] = "ReloadStart";
	stateScript[18]        = "AEOnEmpty";

	stateName[19]           = "Dryfire";
	stateTransitionOnTriggerUp[19] = "Empty";
	stateWaitForTimeout[19]    = false;
	stateScript[19]      = "onDryFire";
	
	stateName[20]           = "AnotherAmmoCheck"; //heeeey
	stateTransitionOnTimeout[20]	  	= "EndReload";
	stateScript[20]				= "AELoadCheck";
	
	stateName[21]           = "SemiAutoCheck"; //heeeeeeeeeeeeey
	stateTransitionOnTriggerUp[21]	  	= "FireLoadCheckA";
};

function BNE_PythonImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.insertshellSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_PythonFire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_PythonImage::AEOnLowClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(2, plant);
}

function BNE_PythonImage::onReloadExtract(%this, %obj, %slot)
{
	%obj.playAudio(0, PythonOutSound);
	%obj.AEUnloadMag();
	%obj.baadDisplayAmmo(%this);
}

function BNE_PythonImage::onReloadStart(%this, %obj, %slot)
{
	%obj.playAudio(0, PythonOpenSound);
	%obj.aeplayThread(2, shiftleft); 
}

function BNE_PythonImage::onReloadEnd(%this,%obj,%slot)
{
	%obj.reloadSoundSchedule = %obj.schedule(100, playAudio, 0, PythonCloseSound);
	cancel(%obj.insertshellSchedule);
}

function BNE_PythonImage::onReloadStart2(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftleft); 
}

function BNE_PythonImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_PythonImage::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function BNE_PythonImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function BNE_PythonImage::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.pumpSoundSchedule);
	cancel(%obj.insertshellSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function BNE_PythonImage::LoadEffect(%this,%obj,%slot)
{
	//%obj.reloadSoundSchedule = %obj.schedule(50, playAudio, 0, "PythonInsert" @ getRandom(1, 3) @ "Sound");
	%obj.stopAudio(0);
	%obj.playAudio(0, "PythonInsert" @ getRandom(1, 3) @ "Sound");
    // %obj.schedule(100, "aeplayThread", "3", "plant");
    // %obj.schedule(150, "aeplayThread", "2", "shiftright");
    %obj.insertshellSchedule = %this.schedule(50,AEShotgunLoadOne,%obj,%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_PythonSafetyImage)
{
   shapeFile = "./Python/Python.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_PythonItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = BNE_PythonImage;
   doColorShift = true;
   colorShiftColor = BNE_PythonItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function BNE_PythonSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_PythonSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_PythonIronsightImage : BNE_PythonImage)
{
	recoilHeight = 0.5;

	scopingImage = BNE_PythonImage;
	sourceImage = BNE_PythonImage;
	
   offset = "0 0 -0.015";
	eyeOffset = "-0.0004 1.1 -0.7893";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;

  stateName[22]                  = "ReloadStart2";
	stateScript[22]                = "onDone";
	stateTimeoutValue[22]            = 1;
	stateTransitionOnTimeout[22]        = "";
	stateSound[22]                = "";
	
	stateName[9]				= "ReloadStart";
	stateScript[9]				= "onDone";
	stateTimeoutValue[9]			= 1;
	stateTransitionOnTimeout[9]		= "";
	stateSound[9]				= "";
};

function BNE_PythonIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_PythonIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_PythonIronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_PythonFire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_PythonIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_PythonIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound, %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_PythonIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound, %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
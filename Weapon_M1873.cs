datablock AudioProfile(BNE_M1873Fire1Sound)
{
   filename    = "./Sounds/Fire/M1873/M1873_fire1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_M1873Fire2Sound)
{
   filename    = "./Sounds/Fire/M1873/M1873_fire2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_M1873Fire3Sound)
{
   filename    = "./Sounds/Fire/M1873/M1873_fire3.wav";
   description = HeavyClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(BNE_M1873Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./M1873/M1873.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Winchester 1873";
	iconName = "./Icons/58";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = BNE_M1873Image;
	canDrop = true;
	
	AEAmmo = 6;
	AEType = AE_HeavyRAmmoItem.getID(); 
	AEBase = 1;

  RPM = 60;
  Recoil = "Medium";
	uiColor = "1 1 1";
  description = "The M1873 is a heavy Soviet shotgun designed to fire multiple different round types, such as tear gas grenades or less-lethal rounds.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_M1873Image)
{
   // Basic Item properties
   shapeFile = "./M1873/M1873.dts";
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
   item = BNE_M1873Item;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BERifleShellDebris;
   shellExitDir        = "0.2 0 1";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   safetyImage = BNE_M1873SafetyImage;
   scopingImage = BNE_M1873IronsightImage;
   doColorShift = true;
   colorShiftColor = BNE_M1873Item.colorShiftColor;
//   shellSound = AEShellRifle;
//   shellSoundMin = 450; //min delay for when the shell sound plays
//   shellSoundMax = 550; //max delay for when the shell sound plays
	muzzleFlashScale = "1.5 1.5 1.5";
	bulletScale = "1 1 1";

	projectileDamage = 38;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 200;
	projectileTagStrength = 0.35;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	projectileFalloffStart = 64;
	projectileFalloffEnd = 128;
	projectileFalloffDamage = 0.5;

	recoilHeight = 0.6;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadBase = 125;
	spreadReset = 800; // m
	spreadMin = 125;
	spreadMax = 800;
	screenshakeMin = "0.25 0.25 0.25"; 
	screenshakeMax = "0.5 0.5 0.5"; 
	farShotSound = SniperADistantSound;
	farShotDistance = 40;

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

	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

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
//	stateTransitionOnNoAmmo[2]       	= "FireEmpty";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateTimeoutValue[3]            	= 0.3;
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[4]                    	= "Pump";
	stateTimeoutValue[4]            	= 0.4;
	stateScript[4]                  	= "onPump";
	stateTransitionOnTimeout[4]     	= "FireLoadCheckA";
	stateAllowImageChange[4]        	= false;
	stateSequence[4]			= "Pump";
	stateWaitForTimeout[4]		  	= true;
	stateEjectShell[4]                = true;
	
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

	stateName[9]			  	= "ReloadStart";
	stateScript[9]				= "onReloadStart";
	stateTransitionOnTimeout[9]	  	= "Reload";
	stateTimeoutValue[9]		  	= 0.25;
	stateWaitForTimeout[9]		  	= false;
	stateTransitionOnTriggerDown[9]	= "AnotherAmmoCheck";
	stateSequence[9]			= "ReloadStart";
	
	stateName[10]			  	= "ReloadStart2";
	stateScript[10]				= "onReloadStart2";
	stateTransitionOnTimeout[10]	  	= "ReloadStart2A";
	stateTimeoutValue[10]			= 0.4;
	stateSequence[10]			= "ReloadStartEmpty";
	
	stateName[11]			  	= "ReloadStart2A";
	stateScript[11]				= "LoadEffectEmpty";
	stateTransitionOnTimeout[11]	  	= "ReloadEnd2";
	stateTimeoutValue[11]		  	= 0.25;
	stateSequence[11]			= "ReloadInEmpty";
	stateWaitForTimeout[11]		  	= true;

	stateName[22] = "ReloadEnd2";
	stateTransitionOnTimeout[22] = "ReloadStart2B";
	stateTimeoutValue[22] = 0.3;
	stateSequence[22] = "ReloadEndEmpty";
	stateWaitForTimeout[22] = true;
	
	stateName[12]				= "ReloadStart2B";
	stateScript[12]				= "AEShotgunAltLoadCheck";
	stateTimeoutValue[12]			= 0.1;
	stateWaitForTimeout[12]		  	= false;
	stateTransitionOnTriggerDown[12]	= "AnotherAmmoCheck";
	stateTransitionOnTimeout[12]	  	= "ReloadStart";
	stateTransitionOnNotLoaded[12] = "LoadCheckA";
	
	stateName[13]				= "Reload";
	stateTransitionOnTimeout[13]     	= "Reloaded";
	stateTransitionOnTriggerDown[13]	= "AnotherAmmoCheck";
	stateWaitForTimeout[13]			= false;
	stateTimeoutValue[13]			= 0.35;
	stateSequence[13]			= "ReloadIn";
	stateScript[13]				= "LoadEffect";
	
	stateName[14]				= "Reloaded";
	stateTransitionOnTimeout[14]     	= "CheckAmmoA";
	stateTransitionOnTriggerDown[14]	= "AnotherAmmoCheck";
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
	stateTransitionOnTriggerDown[17]	= "AnotherAmmoCheck";
	stateScript[17]				= "onEndReload";
	stateTimeoutValue[17]		  	= 0.3;
	stateSequence[17]			= "ReloadEnd";
	stateTransitionOnTimeout[17]	  	= "Ready";
	stateWaitForTimeout[17]		  	= false;

	stateName[18]          = "Empty";
	stateTransitionOnTriggerDown[18]  = "Dryfire";
	stateTransitionOnLoaded[18] = "ReloadStart2";
	stateScript[18]        = "AEOnEmpty";

	stateName[19]           = "Dryfire";
	stateTransitionOnTriggerUp[19] = "Empty";
	stateWaitForTimeout[19]    = false;
	stateScript[19]      = "onDryFire";
	
	stateName[20]           = "AnotherAmmoCheck"; //heeeey
	stateTransitionOnTimeout[20]	  	= "preFire";
	stateScript[20]				= "AELoadCheck";
	
	stateName[21]           = "SemiAutoCheck"; //heeeeeeeeeeeeey
	stateTransitionOnTriggerUp[21]	  	= "Pump";
};

function BNE_M1873Image::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.insertshellSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_M1873Fire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_M1873Image::AEOnLowClimb(%this, %obj, %slot) 
{
    %obj.aeplayThread(2, jump); 
}

function BNE_M1873Image::onReloadStart(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftleft); 
}

function BNE_M1873Image::onReloadStart2(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftLeft);
	serverPlay3D(BNE_M1873PumpBackSound,%obj.getPosition());	
}

function BNE_M1873Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_M1873Image::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function BNE_M1873Image::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function BNE_M1873Image::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.pumpSoundSchedule);
	cancel(%obj.insertshellSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function BNE_M1873Image::LoadEffect(%this,%obj,%slot)
{
    %obj.reloadSoundSchedule = schedule(150, %obj, serverPlay3D, "BNE_M1873Insert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
    %obj.schedule(150, "aeplayThread", "3", "plant");
    %obj.insertshellSchedule = %this.schedule(150,AEShotgunLoadOne,%obj,%slot);
}

function BNE_M1873Image::LoadEffectEmpty(%this,%obj,%slot)
{
    %obj.reloadSoundSchedule = schedule(100, %obj, serverPlay3D, "BNE_M1873Insert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
    %obj.schedule(100, "aeplayThread", "3", "plant");
    %obj.schedule(250, "aeplayThread", "2", "shiftLeft");
    %obj.schedule(250, "aeplayThread", "3", "shiftRight");
    %obj.insertshellSchedule = %this.schedule(100,AEShotgunLoadOne,%obj,%slot);
    schedule(300, %obj, serverPlay3D, BNE_M1873PumpForwardSound, %obj.getPosition());
}

function BNE_M1873Image::AEShotgunLoadOneEffectless(%this,%obj,%slot)
{
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
}

function BNE_M1873Image::onPump(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	schedule(300, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(BNE_M1873PumpSound,%obj.getPosition());	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M1873SafetyImage)
{
   shapeFile = "./M1873/M1873.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_M1873Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_M1873IronsightImage;
   safetyImage = BNE_M1873Image;
   doColorShift = true;
   colorShiftColor = BNE_M1873Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_M1873SafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_M1873SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_M1873SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M1873IronsightImage : BNE_M1873Image)
{
	recoilHeight = 0.25;

	scopingImage = BNE_M1873Image;
	sourceImage = BNE_M1873Image;
	
   offset = "0 0 -0.015";
	eyeOffset = "-0.0 0.9 -0.651";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
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

function BNE_M1873IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_M1873IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_M1873IronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_M1873Fire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_M1873IronsightImage::onPump(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(500, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(BNE_M1873PumpSound,%obj.getPosition());	
}

function BNE_M1873IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_M1873IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_M1873IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
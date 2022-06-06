datablock AudioProfile(BNE_M500Fire1Sound)
{
   filename    = "./Sounds/Fire/M500/M500_fire1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_M500Fire2Sound)
{
   filename    = "./Sounds/Fire/M500/M500_fire2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_M500Fire3Sound)
{
   filename    = "./Sounds/Fire/M500/M500_fire3.wav";
   description = HeavyClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(BNE_M500Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./M500/M500.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Mossberg 500";
	iconName = "./Icons/21";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = BNE_M500Image;
	canDrop = true;
	
	AEAmmo = 8;
	AEType = AE_LightSAmmoItem.getID(); 
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
datablock ShapeBaseImageData(BNE_M500Image)
{
   // Basic Item properties
   shapeFile = "./M500/M500.dts";
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
   item = BNE_M500Item;
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
   safetyImage = BNE_M500SafetyImage;
   scopingImage = BNE_M500IronsightImage;
   doColorShift = true;
   colorShiftColor = BNE_M500Item.colorShiftColor;
//   shellSound = AEShellRifle;
//   shellSoundMin = 450; //min delay for when the shell sound plays
//   shellSoundMax = 550; //max delay for when the shell sound plays
   muzzleFlashScale = "1.5 1.5 1.5";
   bulletScale = "1 1 1";

   projectileDamage = 13;
   projectileCount = 8;
   projectileHeadshotMult = 1.75;
   projectileVelocity = 200;
   projectileTagStrength = 0.35;  // tagging strength
   projectileTagRecovery = 0.03; // tagging decay rate

   recoilHeight = 2;
   recoilWidth = 0;
   recoilWidthMax = 0;
   recoilHeightMax = 20;
   spreadBurst = 1; // how much shots it takes to trigger spread i think
   spreadBase = 250;
   spreadReset = 900; // m
   spreadMin = 250;
   spreadMax = 250;
   screenshakeMin = "0.25 0.25 0.25"; 
   screenshakeMax = "0.5 0.5 0.5"; 
   farShotSound = ShottyBDistantSound;
   farShotDistance = 40;

		sonicWhizz = true;
		whizzSupersonic = false;
		whizzThrough = false;
		whizzDistance = 12;
		whizzChance = 100;
		whizzAngle = 80;
	staticTotalRange = 100;			

	projectileFalloffStart = $ae_falloffShotgunStart;
	projectileFalloffEnd = $ae_falloffShotgunEnd;
	projectileFalloffDamage = $ae_falloffShotgun;

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
	
	stateName[4]                    	= "Pump";
	stateTimeoutValue[4]            	= 0.5;
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
	stateTimeoutValue[9]		  	= 0.2;
	stateWaitForTimeout[9]		  	= false;
	stateTransitionOnTriggerDown[9]	= "AnotherAmmoCheck";
	stateSequence[9]			= "ReloadStart";
	
	stateName[10]			  	= "ReloadStart2";
	stateScript[10]				= "onReloadStart2";
	stateTransitionOnTimeout[10]	  	= "ReloadStart2A";
	stateTimeoutValue[10]			= 0.35;
	stateSequence[10]			= "ReloadStartEmpty";
	
	stateName[11]			  	= "ReloadStart2A";
	stateScript[11]				= "LoadEffectEmpty";
	stateTransitionOnTimeout[11]	  	= "ReloadEnd2";
	stateTimeoutValue[11]		  	= 0.4;
	stateSequence[11]			= "ReloadEmpty";
	stateWaitForTimeout[11]		  	= true;

	stateName[12] = "ReloadEnd2";
	stateTransitionOnTimeout[12] = "ReloadStart2B";
	stateTimeoutValue[12] = 0.2;
	stateSequence[12] = "ReloadEndEmpty";
	stateWaitForTimeout[12] = true;
	
	stateName[13]				= "ReloadStart2B";
	stateScript[13]				= "AEShotgunAltLoadCheck";
	stateTimeoutValue[13]			= 0.1;
	stateWaitForTimeout[13]		  	= false;
	stateTransitionOnTriggerDown[13]	= "AnotherAmmoCheck";
	stateTransitionOnTimeout[13]	  	= "ReloadStart";
	stateTransitionOnNotLoaded[13] = "LoadCheckA";
	
	stateName[14]				= "Reload";
	stateTransitionOnTimeout[14]     	= "Reloaded";
	stateTransitionOnTriggerDown[14]	= "AnotherAmmoCheck";
	stateWaitForTimeout[14]			= false;
	stateTimeoutValue[14]			= 0.4;
	stateSequence[14]			= "Reload";
	stateScript[14]				= "LoadEffect";
	
	stateName[15]				= "Reloaded";
	stateTransitionOnTimeout[15]     	= "CheckAmmoA";
	stateTransitionOnTriggerDown[15]	= "AnotherAmmoCheck";
	stateWaitForTimeout[15]			= false;
	//stateTimeoutValue[15]			= 0.2;
	
	stateName[16]				= "CheckAmmoA";
	stateTransitionOnTriggerDown[16]	= "AnotherAmmoCheck";
	stateScript[16]				= "AEShotgunLoadCheck";
	stateTransitionOnTimeout[16]		= "CheckAmmoB";	
	
	stateName[17]				= "CheckAmmoB";
	stateTransitionOnTriggerDown[17]	= "AnotherAmmoCheck";
	stateTransitionOnNotLoaded[17]  = "EndReload";
	stateTransitionOnAmmo[17]		= "EndReload";
	stateTransitionOnNoAmmo[17]		= "Reload";
	
	stateName[18]			  	= "EndReload";
	stateTransitionOnTriggerDown[18]	= "AnotherAmmoCheck";
	stateScript[18]				= "onEndReload";
	stateTimeoutValue[18]		  	= 0.2;
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
	
	stateName[21]           = "AnotherAmmoCheck"; //heeeey
	stateTransitionOnTimeout[21]	  	= "preFire";
	stateScript[21]				= "AELoadCheck";
	
	stateName[22]           = "SemiAutoCheck"; //heeeeeeeeeeeeey
	stateTransitionOnTriggerUp[22]	  	= "Pump";
};

function BNE_M500Image::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.insertshellSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_M500Fire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_M500Image::AEOnLowClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(2, plant);
}

function BNE_M500Image::onReloadStart(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftleft); 
}

function BNE_M500Image::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_M500Image::onReload2End(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BNE_M500Image::onReloadStart2(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftright);
	serverPlay3D(BNE_M500PumpBackSound,%obj.getPosition());	
}

function BNE_M500Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_M500Image::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function BNE_M500Image::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function BNE_M500Image::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.pumpSoundSchedule);
	cancel(%obj.insertshellSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function BNE_M500Image::LoadEffect(%this,%obj,%slot)
{
	%obj.reloadSoundSchedule = schedule(150, 0, serverPlay3D, "BNE_Ithaca37Insert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
    %obj.schedule(50, "aeplayThread", "3", "plant");
    %obj.schedule(50, "aeplayThread", "2", "shiftright");
    %obj.insertshellSchedule = %this.schedule(200,AEShotgunLoadOne,%obj,%slot);
}

function BNE_M500Image::LoadEffectEmpty(%this,%obj,%slot)
{
	%obj.reloadSoundSchedule = schedule(200, 0, serverPlay3D, "BNE_Ithaca37Insert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
    %obj.schedule(100, "aeplayThread", "3", "plant");
    %obj.schedule(250, "aeplayThread", "2", "shiftLeft");
    %obj.insertshellSchedule = %this.schedule(200,AEShotgunLoadOne,%obj,%slot);
    schedule(300, %obj, serverPlay3D, BNE_M500PumpForwardSound, %obj.getPosition());
}

function BNE_M500Image::AEShotgunLoadOneEffectless(%this,%obj,%slot)
{
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
}

function BNE_M500Image::onPump(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(300, 0, serverPlay3D, AEShellShotgun @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(BNE_M500PumpSound,%obj.getPosition());	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M500SafetyImage)
{
   shapeFile = "./M500/M500.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_M500Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_M500IronsightImage;
   safetyImage = BNE_M500Image;
   doColorShift = true;
   colorShiftColor = BNE_M500Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_M500SafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_M500SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_M500SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_M500IronsightImage : BNE_M500Image)
{
	recoilHeight = 0.5;

	scopingImage = BNE_M500Image;
	sourceImage = BNE_M500Image;
	
   offset = "0 0 -0.015";
	eyeOffset = "0 1.25 -0.84";
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

function BNE_M500IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_M500IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_M500IronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_M500Fire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_M500IronsightImage::onPump(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(500, 0, serverPlay3D, AEShellShotgun @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(BNE_M500PumpSound,%obj.getPosition());	
}

function BNE_M500IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_M500IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_M500IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
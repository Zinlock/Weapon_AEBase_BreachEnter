datablock AudioProfile(BNE_KS23Fire1Sound)
{
   filename    = "./Sounds/Fire/KS23/KS23_fire1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_KS23Fire2Sound)
{
   filename    = "./Sounds/Fire/KS23/KS23_fire2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(BNE_KS23Fire3Sound)
{
   filename    = "./Sounds/Fire/KS23/KS23_fire3.wav";
   description = HeavyClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(BNE_KS23Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./KS23/KS23.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: KS-23";
	iconName = "./Icons/58";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	 // Dynamic properties defined by the scripts
	image = BNE_KS23Image;
	canDrop = true;
	
	AEAmmo = 4;
	AEType = AE_HeavySAmmoItem.getID(); 
	AEBase = 1;

  RPM = 60;
  Recoil = "Heavy";
	uiColor = "1 1 1";
  description = "The KS23 is a heavy Soviet shotgun designed to fire multiple different round types, such as tear gas grenades or less-lethal rounds.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_KS23Image)
{
   // Basic Item properties
   shapeFile = "./KS23/KS23.dts";
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
   item = BNE_KS23Item;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BEHeavyShotgunDebris;
   shellExitDir        = "1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   safetyImage = BNE_KS23SafetyImage;
   scopingImage = BNE_KS23IronsightImage;
   doColorShift = true;
   colorShiftColor = BNE_KS23Item.colorShiftColor;
//   shellSound = AEShellRifle;
//   shellSoundMin = 450; //min delay for when the shell sound plays
//   shellSoundMax = 550; //max delay for when the shell sound plays
	muzzleFlashScale = "1.5 1.5 1.5";
	bulletScale = "1 1 1";

	projectileDamage = 17;
	projectileCount = 10;
	projectileHeadshotMult = 1.5;
	projectileVelocity = 200;
	projectileTagStrength = 0.35;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	projectileFalloffStart = 8;
	projectileFalloffEnd = 32;
	projectileFalloffDamage = 0.2;

	recoilHeight = 3;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;
	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadBase = 600;
	spreadReset = 900; // m
	spreadMin = 600;
	spreadMax = 600;
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
	stateTimeoutValue[3]            	= 0.45;
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

	stateName[22] = "ReloadEnd2";
	stateTransitionOnTimeout[22] = "ReloadStart2B";
	stateTimeoutValue[22] = 0.2;
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
	stateTimeoutValue[13]			= 0.55;
	stateSequence[13]			= "Reload";
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
	stateTimeoutValue[17]		  	= 0.2;
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

function BNE_KS23Image::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.insertshellSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_KS23Fire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_KS23Image::AEOnLowClimb(%this, %obj, %slot) 
{
    %obj.aeplayThread(2, jump); 
}

function BNE_KS23Image::onReloadStart(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftleft); 
}

function BNE_KS23Image::onReloadStart2(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftright);
	serverPlay3D(BNE_KS23PumpBackSound,%obj.getPosition());	
}

function BNE_KS23Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_KS23Image::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function BNE_KS23Image::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function BNE_KS23Image::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.pumpSoundSchedule);
	cancel(%obj.insertshellSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function BNE_KS23Image::LoadEffect(%this,%obj,%slot)
{
    %obj.reloadSoundSchedule = schedule(250, %obj, serverPlay3D, "BNE_KS23Insert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
    %obj.schedule(150, "aeplayThread", "3", "plant");
    %obj.insertshellSchedule = %this.schedule(250,AEShotgunLoadOne,%obj,%slot);
}

function BNE_KS23Image::LoadEffectEmpty(%this,%obj,%slot)
{
    %obj.reloadSoundSchedule = schedule(200, %obj, serverPlay3D, "BNE_KS23Insert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
    %obj.schedule(100, "aeplayThread", "3", "plant");
    %obj.schedule(250, "aeplayThread", "2", "shiftLeft");
    %obj.insertshellSchedule = %this.schedule(200,AEShotgunLoadOne,%obj,%slot);
    schedule(300, %obj, serverPlay3D, BNE_KS23PumpForwardSound, %obj.getPosition());
}

function BNE_KS23Image::AEShotgunLoadOneEffectless(%this,%obj,%slot)
{
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
}

function BNE_KS23Image::onPump(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(300, 0, serverPlay3D, AEShellHeavyShotgun @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(BNE_KS23PumpSound,%obj.getPosition());	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_KS23SafetyImage)
{
   shapeFile = "./KS23/KS23.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_KS23Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = BNE_KS23Image;
   doColorShift = true;
   colorShiftColor = BNE_KS23Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function BNE_KS23SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_KS23SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_KS23IronsightImage : BNE_KS23Image)
{
	recoilHeight = 0.5;

	scopingImage = BNE_KS23Image;
	sourceImage = BNE_KS23Image;
	
   offset = "0 0 -0.015";
	eyeOffset = "-0.0025 1.25 -0.821";
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

function BNE_KS23IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_KS23IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_KS23IronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_KS23Fire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_KS23IronsightImage::onPump(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(500, 0, serverPlay3D, AEShellHeavyShotgun @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(BNE_KS23PumpSound,%obj.getPosition());	
}

function BNE_KS23IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_KS23IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound, %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_KS23IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound, %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
datablock AudioProfile(BNE_MosinFire1Sound)
{
   filename    = "./Sounds/Fire/Mosin/Mosin_fire1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_MosinFire2Sound)
{
   filename    = "./Sounds/Fire/Mosin/Mosin_fire2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_MosinFire3Sound)
{
   filename    = "./Sounds/Fire/Mosin/Mosin_fire3.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_MosinStabSound)
{
   filename    = "./Fire/Mosin/MosinStab.wav";
   description = AudioClose3d;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(BNE_MosinItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Mosin/Mosin.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Mosin Nagant";
	iconName = "./Icons/21";
	doColorShift = true;
	colorShiftColor = "0.75 0.75 0.75 1";

	 // Dynamic properties defined by the scripts
	image = BNE_MosinImage;
	canDrop = true;
	
	AEAmmo = 5;
	AEType = AE_HeavyRAmmoItem.getID();
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
datablock ShapeBaseImageData(BNE_MosinImage)
{
   // Basic Item properties
   shapeFile = "./Mosin/Mosin.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.05";
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
   item = BNE_MosinItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AE_BERifleShellDebris;
   shellExitDir        = "1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   safetyImage = BNE_MosinBayonetImage;
   scopingImage = BNE_MosinIronsightImage;
   doColorShift = true;
   colorShiftColor = BNE_MosinItem.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 30;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 400;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.77;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 350; // m
	spreadBase = 150;
	spreadMin = 150;
	spreadMax = 1000;

	screenshakeMin = "0.2 0.2 0.2";
	screenshakeMax = "0.4 0.4 0.4";

	farShotSound = SniperBDistantSound;
	farShotDistance = 40;

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
	staticUnitsPerSecond = $ae_RifleUPS;

	projectileFalloffStart = 64;
	projectileFalloffEnd = 192;
	projectileFalloffDamage = 2;

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
	
	stateName[4]                    	= "Bolt";
	stateTimeoutValue[4]            	= 0.6;
	stateScript[4]                  	= "onBolt";
	stateTransitionOnTimeout[4]     	= "FireLoadCheckA";
	stateAllowImageChange[4]        	= false;
	stateSequence[4]			= "Bolt";
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

	stateName[9]			  	= "ReloadStart2";
	stateTransitionOnTimeout[9]	  	= "ReloadStart2A";
	stateTimeoutValue[9]			= 0.3;
	stateSequence[9]			= "ReloadStart";
	stateScript[9]				= "onReloadStart";
	stateSound[9]				= BNE_MosinBoltOpenSound;

	stateName[10]			  	= "ReloadStart2A";
	stateScript[10]				= "LoadEffect";
	stateTransitionOnTimeout[10]	  	= "ReloadStart2B";
	stateTimeoutValue[10]		  	= 0.4;
	stateSequence[10]			= "ReloadIn";
	stateWaitForTimeout[10]		  	= true;
	
	stateName[11]				= "ReloadStart2B";
	stateScript[11]				= "AEShotgunAltLoadCheck";
	stateTimeoutValue[11]			= 0.1;
	stateWaitForTimeout[11]		  	= false;
	stateTransitionOnTriggerDown[11]	= "AnotherAmmoCheck";
	stateTransitionOnTimeout[11]	  	= "Reload";
	stateTransitionOnNotLoaded[11] = "LoadCheckA";

	stateName[12]			  	= "ReloadStart";
	stateScript[12]				= "onReloadStart";
	stateTransitionOnTimeout[12]	  	= "Reload";
	stateTimeoutValue[12]		  	= 0.3;
	stateWaitForTimeout[12]		  	= false;
	stateSequence[12]			= "ReloadStart";
	stateSound[12]				= BNE_MosinBoltOpenSound;
	
	stateName[13]				= "Reload";
	stateTransitionOnTimeout[13]     	= "Reloaded";
	stateTransitionOnTriggerDown[13]	= "AnotherAmmoCheck";
	stateWaitForTimeout[13]			= false;
	stateTimeoutValue[13]			= 0.5;
	stateSequence[13]			= "reloadIn";
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
	stateTimeoutValue[17]		  	= 0.3;
	stateSequence[17]			= "reloadEnd";
	stateTransitionOnTimeout[17]	  	= "Ready";
	stateWaitForTimeout[17]		  	= true;
	stateSound[17]				= BNE_MosinBoltCloseSound;

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
	stateTransitionOnTriggerUp[21]	  	= "Bolt";
};

function BNE_MosinImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_MosinFire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_MosinImage::onReloadEnd(%this,%obj,%slot)
{
	cancel(%obj.insertshellSchedule);
}

function BNE_MosinImage::onReloadStart(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant); 
}

function BNE_MosinImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_MosinImage::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function BNE_MosinImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function BNE_MosinImage::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.BoltSoundSchedule);
	cancel(%obj.insertshellSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function BNE_MosinImage::LoadEffect(%this,%obj,%slot)
{
	%obj.reloadSoundSchedule = schedule(150, 0, serverPlay3D, "BNE_MosinInsert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
    %obj.schedule(50, "aeplayThread", "3", "plant");
    %obj.insertshellSchedule = %this.schedule(200,AEShotgunLoadOne,%obj,%slot);
}

function BNE_MosinImage::AEShotgunLoadOneEffectless(%this,%obj,%slot)
{
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
}

function BNE_MosinImage::onBolt(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(400, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(BNE_MosinBoltSound,%obj.getPosition());	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_MosinBayonetImage)
{
   shapeFile = "./Mosin/Mosin.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_MosinItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_MosinIronsightImage;
   safetyImage = BNE_MosinImage;
   doColorShift = true;
   colorShiftColor = BNE_MosinItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.35;
	stateTransitionOnTimeout[0]     	= "Ready";
	stateSequence[0]			= "bayonetOpen";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Stab";
	stateSequence[1]			= "bayonetRoot";
	
	stateName[2]				= "Stab";
	stateScript[2]				= "onStab";
	stateTimeoutValue[2]            	= 1;
	stateTransitionOnTimeout[2]     	= "Ready";
};

function BNE_MosinBayonetImage::onStab(%this,%obj,%slot)
{
	%obj.playAudio(0, BNE_MosinStabSound);	
    %obj.schedule(350, "aeplayThread", "2", "jump");
	%obj.aeplayThread(2, wrench); 
}

function BNE_MosinBayonetImage::onMount(%this,%obj,%slot)
{
    %obj.schedule(400, "aeplayThread", "2", "plant");
	%this.AEMountSetup(%obj, %slot);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_MosinBayonetImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_MosinIronsightImage : BNE_MosinImage)
{
	recoilHeight = 0.5;

	scopingImage = BNE_MosinImage;
	sourceImage = BNE_MosinImage;
	
	eyeOffset = "0.0175 0.5 -0.35";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;
   
    stateName[9]                  = "ReloadStart2";
	stateScript[9]                = "onDone";
	stateTimeoutValue[9]            = 1;
	stateTransitionOnTimeout[9]        = "";
	stateSound[9]                = "";
	
	stateName[12]				= "ReloadStart";
	stateScript[12]				= "onDone";
	stateTimeoutValue[12]			= 1;
	stateTransitionOnTimeout[12]		= "";
	stateSound[12]				= "";
};

function BNE_MosinIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_MosinIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_MosinIronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_MosinFire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_MosinIronsightImage::onBolt(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(500, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
	serverPlay3D(BNE_MosinBoltSound,%obj.getPosition());	
}

function BNE_MosinIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_MosinIronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_MosinIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
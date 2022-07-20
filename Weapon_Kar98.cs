datablock AudioProfile(BNE_Kar98Fire1Sound)
{
   filename    = "./Sounds/Fire/Kar98/Kar98_fire1.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_Kar98Fire2Sound)
{
   filename    = "./Sounds/Fire/Kar98/Kar98_fire2.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(BNE_Kar98Fire3Sound)
{
   filename    = "./Sounds/Fire/Kar98/Kar98_fire3.wav";
   description = MediumClose3D;
   preload = true;
};

// Kar98
datablock DebrisData(BNE_Kar98StripperDebris)
{
	shapeFile = "./Kar98/Kar98Stripper.dts";
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
datablock ItemData(BNE_Kar98Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Kar98/Kar98.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Kar98k";
	iconName = "./Icons/Icon_Kar98";
	doColorShift = true;
	colorShiftColor = "0.75 0.75 0.75 1";

	 // Dynamic properties defined by the scripts
	image = BNE_Kar98Image;
	canDrop = true;
	
	AEAmmo = 5;
	AEType = AE_HeavierRAmmoItem.getID();
	AEBase = 1;

  RPM = 60;
  Recoil = "Medium";
	uiColor = "1 1 1";
  description = "The Karabiner 98 kurz is a bolt-action rifle chambered for the 7.92 Mauser cartridge. " NL " It was adopted on 21 June 1935 as the standard service rifle by the German Wehrmacht.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BNE_Kar98Image)
{
   // Basic Item properties
   shapeFile = "./Kar98/Kar98.dts";
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
   item = BNE_Kar98Item;
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
   safetyImage = BNE_Kar98SafetyImage;
   scopingImage = BNE_Kar98IronsightImage;
   doColorShift = true;
   colorShiftColor = BNE_Kar98Item.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 35;
	projectileCount = 1;
	projectileHeadshotMult = 3.5;
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

	projectileFalloffStart = $ae_falloffSniperStart;
	projectileFalloffEnd = $ae_falloffSniperEnd;
	projectileFalloffDamage = $ae_falloffSniper;

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
	staticUnitsPerSecond = $ae_SniperUPS;

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
	stateSound[4]				= BNE_MosinBoltSound;
	
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

	stateName[9]				= "ReloadStart2";
	stateTimeoutValue[9]			= 0.3;
	stateScript[9]				= "onReloadStart";
	stateTransitionOnTimeout[9]		= "StripperStart";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "ReloadStart";
	stateSound[9]				= BNE_MosinBoltOpenSound;
	
	stateName[10]				= "StripperStart";
	stateTimeoutValue[10]			= 0.15;
	stateScript[10]				= "onStripperStart";
	stateTransitionOnTimeout[10]		= "StripperWait";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "StripperStart";
	stateSound[10]				= BNE_Kar98ClipInSound;
	
	stateName[11]				= "StripperWait";
	stateTransitionOnTimeout[11]     	= "StripperLoad";
	stateTimeoutValue[11]			= 0.1;
	
	stateName[12]				= "StripperLoad";
	stateTimeoutValue[12]			= 0.4;
	stateScript[12]				= "onStripperLoad";
	stateTransitionOnTimeout[12]		= "StripperEnd";
	stateWaitForTimeout[12]			= true;
	stateSequence[12]			= "StripperLoad";
	
	stateName[13]				= "StripperEnd";
	stateTimeoutValue[13]			= 0.2;
	stateScript[13]				= "onStripperEnd";     
	stateTransitionOnTimeout[13]		= "Reloaded2";
	stateWaitForTimeout[13]			= true;
	stateSequence[13]			= "StripperEnd";
	stateSound[13]				= BNE_Kar98RemoveClipSound;
	
	stateName[14]				= "Reloaded2";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "EndReload";

	stateName[15]			  	= "ReloadStart";
	stateScript[15]				= "onReloadStart";
	stateTransitionOnTimeout[15]	  	= "Reload";
	stateTimeoutValue[15]		  	= 0.3;
	stateWaitForTimeout[15]		  	= false;
	stateSequence[15]			= "ReloadStart";
	stateSound[15]				= BNE_MosinBoltOpenSound;
	
	stateName[16]				= "Reload";
	stateTransitionOnTimeout[16]     	= "Reloaded";
	stateTransitionOnTriggerDown[16]	= "AnotherAmmoCheck";
	stateWaitForTimeout[16]			= false;
	stateTimeoutValue[16]			= 0.5;
	stateSequence[16]			= "reloadIn";
	stateScript[16]				= "LoadEffect";
	
	stateName[17]				= "Reloaded";
	stateTransitionOnTimeout[17]     	= "CheckAmmoA";
	stateWaitForTimeout[17]			= false;
	//stateTimeoutValue[17]			= 0.2;
	
	stateName[18]				= "CheckAmmoA";
	stateTransitionOnTriggerDown[18]	= "AnotherAmmoCheck";
	stateScript[18]				= "AEShotgunLoadCheck";
	stateTransitionOnTimeout[18]		= "CheckAmmoB";	
	
	stateName[19]				= "CheckAmmoB";
	stateTransitionOnTriggerDown[19]	= "AnotherAmmoCheck";
	stateTransitionOnNotLoaded[19]  = "EndReload";
	stateTransitionOnAmmo[19]		= "EndReload";
	stateTransitionOnNoAmmo[19]		= "Reload";
	
	stateName[20]			  	= "EndReload";
	stateScript[20]				= "onReloadEnd";
	stateTimeoutValue[20]		  	= 0.3;
	stateSequence[20]			= "reloadEnd";
	stateTransitionOnTimeout[20]	  	= "Ready";
	stateWaitForTimeout[20]		  	= true;
	stateSound[20]				= BNE_MosinBoltCloseSound;

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "ReloadStart";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";
	
	stateName[23]           = "AnotherAmmoCheck"; //heeeey
	stateTransitionOnTimeout[23]	  	= "EndReload";
	stateScript[23]				= "AELoadCheck";
	
	stateName[24]           = "SemiAutoCheck"; //heeeeeeeeeeeeey
	stateTransitionOnTriggerUp[24]	  	= "Bolt";
};

function BNE_Kar98Image::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_Kar98Fire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_Kar98Image::onReloadEnd(%this,%obj,%slot)
{
	cancel(%obj.insertshellSchedule);
}

function BNE_Kar98Image::onStripperStart(%this, %obj, %slot)
{
    %obj.schedule(50, "aeplayThread", "3", "plant");
}

function BNE_Kar98Image::onStripperLoad(%this, %obj, %slot)
{
	serverPlay3D("BNE_Kar98ClipInsert" @ getRandom(1, 2) @ "Sound", %obj.getHackPosition());
	%obj.aeplayThread(2, shiftright); 
	%obj.aeplayThread(3, shiftleft); 
}

function BNE_Kar98Image::onStripperEnd(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftright); 
    %this.onMagDrop(%obj, %slot);
}

function BNE_Kar98Image::onReloadStart(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant); 
}

function BNE_Kar98Image::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function BNE_Kar98Image::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function BNE_Kar98Image::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function BNE_Kar98Image::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.BoltSoundSchedule);
	cancel(%obj.insertshellSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function BNE_Kar98Image::LoadEffect(%this,%obj,%slot)
{
	%obj.reloadSoundSchedule = schedule(150, 0, serverPlay3D, "BNE_MosinInsert" @ getRandom(1, 3) @ "Sound", %obj.getPosition());
    %obj.schedule(50, "aeplayThread", "3", "plant");
    %obj.insertshellSchedule = %this.schedule(200,AEShotgunLoadOne,%obj,%slot);
}

function BNE_Kar98Image::AEShotgunLoadOneEffectless(%this,%obj,%slot)
{
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
}

function BNE_Kar98Image::onBolt(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(400, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BNE_Kar98Image::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BNE_Kar98StripperImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////


datablock ShapeBaseImageData(BNE_Kar98StripperImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.1 0.2 0.4";
   rotation = eulerToMatrix( "0 0 0" );	
	
	casing = BNE_Kar98StripperDebris;
	shellExitDir        = "1 0 1";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;	
	shellVelocity       = 3.0;
	
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

function BNE_Kar98StripperImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BNE_Kar98SafetyImage)
{
   shapeFile = "./Kar98/Kar98.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BNE_Kar98Item;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   scopingImage = BNE_Kar98IronsightImage;
   safetyImage = BNE_Kar98Image;
   doColorShift = true;
   colorShiftColor = BNE_Kar98Item.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function BNE_Kar98SafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function BNE_Kar98SafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function BNE_Kar98SafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}



datablock ShapeBaseImageData(BNE_Kar98IronsightImage : BNE_Kar98Image)
{
	recoilHeight = 0.5;

	spreadBase = 50;
	spreadMin = 50;

	scopingImage = BNE_Kar98Image;
	sourceImage = BNE_Kar98Image;
	
	eyeOffset = "0 0.5 -0.395";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;
   
    stateName[9]                  = "ReloadStart2";
	stateScript[9]                = "onDone";
	stateTimeoutValue[9]            = 1;
	stateTransitionOnTimeout[9]        = "";
	stateSound[9]                = "";
	
	stateName[15]				= "ReloadStart";
	stateScript[15]				= "onDone";
	stateTimeoutValue[15]			= 1;
	stateTransitionOnTimeout[15]		= "";
	stateSound[15]				= "";
};

function BNE_Kar98IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function BNE_Kar98IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function BNE_Kar98IronsightImage::AEOnFire(%this,%obj,%slot)
{
	%obj.blockImageDismount = true;
	%obj.schedule(800, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, BNE_Kar98Fire @ getRandom(1, 3) @ Sound);	

	Parent::AEOnFire(%this, %obj, %slot);
}

function BNE_Kar98IronsightImage::onBolt(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	schedule(500, 0, serverPlay3D, AEShellRifle @ getRandom(1,2) @ Sound, %obj.getPosition());
}

function BNE_Kar98IronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function BNE_Kar98IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound); // %obj.getHackPosition());
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BNE_Kar98IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound); // %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}
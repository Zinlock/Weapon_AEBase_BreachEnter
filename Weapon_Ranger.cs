datablock AudioProfile(RangerFire1Sound)
{
   filename    = "./Sounds/Fire/Ranger/Ranger_FIRE_1.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(RangerFire2Sound)
{
   filename    = "./Sounds/Fire/Ranger/Ranger_FIRE_2.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(RangerFire3Sound)
{
   filename    = "./Sounds/Fire/Ranger/Ranger_FIRE_3.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(RangerFire4Sound)
{
   filename    = "./Sounds/Fire/Ranger/Ranger_FIRE_4.wav";
   description = HeavyClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(RangerItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Ranger/Ranger.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "B&E: Ranger";
	iconName = "./Icons/39";
	doColorShift = true;
	colorShiftColor = "0.4 0.4 0.4 1";

	 // Dynamic properties defined by the scripts
	image = RangerImage;
	canDrop = true;

	AEAmmo = 2;
	AEType = AE_HeavySAmmoItem.getID();
	AEBase = 1;

	RPM = 100;
	recoil = "Heavy"; 
	uiColor = "1 1 1";
	description = "The Ranger is an improvised short double barrel shotgun made with Russian gun parts.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(RangerImage)
{
   // Basic Item properties
   shapeFile = "./Ranger/Ranger.dts";
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
   item = RangerItem;
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
	hideHands = false;
	safetyImage = RangerSafetyImage;
    scopingImage = RangerIronsightImage;
	doColorShift = true;
	colorShiftColor = RangerItem.colorShiftColor;//"0.400 0.196 0 1.000";

	muzzleFlashScale = "1.5 1.5 1.5";
	bulletScale = "1 1 1";

   projectileDamage = 29;
   projectileCount = 6;
   projectileHeadshotMult = 1.65;
   projectileVelocity = 200;
   projectileTagStrength = 0.35;  // tagging strength
   projectileTagRecovery = 0.03; // tagging decay rate

   recoilHeight = 20;
   recoilWidth = 0;
   recoilWidthMax = 0;
   recoilHeightMax = 20;
   spreadBurst = 1; // how much shots it takes to trigger spread i think
   spreadBase = 1500;
   spreadReset = 350; // m
   spreadMin = 1500;
   spreadMax = 1500;
   screenshakeMin = "0.35 0.35 0.35"; 
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
	projectileFalloffStart = 18;
	projectileFalloffEnd = 64;
	projectileFalloffDamage = 0.1;

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
	stateTransitionOnNoAmmo[1]       	= "ReloadStart";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseShotgunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[4]				= "FireLoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.05;
	stateTransitionOnTimeout[4]		= "FireLoadCheckB";
	
	stateName[20]				= "FireLoadCheckB";
	stateTransitionOnAmmo[20]		= "Ready";
	stateTransitionOnNotLoaded[20]  = "Ready";
	stateTransitionOnNoAmmo[20]		= "ReloadStart2";
	
	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";
	
	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "ReloadStart2";

	stateName[7]			  	= "ReloadStart";
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]	  	= "Reload";
	stateTimeoutValue[7]		  	= 0.5;
	stateWaitForTimeout[7]		  	= true;
	stateSequence[7]			= "ReloadStart";
	stateSound[7]				= RangerOpenSound;
	
	stateName[8]				= "Reload";
	stateTransitionOnTimeout[8]     	= "EndReload";
	stateWaitForTimeout[8]			= false;
	stateTimeoutValue[8]			= 0.2;
	stateSequence[8]			= "ShellInLeft";
	stateScript[8]				= "AEShotgunLoadOneB";
	stateSound[8]				= RangerInsert1Sound;
	
	stateName[10]			  	= "ReloadStart2";
	stateScript[10]				= "onReloadStart2";
	stateTransitionOnTimeout[10]	  	= "Reload2A";
	stateTimeoutValue[10]		  	= 0.5;
	stateWaitForTimeout[10]		  	= true;
	stateSequence[10]			= "ReloadStartEmpty";
	stateSound[10]				= RangerOpenSound;
	
	stateName[11]			  	= "Reload2A";
	stateScript[11]				= "AEShotgunLoadOne";
	stateTransitionOnTimeout[11]	  	= "CheckAmmoA";
	stateTimeoutValue[11]		  	= 0.2;
	stateSequence[11]			= "ShellInLeftEmpty";
	stateWaitForTimeout[11]		  	= true;
	stateSound[11]				= RangerInsert1Sound;
	
	stateName[12]				= "CheckAmmoA";
	stateTimeoutValue[12]		  	= 0.1;
	stateScript[12]				= "AEShotgunAltLoadCheck";
	stateTransitionOnTimeout[12]		= "CheckAmmoB";	
	
	stateName[13]				= "CheckAmmoB";
	stateTimeoutValue[13]		  	= 0.1;
	stateTransitionOnNotLoaded[13]  = "EndReloadAlt";
	stateTransitionOnTimeout[13]	  	= "Reload2B";
	
	stateName[14]				= "Reload2B";
	stateScript[14]				= "AEShotgunLoadOneC";
	stateTimeoutValue[14]			= 0.2;
	stateWaitForTimeout[14]		  	= false;
	stateSequence[14]			= "ShellInRight";
	stateTransitionOnTimeout[14]	  	= "EndReload";
	stateSound[14]				= RangerInsert2Sound;
	
	stateName[15]			  	= "EndReload";
	stateScript[15]				= "onReloadEnd";
	stateTimeoutValue[15]		  	= 0.45;
	stateSequence[15]			= "ReloadEnd";
	stateTransitionOnTimeout[15]	  	= "Ready";
	stateWaitForTimeout[15]		  	= false;
	
	stateName[16]			  	= "EndReloadAlt";
	stateScript[16]				= "onReloadEndAlt";
	stateTimeoutValue[16]		  	= 0.45;
	stateSequence[16]			= "ReloadEndOut";
	stateTransitionOnTimeout[16]	  	= "LoadCheckA";
	stateWaitForTimeout[16]		  	= false;
	
	stateName[17]				= "ReadyLoop";
	stateTransitionOnTimeout[17]		= "Ready";

	stateName[18]          = "Empty";
	stateTransitionOnTriggerDown[18]  = "Dryfire";
	stateTransitionOnLoaded[18] = "ReloadStart2";
	stateScript[18]        = "AEOnEmpty";

	stateName[19]           = "Dryfire";
	stateTransitionOnTriggerUp[19] = "Empty";
	stateWaitForTimeout[19]    = false;
	stateScript[19]      = "onDryFire";
	
	stateName[21]           = "SemiAutoCheck"; //heeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeey
	stateTransitionOnTriggerUp[21]	  	= "FireLoadCheckA";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function RangerImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, RangerFire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function RangerImage::AEOnLowClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(2, jump);
}

function RangerImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function RangerImage::onReloadMagIn(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftright);
   %obj.schedule(200, "aeplayThread", "2", "shiftleft");
}

function RangerImage::onReload2MagIn(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftright);
}

function RangerImage::onReload2Bolt(%this,%obj,%slot)
{
   %obj.aeplayThread(2, plant);
   %obj.schedule(200, "aeplayThread", "2", "shiftleft");
}

function RangerImage::onReloadEnd(%this,%obj,%slot)
{
  %obj.schedule(150, playAudio, 1, RangerCloseSound);
}

function RangerImage::onReloadEndAlt(%this,%obj,%slot)
{
  %obj.schedule(150, playAudio, 1, RangerCloseSound);
    %obj.rangerEmpty = true;
}

// MAGAZINE DROPPING

function RangerImage::AEShotgunLoadOne(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
}

function RangerImage::AEShotgunLoadOneB(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
   %obj.schedule(350, "aeplayThread", "2", "shiftaway");
}

function RangerImage::AEShotgunLoadOneC(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant); 
	Parent::AEShotgunLoadOne(%this, %obj, %slot);
   %obj.schedule(250, "aeplayThread", "2", "shiftaway");
}

function RangerImage::onReloadStart(%this,%obj,%slot)
{
   %obj.schedule(75, "aeplayThread", "2", "shiftleft");
   %obj.schedule(150, "aeplayThread", "3", "plant");
   %obj.reload3Schedule = %this.schedule(125,onMagDrop,%obj,%slot, 0);
   %obj.reload4Schedule = schedule(getRandom(700,800),0,serverPlay3D,AEShellHeavyShotgun @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function RangerImage::onReloadStart2(%this,%obj,%slot)
{
   %obj.schedule(75, "aeplayThread", "2", "shiftleft");
   %obj.schedule(150, "aeplayThread", "3", "plant");
   if(%obj.rangerEmpty)
        %obj.rangerEmpty = false;
   else
        %obj.reload2Schedule = %this.schedule(125,onMagDrop,%obj,%slot, 1);
   %obj.reload3Schedule = %this.schedule(125,onMagDrop,%obj,%slot, 0);
   %obj.reload4Schedule = schedule(getRandom(700,800),0,serverPlay3D,AEShellHeavyShotgun @ getRandom(1,3) @ Sound,%obj.getPosition());
   %obj.reload5Schedule = schedule(getRandom(700,800),0,serverPlay3D,AEShellHeavyShotgun @ getRandom(1,3) @ Sound,%obj.getPosition());
}

function RangerImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function RangerImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function RangerImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	cancel(%obj.reload5Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function RangerImage::onMagDrop(%this,%obj,%slot,%right)
{
    %a = new aiPlayer()
    {
        datablock = emptyPlayer;
        position = %obj.getPosition();
        scale = "1 1 1";
    };
    %a.setDamageLevel(100);
    %side = %right * 0.2;
    %trf = %obj.getSlotTransform(0);
    %pos = getWords(%trf, 0, 2);
    %rot = getWords(%trf, 3, 7);
    %pos = vectorAdd(%pos, vectorScale(%obj.getRightVector(), %side));
    %a.setTransform(%pos SPC %rot);
    %a.mountImage(RangerCasingImage,0);
    %a.schedule(1000,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(RangerCasingImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.1 1 0.1";
   rotation = eulerToMatrix( "0 0 0" );	
	
	casing = AE_BEHeavyShotgunDebris;
	shellExitDir        = "0 -0.25 1";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;	
	shellVelocity       = 7.0;
	
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

function RangerCasingImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(RangerSafetyImage)
{
   shapeFile = "./Ranger/Ranger.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.015";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = RangerItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = RangerImage;
   doColorShift = true;
   colorShiftColor = RangerItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateWaitForTimeout[0]		  	= false;
	stateTransitionOnTimeout[0]     	= "";
	stateSound[0]				= "";

};

function RangerSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function RangerSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}


///////// IRONSIGHTS?

datablock ShapeBaseImageData(RangerIronsightImage : RangerImage)
{
	recoilHeight = 5;

	scopingImage = RangerImage;
	sourceImage = RangerImage;
	
   offset = "0 0 -0.015";
	eyeOffset = "-0 1.0 -0.825";
	rotation = eulerToMatrix( "0 -20 0" );

	desiredFOV = $ae_LowIronsFOV;
	projectileZOffset = 0;
	R_MovePenalty = 0.5;
   
	stateName[10]				= "ReloadStart2";
	stateScript[10]				= "onDone";
	stateTimeoutValue[10]			= 1;
	stateTransitionOnTimeout[10]		= "";
	stateSound[10]				= "";
	
	stateName[7]				= "ReloadStart";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function RangerIronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function RangerIronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function RangerIronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, RangerFire @ getRandom(1, 4) @ Sound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function RangerIronsightImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function RangerIronsightImage::onMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound, %obj.getHackPosition());
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function RangerIronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound, %obj.getHackPosition());
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}